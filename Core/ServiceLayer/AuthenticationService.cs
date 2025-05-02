using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager
        , IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserDTO> GetCurrentUserAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ??throw new UserNotFoundException(Email);
            return new UserDTO() { DisplayName=User.DisplayName, Email=User.Email , Token= await CreateTokenAsync(User)};
        }

        public async Task<AddressDTO> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include(U => U.Address)
                                               .FirstOrDefaultAsync(U => U.Email == Email) ?? throw new UserNotFoundException(Email);

            if (User.Address is not null)
                return _mapper.Map<Address, AddressDTO>(User.Address);
            else
                throw new AddressNotFoundException(User.UserName);
        }
       
        public async Task<AddressDTO> UpdateCurrentUserAddressAsync(string Email, AddressDTO addressDTO)
        {
            var User = await _userManager.Users.Include(U => U.Address)
                                               .FirstOrDefaultAsync(U => U.Email == Email) ?? throw new UserNotFoundException(Email);

            if (User.Address is not null) // Update
            {
                User.Address.FirstName=addressDTO.FirstName;
                User.Address.LastName=addressDTO.LastName;
                User.Address.City=addressDTO.City;
                User.Address.Country=addressDTO.Country;
                User.Address.Street=addressDTO.Street;

            }
            else // Add New Address
            {
                User.Address = _mapper.Map<AddressDTO,Address>(addressDTO);
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDTO>(User.Address);
                
        }

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
