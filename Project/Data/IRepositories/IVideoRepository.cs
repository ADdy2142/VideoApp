using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در VideoRepository پیاده سازی شوند مشخص می کند
    public interface IVideoRepository
    {
        void Add(Video video);

        Task<Video> GetAsync(int id);

        Task<Video> GetLastAsync();

        Task<ObservableCollection<Video>> GetAllAsync();

        Task<ObservableCollection<Video>> GetVideosAsync(string searchText = "");

        Task<ObservableCollection<Video>> GetVideosByFilterAsync(List<Group> groups, List<Genre> genres);

        Task<ObservableCollection<Video>> GetVideosByPageAsync(int page, List<Group> groups, List<Genre> genres, OrderVideosBy orderVideosBy);

        Task<ObservableCollection<Video>> GetSimilarVideosAsync(Video video);

        void Update(Video video);

        void Delete(int id);

        void Delete(Video video);

        Task<bool> IsFavorite(int userId, int videoId);
    }
}