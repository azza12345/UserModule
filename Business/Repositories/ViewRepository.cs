using Core.IRepositories;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Business.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private readonly ApplicationDbContext _context;

        public ViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(View view)
        {
            await _context.Views.AddAsync(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var view = await GetByIdAsync(id);
            if (view != null)
            {
                _context.Views.Remove(view);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<View> GetByIdAsync(Guid id)
        {
            return await _context.Views
                .Include(a => a.System)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(View view)
        {
            _context.Views.Update(view);
            await _context.SaveChangesAsync();
        }
    }
}
