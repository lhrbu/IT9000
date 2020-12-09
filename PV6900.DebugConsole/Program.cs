using IT9000.Shared.Models;
using IT9000.Shared.Services;
using PV6900.Models;
using System;

namespace PV6900.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDevicePanelFactory();
            TestMockIteInteropService();
            Console.WriteLine("Hello World!");
        }

        static void TestMockIteInteropService()
        {
            MockIterInteropService service = new();
            int[] buffer = new int[255];
            int count=0;
            service.SystemTest(buffer,ref count);
        }

        static void TestDevicePanelFactory()
        {
            PV6900DevicePanelFactory factory = new();
            factory.Initialize();
            IDevicePanel panel = factory.CreateDevicePanel(new DeviceInfo
            {
                Address = 1,
                Name = "Mock1",
                InterfaceType ="USB",
                InterfaceParameter="1"
            });
        }
    }
}
