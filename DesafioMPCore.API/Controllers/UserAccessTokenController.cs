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
    [Route("api")]
    public class UserAccessTokenController : Controller
    {
        IAccessTokenApp _accessTokenService;

        public UserAccessTokenController(IAccessTokenApp accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        // POST: api/user/AccessToken
        [HttpPost("user/AccessToken", Name = "user/AccessToken")]
        public async Task<IActionResult> Post([FromBody]Models.User userModel)
        {
            try
            {
                if (userModel == null || String.IsNullOrEmpty(userModel.UserName) || String.IsNullOrEmpty(userModel.Password))
                    return BadRequest();

                var user = userModel.ToDomain();
                if (user == null)
                    return BadRequest();

                var token = await _accessTokenService.AuthenticateToCreateUserAccessToken(user);

                if (token == null || String.IsNullOrEmpty(token.AccessToken))
                    return Forbid("Login negado.");
                else
                    return Ok(token);
            }
            catch
            {
                return StatusCode(500, Error.CreateInternalError());
            }

        }

    }
}
