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
    // پیاده سازی سرویس های مربوط به VideoGenre
    public class VideoGenreRepository : IVideoGenreRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public VideoGenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک رابطه جدید بین Video و Genre
        public void Add(VideoGenre videoGenre)
        {
            _context.VideoGenres.Add(videoGenre);
        }

        // یک ویدئو را با شناسه ویدئو و شناسه ژانر پیدا می کند و آنرا به خروجی پاس می دهد
        public async Task<VideoGenre> GetAsync(int videoId, int genreId)
        {
            return await _context
                .VideoGenres
                .FirstOrDefaultAsync(vg => vg.VideoId == videoId && vg.GenreId == genreId);
        }

        // ویرایش یک VideoGenre
        public void Update(VideoGenre videoGenre)
        {
            _context.Entry(videoGenre).State = EntityState.Modified;
        }

        // حذف یک VideoGenre با شناسه ویدئو و شناسه ژانر
        public async void Delete(int videoId, int genreId)
        {
            var videoGenre = await GetAsync(videoId, genreId);
            Delete(videoGenre);
        }

        // حذف یک VideoGenre
        public void Delete(VideoGenre videoGenre)
        {
            _context.VideoGenres.Remove(videoGenre);
        }

        // این متد بررسی می کند که آیا ویدئو دارای ژانر مشخص شده می باشد یا خیر
        public async Task<bool> IsExitsAsync(int videoId, int genreId)
        {
            var videoGenre = await GetAsync(videoId, genreId);
            return videoGenre != null;
        }
    }
}