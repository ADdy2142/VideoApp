using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در VideoGenreRepository پیاده سازی شوند مشخص می کند
    public interface IVideoGenreRepository
    {
        void Add(VideoGenre videoGenre);

        Task<VideoGenre> GetAsync(int videoId, int genreId);

        void Update(VideoGenre videoGenre);

        void Delete(int videoId, int genreId);

        void Delete(VideoGenre videoGenre);

        Task<bool> IsExitsAsync(int videoId, int genreId);
    }
}