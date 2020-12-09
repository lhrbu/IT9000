using PV6900.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PV6900.UI.Wpf.Components
{
    /// <summary>
    /// ProgramEditor.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramEditor : Grid
    {
        public ProgramEditor(/*ProgramEditorVM programEditorVM*/)
        {
            InitializeComponent();
            //DataContext = programEditorVM;
        }
    }
}
