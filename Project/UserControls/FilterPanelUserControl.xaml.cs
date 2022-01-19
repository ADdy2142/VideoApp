using Project.Infrastructure;
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

namespace Project.UserControls
{
    /// <summary>
    /// Interaction logic for FilterPanelUserControl.xaml
    /// </summary>
    public partial class FilterPanelUserControl : UserControl
    {
        // لیست گروه های انتخاب شده توسط کاربر برای اعمال فیلتر روی ویدئوها
        public List<int> GroupIds { get; set; }

        // لیست ژانرهای انتخاب شده توسط کاربر برای اعمال فیلتر روی ویدئوها
        public List<int> GenreIds { get; set; }

        // رویداد تغییر فیلترها
        public event EventHandler FilterChanged;

        // سازنده
        public FilterPanelUserControl()
        {
            InitializeComponent();

            GroupIds = new List<int>();
            GenreIds = new List<int>();
        }

        // این متد هنگامی فراخوانی می شود که کاربر گروهی را انتخاب یا آن را حذف کند
        private void CkbGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as CheckBox;
            var tag = button.Tag.ToString();
            var groupId = int.Parse(tag);
            switch (button.IsChecked)
            {
                case true:
                    GroupIds.Add(groupId);
                    break;

                case false:
                    GroupIds.Remove(groupId);
                    break;

                default:
                    break;
            }

            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        // این متد هنگامی فراخوانی می شود که کاربر ژانری را انتخاب یا آن را حذف کند
        private void CkbGenre_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as CheckBox;
            var tag = button.Tag.ToString();
            var genreId = int.Parse(tag);
            switch (button.IsChecked)
            {
                case true:
                    GenreIds.Add(genreId);
                    break;

                case false:
                    GenreIds.Remove(genreId);
                    break;

                default:
                    break;
            }

            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        // هنگام کلیک روی حذف همه گروه ها این متد فراخوانی می شود
        private void BtnDeleteAllGroups_Click(object sender, RoutedEventArgs e)
        {
            if (GroupIds.Count > 0)
            {
                ViewModel.LoadGroups();

                GroupIds.Clear();
                FilterChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // هنگام کلیک روی حذف همه ژانر ها این متد فراخوانی می شود
        private void BtnDeleteAllGenres_Click(object sender, RoutedEventArgs e)
        {
            if (GenreIds.Count > 0)
            {
                ViewModel.LoadGenres();

                GenreIds.Clear();
                FilterChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}