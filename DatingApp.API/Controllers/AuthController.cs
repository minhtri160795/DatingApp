using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    // http://localhost:5000/api/ValueController
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _map;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper map)
        {
            _config = config;
            _repo = repo;
            _map = map;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dtoUser)
        {
            var userFromRepo = await _repo.Login(dtoUser.UserName, dtoUser.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = _map.Map<UsersForListDto>(userFromRepo);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dtoUser)
        {
            dtoUser.UserName = dtoUser.UserName.ToLower();
            if (await _repo.UserExists(dtoUser.UserName))
                return BadRequest("Username already exists");
            var userToCreate = _map.Map<Users>(dtoUser);
            var createdUser = await _repo.Register(userToCreate, dtoUser.Password);
            // sai đây nè
            
            var userToReturn = _map.Map<UsersForDetailDto>(createdUser);
            return CreatedAtRoute("GetUser", new { controller = "Users", id = createdUser.Id }, userToReturn);
        }
    }
}
