using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Repository.Interfaces;
using FinanceTracker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => (_userRepository) = (userRepository);

        public async Task<BaseResponse<UserModel>> GetByIdAsync(Guid id)
        {
            try
            {
                UserModel user = await _userRepository.GetByIdAsync(id);

                if(user == null)
                {
                    return new BaseResponse<UserModel>(false, "There is no user with this id");
                }

                return new BaseResponse<UserModel>(true, user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<UserModel>> GetByLoginAsync(string login)
        {
            try
            {
                UserModel user = await _userRepository.GetByLoginAsync(login);

                if (user == null)
                {
                    return new BaseResponse<UserModel>(false, "There is no user with this login");
                }

                return new BaseResponse<UserModel>(true, user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel model)
        {
            try
            {
                UserModel user = await _userRepository.GetByLoginAsync(model.Login);

                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>(false, "Login or password entered incorrectly");
                }

                if (user.Password != model.Password)
                {
                    return new BaseResponse<ClaimsIdentity>(false, "Login or password entered incorrectly");
                }

                ClaimsIdentity claimsIdentity = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>(true, claimsIdentity);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                UserModel user = await _userRepository.GetByLoginAsync(model.Login);

                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>(false, "This login is already in use");
                }

                user = new UserModel(model.Login, model.Password, model.Age);

                await _userRepository.CreateAsync(user);

                ClaimsIdentity claimsIdentity = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>(true, claimsIdentity);

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>(false, ex.Message);
            }
        }

        private ClaimsIdentity Authenticate(UserModel user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim("UserId", user.Id.ToString()),
            };

            return new ClaimsIdentity(claims, "Authentication", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}