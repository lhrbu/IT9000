using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IT9000.Shared.Services;
using PV6900.NativeInterop;

namespace PV6900.Services
{
    public class PV6900IteInteropService:IIteInteropService
    {
        public EventWaitHandle WaitHandle => PV6900IteVirtualLayerInterop.WaitHandle;

        public int GetDeviceName(int address, byte[] ptr)
            => PV6900IteVirtualLayerInterop.GetDeviceName(address, ptr);

        public int IteDC_GetUsb(byte[] ptr, ref int value)
            => PV6900IteVirtualLayerInterop.IteDC_GetUsb(ptr, ref value);

        public int IteDC_ReadCmd(int address, string scmd, byte[] reChar)
            => PV6900IteVirtualLayerInterop.IteDC_ReadCmd(address, scmd, reChar);

        public int IteDC_WriteCmd(int address, string scmd)
            => PV6900IteVirtualLayerInterop.IteDC_WriteCmd(address, scmd);

        public int IteDMM_GetMeasureCurrent(int address, string sChannel, byte[] reChar)
            => PV6900IteVirtualLayerInterop.IteDMM_GetMeasureCurrent(address, sChannel, reChar);

        public int IteDMM_GetMeasureVoltage(int address, string sChannel, byte[] reChar)
            => PV6900IteVirtualLayerInterop.IteDMM_GetMeasureVoltage(address, sChannel, reChar);

        public int ItePow_GetOutputState(int address, byte[] reChar)
            => PV6900IteVirtualLayerInterop.ItePow_GetOutputState(address, reChar);
        public int ItePow_SetOutputState(int address, string value)
            => PV6900IteVirtualLayerInterop.ItePow_SetOutputState(address, value);

        public int SystemTest(int[] ptr, ref int value)
            => PV6900IteVirtualLayerInterop.SystemTest(ptr, ref value);

        public int ItePow_RemoteMode(int address)
            => PV6900IteVirtualLayerInterop.ItePow_RemoteMode(address);
    }
}
