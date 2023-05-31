using E_Library.Models;
using E_Library.Repositories.Abstractions;
using E_Library.Services.Abstractions;
using E_Library.Services.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class UserService : Service<IdentityUser>, IUserService
    {
        IUsersRepository _userRepository;
        public UserService(IUsersRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override Result Create(IdentityUser entity)

        {

            var result = new Result();
            //code unique
            var codeResult = _userRepository.Get(c => c.UserName == entity.UserName);
            if (codeResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("User already Exists with same UserName..!");
            }
            //unique phone no
            var phoneResult = _userRepository.Get(c => c.PhoneNumber == entity.PhoneNumber);
            if (phoneResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("User already Exists with same Phone no..!");
            }
            if (result.ErrorMessages.Any())
            {
                return result;
            }
            bool isSuccess = _userRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("User not Created");

            return result;
        }
        public override Result Update(IdentityUser entity)
        {
            var result = new Result();
            bool isSuccess = _userRepository.Update(entity);

            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }

            result.ErrorMessages.Add("User not Updated");

            return result;
        }
        public override Result Remove(IdentityUser entity)
        {
            var result = new Result();
            bool isSuccess = _userRepository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("User not Removed");

            return result;
        }
    }
}
