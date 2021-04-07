using Data;
using Data.Contracts;
using Entities.IdntityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Seed
{
    public class YourDbContextSeedData
    {
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Role> _RoleRepository;
        private readonly ApplicationDbContext _dbContext;
        public YourDbContextSeedData(IRepository<User> UserRepository, IRepository<Role> RoleRepository, ApplicationDbContext dbContext)
        {
            _UserRepository = UserRepository;
            _RoleRepository = RoleRepository;
            _dbContext = dbContext;
        }
        public async void SeedAdminUser()
        {
            var user = new User
            {
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
            var roleStore = new RoleStore<Role, ApplicationDbContext, int>(_dbContext);

            if (!_RoleRepository.TableNoTracking.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new Role { Name = "Admin", NormalizedName = "ADMIN" , Description= "ادمین سایت"});
                await roleStore.CreateAsync(new Role { Name = "Leader", NormalizedName = "LEADER" , Description= "ادمین سایت"});
            }

            if (!_UserRepository.TableNoTracking.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "Mh!123456");
                user.PasswordHash = hashed;
                var userStore = new UserStore<User, Role, ApplicationDbContext, int>(_dbContext);
                await userStore.CreateAsync(user);
                userStore = new UserStore<User, Role, ApplicationDbContext, int>(_dbContext);
                var roles = _dbContext.Roles.Where(c=>c.NormalizedName == "leader".ToUpper() || c.NormalizedName == "admin".ToUpper()).ToList();
                var identityRoles = new List<IdentityUserRole<int>> { new IdentityUserRole<int> { RoleId = roles.First().Id, UserId = user.Id}, new IdentityUserRole<int> { RoleId = roles.Last().Id, UserId = user.Id} };
                await _dbContext.UserRoles.AddRangeAsync(identityRoles);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
