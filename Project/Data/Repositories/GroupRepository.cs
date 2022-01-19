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
    // پیاده سازی سرویس های مربوط به Group
    public class GroupRepository : IGroupRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک گروه جدید
        public void Add(Group group)
        {
            _context.Groups.Add(group);
        }

        // آخرین گروه ذخیره شده در پایگاه داده را بر می گرداند
        public async Task<Group> GetLastAsync()
        {
            var groups = await _context
                .Groups
                .ToListAsync();

            return groups.LastOrDefault();
        }

        // گروهی که شناسه آن با شناسه موجود در ورودی یکی باشد پیدا کرده و آنرا بر می گرداند
        public async Task<Group> GetAsync(int id)
        {
            var groups = await _context
                .Groups
                .ToListAsync();

            return groups.FirstOrDefault(g => g.Id == id);
        }

        // همه گروه ها را بر می گرداند
        public async Task<ObservableCollection<Group>> GetAllAsync()
        {
            var groups = await _context
                .Groups
                .OrderBy(g => g.Title)
                .ToListAsync();

            return new ObservableCollection<Group>(groups);
        }

        // گروه ها را بر اساس متن ورودی متد فیلتر کرده و به خروجی پاس می دهد و اگر رشته خالی باشد همه گروه ها را بر می گرداند
        public async Task<ObservableCollection<Group>> GetGroupsAsync(string searchText = "")
        {
            List<Group> groups;

            if (string.IsNullOrEmpty(searchText))
            {
                groups = await _context
                    .Groups
                    .Include(g => g.Videos)
                    .OrderBy(g => g.Title)
                    .ToListAsync();
            }
            else
            {
                groups = await _context
                    .Groups
                    .Include(g => g.Videos)
                    .Where(g => g.Title.Contains(searchText))
                    .OrderBy(g => g.Title)
                    .ToListAsync();
            }

            return await Task.FromResult(new ObservableCollection<Group>(groups));
        }

        // ویرایش یک گروه
        public void Update(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
        }

        // حذف یک گروه با شناسه
        public async void Delete(int id)
        {
            var group = await GetAsync(id);
            Delete(group);
        }

        // حذف یک گروه
        public void Delete(Group group)
        {
            _context.Groups.Remove(group);
        }
    }
}