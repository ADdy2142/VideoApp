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
    // پیاده سازی سرویس های مربوط به Video
    public class VideoRepository : IVideoRepository
    {
        // تعریف یک شی از نوع ApplicationDbContext
        private ApplicationDbContext _context;

        // سازنده
        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // اضافه کردن یک ویدئو جدید
        public void Add(Video video)
        {
            _context.Videos.Add(video);
        }

        // پیدا کردن یک ویدئو با شناسه آن و پاس دادن آن به خروجی
        public async Task<Video> GetAsync(int id)
        {
            return await _context
                .Videos
                .Include(v => v.FavoriteForUsers)
                .Include(v => v.Genres)
                .Include(v => v.Group)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // آخرین ویدئو اضافه شده به پایگاه داده را پیدا کرده و آنرا بر می گرداند
        public async Task<Video> GetLastAsync()
        {
            var videos = await _context
                .Videos
                .ToListAsync();

            return videos.LastOrDefault();
        }

        // همه ویدئوهای موجود در پایگاه داده را بر می گرداند
        public async Task<ObservableCollection<Video>> GetAllAsync()
        {
            var videos = await _context
                .Videos
                .ToListAsync();

            return new ObservableCollection<Video>(videos);
        }

        // ویدئوها را بر اساس متن ورودی فیلتر کرده و به خروجی می برد و اگر متن خالی باشد همه ویدئوها را بر می گرداند
        public async Task<ObservableCollection<Video>> GetVideosAsync(string searchText = "")
        {
            List<Video> videos;

            if (string.IsNullOrEmpty(searchText))
            {
                videos = await _context
                    .Videos
                    .Include(v => v.Genres)
                    .Include(v => v.FavoriteForUsers)
                    .Include(v => v.Group)
                    .OrderBy(v => v.Title)
                    .ToListAsync();
            }
            else
            {
                videos = await _context
                    .Videos
                    .Include(v => v.Genres)
                    .Include(v => v.FavoriteForUsers)
                    .Include(v => v.Group)
                    .Where(v => v.Title.ToLower().Contains(searchText) || v.Genres.Any(g => g.Genre.Title.ToLower().Contains(searchText)))
                    .OrderByDescending(v => v.Visits)
                    .ToListAsync();
            }

            return new ObservableCollection<Video>(videos);
        }

        // ویدئوها را بر اساس گروه و ژانر فیلتر کرده و به خروجی می برد
        public async Task<ObservableCollection<Video>> GetVideosByFilterAsync(List<Group> groups, List<Genre> genres)
        {
            var videos = await _context
                .Videos
                .Include(v => v.Group)
                .Include(v => v.Genres)
                .ToListAsync();

            if (groups != null && groups.Count > 0)
            {
                videos = videos
                    .Where(v => groups.Any(g => g.Id == v.GroupId))
                    .ToList();
            }

            if (genres != null && genres.Count > 0)
            {
                videos = videos
                    .Where(v => genres.Any(g => v.Genres.Any(genre => genre.GenreId == g.Id)))
                    .ToList();
            }

            return new ObservableCollection<Video>(videos);
        }

        // ویدئوها را بر اساس شماره صفحه، گروه، ژانر و به ترتیب تعیین شده به خروجی بر می گرداند
        public async Task<ObservableCollection<Video>> GetVideosByPageAsync(int page, List<Group> groups, List<Genre> genres, OrderVideosBy orderVideosBy)
        {
            var videos = await _context
                .Videos
                .Include(v => v.Group)
                .Include(v => v.Genres)
                .ToListAsync();

            if (groups != null && groups.Count > 0)
            {
                videos = videos
                    .Where(v => groups.Any(g => g.Id == v.GroupId))
                    .ToList();
            }

            if (genres != null && genres.Count > 0)
            {
                videos = videos
                    .Where(v => genres.Any(g => v.Genres.Any(genre => genre.GenreId == g.Id)))
                    .ToList();
            }

            switch (orderVideosBy)
            {
                case OrderVideosBy.Newest:
                    videos = videos
                        .OrderByDescending(v => int.Parse(v.PublishYear))
                        .Skip((page - 1) * 15)
                        .Take(15)
                        .ToList();
                    break;

                case OrderVideosBy.MostVisited:
                    videos = videos
                        .OrderByDescending(v => v.Visits)
                        .Skip((page - 1) * 15)
                        .Take(15)
                        .ToList();
                    break;

                default:
                    break;
            }

            return new ObservableCollection<Video>(videos);
        }

        // ویدئوهای مشابه را به خروجی می برد
        public async Task<ObservableCollection<Video>> GetSimilarVideosAsync(Video video)
        {
            var genres = video
                .Genres
                .Select(g => g.Genre.Title).ToArray();

            var videos = await _context
                .Videos
                .Include(v => v.Group)
                .Include(v => v.Genres)
                .Where(v => v.Id != video.Id && (v.Group.Title == video.Group.Title || v.Genres.Any(genre => genres.Contains(genre.Genre.Title))))
                .OrderByDescending(v => v.Visits)
                .Take(10)
                .ToListAsync();

            return new ObservableCollection<Video>(videos);
        }

        // ویرایش یک ویدئو
        public void Update(Video video)
        {
            _context.Entry(video).State = EntityState.Modified;
        }

        // حذف یک ویدئو با شناسه
        public async void Delete(int id)
        {
            var video = await GetAsync(id);
            Delete(video);
        }

        // حذف یک ویدئو
        public void Delete(Video video)
        {
            _context.Videos.Remove(video);
        }

        // مشخص می کند که آیا ویدئو برای کاربر نشان شده است یا خیر
        public async Task<bool> IsFavorite(int userId, int videoId)
        {
            var video = await GetAsync(videoId);
            if (video == null)
                return false;

            return video.FavoriteForUsers.Any(f => f.UserId == userId && f.VideoId == videoId);
        }
    }
}