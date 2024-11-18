using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<SystemModule> Systems { get; set; }
  //  public DbSet<View> Views { get; set; }
    public DbSet<Data.Action> Actions { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

        
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");

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

        //builder.Entity<Permission>()
        //    .HasOne(p => p.View)
        //    .WithMany()
        //    .HasForeignKey(p => p.ViewId)
        //    .OnDelete(DeleteBehavior.Restrict);

    }
    }



