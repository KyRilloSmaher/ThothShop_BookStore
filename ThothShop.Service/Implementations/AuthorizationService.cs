using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;

namespace ThothShop.Service.implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> CreateRole(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditRoleAsync(EditRoleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> GetRoleById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityRole>> GetRolesList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRoleExistById(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRoleExistByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
