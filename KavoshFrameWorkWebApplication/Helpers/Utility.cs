using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Helpers
{
   
    public static class Utility
    {
        public static double? Growth(double current, double prev)
        {
            if (prev == 0)
            {
                return null;
            }

            if (prev * current >= 0 || current >= 0)
                return (prev / Math.Abs(prev)) * (current / prev - 1) ;
            else {
                if (Math.Abs(prev) < Math.Abs(current))
                {
                    return (current / prev - 1) ; // current=-200, prev=100 => -300%
                }
                else
                {
                    return (prev / current - 1) ; // current=-100, prev=200 => -300%
                }
            }
        }

        public static async Task<bool> HasAccess(this ClaimsPrincipal User, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, string access)
        {
            if (User.IsInRole("Admin"))
                return true;
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var claims = await userManager.GetClaimsAsync(user);
            return claims.Any(x => x.Value == access);
        }

        public class TreeUtility<TEntity> where TEntity : BaseTree
        {
            public static IEnumerable<TEntity> FindAllParents(List<TEntity> all_data, TEntity child)
            {
                var parent = all_data.FirstOrDefault(x => x.Id == child.ParentId);

                if (parent == null)
                    return Enumerable.Empty<TEntity>();

                return new[] { parent }.Concat(FindAllParents(all_data, parent));
            }
            public static IEnumerable<TEntity> FindAllChilderen(List<TEntity> all_data, TEntity child)
            {
                var res = all_data.FirstOrDefault(x => x.ParentId == child.Id);
                if (res == null)
                    return Enumerable.Empty<TEntity>();

                return new[] { res }.Concat(FindAllChilderen(all_data, res));
            }
            public static IEnumerable<TEntity> Recursive(List<TEntity> items, string toplevelid)
            {
                List<TEntity> inner = new List<TEntity>();
                foreach (var t in items.Where(item => item.ParentId == toplevelid))
                {
                    inner.Add(t);
                    inner = inner.Union(Recursive(items, t.Id)).ToList();
                }
                return inner;
            }
        }

    }
}
