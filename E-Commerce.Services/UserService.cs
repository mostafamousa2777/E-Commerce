using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
            var user= await _userManager.FindByEmailAsync(loginDto.Email);
            if(user != null) {
                var result = await _SignInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
                if (result.Succeeded)
                    return new UserDto {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = _tokenService.GenerateToken(user)

                    
                    };
            
            }
            return null;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto Dto)
        {
            var user= await _userManager.FindByEmailAsync(Dto.Email);
            if (user != null) throw new Exception("Email Alredy Exsist");
            var appuser = new ApplicationUser
            {
                DisplayName = Dto.DisplayName,
                Email = Dto.Email,
                UserName = Dto.DisplayName,
            };
            var resualt=await _userManager.CreateAsync(appuser,Dto.Password);
            if(!resualt.Succeeded) throw new Exception("Invalid Email or Password");
            return new UserDto { 
            DisplayName= appuser.DisplayName,
            Email = appuser.Email,  
            Token= _tokenService.GenerateToken(appuser)

            };
        }
    }
}
