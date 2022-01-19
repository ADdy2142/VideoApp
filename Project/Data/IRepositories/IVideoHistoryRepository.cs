using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در VideoHistoryRepository پیاده سازی شوند مشخص می کند
    public interface IVideoHistoryRepository
    {
        void Add(VideoHistory videoHistory);

        Task<VideoHistory> GetAsync(int videoId, int historyId);

        void Delete(int videoId, int historyId);

        void Delete(VideoHistory videoHistory);
    }
}