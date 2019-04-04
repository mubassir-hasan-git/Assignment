using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Persistence;
using Assignment.Web.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Web.Service.Repositories
{
    public class AgentRepository:IAgentRepository
    {

        private readonly AssignmentDbContext _context;
        public AgentRepository(AssignmentDbContext dbContext)
        {
            this._context = dbContext;
        }
       

        


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public async Task<IEnumerable<BusinessEntities>> GetAgents()
        {
            return await _context.BusinessEntities.ToListAsync();
        }

        public async Task<IEnumerable<BusinessEntities>> GetFilteredAgents(Expression<Func<BusinessEntities, bool>> filter = null,
            Func<IQueryable<BusinessEntities>, IOrderedQueryable<BusinessEntities>> orderBy = null,  int? page = null,
            int? pageSize = null)
        {
            IQueryable<BusinessEntities> query = _context.Set<BusinessEntities>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<BusinessEntities> GetAgentById(int agentId)
        {
            return await _context.BusinessEntities.FindAsync(agentId);
        }

        public async Task InsertAgent(BusinessEntities agent)
        {
            await _context.BusinessEntities.AddAsync(agent);
        }

        public async Task DeleteAgent(int agentId)
        {
            var agent = await _context.BusinessEntities.FindAsync(agentId);
            if (agent!=null)
            {
                 _context.BusinessEntities.Remove(agent);
            }
        }

        public void UpdateAgent(BusinessEntities agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
