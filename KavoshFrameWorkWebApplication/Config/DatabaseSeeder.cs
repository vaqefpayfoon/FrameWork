//using KavoshFrameWorkCore.Models;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Threading.Tasks;

//namespace KavoshFrameWorkWebApplication.Config
//{
//    public class DatabaseSeeder
//    {
//        private readonly string[] Roles = new string[] 
//        {
//            "Admin",
//            "User",
//            "Worker"
//        };

//        readonly RoleManager<ApplicationRole> _roleManager;
//        readonly UserManager<ApplicationUser>    _userManager;

//        public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
//        {
//            _roleManager = roleManager;
//            _userManager = userManager;
//        }

//        /// <summary>
//        /// Populates the database with users and roles
//        /// </summary>
//        public async Task Seed()
//        {
//            foreach (string role in Roles)
//                await AddRole(role);


//            await AddAdminUser();
//            await AddWorkerUser();
//        }

//        async Task AddAdminUser()
//        {
//            if (await _userManager.FindByNameAsync("admin@localhost") == null)
//            {
//                var admin = new ApplicationUser
//                {
//                    UserName = "admin@localhost",
//                    FirstName = "Admin",
//                    LastName = "Admin",
//                    Email = "admin@localhost"
//                };

//                var result = await _userManager.CreateAsync(admin);

//                if (result.Succeeded)
//                {
//                    await _userManager.AddPasswordAsync(admin, "admin");
//                    await _userManager.AddToRoleAsync(admin, "Admin");
//                }
//            }
//        }
//        async Task AddWorkerUser()
//        {
//            if (await _userManager.FindByNameAsync("worker@localhost.dk") == null)
//            {
//                var worker = new ApplicationUser
//                {
//                    UserName = "worker@localhost.dk",
//                    FirstName = "Worker",
//                    LastName = "Worker",
//                    Email = "Worker@localhost.dk"
//                };

//                var result = await _userManager.CreateAsync(worker);

//                if (result.Succeeded)
//                {
//                    await _userManager.AddPasswordAsync(worker, "Worker");
//                    await _userManager.AddToRoleAsync(worker, "Worker");
//                }
//            }
//        }

//        /// <summary>
//        /// Add a role if one doesn't exist
//        /// </summary>
//        /// <param name="roleName">The name of the role to add</param>
//        async Task AddRole(string roleName)
//        {
//            if (await _roleManager.FindByNameAsync(roleName) == null)
//            {
//                var newRole = new ApplicationRole
//                {
//                    Id =Guid.NewGuid().ToString(),
//                    Name = roleName
//                };

//                await _roleManager.CreateAsync(newRole);
//            }
//        }
//    }
//}
