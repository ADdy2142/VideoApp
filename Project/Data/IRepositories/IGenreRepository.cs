using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در GenreRepository پیاده سازی شوند مشخص می کند
    public interface IGenreRepository
    {
        void Add(Genre genre);

        Task<Genre> GetLastAsync();

        Task<Genre> GetAsync(int id);

        Task<ObservableCollection<Genre>> GetAllAsync();

        Task<ObservableCollection<Genre>> GetGenresAsync(string searchText = "");

        void Update(Genre genre);

        void Delete(int id);

        void Delete(Genre genre);
    }
}