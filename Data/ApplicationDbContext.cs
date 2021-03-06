using Common.Utilities;
using EFCoreSecondLevelCacheInterceptor;
using Entities;
using Entities.Common;
using Entities.IdntityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        // آگر کانکشن استرینگ رو در استارتاپ ساختیم باید این سازنده باشد
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //    base.OnConfiguring(optionsBuilder); 
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // باید اولین متد باشد
            base.OnModelCreating(modelBuilder);

            // define assembly interface
            Assembly entitiesAssembly = typeof(IEntity).Assembly;

            // add all entities instead of DbSet<ClassName>
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);

            // apply all fluent api under Entities
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);

            // این متد میاد اون هایی که آن دیلیت روی کسکید هستند رو رستریک میکند 
            // اگر پدری دارای پدری دارای چند فرزند بود اگر پدر را پاک کنیم ارور میدهد و پدر تا بودن فرزندان حذف نمیشوند
            //modelBuilder.AddRestrictDeleteBehaviorConvention();

            // هر فیلدی که اسمش آیدی بود رو از نوع جیوآیدی بود رو دیفالت ولیواش رو تغغیر میدهد به NEWSEQUENTIALID() 
            modelBuilder.AddSequentialGuidForIdConvention();

            // اسم جدول هارو جمع میکند مثلا یوزر میشود یوزرز چون رجیستر آل اینتیتیز همان اسم کلاس رو تیبل میکند
            modelBuilder.AddPluralizingTableNameConvention();

            #region seed
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Leader",
                    Description = "ادمین اصلی سایت",
                    NormalizedName = "LEADER"
                },
                    new Role
                    {
                        Id = 2,
                        Name = "Admin",
                        Description = "جانشین",
                        NormalizedName = "ADMIN"
                    }
                );
            //
            var user = new User()
            {
                Id=1,
                UserName = "mahdi_hajian",
                FullName = "mahdi hajian",
                Age = 24,
                IsActive = true,
                Gender = GenderType.Male,
                NormalizedUserName = "mahdi_hajian".ToUpper(),
                Email = "admin@mahdihajian.ir",
                NormalizedEmail = "admin@mahdihajian.ir".ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "Mh!123456");
            user.PasswordHash = hashed;
            modelBuilder.Entity<User>().HasData(user);
            //
            var userrole1 = new IdentityUserRole<int>() {RoleId = 1 , UserId = 1 };
            var userrole2 = new IdentityUserRole<int>() {RoleId = 2 , UserId = 1 };
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(userrole1, userrole2);

            #endregion

            #region Change Name
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            #endregion
        }

        #region replacestrignPersianBug
        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
        #endregion
    }
}
