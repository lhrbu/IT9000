using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT9000.Shared.Services
{
    public interface IIteInteropService
    {
        EventWaitHandle WaitHandle { get; }
        int IteDC_GetUsb(byte[] ptr, ref int value);
        int SystemTest(int[] ptr, ref int value);
        int GetDeviceName(int address, byte[] ptr);
        int IteDC_WriteCmd(int address, string scmd);
        int IteDC_ReadCmd(int address, string scmd, byte[] reChar);
        int ItePow_GetOutputState(int address, byte[] reChar);
        int ItePow_SetOutputState(int address, string value);
        int IteDMM_GetMeasureVoltage(int address, string sChannel, byte[] reChar);
        int IteDMM_GetMeasureCurrent(int address, string sChannel, byte[] reChar);
        int ItePow_RemoteMode(int address);
    }
}
