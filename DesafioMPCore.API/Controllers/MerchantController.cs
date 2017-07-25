using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioMPCore.API.Models;
using DesafioMPCore.Domain.Interface.Application;

namespace DesafioMPCore.API.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class MerchantController : Controller
    {
        IMerchantApp _merchantApp;

        public MerchantController(IMerchantApp merchantApp)
        {
            _merchantApp = merchantApp;
        }

        // GET: api/merchants/{idUser}
        [HttpGet("merchants/{idUser}", Name = "merchants")]
        public async Task<IActionResult> Get(string idUser)
        {
            try
            {
                if (String.IsNullOrEmpty(idUser))
                    return BadRequest();

                var merchants = await _merchantApp.SeekMerchants(idUser);

                if (merchants == null)
                    return NoContent();

                return Ok(merchants);
            }
            catch
            {
                return StatusCode(500, Error.CreateInternalError());
            }
        }

    }
}
