using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در GroupRepository پیاده سازی شوند مشخص می کند
    public interface IGroupRepository
    {
        void Add(Group group);

        Task<Group> GetLastAsync();

        Task<Group> GetAsync(int id);

        Task<ObservableCollection<Group>> GetAllAsync();

        Task<ObservableCollection<Group>> GetGroupsAsync(string searchText = "");

        void Update(Group group);

        void Delete(int id);

        void Delete(Group group);
    }
}