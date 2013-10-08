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

namespace BIPIDE_4._0.ViewResources
{
    /// <summary>
    /// Interaction logic for UCHelpPractice.xaml
    /// </summary>
    public partial class UCHelpPractice : UserControl
    {
        private SimulationControl.Processors _SimulationSelectedProcessor;
        internal SimulationControl.Processors SimulationSelectedProcessor
        {
            get { return _SimulationSelectedProcessor; }
            set
            {
                _Simulator.setProcessador((int)value);
                _SimulationSelectedProcessor = value;
            }
        }

        public UCHelpPractice()
        {
            InitializeComponent();
        }
    }
}
