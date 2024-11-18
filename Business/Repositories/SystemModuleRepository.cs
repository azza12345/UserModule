using Core.IRepositories;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class SystemModuleRepository : ISystemModuleRepository
    {
        private readonly ApplicationDbContext _context;
        public SystemModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(SystemModule systemModule)
        {
            await _context.Systems.AddAsync(systemModule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var systemModule = await GetByIdAsync(id);
            if (systemModule != null)
            {
                _context.Systems.Remove(systemModule);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SystemModule>> GetAllAsync()
        {
            return await _context.Systems.ToListAsync();
        }

        public async Task<SystemModule> GetByIdAsync(Guid id)
        {
            return await _context.Systems
           .Include(sm => sm.Groups)
           .Include(sm => sm.Views)
           .Include(sm => sm.Actions)
           .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task UpdateAsync(SystemModule systemModule)
        {
            _context.Systems.Update(systemModule);
            await _context.SaveChangesAsync();
        }
    }
}
