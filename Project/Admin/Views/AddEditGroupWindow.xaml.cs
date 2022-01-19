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
    /// Interaction logic for AdminAddEditGroupWindow.xaml
    /// </summary>
    public partial class AddEditGroupWindow : Window
    {
        // یک شی از نوع Group
        private Group _group;

        // سازنده
        public AddEditGroupWindow(Group group = null)
        {
            InitializeComponent();

            _group = group;
        }

        // با اجرای این متد یک گروه جدید اضافه یا گروهی که مشخص شده ویرایش می شود
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
                return;

            ApplicationDbContext context = new ApplicationDbContext();
            using (IAccessData accessData = new AccessData())
            {
                if (_group == null)
                {
                    var group = new Group() { Title = TxtTitle.Text };
                    accessData.Groups.Add(group);
                }
                else
                {
                    var group = new Group() { Id = _group.Id, Title = TxtTitle.Text };
                    accessData.Groups.Update(group);
                }

                await accessData.SaveAsync();
            }

            this.DialogResult = true;
        }

        // این متد هنگامی اجرا می شود که پنجره به صورت کامل بارگذاری شده باشد
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_group != null)
            {
                RunWindowFirstTitle.Text = "ویرایش گروه";
                RunWindowLastTitle.Text = _group.Title;
                TxtTitle.Text = _group.Title;
            }

            TxtTitle.Focus();
        }
    }
}