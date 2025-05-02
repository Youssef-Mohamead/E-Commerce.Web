using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email) ?? throw new UserNotFoundException(loginDTO.Email);
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDTO.Password);

            if (IsPasswordValid)
            {
                return new UserDTO()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };

            }
            else
                throw new UnathorizedException();
        }


        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser()
            {

                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,
            };

            var Result = await _userManager.CreateAsync(User, registerDTO.Password);
            if (Result.Succeeded)
                return new UserDTO()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);

            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id!)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds );
            return new JwtSecurityTokenHandler().WriteToken(Token); 
        }

    }
}
