﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sieve.Models;
using Capri.Web.ViewModels.Proposal;
using Capri.Web.ViewModels.Common;

namespace Capri.Services.Proposals
{
    public interface IProposalGetter
    {
        Task<IServiceResult<ProposalViewModel>> Get(int id);
        Task<IServiceResult<FileDescription>> GetCsvFileDescription(int id);
        Task<IServiceResult<IEnumerable<ProposalViewModel>>> GetMyProposals();
        Task<IServiceResult<FileDescription>> GetDiplomaCard(int id);
        IServiceResult<IEnumerable<ProposalViewModel>> GetAll();
        Task<IServiceResult<List<ProposalViewModel>>> GetFiltered(SieveModel sieveModel);
        IServiceResult<int> Count(SieveModel sieveModel);
    }
}
