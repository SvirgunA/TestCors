using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCors.Common;
using TestCors.DTO;

namespace TestCors.Services.Core
{
    public interface IAuthService
    {
        Task<ServiceResult<JwtTokenApiModel>> LoginAsync(LoginDto model);
        Task<ServiceResult> RegisterAsync(RegisterDto model);
        Task<ServiceResult> ChangePasswordAsync(ChangePasswordDto model, int userId);
    }
}
