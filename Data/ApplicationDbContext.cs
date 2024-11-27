using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Action = Data.Action;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<SystemModule> Systems { get; set; }
    public DbSet<View> Views { get; set; }
    public DbSet<Data.Action> Actions { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var systemId = Guid.NewGuid();
        builder.Entity<SystemModule>().HasData(
            new SystemModule
            {
                Id = systemId,
                Name = "Default System",
                Description = " default system "
            }
        );


        var userId = Guid.NewGuid();
        builder.Entity<User>().HasData(
            new User
            {
                Id = userId,
                Fname = "Azza",
                Lname = "Mohamed",
                PasswordHash="Azza123#",
                Email = "azzaAdmin@gmail.com",
                Mobile = "1234567890",
                Address = "6Th October",
              //  IsEmailVerified = true,
              //  IsActive = true,
                SystemId = systemId 
            }
            );

        var groupId = Guid.NewGuid();
        builder.Entity<Group>().HasData(
            new Group
            {
                Id = groupId,
                Name = "Meters",
                Description = "Meters System ",
                IsActive = true,
                SystemId = systemId,
                ParentGroupId = null 
            }
        );

        var actionId = Guid.NewGuid();
        builder.Entity<Action>().HasData(
            new Action
            {
                Id = actionId,
                Name = "Create User",
                Description = "Allow User creating new users",
                SystemId = systemId
            }
        );


        var viewId = Guid.NewGuid();
        builder.Entity<View>().HasData(
            new View
            {
                Id = viewId,
                Name = "Admin Dashboard",
                Description="admin dashboard",
                SystemId = systemId
            }
        );


        var permissionId = Guid.NewGuid();
        builder.Entity<Permission>().HasData(
            new Permission
            {
                Id = permissionId,
               
                ActionId = actionId, 
                ViewId = viewId
            }
        );

        
        builder.Entity<GroupPermission>().HasData(
            new GroupPermission
            {
                GroupId = groupId, 
                PermissionId = permissionId 
            }
        );

        
        



        builder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

        
        builder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            builder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        
        builder.Entity<Group>()
            .HasOne(g => g.ParentGroup)
            .WithMany()
            .HasForeignKey(g => g.ParentGroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction); 

        
        builder.Entity<GroupPermission>()
            .HasKey(gp => new { gp.GroupId, gp.PermissionId });

        builder.Entity<GroupPermission>()
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPermissions)
            .HasForeignKey(gp => gp.GroupId)
            .OnDelete(DeleteBehavior.NoAction); 

        builder.Entity<GroupPermission>()
            .HasOne(gp => gp.Permission)
            .WithMany(p => p.GroupPermissions)
            .HasForeignKey(gp => gp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction); 

    
        builder.Entity<SystemModule>()
            .HasMany(sm => sm.Users)
            .WithOne(u => u.SystemModule)
            .HasForeignKey(u => u.SystemId)
            .OnDelete(DeleteBehavior.Restrict);

       

        builder.Entity<SystemModule>()
            .HasMany(sm => sm.Views)
            .WithOne(v => v.System)
            .HasForeignKey(v => v.SystemId)
            .OnDelete(DeleteBehavior.NoAction); 

        builder.Entity<SystemModule>()
            .HasMany(sm => sm.Actions)
            .WithOne(a => a.System)
            .HasForeignKey(a => a.SystemId)
            .OnDelete(DeleteBehavior.Restrict); 

        
        builder.Entity<Permission>()
            .HasOne(p => p.Action)
            .WithMany()
            .HasForeignKey(p => p.ActionId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.Entity<Permission>()
           .HasOne(p => p.View)
           .WithMany()
           .HasForeignKey(p => p.ViewId)
           .OnDelete(DeleteBehavior.Restrict);

        


    }
}



