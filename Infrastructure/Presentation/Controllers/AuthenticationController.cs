using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;

namespace Presentation.Controllers
{
    
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {

        //Login
        [HttpPost("Login")] //POST BaseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(User);
        }

        //Register
        [HttpPost("Register")] //POST BaseUrl/api/Authentication/Register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(User);
        }
    }
}
