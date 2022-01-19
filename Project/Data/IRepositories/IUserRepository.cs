using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در UserRepository پیاده سازی شوند مشخص می کند
    public interface IUserRepository
    {
        bool SignUp(User user);

        Task<User> SignInAsync(string email, string password);

        Task<User> GetAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        void Update(User user);

        Task<bool> CheckOldPasswordAsync(int userId, string oldPassword);

        Task<bool> IsEmailExistAsync(string email);
    }
}