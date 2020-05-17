using System;
using System.Threading.Tasks;
using AutoMapper;
using Capri.Database;
using Capri.Database.Entities;
using Capri.Database.Entities.Identity;
using Capri.Services.Users;
using Capri.Services.Institutes;
using Capri.Web.ViewModels.Promoter;

namespace Capri.Services.Promoters
{
    public class PromoterCreator : IPromoterCreator
    {
        private readonly ISqlDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserCreator _userCreator;
        private readonly IInstituteGetter _instituteGetter;

        public PromoterCreator(
            ISqlDbContext context,
            IMapper mapper,
            IUserCreator userCreator,
            IInstituteGetter instituteGetter
            )
        {
            _context = context;
            _mapper = mapper;
            _userCreator = userCreator;
            _instituteGetter = instituteGetter;
        }

        public async Task<IServiceResult<PromoterViewModel>> Create(
            PromoterRegistration registration)
        {
            //var instituteResult = await _instituteGetter.IsExists(registration.InstituteId);
            if(!(await _instituteGetter.IsExists(registration.InstituteId)))
            {
                return ServiceResult<PromoterViewModel>.Error($"Institute with id {registration.InstituteId} does not exist");
            }

            var userResult = 
                await _userCreator
                .CreateUserWithId(
                    registration.Email, 
                    registration.Password,
                    new RoleType[] {
                        RoleType.Promoter
                    });

            if(!userResult.Successful())
            {
                return ServiceResult<PromoterViewModel>.Error(userResult.GetAggregatedErrors());
            }

            //var user = userResult.Body();
            var promoter = _mapper.Map<Promoter>(registration);
            //promoter.Id = Guid.NewGuid();
            promoter.UserId = userResult.Body();

            _context.Promoters.Add(promoter);
            await _context.SaveChangesAsync();

            var promoterViewModel = _mapper.Map<PromoterViewModel>(promoter);
            return ServiceResult<PromoterViewModel>.Success(promoterViewModel);
        }
    }
}
