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
    public class GroupsViewModel : BaseViewModel
    {
        // رشته جستجو شده توسط کاربر
        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                GetGroups();
            }
        }

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

        // سازنده
        public GroupsViewModel()
        {
            GetGroups();
        }

        // تمام ویدئوها را از پایگاه داده دریافت می کند و آنها را در لیست ذخیره می کند
        private async void GetGroups()
        {
            Groups = await AccessData.Groups.GetGroupsAsync(SearchText);
        }

        // هدایت به پنجره افزودن یا ویرایش گروه
        public async void GoToAddEditWindow(int id)
        {
            var group = await AccessData.Groups.GetAsync(id);
            var window = new AddEditGroupWindow(group);
            var result = window.ShowDialog();
            if (result == true)
            {
                ReloadEntity(group ?? await AccessData.Groups.GetLastAsync());
                GetGroups();
            }
        }

        // حذف گروه
        public async void DeleteGroup(int id)
        {
            var result = MessageBox.Show("آیا از حذف این گروه اطمینان دارید؟", "حذف گروه", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (result == MessageBoxResult.Yes)
            {
                var group = await AccessData.Groups.GetAsync(id);
                AccessData.Groups.Delete(group);
                await AccessData.SaveAsync();

                GetGroups();
            }
        }
    }
}