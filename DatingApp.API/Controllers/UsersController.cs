using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _map;
        public UsersController(IDatingRepository repo, IMapper map)
        {
            _repo = repo;
            _map = map;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var returnUsers = _map.Map<IEnumerable<UsersForListDto>>(users);
            return Ok(returnUsers);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var returnUser = _map.Map<UsersForDetailDto>(user);
            return Ok(returnUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UsersForUpdateDto obj)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var user = await _repo.GetUser(id);
            _map.Map(obj, user);
            if(await _repo.SaveAll())
            return NoContent();

            throw new System.Exception($"Updating user {id} failed on save");
        }
    }
}