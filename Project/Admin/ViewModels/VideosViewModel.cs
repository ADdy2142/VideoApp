using Project.Admin.Views;
using Project.Infrastructure;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Admin.ViewModels
{
    public class VideosViewModel : BaseViewModel
    {
        // رشته جستجو شده توسط کاربر
        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                GetVideos();
            }
        }

        // لیست قابل نمایش از تمام ویدئوهای موجود در پایگاه داده
        private ObservableCollection<Video> _videos = new ObservableCollection<Video>();

        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set
            {
                _videos = value;
                OnPropertyChanged(nameof(Videos));
            }
        }

        // سازنده
        public VideosViewModel()
        {
            GetVideos();
        }

        // تمام ویدئوهای موجود در پایگاه داده را دریافت کرده و در لیست ذخیره می کند
        private async void GetVideos()
        {
            Videos = await AccessData.Videos.GetVideosAsync(SearchText);
        }

        // هدایت به پنجره افزودن یا ویرایش ویدئو
        public async void GoToAddEditWindow(int id)
        {
            var video = await AccessData.Videos.GetAsync(id);
            var window = new AddEditVideoWindow(video);
            var result = window.ShowDialog();
            if (result == true)
            {
                if (video != null)
                {
                    ReloadEntity(video ?? await AccessData.Videos.GetLastAsync());
                    video = await AccessData.Videos.GetAsync(id);
                    int count = video.Genres.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var videoGenre = video.Genres[i];
                        ReloadEntity(videoGenre);

                        if (video.Genres.Count != count)
                        {
                            count = video.Genres.Count;
                            i = -1;
                        }
                    }
                }

                GetVideos();
            }
        }

        // حذف یک ویدئو
        public async void DeleteVideo(int id)
        {
            var result = MessageBox.Show("آیا از حذف این ویدئو اطمینان دارید؟", "حذف ویدئو", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (result == MessageBoxResult.Yes)
            {
                var video = await AccessData.Videos.GetAsync(id);
                AccessData.Videos.Delete(video);
                await AccessData.SaveAsync();

                GetVideos();
            }
        }
    }
}