using System.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Sieve.Models;
using Sieve.Services;
using Capri.Database;
using Capri.Services.Files;
using Capri.Services.Users;
using Capri.Web.ViewModels.Proposal;
using Capri.Web.ViewModels.Common;
using System.IO.Compression;
using System.IO;
using Capri.Database.Entities;

namespace Capri.Services.Proposals
{
    public class ProposalGetter : IProposalGetter
    {
        private readonly ISqlDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly ICsvCreator _csvCreator;
        private readonly IUserGetter _userGetter;
        private readonly IDiplomaCardCreator _diplomaCardCreator;

        public ProposalGetter(
            ISqlDbContext context,
            IMapper mapper,
            ISieveProcessor sieveProcessor,
            ICsvCreator csvCreator,
            IUserGetter userGetter,
            IDiplomaCardCreator diplomaCardCreator)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _csvCreator = csvCreator;
            _userGetter = userGetter;
            _diplomaCardCreator = diplomaCardCreator;
        }

        public async Task<IServiceResult<ProposalViewModel>> Get(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Students)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(proposal == null)
            {
                return ServiceResult<ProposalViewModel>.Error(
                    $"Proposal with id {id} does not exist");
            }

            var proposalViewModel = _mapper.Map<ProposalViewModel>(proposal);
            return ServiceResult<ProposalViewModel>.Success(proposalViewModel);
        }

        public async Task<IServiceResult<FileDescription>> GetCsvFileDescription(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Students)
                .Include(p => p.Course.Faculty)
                .Include(p => p.Promoter.Institute)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(proposal == null)
            {
                return ServiceResult<FileDescription>.Error(
                    $"Proposal with id {id} does not exist");
            }
            
            var proposalCsvRecord = _mapper.Map<ProposalCsvRecord>(proposal);
            var records = new ProposalCsvRecord[] { proposalCsvRecord };

            var csvStringResult = _csvCreator.CreateCsvStringFrom<ProposalCsvRecord>(records);
            if(!csvStringResult.Successful())
            {
                return ServiceResult<FileDescription>.Error(csvStringResult.GetAggregatedErrors());
            }

            var csvString = csvStringResult.Body();
            var bytes = Encoding.UTF8.GetBytes(csvString);
            var fileName = $"proposal-{proposal.Id}.csv";

            var fileDescription = new FileDescription {
                Name = fileName,
                Bytes = bytes
            };

            return ServiceResult<FileDescription>.Success(fileDescription);
        }

        public async Task<IServiceResult<IEnumerable<ProposalViewModel>>> GetMyProposals() {
            var userResult = await _userGetter.GetCurrentUser();
            if(!userResult.Successful())
            {
                var errors = userResult.GetAggregatedErrors();
                return ServiceResult<IEnumerable<ProposalViewModel>>.Error(errors);
            }

            var currentUser = userResult.Body();
            var promoter = 
                await _context
                .Promoters
                .Include(p => p.Proposals)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if(promoter == null)
            {
                return ServiceResult<IEnumerable<ProposalViewModel>>.Error("The current user has no associated promoter");
            }

            var proposalViewModels = promoter
                .Proposals
                .Select(p => _mapper.Map<ProposalViewModel>(p));

            return ServiceResult<IEnumerable<ProposalViewModel>>.Success(proposalViewModels);
        }

        public async Task<IServiceResult<FileDescription>> GetDiplomaCard(int id)
        {
            //var proposal = await _context.Proposals
            //    .Include(p => p.Students)
            //    .Include(p => p.Course.Faculty)
            //    .Include(p => p.Promoter.Institute)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(p => p.Id == id);
            var proposal = await _context.Proposals.AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProposalDocRecord
            {
                Faculty = p.Course.Faculty.Name,
                Course = p.Course.Name,
                Specialization = p.Specialization,
                StudyProfile = p.StudyProfile.GetDescription(),//Enum.GetName(typeof(StudyProfile), p.StudyProfile),
                Mode = p.Mode.GetDescription(),//Enum.GetName(typeof(StudyMode), p.Mode),
                Level = p.Level.GetDescription(),//Enum.GetName(typeof(StudyLevel), p.Level),
                Students = p.Students,
                TopicPolish = p.TopicPolish,
                TopicEnglish = p.TopicEnglish,
                OutputData = p.OutputData,
                Description = p.Description,
                StartingDate = p.StartingDate,
                Promoter = p.Promoter.TitlePrefix + p.Promoter.FirstName + p.Promoter.LastName,
                Institute = p.Promoter.Institute.Name
            }).SingleOrDefaultAsync();

            if (proposal == null)
            {
                return ServiceResult<FileDescription>.Error($"Proposal with id {id} does not exist.");
            }

            //var proposalDocRecord = _mapper.Map<ProposalDocRecord>(proposal);

            //var result = _diplomaCardCreator.CreateDiplomaCard(proposalDocRecord);
            var result = _diplomaCardCreator.CreateDiplomaCard(proposal);

            var fileName = $"karta_tematu_pracy_{id}.docx";
            //var fileNameZip = $"karta_tematu_pracy_{proposal.Id}.zip";

            //byte[] compressedBytes;
            //using (var outStream = new MemoryStream())
            //{
            //    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
            //    {
            //        var fileInArchive = archive.CreateEntry(fileName, CompressionLevel.Optimal);
            //        using (var entryStream = fileInArchive.Open())
            //        using (var fileToCompressStream = new MemoryStream(result.Body().ToArray()))
            //        {
            //            fileToCompressStream.CopyTo(entryStream);
            //        }
            //    }
            //    compressedBytes = outStream.ToArray();
            //}
            //var fileDescription = new FileDescription
            //{
            //    Name = fileNameZip,
            //    Bytes = compressedBytes
            //};

            var fileDescription = new FileDescription
            {
                Name = fileName,
                Bytes = result.Body().ToArray()
            };

            return ServiceResult<FileDescription>.Success(fileDescription);
        }

        public IServiceResult<IEnumerable<ProposalViewModel>> GetAll()
        {
            var proposals = _context.Proposals
                .Include(p => p.Students);

            var proposalViewModels = proposals.Select(p => _mapper.Map<ProposalViewModel>(p));
            return ServiceResult<IEnumerable<ProposalViewModel>>.Success(proposalViewModels);
        }

        public async Task<IServiceResult<List<ProposalViewModel>>> GetFiltered(SieveModel sieveModel)
        {
            var proposals = _context.Proposals.AsNoTracking();

            proposals = _sieveProcessor.Apply(sieveModel, proposals);
            
            var proposalViewModels = await proposals.Select(p => _mapper.Map<ProposalViewModel>(p)).ToListAsync();
            return ServiceResult<List<ProposalViewModel>>.Success(proposalViewModels);
        }

        public IServiceResult<int> Count(SieveModel sieveModel)
        {
            var proposals = _context.Proposals.AsQueryable();
            var filtered = _sieveProcessor.Apply(sieveModel, proposals, null, true, true, false);
            var total = filtered.Count();

            return ServiceResult<int>.Success(total);
        }
    }
}
