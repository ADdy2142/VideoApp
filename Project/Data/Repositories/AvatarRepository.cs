using Project.Data.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    // پیاده سازی سرویس های مربوط به Avatar
    public class AvatarRepository : IAvatarRepository
    {
        // این متد همه Avatar ها را بر می گرداند
        public ObservableCollection<Avatar> GetAll()
        {
            return new ObservableCollection<Avatar>()
                {
                    new Avatar()
                    {
                        Name = "avatar-1",
                        IsSelected = true
                    },
                    new Avatar()
                    {
                        Name = "avatar-2",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-3",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-4",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-5",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-6",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-7",
                        IsSelected = false
                    },
                    new Avatar()
                    {
                        Name = "avatar-8",
                        IsSelected = false
                    }
                };
        }
    }
}