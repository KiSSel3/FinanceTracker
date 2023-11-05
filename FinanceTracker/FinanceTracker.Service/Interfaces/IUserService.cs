using FinanceTracker.Domain.Response;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel model);
        public Task<BaseResponse<ClaimsIdentity>> RegisterAsync(RegisterViewModel model);
        public Task<BaseResponse<UserModel>> GetByIdAsync(Guid id);
        public Task<BaseResponse<UserModel>> GetByLoginAsync(string login);
    }
}
