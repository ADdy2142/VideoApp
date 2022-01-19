using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.IRepositories
{
    // سرویس هایی را که باید در AvatarRepository پیاده سازی شوند مشخص می کند
    public interface IAvatarRepository
    {
        ObservableCollection<Avatar> GetAll();
    }
}