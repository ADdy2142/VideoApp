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

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        #region Properties

        // رنگ دکمه اصلی
        public SolidColorBrush PrimaryColor
        {
            get { return (SolidColorBrush)GetValue(PrimaryColorProperty); }
            set { SetValue(PrimaryColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.Register("PrimaryColor", typeof(SolidColorBrush), typeof(AlertWindow));

        // رنگ دکمه دوم
        public SolidColorBrush SecondaryColor
        {
            get { return (SolidColorBrush)GetValue(SecondaryColorProperty); }
            set { SetValue(SecondaryColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(SolidColorBrush), typeof(AlertWindow));

        // رنگ دکمه سوم
        public SolidColorBrush ThridColor
        {
            get { return (SolidColorBrush)GetValue(ThridColorProperty); }
            set { SetValue(ThridColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThridColorProperty =
            DependencyProperty.Register("ThridColor", typeof(SolidColorBrush), typeof(AlertWindow));

        // وضعیت نمایش دکمه OK
        public Visibility OkButtonVisibility
        {
            get { return (Visibility)GetValue(OkButtonVisibilityProperty); }
            set { SetValue(OkButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OkButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OkButtonVisibilityProperty =
            DependencyProperty.Register("OkButtonVisibility", typeof(Visibility), typeof(AlertWindow));

        // وضعیت نمایش دکمه Yes
        public Visibility YesButtonVisibility
        {
            get { return (Visibility)GetValue(YesButtonVisibilityProperty); }
            set { SetValue(YesButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YesButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YesButtonVisibilityProperty =
            DependencyProperty.Register("YesButtonVisibility", typeof(Visibility), typeof(AlertWindow));

        // وضعیت نمایش دکمه No
        public Visibility NoButtonVisibility
        {
            get { return (Visibility)GetValue(NoButtonVisibilityProperty); }
            set { SetValue(NoButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YesButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoButtonVisibilityProperty =
            DependencyProperty.Register("NoButtonVisibility", typeof(Visibility), typeof(AlertWindow));

        // وضعیت نمایش دکمه Cancel
        public Visibility CancelButtonVisibility
        {
            get { return (Visibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YesButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelButtonVisibilityProperty =
            DependencyProperty.Register("CancelButtonVisibility", typeof(Visibility), typeof(AlertWindow));

        // مقدار Margin هر دکمه
        public Thickness ButtonsMargin
        {
            get { return (Thickness)GetValue(ButtonsMarginProperty); }
            set { SetValue(ButtonsMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsMarginProperty =
            DependencyProperty.Register("ButtonsMargin", typeof(Thickness), typeof(AlertWindow));

        // نوع پیام
        public AlertWindowType AlertWindowType
        {
            get { return (AlertWindowType)GetValue(AlertWindowTypeProperty); }
            set { SetValue(AlertWindowTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlertWindowType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlertWindowTypeProperty =
            DependencyProperty.Register("AlertWindowType", typeof(AlertWindowType), typeof(AlertWindow));

        #endregion Properties

        // سازنده و ورودی های آن
        public AlertWindow(string title, string message, AlertWindowType alertWindowType, AlertWindowButtons alertWindowButtons)
        {
            InitializeComponent();

            AlertWindowType = alertWindowType;
            TxbTitle.Text = title;
            TxbMessage.Text = message;
            ChangeWindowColor(alertWindowType);
            ChangeWindowButtons(alertWindowButtons);
        }

        // تایید درخواست
        private void BtnPrimary_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        // مخالفت با درخواست
        private void BtnSecondary_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        // لغو درخواست
        private void BtnThird_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
        }

        // تغییر رنگ پنجره
        private void ChangeWindowColor(AlertWindowType alertWindowType)
        {
            switch (alertWindowType)
            {
                case AlertWindowType.Success:
                    PrimaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                    SecondaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336"));
                    break;

                case AlertWindowType.Warning:
                    PrimaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107"));
                    SecondaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5722"));
                    break;

                case AlertWindowType.Danger:
                    PrimaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336"));
                    SecondaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800"));
                    break;

                case AlertWindowType.Info:
                    PrimaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03A9F4"));
                    SecondaryColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009688"));
                    break;

                default:
                    break;
            }

            ThridColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9E9E9E"));
        }

        // تغییر دکمه های موجود در پنجره
        private void ChangeWindowButtons(AlertWindowButtons alertWindowButtons)
        {
            switch (alertWindowButtons)
            {
                case AlertWindowButtons.Ok:
                    OkButtonVisibility = Visibility.Visible;
                    YesButtonVisibility = Visibility.Collapsed;
                    NoButtonVisibility = Visibility.Collapsed;
                    CancelButtonVisibility = Visibility.Collapsed;
                    ButtonsMargin = new Thickness(0);
                    break;

                case AlertWindowButtons.YesNo:
                    OkButtonVisibility = Visibility.Collapsed;
                    YesButtonVisibility = Visibility.Visible;
                    NoButtonVisibility = Visibility.Visible;
                    CancelButtonVisibility = Visibility.Collapsed;
                    ButtonsMargin = new Thickness(0, 0, 5, 0);
                    break;

                case AlertWindowButtons.YesNoCancel:
                    OkButtonVisibility = Visibility.Collapsed;
                    YesButtonVisibility = Visibility.Visible;
                    NoButtonVisibility = Visibility.Visible;
                    CancelButtonVisibility = Visibility.Visible;
                    ButtonsMargin = new Thickness(0, 0, 5, 0);
                    break;

                default:
                    break;
            }
        }
    }

    // نوع پیام
    public enum AlertWindowType : byte
    {
        Success = 0,
        Warning = 1,
        Danger = 2,
        Info = 3
    }

    // انواع دکمه های موجود در پنجره
    public enum AlertWindowButtons : byte
    {
        Ok = 0,
        YesNo = 1,
        YesNoCancel = 2
    }
}