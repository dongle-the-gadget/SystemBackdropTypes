using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using static SystemBackdropTypes.PInvoke.ParameterTypes;
using static SystemBackdropTypes.PInvoke.Methods;
using ModernWpf;

namespace SystemBackdropTypes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshFrame();
            RefreshDarkMode();
            ThemeManager.Current.ActualApplicationThemeChanged += (_, _) => RefreshDarkMode();
        }

        private void RefreshFrame()
        {
            IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = -1;
            margins.cxRightWidth = -1;
            margins.cyTopHeight = -1;
            margins.cyBottomHeight = -1;

            ExtendFrame(mainWindowSrc.Handle, margins);
        }

        private void RefreshDarkMode()
        {
            var isDark = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;
            int flag = isDark ? 1 : 0;
            SetWindowAttribute(
                new WindowInteropHelper(this).Handle, 
                DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, 
                flag);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            int flag = int.Parse((string)((RadioButton)sender).Tag);
            SetWindowAttribute(
                new WindowInteropHelper(this).Handle, 
                DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 
                flag);
        }
    }
}
