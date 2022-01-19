using Project.Data.AccessData;
using Project.Data.Context;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Infrastructure
{
    // این کلاس پایه تمام ViewModel ها می باشد و دارای خصوصیات کلیدی است که در ViewModel ها استفاده می شود.
    public class BaseViewModel : INotifyPropertyChanged
    {
        public int UserId => Properties.Settings.Default.UserId;
        public User CurrentUser => GetCurrentUser().Result;

        private IAccessData _accessData;

        public IAccessData AccessData
        {
            get
            {
                if (_accessData == null)
                    _accessData = new AccessData();

                return _accessData;
            }
        }

        private async Task<User> GetCurrentUser()
        {
            return await AccessData.Users.GetAsync(UserId);
        }

        protected internal void ReloadEntity<TEntity>(TEntity entity) where TEntity : class
        {
            AccessData.Context.ReloadEntity(entity);
        }

        protected internal bool? ShowAlert(string title, string message, AlertWindowType alertWindowType, AlertWindowButtons alertWindowButtons = AlertWindowButtons.Ok)
        {
            var alertWindow = new AlertWindow(title, message, alertWindowType, alertWindowButtons);
            return alertWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}