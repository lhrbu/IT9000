using IT9000.Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace IT9000.UI.Wpf.Services
{
    public class PluginService
    {
        private readonly ConcurrentDictionary<string, IDevicePanelFactory> deviceModelFactories = new();
        private string PluginsDirectory => Path.Combine(Environment.CurrentDirectory, "Plugins");
        private string GetPluginDllPath(string deviceModel) => Path.Combine(
            PluginsDirectory, $"{deviceModel}.Wpf.dll");
        private string GetPluginDependencyDirectory(string deviceModel) => Path
            .Combine(PluginsDirectory, $"{deviceModel}.Refs");

        public void Load(DeviceInfo deviceInfo)
        {
            if (!deviceModelFactories.ContainsKey(deviceInfo.Model))
            {
                foreach (Type type in Assembly.LoadFrom(GetPluginDllPath(deviceInfo.Model)).GetTypes())
                {
                    Type? idevicePanelFactoryType = type.GetInterface(nameof(IDevicePanelFactory));
                    if (idevicePanelFactoryType is not null)
                    {
                        IDevicePanelFactory devicePanelFactory = (Activator.CreateInstance(type) as IDevicePanelFactory)!;
                        deviceModelFactories.TryAdd(deviceInfo.Model, devicePanelFactory);
                    }
                }
            }
        }

        public void LoadDependency(DeviceInfo deviceInfo)
        {
            if (!deviceModelFactories.ContainsKey(deviceInfo.Model))
            {
                IEnumerable<string> dllPaths = Directory.GetFiles(GetPluginDependencyDirectory(deviceInfo.Model)).Where(item => item.EndsWith(".dll"));
                foreach (string dllPath in dllPaths)
                { AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath); }
            }
        }
    }
}
