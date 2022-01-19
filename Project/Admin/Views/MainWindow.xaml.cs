using Project.Admin.UserControls;
using Project.Views;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // عنوان محتوای فعلی صفحه در این متغیر ذخیره می شود
        private string _currentContent = "گروه";

        // سازنده
        public MainWindow()
        {
            InitializeComponent();
        }

        // این متد هنگامی فراخوانی می شود که روی دکمه های موجود در بالای پنجره مدیریت کلیک شود
        private void NavbarButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            var content = button.Content.ToString();
            if (content == _currentContent)
                return;

            _currentContent = content;
            GrdContent.Children.Clear();
            UserControl control;

            switch (content)
            {
                case "گروه":
                    control = new GroupsUserControl();
                    GrdContent.Children.Add(control);
                    break;

                case "ژانر":
                    control = new GenresUserControl();
                    GrdContent.Children.Add(control);
                    break;

                case "ویدئو":
                    control = new VideosUserControl();
                    GrdContent.Children.Add(control);
                    break;

                default:
                    break;
            }
        }

        // با اجرای این متد به پنجره ورود هدایت می شوید
        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}