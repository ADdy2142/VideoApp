using Project.Infrastructure;
using Project.Models;
using Project.UserControls;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class VideoListViewModel : BaseViewModel
    {
        // لیست قابل نمایش از نوع ویدئو برای نمایش ویدئوهای فیلتر شده
        private ObservableCollection<Video> _videosByFilter = new ObservableCollection<Video>();

        // نمایش ویدئو با ترتیبی خاص
        public OrderVideosBy OrderVideosBy { get; set; } = OrderVideosBy.Newest;

        // فیلتر ویدئوها بر اساس گروه های موجود در لیست زیر
        public List<int> FilterGroupIds { get; set; }

        // فیلتر ویدئوها بر اساس ژانرهای موجود در لیست زیر
        public List<int> FilterGenreIds { get; set; }

        // ویدئو انتخاب شده جهت مشاهده جرئیات آن
        private Video _selectedVideo;

        public Video SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                OnPropertyChanged(nameof(SelectedVideo));
            }
        }

        // لیست قابل نمایش از نوع ویدئو جهت نمایش تمام ویدئوهای موجود در پایگاه داده
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

        // صفحه بندی ویدئوها و نمایش تعداد خاصی از آنها در یک صفحه
        private ObservableCollection<Pagination> _paginations = new ObservableCollection<Pagination>();

        public ObservableCollection<Pagination> Paginations
        {
            get => _paginations;
            set
            {
                _paginations = value;
                OnPropertyChanged(nameof(Paginations));
            }
        }

        // بارگذاری داده های موجود
        public void LoadData()
        {
            GetVideosByPage();
        }

        // ویدئوها را بر اساس صفحه آنها نمایش می دهد
        public async void GetVideosByPage(int page = 1)
        {
            var groups = new List<Group>();
            var genres = new List<Genre>();

            if (FilterGroupIds != null)
            {
                foreach (var groupId in FilterGroupIds)
                {
                    var group = await AccessData.Groups.GetAsync(groupId);
                    groups.Add(group);
                }
            }

            if (FilterGenreIds != null)
            {
                foreach (var genreId in FilterGenreIds)
                {
                    var genre = await AccessData.Genres.GetAsync(genreId);
                    genres.Add(genre);
                }
            }

            if (OrderVideosBy == OrderVideosBy.Favorite)
            {
                var userVideos = await AccessData.UserVideos.GetFavoritesByPageAsync(UserId, page);
                var videos = userVideos.Select(uv => uv.Video);
                Videos = new ObservableCollection<Video>(videos);
            }
            else
            {
                var videos = await AccessData.Videos.GetVideosByPageAsync(page, groups, genres, OrderVideosBy);
                Videos = videos;
            }

            _videosByFilter = await AccessData.Videos.GetVideosByFilterAsync(groups, genres);
            Paginations = CalculatePaginations(page);
        }

        // این متد یک لیست قابل نمایش از نوع صفحه بندی را به خروجی پاس می دهد که نشان دهنده تعداد صفحات موجود برای نمایش ویدئوها است
        private ObservableCollection<Pagination> CalculatePaginations(int page = 1)
        {
            var result = new ObservableCollection<Pagination>();
            for (int i = 1; i < _videosByFilter.Count; i = i + 15)
                result.Add(new Pagination() { PageNumber = i / 15 + 1, IsActive = i / 15 + 1 == page });

            return result;
        }

        // هدایت به پنجره جزئیات ویدئو
        public async void GoToVideoDetailWindow()
        {
            if (SelectedVideo == null)
                return;

            var video = await AccessData.Videos.GetAsync(SelectedVideo.Id);
            var dataContext = new VideoDetailViewModel() { Video = video };
            var userControl = new VideoDetailUserControl() { DataContext = dataContext };
            BaseNavigation.Push($"مشخصات {SelectedVideo.Title}", userControl);
            SelectedVideo = null;
        }

        // افزودن ویدوئو یا حذف آن از لیست نشان شده ها
        public async void ToggleFavoriteVideo(int videoId)
        {
            var video = await AccessData.Videos.GetAsync(videoId);
            if (video == null)
                return;

            if (CurrentUser == null)
                return;

            int userId = Properties.Settings.Default.UserId;
            if (await AccessData.UserVideos.IsFavoriteForUserAsync(userId, video.Id))
            {
                var userVideo = await AccessData.UserVideos.GetAsync(userId, video.Id);
                AccessData.UserVideos.Delete(userVideo);
            }
            else
            {
                var userVideo = new UserVideo() { UserId = userId, VideoId = video.Id };
                AccessData.UserVideos.Add(userVideo);
            }

            await AccessData.SaveAsync();
        }
    }
}