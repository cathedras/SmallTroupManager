using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SmallTroupManager.Window
{
    public class WindowBase : System.Windows.Window
    {
        private static DependencyProperty HeaderHeightProperty;
        public int HeaderHeight
        {
            get => (int)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }

        private static int maxCornerRadius = 10;
        public static DependencyProperty CornerRadiusProperty;
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        static WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));

            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.Inherits = true;
            metadata.DefaultValue = 2;
            metadata.AffectsMeasure = true;
            metadata.PropertyChangedCallback += (d, e) => { };
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius",
                typeof(int), typeof(WindowBase), metadata,
                o => {
                    int radius = (int)o;
                    if (radius >= 0 && radius <= maxCornerRadius) return true;
                    return false;
                });

            metadata = new FrameworkPropertyMetadata();
            metadata.Inherits = true;
            metadata.DefaultValue = 40;
            metadata.AffectsMeasure = true;
            metadata.PropertyChangedCallback += (d, e) => { };
            HeaderHeightProperty = DependencyProperty.Register("HeaderHeight",
                typeof(int), typeof(WindowBase), metadata,
                o => {
                    int radius = (int)o;
                    if (radius >= 0 && radius <= 1000) return true;
                    return false;
                });
        }

        public WindowBase() : base()
        {
            
        }

        private void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WorkArea")
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    double top = SystemParameters.WorkArea.Top;
                    double left = SystemParameters.WorkArea.Left;
                    double right = SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right;
                    double bottom = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom;
                    root.Margin = new Thickness(left, top, right, bottom);
                }
            }
        }

        private double normaltop;
        private double normalleft;
        private double normalwidth;
        private double normalheight;
        private Grid root;
        private Button minBtn;
        private Button maxBtn;
        private Button closeBtn;
        private Border header;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            minBtn = (Button)Template.FindName("btnMin", this);
            minBtn.Click += (o, e) => WindowState = WindowState.Minimized;

            maxBtn = (Button)Template.FindName("btnMax", this);
            root = (Grid)Template.FindName("root", this);
            maxBtn.Click += (o, e) =>
            {
                if (WindowState == WindowState.Normal)
                {
                    normaltop = this.Top;
                    normalleft = this.Left;
                    normalwidth = this.Width;
                    normalheight = this.Height;

                    double top = SystemParameters.WorkArea.Top;
                    double left = SystemParameters.WorkArea.Left;
                    double right = SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right;
                    double bottom = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom;
                    root.Margin = new Thickness(left, top, right, bottom);

                    WindowState = WindowState.Maximized;
                    maxBtn.Content = "\xf2d2";
                }
                else
                {
                    WindowState = WindowState.Normal;
                    maxBtn.Content = "\xf2d0";

                    Top = 0;
                    Left = 0;
                    Width = 0;
                    Height = 0;

                    this.Top = normaltop;
                    this.Left = normalleft;
                    this.Width = normalwidth;
                    this.Height = normalheight;

                    root.Margin = new Thickness(0);
                }
            };

            closeBtn = (Button)Template.FindName("btnClose", this);
            closeBtn.Click += (o, e) => Close();

            header = (Border)Template.FindName("header", this);
            header.MouseMove += (o, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            header.MouseLeftButtonDown += (o, e) =>
            {
                if (e.ClickCount >= 2)
                {
                    maxBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            };
        }
    }
}