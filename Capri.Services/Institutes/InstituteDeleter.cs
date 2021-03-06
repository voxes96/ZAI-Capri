using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Capri.Database;
using Capri.Web.ViewModels.Institute;

namespace Capri.Services.Institutes
{
    public class InstituteDeleter : IInstituteDeleter
    {
        private readonly ISqlDbContext _context;
        private readonly IMapper _mapper;
        private readonly IInstituteGetter _instituteGetter;

        public InstituteDeleter(
            ISqlDbContext context,
            IMapper mapper,
            IInstituteGetter instituteGetter
            )
        {
            _context = context;
            _mapper = mapper;
            _instituteGetter = instituteGetter;
        }

        public async Task<IServiceResult<InstituteViewModel>> Delete(int id)
        {
            var institute = await _context.Institutes.FirstOrDefaultAsync(i => i.Id == id);
            
            if(institute == null)
            {
                return ServiceResult<InstituteViewModel>.Error(
                    $"Institute with id {id} does not exist");
            }

            var instituteViewModel = _mapper.Map<InstituteViewModel>(institute);

            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();

            return ServiceResult<InstituteViewModel>.Success(instituteViewModel);
        }
    }
}