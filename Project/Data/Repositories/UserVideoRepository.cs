using Project.Data.Context;
using Project.Data.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    // پیاده سازی سرویس های مربوط به UserVideo
    public class UserVideoRepository : IUserVideoRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public UserVideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک رابطه جدید بین User و Video
        public void Add(UserVideo userVideo)
        {
            _context.UserVideos.Add(userVideo);
        }

        // یک UserVideo با شناسه کاربر و شناسه ویدئو مشخص شده پیدا می کند و آنرا به خروجی می برد
        public async Task<UserVideo> GetAsync(int userId, int videoId)
        {
            return await _context
                .UserVideos
                .FirstOrDefaultAsync(uv => uv.UserId == userId && uv.VideoId == videoId);
        }

        // ویدئوهای نشان شده را بر اساس شماره صفحه یه خروجی پاس می دهد
        public async Task<ObservableCollection<UserVideo>> GetFavoritesByPageAsync(int userId, int page)
        {
            var userVideos = await _context
                .UserVideos
                .Include(uv => uv.User)
                .Include(uv => uv.Video)
                .Where(uv => uv.UserId == userId)
                .OrderBy(uv => uv.Video.Title)
                .Skip((page - 1) * 15)
                .Take(15)
                .ToListAsync();

            return new ObservableCollection<UserVideo>(userVideos);
        }

        // حذف یک UserVideo با شناسه کاربر و شناسه ویدئو
        public async void Delete(int userId, int videoId)
        {
            var userVideo = await GetAsync(userId, videoId);
            Delete(userVideo);
        }

        // حذف یک UserVideo
        public void Delete(UserVideo userVideo)
        {
            _context.UserVideos.Remove(userVideo);
        }

        // این متد بررسی می کند که آیا ویدئو با شناسه مشخص شده جزو ویدئوهای نشان شده کاربر می باشد یا خیر
        public async Task<bool> IsFavoriteForUserAsync(int userId, int videoId)
        {
            return await _context
                .UserVideos
                .AnyAsync(uv => uv.UserId == userId && uv.VideoId == videoId);
        }
    }
}