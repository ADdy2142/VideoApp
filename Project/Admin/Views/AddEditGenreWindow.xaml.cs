using Project.Data.AccessData;
using Project.Data.Context;
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
    /// Interaction logic for AdminAddEditGenreWindow.xaml
    /// </summary>
    public partial class AddEditGenreWindow : Window
    {
        // یک شی از نوع Genre
        private Genre _genre;

        // سازنده
        public AddEditGenreWindow(Genre genre)
        {
            InitializeComponent();

            _genre = genre;
        }

        // با اجرای این متد یک ژانر جدید اضافه یا ژانر مشخص شده ویرایش می شود
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
                return;

            ApplicationDbContext context = new ApplicationDbContext();
            using (IAccessData accessData = new AccessData())
            {
                if (_genre == null)
                {
                    var genre = new Genre() { Title = TxtTitle.Text };
                    accessData.Genres.Add(genre);
                }
                else
                {
                    var genre = new Genre() { Id = _genre.Id, Title = TxtTitle.Text };
                    accessData.Genres.Update(genre);
                }

                await accessData.SaveAsync();
            }

            this.DialogResult = true;
        }

        // این متد هنگامی اجرا می شود که پنجره به صورت کامل بارگذاری شده باشد
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_genre != null)
            {
                RunWindowFirstTitle.Text = "ویرایش ژانر";
                RunWindowLastTitle.Text = _genre.Title;
                TxtTitle.Text = _genre.Title;
            }

            TxtTitle.Focus();
        }
    }
}