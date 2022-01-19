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
    // پیاده سازی سرویس های مربوط به VideoHistory
    public class VideoHistoryRepository : IVideoHistoryRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public VideoHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک رابطه جدید بین Video و History
        public void Add(VideoHistory videoHistory)
        {
            _context.VideoHistories.Add(videoHistory);
        }

        // یک VideoHistory با شناسه ویدئو و شناسه تاریخچه ورودی متد پیدا کرده و آنرا پاس می دهد
        public async Task<VideoHistory> GetAsync(int videoId, int historyId)
        {
            return await _context
                .VideoHistories
                .FirstOrDefaultAsync(vh => vh.VideoId == videoId && vh.HistoryId == historyId);
        }

        // حذف یک VideoHistory با شناسه ویدئو و شناسه تاریخچه
        public async void Delete(int videoId, int historyId)
        {
            var videoHistory = await GetAsync(videoId, historyId);
            Delete(videoHistory);
        }

        // حذف یک VideoHistory
        public void Delete(VideoHistory videoHistory)
        {
            _context.VideoHistories.Remove(videoHistory);
        }
    }
}