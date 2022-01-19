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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.Admin.UserControls
{
    /// <summary>
    /// Interaction logic for AdminGenresUserControl.xaml
    /// </summary>
    public partial class GenresUserControl : UserControl
    {
        // سازنده
        public GenresUserControl()
        {
            InitializeComponent();
        }

        // این متد یک ژانر جدید را اضافه می کند
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToAddEditWindow(0);
        }

        // این متد یک ژانر مشخص را ویرایش می کند
        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var id = int.Parse(tag);
            ViewModel.GoToAddEditWindow(id);
        }

        // این متد یک ژانر را حذف می کند
        private void BtnDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var id = int.Parse(tag);
            ViewModel.DeleteGenre(id);
        }

        // این متد هنگامی اجرا می شود که مدیر اقدام به جستجو یک ژانر کند
        private void TxtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var searchText = (sender as TextBox).Text;
            ViewModel.SearchText = searchText;
        }
    }
}