using BIPIDE_4._0.UIResources;
using BIPIDE_4._0.ViewResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace BIPIDE_4._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private SimulationControl _SimulationControl;

        public MainWindow()
        {
            InitializeComponent();
            _SimulationControl = new SimulationControl( _ButtonStart    , 
                                                        _ButtonPause    , 
                                                        _ButtonRepeat   , 
                                                        _ButtonNext     , 
                                                        _ButtonContinue , 
                                                        _ButtonStop     ) ;
            //SetLanguageDictionary();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary iResourceDictionary = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    iResourceDictionary.Source = new Uri("..\\LanguageResources\\LanguagePortuguese.xaml", UriKind.Relative);
                    break;
                case "fr-CA":
                    iResourceDictionary.Source = new Uri("..\\LanguageResources\\LanguagePortuguese.xaml", UriKind.Relative);
                    break;
                default:
                    iResourceDictionary.Source = new Uri("..\\LanguageResources\\LanguagePortuguese.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(iResourceDictionary);
        }

        #region Programação

        private void _MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iProgrammingDocument          = new UCProgrammingDocument();
            iProgrammingDocument.SimulationContext              = _ContextualTabGroupSimulation;
            iProgrammingDocument.SimulationTab                  = _TabSimulation;
            iProgrammingDocument.RibbonMain                     = _RibbonMain;
            iProgrammingDocument.SimulationSelectedProcessor    = SimulationControl.Processors.psBipIV;

            LayoutDocument iLayoutDocument  = new LayoutDocument();
            iLayoutDocument.Title           = "Documento";
            iLayoutDocument.Content         = iProgrammingDocument;
            iLayoutDocument.IsSelectedChanged += iLayoutDocument_IsSelectedChanged;

            _DocumentPane.Children.Add(iLayoutDocument);
        }

        void iLayoutDocument_IsSelectedChanged(object sender, EventArgs e)
        {
            if (((sender as LayoutDocument).Content as UCProgrammingDocument)._TabItemProgramming.IsSelected)
                _ContextualTabGroupSimulation.Visibility = System.Windows.Visibility.Collapsed;

            if (((sender as LayoutDocument).Content as UCProgrammingDocument)._TabItemSimulation.IsSelected)
                _ContextualTabGroupSimulation.Visibility = System.Windows.Visibility.Visible;

            switch (((sender as LayoutDocument).Content as UCProgrammingDocument).SimulationSelectedProcessor)
            {
                case SimulationControl.Processors.psBipI:
                    _RadioButtonSimulationBipI.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipII:
                    _RadioButtonSimulationBipII.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipIII:
                    _RadioButtonSimulationBipIII.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipIV:
                    _RadioButtonSimulationBipIV.IsChecked = true;
                    break;
            }
        }

        #endregion Programação

        #region Simulation

        private void _ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scStart;
        }

        private void _ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scPause;
        }

        private void _ButtonRepeat_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scRepeat;
        }

        private void _ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scNext;
        }

        private void _ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scContinue;
        }

        private void _ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scStop;
        }

        private void _RadioButtonSimulationBipI_Checked(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipI;
        }

        private void _RadioButtonSimulationBipII_Checked(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipII;
        }

        private void _RadioButtonSimulationBipIII_Checked(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipIII;
        }

        private void _RadioButtonSimulationBipIV_Checked(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipIV;
        }

        #endregion Simulation

        private void _ButtonFundamentals_Click(object sender, RoutedEventArgs e)
        {
            UCHelp iHelp = new UCHelp();

            LayoutDocument iLayoutDocument = new LayoutDocument();
            iLayoutDocument.Title = "Fundamentos";
            iLayoutDocument.Content = iHelp;

            if (_DocumentPane.Children.Count(x => x.Content.GetType() == typeof(UCHelp)) == 0)
                _DocumentPane.Children.Add(iLayoutDocument);
        }
    }
}
