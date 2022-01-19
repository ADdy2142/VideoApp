using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Infrastructure
{
    // کار اصلی این متد Navigate کردن صفحات است.
    // از یک استک برای برگشت به صفحه قبلی استفاده می کند
    public static class BaseNavigation
    {
        private static Stack<Navigation> _navigationStack;

        public static Stack<Navigation> NavigationStack
        {
            get => _navigationStack ?? (_navigationStack = new Stack<Navigation>());
            set => _navigationStack = value;
        }

        private static NavigationWindow _navigationWindow;

        public static NavigationWindow NavigationWindow
        {
            get => _navigationWindow ?? (_navigationWindow = new NavigationWindow());
            set => _navigationWindow = value;
        }

        public static void Push(string title, UserControl control)
        {
            var navigation = new Navigation() { Title = title, Control = control };
            NavigationStack.Push(navigation);
            NavigationWindow.Navigation = navigation;
            if (!NavigationWindow.IsShown)
                NavigationWindow.ShowDialog();
        }

        public static void Pop()
        {
            NavigationStack.Pop();

            if (NavigationStack.Count > 0)
            {
                var navigation = NavigationStack.Peek();
                NavigationWindow.Navigation = navigation;
            }
            else
            {
                NavigationStack = null;
                NavigationWindow.DialogResult = true;
                NavigationWindow = null;
            }
        }
    }
}