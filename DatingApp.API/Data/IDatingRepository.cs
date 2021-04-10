using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveAll();
         Task<IEnumerable<Users>> GetUsers();
         Task<Users> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetCurrentMainPhoto(int id);
    }


}