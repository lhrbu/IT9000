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

namespace IT9000.UI.Wpf.Repositories
{
    public class PluginsRepository
    {
        public IReadOnlyDictionary<string, IDevicePanelFactory> ReadOnlyDeviceModelFactories => _deviceModelFactories;
        private readonly ConcurrentDictionary<string, IDevicePanelFactory> _deviceModelFactories = new();
        private string PluginsDirectory => Path.Combine(Environment.CurrentDirectory, "Plugins");
        private string GetPluginDllPath(string deviceModel) => Path.Combine(
            PluginsDirectory, $"{deviceModel}.UI.Wpf.dll");
        private string GetPluginDependencyDirectory(string deviceModel) => Path
            .Combine(PluginsDirectory, $"{deviceModel}.Refs");
        public IDevicePanelFactory GetDevicePanelFactory(string modelName)
        {
            if(ReadOnlyDeviceModelFactories.TryGetValue(modelName,out IDevicePanelFactory? devicePanelFactory))
            {
                if(devicePanelFactory is not null) { return devicePanelFactory; }
            }
            throw new ArgumentException($"{modelName} is not loaded yet!");

        }
        public void Load(string modelName)
        {
            if (!_deviceModelFactories.ContainsKey(modelName))
            {
                foreach (Type type in Assembly.LoadFrom(GetPluginDllPath(modelName)).GetTypes())
                {
                    Type? idevicePanelFactoryType = type.GetInterface(nameof(IDevicePanelFactory));
                    if (idevicePanelFactoryType is not null)
                    {
                        IDevicePanelFactory devicePanelFactory = (Activator.CreateInstance(type) as IDevicePanelFactory)!;
                        devicePanelFactory.Initialize();
                        _deviceModelFactories.TryAdd(modelName, devicePanelFactory);
                    }
                }
            }
        }
        public void LoadDependency(string modelName)
        {
            if (!_deviceModelFactories.ContainsKey(modelName))
            {
                IEnumerable<string> dllPaths = Directory.GetFiles(GetPluginDependencyDirectory(modelName)).Where(item => item.EndsWith(".dll"));
                foreach (string dllPath in dllPaths)
                { AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath); }
            }
        }
        
    }
}
