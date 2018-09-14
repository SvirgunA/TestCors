using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCors.Common;
using TestCors.Common.Constants;
using TestCors.Common.Enums;
using TestCors.Common.Logging;
using TestCors.Common.Services;
using TestCors.Common.Settings;
using TestCors.Data.UoW;
using TestCors.DTO;
using TestCors.Models;
using TestCors.Services.Helpers;

namespace TestCors.Services.Core.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;
        private readonly AuthSettings _authSettings;

        public AuthService(IUnitOfWork uow, ILoggerFactory loggerFactory, IOptions<AuthSettings> authSettings)
        {
            this._uow = uow;
            this._logger = loggerFactory.CreateLogger(GetType()); 
            this._authSettings = authSettings.Value;
        }

        public Task<ServiceResult> ChangePasswordAsync(ChangePasswordDto model, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<JwtTokenApiModel>> LoginAsync(LoginDto model)
        {
            var result = ServiceResult<JwtTokenApiModel>.OK();
            try
            {
                var user = await _uow.Repository<User>().Get()
                    .FirstOrDefaultAsync(u => u.Email == model.Email.ToLower());
                if(user == null || !user.PasswordHash.VerifyHashed(model.Password))
                {
                    return ServiceResult<JwtTokenApiModel>.Error(StringConstants.AuthFailed);
                }
                if (!user.Verified)
                {
                    return ServiceResult<JwtTokenApiModel>.Error(StringConstants.NotActivated);
                }
                result.Entity = new JwtTokenApiModel()
                {
                    AccessToken = user.GenerateIdentity().GenerateIdentityToken(_authSettings)
                };
            }
            catch(Exception e)
            {
                _logger.LogException(e);
                result = ServiceResult<JwtTokenApiModel>.Error(StringConstants.DatabaseError);
            }
            return result;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto model)
        {
            var result = new ServiceResult() { Status = EServiceResultStatus.Success };
            try
            {
                var userExist = await _uow.Repository<User>().Get()
                    .AnyAsync(u => u.Email == model.Email.ToLower());
                if (userExist)
                {
                    return new ServiceResult { Status = EServiceResultStatus.Error, Message = StringConstants.UserAlreadyExist };
                }
                await _uow.BeginTransactionAsync();
                var newUser = model.ToEntity();
                await _uow.Repository<User>().AddAsync(newUser);
                await _uow.SaveAsync();
                _uow.CommitTransaction();

            }
            catch(Exception e)
            {
                _logger.LogException(e);
                result.Status = EServiceResultStatus.Error;
                result.Message = StringConstants.DatabaseError;
            }
            return result;
        }
    }
}
