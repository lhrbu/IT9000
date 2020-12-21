using IT9000.Shared.Services;
using IT9000.UI.Wpf.Repositories;
using IT9000.UI.Wpf.Services;
using IT9000.UI.Wpf.ViewModels;
using IT9000.UI.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.DevKits.Wpf.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IT9000.UI.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDIApplication
    {
        public IServiceProvider ServiceProvider { get; set; } = null!;
        public IConfiguration Configuration { get; set; } = null!;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<ConnectWindow>();

            services.AddTransient<ConnectWindowVM>();
            services.AddTransient<MainWindowVM>();

            services.AddTransient<InitDeviceService>();
            services.AddSingleton<DevicesRepository>();
            services.AddSingleton<PluginsRepository>();

            services.AddSingleton<IIteInteropService, MockIterInteropService>();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.OnStartupProxy<MainWindow>();
        }
    }
}
