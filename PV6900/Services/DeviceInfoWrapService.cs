using System;
using IT9000.Shared.Models;

namespace PV6900.Services
{
    public class DeviceInfoWrapService
    {
        private DeviceInfo? _deviceInfo;
        public void Set(DeviceInfo deviceInfo)=>_deviceInfo = deviceInfo;
        public DeviceInfo? Get()=>_deviceInfo;
    }
}