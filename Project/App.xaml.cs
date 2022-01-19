using Project.Data.AccessData;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CreateAdmin();
        }

        private async void CreateAdmin()
        {
            using (IAccessData accessData = new AccessData())
            {
                var users = await accessData.Users.GetAllAsync();
                if (users.Count() == 0)
                {
                    var admin = new User()
                    {
                        Email = "admin",
                        Password = "admin",
                        Role = "Admin"
                    };

                    accessData.Users.SignUp(admin);
                    await accessData.SaveAsync();
                }
            }
        }
    }
}