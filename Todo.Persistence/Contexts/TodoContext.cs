using Microsoft.EntityFrameworkCore;
using Todo.Domain.Common;
using Todo.Domain.Entities;
using Todo.Infrastructure.Implementation.Services.UserServices;
using Todo.Shared.Services.DateTimeServices;

namespace Todo.Persistence.Contexts
{
    public class TodoContext:DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;
        public TodoContext(DbContextOptions<TodoContext> options, IAuthenticatedUserService authenticatedUser, IDateTimeService dateTimeService) : base(options)
        {
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Task>().HasOne(p => p.User)
                           .WithMany().HasForeignKey(con => con.UserId);

            modelBuilder.Entity<Domain.Entities.Task>().HasOne(p => p.Assigned)
                           .WithMany().HasForeignKey(con => con.AssignedId);

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Permission>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<UserPermission>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Status>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Tag>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Domain.Entities.Task>().HasQueryFilter(p => !p.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<AuditableEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                string username = "SYSTEM";
                if (_authenticatedUser.UserId != null)
                    username = _authenticatedUser.Username;

                entry.Entity.LastModifiedDate = _dateTimeService.GetTurkeyToday();
                entry.Entity.LastModifiedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = _dateTimeService.GetTurkeyToday();
                    entry.Entity.IsDeleted = false;
                    entry.Entity.CreatedBy = username;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
    }
}
