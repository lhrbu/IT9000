using PV6900.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows.Controls;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using Prism.Commands;

namespace PV6900.UI.Wpf.ViewModels
{
    public class ProgramEditorVM:BindableBase
    {
        private readonly ManagedProgramInterpreter _interpreter;
        private readonly MonitorControllerVM _monitorGaugesGroupVM;
        public ProgramEditorVM(ManagedProgramInterpreter interpreter,
            MonitorControllerVM monitorGaugesGroupVM)
        { 
            _interpreter = interpreter;
            _monitorGaugesGroupVM = monitorGaugesGroupVM;
            AddCommand = new(() => ManagedProgramSteps.Add(new()));
            DeleteCommand = new((dataGrid => ManagedProgramSteps
                .Remove((dataGrid.SelectedItem as ManagedProgramStep)!)));
            StartProgramCommand = new(StartProgram);
            StopProgramCommand = new(StopProgram);
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<DataGrid> DeleteCommand { get; }
        public DelegateCommand<DataGrid> StartProgramCommand { get; }
        public DelegateCommand StopProgramCommand { get; }
        public ObservableCollection<ManagedProgramStep> ManagedProgramSteps { get; } = new() { new ManagedProgramStep() };
        public int OuterLoopCount { get => _outerLoopCount; set => SetProperty(ref _outerLoopCount, value); }
        private int _outerLoopCount = 1;

        public bool InRunning { get => _inRunning; set => SetProperty(ref _inRunning, value); }
        private bool _inRunning = false;
        private void StartProgram(DataGrid dataGrid)
        {
            if (InRunning) { return; }
            Application.Current.Dispatcher.Invoke(() =>
            {
                dataGrid.SetBinding(DataGrid.SelectedItemProperty,
            new Binding(nameof(_interpreter.CurrentManagedProgramStep)));
                _monitorGaugesGroupVM.StopMonitorCommand.Execute();
            }
            );
            _bindingClearFlag = false;
            ManagedProgram program = new()
            {
                ManagedProgramSteps = ManagedProgramSteps.ToList(),
                OuterLoopCount = OuterLoopCount
            };
            _interpreter.ExecuteManagedProgramAsync(program).ContinueWith(task=>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    BindingOperations.ClearBinding(dataGrid, DataGrid.SelectedItemProperty);
                    dataGrid.SelectedItem = null;
                });
                GC.Collect();
                _bindingClearFlag = true;
            }).ConfigureAwait(false);
        }
        private void StopProgram()
        { 
            _interpreter.Stop();
            Application.Current.Dispatcher.Invoke(() => _monitorGaugesGroupVM.StopMonitorCommand.Execute());
            while (_interpreter.InRunning && _bindingClearFlag) { Thread.Sleep(0); }
            InRunning = false;
        }
        private bool _bindingClearFlag = true;
    }
}
