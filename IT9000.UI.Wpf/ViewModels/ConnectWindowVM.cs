using IT9000.Shared.Models;
using IT9000.UI.Wpf.Repositories;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IT9000.UI.Wpf.ViewModels
{
    public class ConnectWindowVM
    {
        private readonly DevicesRepository _devicesRepository;
        public ConnectWindowVM(
            DevicesRepository devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }

        public DelegateCommand<ListBox> ConnectSelectionsCommand =>new(ConnectSelections);
        public DelegateCommand<ListBox> ResetSelectionsCommand => new(listBox => listBox.SelectedItem = null);
        public ObservableCollection<Device> OfflineDevices => _devicesRepository.OfflineDevices;
        public Visibility OfflineDevicesEmptyFlag =>
            OfflineDevices.Count() != 0 ? Visibility.Visible : Visibility.Hidden;
        public void ConnectSelections(ListBox listBox)
        {
            try
            {
                IEnumerable<Device> devices = listBox.SelectedItems.Cast<Device>()!.ToList()!;
                TabControl tabControl = (Application.Current.MainWindow as MainWindow)!.TabControl_DevicePanels;
                TabItem? lastTabItem = null;
                foreach (Device device in devices)
                {
                    _devicesRepository.SetDeviceOnline(device);
                    TabItem tabItem = new()
                    {
                        Header = device.Info.Name,
                        Content = device.Panel!.CreateUI(),
                        VerticalContentAlignment = VerticalAlignment.Top
                    };
                    tabControl.Items.Add(tabItem);
                    lastTabItem = tabItem;
                }
                if( lastTabItem is not null)
                { tabControl.SelectedItem = lastTabItem; }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString(), "Can't connect device error");
            }
            finally
            {
                Window.GetWindow(listBox).Close();
            }
        }
    }
}
