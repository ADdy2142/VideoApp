using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در UserVideoRepository پیاده سازی شوند مشخص می کند
    public interface IUserVideoRepository
    {
        void Add(UserVideo userVideo);

        Task<UserVideo> GetAsync(int userId, int videoId);

        Task<ObservableCollection<UserVideo>> GetFavoritesByPageAsync(int userId, int page);

        void Delete(int userId, int videoId);

        void Delete(UserVideo userVideo);

        Task<bool> IsFavoriteForUserAsync(int userId, int videoId);
    }
}