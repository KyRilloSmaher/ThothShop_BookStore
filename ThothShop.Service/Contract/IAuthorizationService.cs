using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Service.Commans;

namespace ThothShop.Service.Contract
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(string userId, string policyName);
        public Task<IdentityRole> CreateRole(string Name);
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(string roleId);
        public Task<bool> IsRoleExistById(string roleId);
        public Task<List<IdentityRole>> GetRolesList();
        public Task<IdentityRole> GetRoleById(string id);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<ManageUserClaimsResult> ManageUserClaimData(User user);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
    }
}
