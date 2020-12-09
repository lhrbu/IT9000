using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT9000.Shared.Services
{
    internal class VirtualMachineState
    {
        public int Address { get; init; }
        public string Name { get; init; } = null!;
        public string USBParam { get; init; } = null!;
        public double SettingVolta { get; set; }
        public double SettingAmpere { get; set; }
        public double Volta { get; set; }
        public double Ampere { get; set; }
        public uint OutputState { get; set; }

    }
    public class MockIterInteropService : IIteInteropService
    {
        public EventWaitHandle WaitHandle => throw new NotImplementedException();

        public int GetDeviceName(int address, byte[] ptr)
        {
            VirtualMachineState state = GetState(address);
            Encoding.Default.GetBytes(state.Name).CopyTo(ptr, 0);
            return 1;
        }

        public int IteDC_GetUsb(byte[] ptr, ref int value)
        {
            value = 6;
            Encoding.Default.GetBytes(_virtualMachineStates[0].USBParam).CopyTo(ptr, 0);
            Encoding.Default.GetBytes(_virtualMachineStates[1].USBParam).CopyTo(ptr, 100);
            Encoding.Default.GetBytes(_virtualMachineStates[2].USBParam).CopyTo(ptr, 200);
            Encoding.Default.GetBytes(_virtualMachineStates[3].USBParam).CopyTo(ptr, 300);
            Encoding.Default.GetBytes(_virtualMachineStates[4].USBParam).CopyTo(ptr, 400);
            Encoding.Default.GetBytes(_virtualMachineStates[5].USBParam).CopyTo(ptr, 500);
            return 1;
        }

        public int IteDC_ReadCmd(int address, string scmd, byte[] reChar)
        {
            VirtualMachineState state = GetState(address);
            switch (scmd)
            {
                case "VOLT?":
                    Encoding.Default.GetBytes(state.SettingVolta.ToString()).CopyTo(reChar, 0); break;
                case "CURR?":
                    Encoding.Default.GetBytes(state.SettingAmpere.ToString()).CopyTo(reChar, 0); break;
                //case "VOLT? MAX":
                //    Encoding.Default.GetBytes(VoltaMax.ToString()).CopyTo(reChar, 0); break;
                //case "VOLT? MIN":
                //    Encoding.Default.GetBytes(VoltaMin.ToString()).CopyTo(reChar, 0); break;
                //case "CURR? MAX":
                //    Encoding.Default.GetBytes(AmpereMax.ToString()).CopyTo(reChar, 0); break;
                //case "CURR? MIN":
                //    Encoding.Default.GetBytes(AmpereMin.ToString()).CopyTo(reChar, 0); break;
            }
            return 1;
        }

        public int IteDC_WriteCmd(int address, string scmd)
        {
            VirtualMachineState state = GetState(address);
            if (scmd.StartsWith("VOLT"))
            {

                state.SettingVolta = double.Parse(scmd.Remove(0, 5));
                state.Volta = state.SettingVolta;
                return 1;
            }
            else if (scmd.StartsWith("CURR"))
            {

                state.SettingAmpere = double.Parse(scmd.Remove(0, 5));
                state.Ampere = state.SettingAmpere;
                return 1;
            }
            return 0;
        }

        public int IteDMM_GetMeasureCurrent(int address, string sChannel, byte[] reChar)
        {
            Encoding.Default.GetBytes(GetState(address).Ampere.ToString()).CopyTo(reChar, 0);
            return 1;
        }

        public int IteDMM_GetMeasureVoltage(int address, string sChannel, byte[] reChar)
        {
            Encoding.Default.GetBytes(GetState(address).Volta.ToString()).CopyTo(reChar, 0);
            return 1;
        }

        public int ItePow_GetOutputState(int address, byte[] reChar)
        {
            Encoding.Default.GetBytes($"{GetState(address)}\n").CopyTo(reChar, 0);
            return 1;
        }

        public int ItePow_RemoteMode(int address)
        {
            return 0;
        }

        public int ItePow_SetOutputState(int address, string value)
        {
            _virtualMachineStates.First(item => item.Address == address).OutputState = 1;
            return 1;
        }

        public int SystemTest(int[] ptr, ref int value)
        {
            value = 6;
            int[] arr = _virtualMachineStates.Select(item => item.Address).ToArray();
            Array.Copy(arr, ptr, 6);
            return 1;
        }
        private VirtualMachineState GetState(int address) =>
            _virtualMachineStates.First(item => item.Address == address);
        private readonly List<VirtualMachineState> _virtualMachineStates = new()
        {
            new VirtualMachineState { Address=85711056,Name= "PV6900_1@1",USBParam="USB1"},
            new VirtualMachineState { Address = 85682472,Name= "PV6900_2@2",USBParam="USB2" },
            new VirtualMachineState { Address=85682760,Name= "PV6900_3@3",USBParam="USB3" },
            new VirtualMachineState { Address=85683912,Name= "PV6900_4@4",USBParam="USB4" },
            new VirtualMachineState { Address=85686216,Name= "PV6900_5@5",USBParam="USB5"},
            new VirtualMachineState { Address=85687080,Name= "PV6900_6@6",USBParam="USB6" }
        };
        
    }
}
