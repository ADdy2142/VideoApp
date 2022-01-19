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
    // پیاده سازی سرویس های مربوط به Genre
    public class GenreRepository : IGenreRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن ژانر جدید به پایگاه داده
        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
        }

        // این متد آخرین ژانر موجود در پایگاه داده را بر می گرداند
        public async Task<Genre> GetLastAsync()
        {
            var genres = await _context
                .Genres
                .ToListAsync();

            return genres.LastOrDefault();
        }

        // ورودی این متد شناسه یک ژانر است و سپس با همان شناسه ژانر مورد نظر را پیدا می کند و آن را به خروجی پاس می دهد
        public async Task<Genre> GetAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        // همه ژانرهای موجود در پایگاه داده را بر می گرداند
        public async Task<ObservableCollection<Genre>> GetAllAsync()
        {
            var genres = await _context
                .Genres
                .OrderBy(g => g.Title)
                .ToListAsync();

            return new ObservableCollection<Genre>(genres);
        }

        // ژانرهای مشابه با ورودی متد را از داخل پایگاه داده پیدا کرده و به خروجی برمی گرداند و اگر ورودی متد خالی باشد همه ژانرها را بر می گرداند
        public async Task<ObservableCollection<Genre>> GetGenresAsync(string searchText = "")
        {
            List<Genre> genres;

            if (string.IsNullOrEmpty(searchText))
            {
                genres = await _context
                    .Genres
                    .Include(g => g.Videos)
                    .OrderBy(g => g.Title)
                    .ToListAsync();
            }
            else
            {
                genres = await _context
                    .Genres
                    .Include(g => g.Videos)
                    .Where(g => g.Title.Contains(searchText))
                    .OrderBy(g => g.Title)
                    .ToListAsync();
            }

            return new ObservableCollection<Genre>(genres);
        }

        // ژانر مورد نظر را در پایگاه داده ویرایش می کند
        public void Update(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
        }

        // یک ژانر را با شناسه آن از پایگاه داده حذف می کند
        public async void Delete(int id)
        {
            var genre = await GetAsync(id);
            Delete(genre);
        }

        // ورودی این متد یک ژانر است و این ژانر را در پایگاه داده حذف می کند
        public void Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
        }
    }
}