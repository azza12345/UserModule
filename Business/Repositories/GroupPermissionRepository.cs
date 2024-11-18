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
   
        public class GroupPermissionRepository : IGroupPermissionRepository
        {
            private readonly ApplicationDbContext _context;

            public GroupPermissionRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GroupPermission> GetByIdAsync(Guid groupId, Guid permissionId)
            {
                return await _context.GroupPermissions
                    .Include(gp => gp.Group)
                    .Include(gp => gp.Permission)
                    .FirstOrDefaultAsync(gp => gp.GroupId == groupId && gp.PermissionId == permissionId);
            }

            public async Task<IEnumerable<GroupPermission>> GetAllAsync()
            {
                return await _context.GroupPermissions
                    .Include(gp => gp.Group)
                    .Include(gp => gp.Permission)
                    .ToListAsync();
            }

            public async Task AddAsync(GroupPermission groupPermission)
            {
                await _context.GroupPermissions.AddAsync(groupPermission);
            }

            public void Remove(GroupPermission groupPermission)
            {
                _context.GroupPermissions.Remove(groupPermission);
            }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }


