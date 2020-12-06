using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace PV6900.NativeInterop
{
    public static class PV6900IteVirtualLayerInterop
    {
        public static EventWaitHandle WaitHandle { get; } =
           new(true, EventResetMode.AutoReset);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDC_GetSeries(byte[] ptr, ref int value);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDC_GetUsb(byte[] ptr, ref int value);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDC_GetGpib(byte[] ptr, ref int value);


        /// <summary>
        /// Get device count and the address of each device.
        /// </summary>
        /// <param name="ptr">address of each device, for example,ptr[2] means 3rd device's address</param>
        /// <param name="value">deviceCount</param>
        /// <returns></returns>
        /// 

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int SystemTest(int[] ptr, ref int value);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int GetDeviceName(int address, byte[] ptr);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int GetStatus(int address, ref int bCanUse);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void VtDispose();

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDC_WriteCmd(int address, string scmd);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDC_ReadCmd(int address, string scmd, byte[] reChar);

        [DllImport("IT9503_F.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void GetArrayValue(sbyte[] pData);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_RemoteMode(int address);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_LocalMode(int address);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetOutputState(int address, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetOutputState(int address, string value);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDMM_GetMeasureVoltage(int address, string sChannel, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int IteDMM_GetMeasureCurrent(int address, string sChannel, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_MeasureDvm(int address, string sChannel, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_MeasurePower(int address, string sChannel, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetStatQuesCond(int address, byte[] svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetStatOperCond(int address, byte[] svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListState(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetListState(int address, byte[] svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListRepeat(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListStep(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListVolt(int address, string stepNum, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListCurr(int address, string stepNum, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListDelay(int address, string stepNum, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListMode(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetTrigSource(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetTrigger(int address);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetListRepeat(int address, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetListVolt(int address, string stepNum, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetListCurr(int address, string stepNum, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_GetListDelay(int address, string stepNum, byte[] reChar);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListSave(int address, string svalue);

        [DllImport("..\\IteVirtualLayer.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int ItePow_SetListRecall(int address, string svalue);
    }
}
