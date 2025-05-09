using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        //Check Email
        [HttpGet("CheckEmail")]//GET BaseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }

        //Get Current User
        [Authorize]
        [HttpGet("CurrentUser")]//GET BaseUrl/api/Authentication/CurrentUser
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken());
            return Ok(AppUser);
        }
        //Get Current User Address
        [Authorize]
        [HttpGet("Address")]//GET BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(Address);
        }
        //Update Current User Address
        [Authorize]
        [HttpPut("Address")]//GET BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDTO>> UpdateCurrentUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdateAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, addressDTO);
            return Ok(UpdateAddress);
        }

    }
}
