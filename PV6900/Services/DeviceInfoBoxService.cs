using System;
using IT9000.Shared.Models;

namespace PV6900.Services
{
    public class DeviceInfoBoxService
    {
        private DeviceInfo? _deviceInfo;
        public void Box(DeviceInfo deviceInfo)=>_deviceInfo = deviceInfo;
        public DeviceInfo? Unbox()=>_deviceInfo;
    }
}