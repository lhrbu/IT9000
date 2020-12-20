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

namespace IT9000.UI.Wpf.Controls
{
    /// <summary>
    /// DevicePanel.xaml 的交互逻辑
    /// </summary>
    public partial class DevicePanel : Grid
    {
        public DevicePanel()
        {
            InitializeComponent();
        }

        public Brush HeaderIconBrush
        {
            get => (Brush)GetValue(HeaderIconBrushProperty);
            set => SetValue(HeaderIconBrushProperty, value);
        }
        public static readonly DependencyProperty HeaderIconBrushProperty =
            DependencyProperty.Register(nameof(HeaderIconBrush), typeof(Brush), typeof(DevicePanel));
        public string DeviceName
        {
            get => (string)GetValue(DeviceNameProperty);
            set => SetValue(DeviceNameProperty, value);
        }

        public static readonly DependencyProperty DeviceNameProperty =
            DependencyProperty.Register(nameof(DeviceName), typeof(string), typeof(DevicePanel));

        public string DeviceModel
        {
            get => (string)GetValue(DeviceModelProperty);
            set => SetValue(DeviceModelProperty, value);
        }
        public static readonly DependencyProperty DeviceModelProperty =
            DependencyProperty.Register(nameof(DeviceModel), typeof(string), typeof(DevicePanel));

        public int DeviceAddress
        {
            get => (int)GetValue(DeviceAddressProperty);
            set => SetValue(DeviceAddressProperty, value);
        }

        public static readonly DependencyProperty DeviceAddressProperty =
           DependencyProperty.Register(nameof(DeviceAddress), typeof(int), typeof(DevicePanel));

        public string PortType
        {
            get => (string)GetValue(PortTypeProperty);
            set => SetValue(PortTypeProperty, value);
        }

        public static readonly DependencyProperty PortTypeProperty =
            DependencyProperty.Register(nameof(PortType), typeof(string), typeof(DevicePanel));
    }
}
