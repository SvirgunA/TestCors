using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCors.Common;
using TestCors.DTO;
using TestCors.Models;

namespace TestCors.Services.Core
{
    public interface IPhoneService
    {
        Task<ServiceResult<IEnumerable<Phone>>> GetPhonesAsync();
        Task<ServiceResult<Phone>> GetPhoneAsync(int id);
        Task<ServiceResult<Phone>> UpdateAsync(PhoneDto phone, int id);
        Task<ServiceResult<Phone>> AddAsync(PhoneDto phone);
        Task<ServiceResult> RemoveAsync(int id);
    }
}
