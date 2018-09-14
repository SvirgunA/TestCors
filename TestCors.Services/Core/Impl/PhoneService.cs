using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCors.Common;
using TestCors.Common.Constants;
using TestCors.Common.Enums;
using TestCors.Common.Logging;
using TestCors.Data.UoW;
using TestCors.DTO;
using TestCors.Models;

namespace TestCors.Services.Core.Impl
{
    public class PhoneService : IPhoneService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;

        public PhoneService(IUnitOfWork uow, ILoggerFactory loggerFactory)
        {
            this._uow = uow;
            this._logger = loggerFactory.CreateLogger(GetType());
        }

        public async Task<ServiceResult<Phone>> AddAsync(PhoneDto phone)
        {
            var result = ServiceResult<Phone>.OK();
            try
            {
                var entity = new Phone();
                phone.Update(entity);
                result.Entity = await _uow.Repository<Phone>().AddAsync(entity);
                await _uow.SaveAsync();
            }
            catch (Exception e)
            {
                result = ServiceResult<Phone>.Error(StringConstants.DatabaseError);
            }
            return result;
            
        }

        public async Task<ServiceResult<Phone>> GetPhoneAsync(int id)
        {
            var result = ServiceResult<Phone>.OK();
            try
            {
                result.Entity = await _uow.Repository<Phone>().Get().Where(p => p.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                result = ServiceResult<Phone>.Error(StringConstants.DatabaseError);
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<Phone>>> GetPhonesAsync()
        {
            var result = ServiceResult<IEnumerable<Phone>>.OK();
            try
            {
                result.Entity = await _uow.Repository<Phone>().Get().ToListAsync();
                throw new DivideByZeroException();
            }
            catch(Exception ex)
            {
                _logger.LogException(ex);
                result = ServiceResult<IEnumerable<Phone>>.Error(StringConstants.DatabaseError);
            }
            return result;
        }

        public async Task<ServiceResult> RemoveAsync(int id)
        {
            var result = new ServiceResult() { Status = EServiceResultStatus.Success };
            try
            {
                var entity = await _uow.Repository<Phone>().Get().FirstOrDefaultAsync(p => p.Id == id);
                if (entity == null)
                {
                    return new ServiceResult() { Status = EServiceResultStatus.Error, Message = StringConstants.NotFound };
                }
                _uow.Repository<Phone>().Delete(entity);
                await _uow.SaveAsync();
            }
            catch
            {
                result = new ServiceResult() { Status = EServiceResultStatus.Error, Message = StringConstants.DatabaseError };
            }
            return result;
        }

        public async Task<ServiceResult<Phone>> UpdateAsync(PhoneDto phone, int id)
        {
            var result = ServiceResult<Phone>.OK();
            try
            {
                var entity = await _uow.Repository<Phone>().Get().FirstOrDefaultAsync(p => p.Id == id);
                if (entity == null)
                {
                    return ServiceResult<Phone>.Error(StringConstants.NotFound);
                }
                phone.Update(entity);
                result.Entity = entity;
                await _uow.SaveAsync();
            }
            catch
            {
                result = ServiceResult<Phone>.Error(StringConstants.DatabaseError);
            }
            return result;
        }
    }
}
