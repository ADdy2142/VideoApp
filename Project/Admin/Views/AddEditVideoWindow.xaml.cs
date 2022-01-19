using Project.Data.AccessData;
using Project.Data.Context;
using Project.Infrastructure;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.Admin.Views
{
    /// <summary>
    /// Interaction logic for AdminAddEditVideoWindow.xaml
    /// </summary>
    public partial class AddEditVideoWindow : Window
    {
        // یک شی از نوع Video
        private Video _video;

        // سازنده
        public AddEditVideoWindow(Video video)
        {
            InitializeComponent();

            _video = video;
        }

        // این متد هنگامی فراخوانی می شود که پنجره به صورت کامل بارگذاری شده باشد
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (IAccessData accessData = new AccessData())
            {
                CmbGroups.ItemsSource = await accessData.Groups.GetAllAsync();
                LstGenres.ItemsSource = await accessData.Genres.GetAllAsync();

                if (_video != null)
                {
                    foreach (var item in LstGenres.Items)
                    {
                        var listViewItem = LstGenres.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                        var genre = listViewItem.DataContext as Genre;
                        if (_video.Genres.Any(g => g.GenreId == genre.Id))
                            listViewItem.IsSelected = true;
                    }

                    TxtTitle.Text = _video.Title;
                    CmbGroups.SelectedItem = await accessData.Groups.GetAsync(_video.Group.Id);
                    TxtSeason.Text = _video.Season;
                    TxtSeasonState.Text = _video.SeasonState;
                    TxtPublishYear.Text = _video.PublishYear;
                    TxtScore.Text = _video.Score.ToString();
                    TxtDuration.Text = _video.Duration.ToString();
                    TxtImageUrl.Text = _video.ImageUrl;
                    TxtDescription.Text = _video.Description;
                }
                else
                {
                    if (CmbGroups.Items.Count > 0)
                        CmbGroups.SelectedIndex = 0;
                }
            }

            TxtTitle.Focus();
        }

        // با اجرای این متد ویدئو جدید اضافه یا ویدئو مشخص شده ویرایش می شود
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            double score;

            if (string.IsNullOrWhiteSpace(TxtTitle.Text) || CmbGroups.SelectedItem == null || string.IsNullOrWhiteSpace(TxtPublishYear.Text) || string.IsNullOrWhiteSpace(TxtScore.Text) || !double.TryParse(TxtScore.Text.Replace('/', '.'), out score) || string.IsNullOrWhiteSpace(TxtDuration.Text) || string.IsNullOrWhiteSpace(TxtImageUrl.Text) || string.IsNullOrWhiteSpace(TxtDescription.Text) || LstGenres.SelectedItems.Count == 0)
            {
                MessageBox.Show("لطفا تمام موارد را کامل کنید.", "مدیریت", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            if (score < 0 && score > 10)
            {
                MessageBox.Show("لطفا نمره فیلم را به درستی وارد کنید.", "مدیریت", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                return;
            }

            using (IAccessData accessData = new AccessData())
            {
                if (_video == null)
                {
                    var video = new Video()
                    {
                        Title = TxtTitle.Text,
                        GroupId = (CmbGroups.SelectedItem as Group).Id,
                        Season = TxtSeason.Text,
                        SeasonState = TxtSeasonState.Text,
                        PublishYear = TxtPublishYear.Text,
                        Score = score,
                        Visits = 0,
                        Description = TxtDescription.Text,
                        Duration = int.Parse(TxtDuration.Text),
                        ImageUrl = TxtImageUrl.Text
                    };

                    accessData.Videos.Add(video);
                    await accessData.SaveAsync();

                    foreach (var item in LstGenres.SelectedItems)
                    {
                        var genre = item as Genre;
                        accessData.VideoGenres.Add(new VideoGenre() { VideoId = video.Id, GenreId = genre.Id });
                    }

                    await accessData.SaveAsync();
                    accessData.Context.ReloadEntity(video);
                }
                else
                {
                    var video = new Video()
                    {
                        Id = _video.Id,
                        Title = TxtTitle.Text,
                        GroupId = (CmbGroups.SelectedItem as Group).Id,
                        Season = TxtSeason.Text,
                        SeasonState = TxtSeasonState.Text,
                        PublishYear = TxtPublishYear.Text,
                        Score = score,
                        Visits = _video.Visits,
                        Description = TxtDescription.Text,
                        Duration = int.Parse(TxtDuration.Text),
                        ImageUrl = TxtImageUrl.Text
                    };

                    accessData.Videos.Update(video);
                    await accessData.SaveAsync();

                    video = await accessData.Videos.GetAsync(video.Id);

                    while (video.Genres.Count > 0)
                    {
                        var videoGenre = video.Genres.First();
                        var item = await accessData.VideoGenres.GetAsync(videoGenre.VideoId, videoGenre.GenreId);
                        accessData.VideoGenres.Delete(item);
                    }

                    await accessData.SaveAsync();

                    foreach (var item in LstGenres.SelectedItems)
                    {
                        var genre = item as Genre;
                        accessData.VideoGenres.Add(new VideoGenre() { VideoId = video.Id, GenreId = genre.Id });
                    }

                    await accessData.SaveAsync();
                    accessData.Context.ReloadEntity(video);

                    foreach (var videoGenre in video.Genres)
                        accessData.Context.ReloadEntity(videoGenre);
                }
            }

            this.DialogResult = true;
        }

        // این متد هنگامی فراخوانی می شود که مقدار موجود در فیلد سال انتشار ویدئو تغییر کند
        private void TxtPublishYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int year;
            e.Handled = !int.TryParse(e.Text, out year);
        }

        // این متد هنگامی فراخوانی می شود که مقدار موجود در فیلد امتیاز ویدئو تغییر کند
        private void TxtScore_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text;
            string source = e.Source.ToString();
            string score = source.Substring(source.LastIndexOf(':') + 1).Trim();
            if (input.Length > 1)
                e.Handled = true;
            else if (score.Length == 1 && score == "0" && input == "0")
                e.Handled = true;
            else if (score.Contains('.') && input.LastOrDefault() == '.')
                e.Handled = true;
            else if (!(char.IsDigit(input.LastOrDefault()) || input.LastOrDefault() == '.'))
                e.Handled = true;
        }

        // این متد هنگامی فراخوانی می شود که مقدار موجود در فیلد مدت زمان ویدئو تغییر کند
        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int duration;
            e.Handled = !int.TryParse(e.Text, out duration);
        }
    }
}