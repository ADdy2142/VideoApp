using Project.Data.Context;
using Project.Data.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    // پیاده سازی سرویس های مربوط به User
    public class UserRepository : IUserRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ثبت یک کاربر جدید در پایگاه داده
        public bool SignUp(User user)
        {
            try
            {
                _context.Users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // بررسی درست اطلاعات کاربر و اجازه ورود به کاربر در صورت درست بودن اطلاعات
        public async Task<User> SignInAsync(string email, string password)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        // یک کاربر را به وسیله شناسه آن پیدا کرده و به خروجی پاس می دهد
        public async Task<User> GetAsync(int id)
        {
            return await _context
                .Users
                .Include(u => u.FavoriteVideos)
                .Include(u => u.Histories)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // تمام کاربران موجود در پایگاه داده را بر می گرداند
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context
                .Users
                .ToListAsync();
        }

        // ویرایش یک کاربر
        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        // بررسی کلمه عبور قدیمی یک کاربر هنگام تغییر کلمه عبور
        public async Task<bool> CheckOldPasswordAsync(int userId, string oldPassword)
        {
            var user = await GetAsync(userId);
            return user.Password.Equals(oldPassword);
        }

        // بررسی موجود بودن ایمیل وارد شده توسط کاربر هنگام عملیات ثبت نام و اگر ایمیل موجود باشد این متد False را بر می گرداند
        public async Task<bool> IsEmailExistAsync(string email)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
        }
    }
}