using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Assignment.Web.Models;

namespace Assignment.Web.Service.Interfaces
{
    public interface IAgentRepository:IDisposable
    {
        Task<IEnumerable<BusinessEntities>> GetAgents();
        Task<BusinessEntities> GetAgentById(int agentId);
        Task InsertAgent(BusinessEntities agent);
        Task DeleteAgent(int agentId);
        void UpdateAgent(BusinessEntities agent);
        Task<IEnumerable<BusinessEntities>> GetFilteredAgents(Expression<Func<BusinessEntities, bool>> filter = null,
            Func<IQueryable<BusinessEntities>, IOrderedQueryable<BusinessEntities>> orderBy = null, int? page = null,
            int? pageSize = null);
        Task Save();
    }
}
