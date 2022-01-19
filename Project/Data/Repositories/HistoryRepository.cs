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
    // پیاده سازی سرویس های مربوط به History
    public class HistoryRepository : IHistoryRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public HistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک تاریخچه جدید
        public void Add(History history)
        {
            _context.Histories.Add(history);
        }

        // پیدا کردن یک تاریخچه با شناسه آن و انتقال آن به خروجی
        public async Task<History> GetAsync(int id)
        {
            return await _context
                .Histories
                .Include(h => h.User)
                .Include(h => h.Videos)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        // این متد تمام تاریخچه های یک کاربر را از پایگاه داده پیدا کرده و آنها را بر می گرداند
        public async Task<ObservableCollection<History>> GetAllAsync(int userId)
        {
            var histories = await _context
                .Histories
                .Include(h => h.User)
                .Include(h => h.Videos)
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.Date)
                .ToListAsync();

            return new ObservableCollection<History>(histories);
        }
    }
}