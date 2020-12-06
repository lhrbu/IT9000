﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PV6900.Models
{
    public class ManagedProgramInterpreter
    {
        
        public ManagedProgramInterpreter(PV6900VirtualMachine machine)
        { _machine = machine; }

        public async Task ExecuteAsync(ManagedProgram managedProgram)
        {
            InRunning = true;
            SettingOutLoopCount = managedProgram.OuterLoopCount;
            _programRunnintTimeStopWatch.Restart();
            foreach (int index in Enumerable.Range(0, managedProgram.OuterLoopCount))
            {
                await ExecuterManagedProgramStepsAsync(managedProgram.ManagedProgramSteps);
            }
        }
        public void Stop()
        {
            _machine.Stop();
            _programRunnintTimeStopWatch.Stop();
            InRunning = false;
        }
        public  bool  InRunning { get; private set; }
        public int SettingOutLoopCount { get; private set; }
        public int SettingInnerLoopCount { get; private set; }
        public int CurrentOutLoopCount { get; private set; }
        public int CurrentInnerLoopCount { get; private set; }
        public TimeSpan ProgramRunningTime => _programRunnintTimeStopWatch.Elapsed;
        public ManagedProgramStep? CurrentManagedProgramStep { get; private set; }
        

        private readonly PV6900VirtualMachine _machine;
        private Stopwatch _programRunnintTimeStopWatch =  Stopwatch.StartNew();
        private async Task ExecuterManagedProgramStepsAsync(IEnumerable<ManagedProgramStep> managedProgramSteps)
        {
            List<ManagedProgramStep> innerLoopStepsBuffer = new();
            bool intoInnerLoop = false;
            foreach(ManagedProgramStep managedProgramStep in managedProgramSteps)
            {
                switch(managedProgramStep.InnerLoopFlag)
                {
                    case InnerLoopFlag.None:
                        if (!intoInnerLoop) { await _machine.ExecuteAsync(GetSettings(managedProgramStep)); }
                        else { innerLoopStepsBuffer.Add(managedProgramStep); }
                        break;
                    case InnerLoopFlag.On:
                        innerLoopStepsBuffer.Add(managedProgramStep);
                        intoInnerLoop = true;
                        break;
                    case InnerLoopFlag.Off:
                        innerLoopStepsBuffer.Add(managedProgramStep);
                        int innerLoopCount = innerLoopStepsBuffer.First().InnerLoopCount;
                        SettingInnerLoopCount = innerLoopCount;
                        foreach(int index in Enumerable.Range(0,innerLoopCount))
                        {
                            CurrentInnerLoopCount = index + 1;
                            foreach(ManagedProgramStep step in innerLoopStepsBuffer)
                            {
                                CurrentManagedProgramStep = step;
                                await _machine.ExecuteAsync(GetSettings(step));
                            }
                        }
                        innerLoopStepsBuffer.Clear();
                        intoInnerLoop = false;
                        break;
                }
            }
        }
        private ProgramStep GetSettings(ManagedProgramStep managedProgramStep) =>
            new ProgramStep { Volta = managedProgramStep.Volta, 
                Ampere = managedProgramStep.Ampere, Duration = managedProgramStep.Duration };
    }
}
