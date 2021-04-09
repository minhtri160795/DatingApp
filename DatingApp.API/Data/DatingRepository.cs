using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _map;
        public DatingRepository(DataContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Users> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            var returnUsers = _map.Map<UsersForDetailDto>(user);
            return user;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            var returnUsers = _map.Map<IEnumerable<UsersForListDto>>(users);

            return users;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}