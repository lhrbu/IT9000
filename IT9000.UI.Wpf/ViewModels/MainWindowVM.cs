using IT9000.UI.Wpf.Services;
using IT9000.UI.Wpf.Views;
using Prism.Commands;
using Raccoon.DevKits.Wpf.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT9000.UI.Wpf.ViewModels
{
    public class MainWindowVM
    {
        public MainWindowVM(InitDeviceService initDeviceService)
        { initDeviceService.LoadDevices();}

        public DelegateCommand ShowConnectWindowCommand => new(() =>
            Application.Current.AsDIApplication()!.GetWindow<ConnectWindow>().Show());
    }
}
