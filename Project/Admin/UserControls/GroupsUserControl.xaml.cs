using Project.Data.AccessData;
using Project.Data.Context;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AdminGroupsUserControl.xaml
    /// </summary>
    public partial class GroupsUserControl : UserControl
    {
        // سازنده
        public GroupsUserControl()
        {
            InitializeComponent();
        }

        // ویرایش یک گروه
        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var id = int.Parse(tag);
            ViewModel.GoToAddEditWindow(id);
        }

        // حذف یک گروه
        private void BtnDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var id = int.Parse(tag);
            ViewModel.DeleteGroup(id);
        }

        // اضافه کردن یک گروه
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToAddEditWindow(0);
        }

        // این متد هنگامی اجرا می شود که مدیر اقدام به جستجو یک گروه کند
        private void TxtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = (sender as TextBox).Text;
            ViewModel.SearchText = searchText;
        }
    }
}