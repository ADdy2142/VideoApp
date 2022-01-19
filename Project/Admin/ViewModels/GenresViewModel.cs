using Project.Admin.Views;
using Project.Infrastructure;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Admin.ViewModels
{
    public class GenresViewModel : BaseViewModel
    {
        // رشته جستجو شده توسط کاربر
        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                GetGenres();
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
        public GenresViewModel()
        {
            GetGenres();
        }

        // این متد ژانرها را گرفته و در لیست ذخیره می کند
        private async void GetGenres()
        {
            Genres = await AccessData.Genres.GetGenresAsync(SearchText);
        }

        // هدایت به صفحه افزودن یا ویرایش ژانر
        public async void GoToAddEditWindow(int id)
        {
            var genre = await AccessData.Genres.GetAsync(id);
            var window = new AddEditGenreWindow(genre);
            var result = window.ShowDialog();
            if (result == true)
            {
                ReloadEntity(genre ?? await AccessData.Genres.GetLastAsync());
                GetGenres();
            }
        }

        // حذف ژانر
        public async void DeleteGenre(int id)
        {
            var result = MessageBox.Show("آیا از حذف این ژانر اطمینان دارید؟", "حذف ژانر", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (result == MessageBoxResult.Yes)
            {
                var genre = await AccessData.Genres.GetAsync(id);
                AccessData.Genres.Delete(genre);
                await AccessData.SaveAsync();

                GetGenres();
            }
        }
    }
}