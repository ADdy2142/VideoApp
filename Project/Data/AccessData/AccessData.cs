using Project.Data.Context;
using Project.Data.IRepositories;
using Project.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.AccessData
{
    // این کلاس مختص دسترسی به داده ها و کار با پایگاه داده تعبیه شده
    public class AccessData : IAccessData
    {
        // تمامی خصوصیات زیر سرویس هایی را برای کار راحتتر با پایگاه داده پیاده سازی می کنند
        public IUserRepository Users { get; private set; }

        public IAvatarRepository Avatars { get; private set; }
        public IVideoRepository Videos { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IHistoryRepository Histories { get; private set; }
        public IUserVideoRepository UserVideos { get; private set; }
        public IVideoGenreRepository VideoGenres { get; private set; }
        public IVideoHistoryRepository VideoHistories { get; private set; }

        // یک نمونه از شی ApplicationDbContext
        private ApplicationDbContext _context;

        public ApplicationDbContext Context
        {
            get
            {
                if (_context == null)
                    _context = new ApplicationDbContext();

                return _context;
            }
        }

        // سازنده
        // پیاده سازی خصوصیات بالا
        public AccessData()
        {
            Users = new UserRepository(Context);
            Avatars = new AvatarRepository();
            Videos = new VideoRepository(Context);
            Groups = new GroupRepository(Context);
            Genres = new GenreRepository(Context);
            Histories = new HistoryRepository(Context);
            UserVideos = new UserVideoRepository(Context);
            VideoGenres = new VideoGenreRepository(Context);
            VideoHistories = new VideoHistoryRepository(Context);
        }

        // تغییرات اعمال شده روی پایگاه داده را ذخیره می کند
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        // شی Context را از بین می برد
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}