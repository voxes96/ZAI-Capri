using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Capri.Database;
using Capri.Database.Entities;
using Capri.Database.Entities.Identity;
using Capri.Services.Users;
using Capri.Web.ViewModels.Student;

namespace Capri.Services.Students
{
    public class StudentCreator : IStudentCreator
    {
        private readonly ISqlDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserCreator _userCreator;

        public StudentCreator(
            ISqlDbContext context,
            IMapper mapper,
            IUserCreator userCreator)
        {
            _context = context;
            _mapper = mapper;
            _userCreator = userCreator;
        }

        public async Task<IServiceResult<StudentViewModel>> Create(StudentRegistration registration)
        {
            if(registration.ProposalId != null)
            {
                var proposal = await _context.Proposals.AsNoTracking().FirstOrDefaultAsync(p => p.Id == registration.ProposalId);
                if(proposal == null)
                {
                    return ServiceResult<StudentViewModel>.Error(
                        $"The proposal with id {registration.ProposalId} does not exist"
                    );
                }
            }

            if(await IsIndexNumberTaken(registration.IndexNumber))
            {
                return ServiceResult<StudentViewModel>.Error(
                    $"Index number {registration.IndexNumber} is already taken"
                );
            }

            var userResult = 
                await _userCreator
                .CreateUserWithId(
                    registration.Email, 
                    registration.Password,
                    new RoleType[] {
                        RoleType.Student
                    });

            if(!userResult.Successful())
            {
                return ServiceResult<StudentViewModel>.Error(userResult.GetAggregatedErrors());
            }

            //var user = userResult.Body();
            var student = _mapper.Map<Student>(registration);
            //student.Id = Guid.NewGuid();
            student.UserId = userResult.Body();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var studentViewModel = _mapper.Map<StudentViewModel>(student);
            return ServiceResult<StudentViewModel>.Success(studentViewModel);
        }

        private async Task<bool> IsIndexNumberTaken(int indexNumber)
        {
            return await _context.Students.AsNoTracking().AnyAsync(s => s.IndexNumber == indexNumber);
        }
    }
}