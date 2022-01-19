using Project.Infrastructure;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class FilterPanelViewModel : BaseViewModel
    {
        // لیست قابل نمایش از تمام گروه های موجود در پایگاه داده
        private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        // لیست قابل نمایش از تمام ژانرهای موجود در پایگاه داده
        private ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        // سازنده
        public FilterPanelViewModel()
        {
            LoadGroups();
            LoadGenres();
        }

        // بارگذاری گروه ها از پایگاه داده و ذخیره آنها در لیست گروها
        public async void LoadGroups()
        {
            Groups = await AccessData.Groups.GetAllAsync();
        }

        // بارگذاری ژانرها از پایگاه داده و ذخیره آنها در لیست ژانرها
        public async void LoadGenres()
        {
            Genres = await AccessData.Genres.GetAllAsync();
        }
    }
}