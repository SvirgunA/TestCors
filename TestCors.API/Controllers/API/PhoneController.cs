using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCors.DTO;
using TestCors.Models;
using TestCors.Services.Core;

namespace TestCors.API.Controllers.API
{
    [Route("api/Phone")]
    public class PhoneController : BaseController
    {
        private readonly IPhoneService _phoneService;

        public PhoneController(IPhoneService phoneService)
        {
            this._phoneService = phoneService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            return ReturnResult(await _phoneService.GetPhonesAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            return ReturnResult(await _phoneService.GetPhoneAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]PhoneDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(await _phoneService.AddAsync(model));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody]PhoneDto model, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(await _phoneService.UpdateAsync(model, id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            return ReturnResult(await _phoneService.RemoveAsync(id));
        }
    }
}