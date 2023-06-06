using E_Library.Models.AuthModels;
using E_Library.Models;
using E_Library.Repositories.Abstractions;
using E_Library.Services.Abstractions;
using E_Library.Services.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class RoleService:Service<IdentityRole>,IRoleService
    {
        IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository =roleRepository;
        }

        public override Result Create(IdentityRole entity)

        {

            var result = new Result();
            //code unique
            var nameResult = _roleRepository.Get(c => c.Name==entity.Name);
            if (nameResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("Role already Exists with same Role Name..!");
            }
           
           
            if (result.ErrorMessages.Any())
            {
                return result;
            }
            bool isSuccess = _roleRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Role not Created");

            return result;
        }
        public override Result Update(IdentityRole entity)
        {
            var result = new Result();
            bool isSuccess = _roleRepository.Update(entity);

            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }

            result.ErrorMessages.Add("Role not Updated");

            return result;
        }
        public override Result Remove(IdentityRole entity)
        {
            var result = new Result();
            bool isSuccess = _roleRepository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Role not Removed");

            return result;
        }
    }
}
