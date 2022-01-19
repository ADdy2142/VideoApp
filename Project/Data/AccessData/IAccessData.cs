using Project.Data.Context;
using Project.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.AccessData
{
    // سرویس هایی را که باید در AccessData پیاده سازی شوند مشخص می کند
    public interface IAccessData : IDisposable
    {
        IUserRepository Users { get; }
        IAvatarRepository Avatars { get; }
        IVideoRepository Videos { get; }
        IGroupRepository Groups { get; }
        IGenreRepository Genres { get; }
        IHistoryRepository Histories { get; }
        IUserVideoRepository UserVideos { get; }
        IVideoGenreRepository VideoGenres { get; }
        IVideoHistoryRepository VideoHistories { get; }

        ApplicationDbContext Context { get; }

        Task SaveAsync();
    }
}