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
    public class ActionRepository : IActionRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public async Task AddAsync(Data.Action action)
        {
            await _context.Actions.AddAsync(action);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var action = await GetByIdAsync(id);
            if (action != null)
            {
                _context.Actions.Remove(action);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Data.Action>> GetAllAsync()
        {
            return await _context.Actions
               .Include(a => a.System)
               .ToListAsync();
        }

        public async Task<Data.Action> GetByIdAsync(Guid id)
        {
            return await _context.Actions
                .Include(a => a.System)  
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Data.Action action)
        {
            _context.Actions.Update(action);
            await _context.SaveChangesAsync();
        }

       

       
    }
}
