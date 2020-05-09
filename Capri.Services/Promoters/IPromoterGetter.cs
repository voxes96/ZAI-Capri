using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capri.Web.ViewModels.Promoter;
using Sieve.Models;

namespace Capri.Services.Promoters
{
    public interface IPromoterGetter
    {
        Task<IServiceResult<PromoterViewModel>> Get(int id);
        IServiceResult<IEnumerable<PromoterViewModel>> GetAll();
        IServiceResult<IQueryable<PromoterViewModel>> GetFiltered(SieveModel sieveModel);
        IServiceResult<int> Count(SieveModel sieveModel);
        Task<IServiceResult<PromoterViewModel>> GetMyData();
    }
}
