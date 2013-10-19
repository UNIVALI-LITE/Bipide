﻿using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using System.Xml;

namespace BIPIDE_4._0.UIResources
{
    /// <summary>
    /// Interaction logic for ProgrammingDocument.xaml
    /// </summary>
    public partial class UCProgrammingDocument : UserControl
    {
        private RibbonContextualTabGroup _SimulationContext;
        public RibbonContextualTabGroup SimulationContext
        {
            set { _SimulationContext = value; }
        }

        private RibbonTab _SimulationTab;
        public RibbonTab SimulationTab
        {
            set { _SimulationTab = value; }
        }

        private Ribbon _RibbonMain;
        public Ribbon RibbonMain
        {
            set { _RibbonMain = value; }
        }

        private SimulationControl.Processors _SimulationSelectedProcessor;
        internal SimulationControl.Processors SimulationSelectedProcessor
        {
            get { return _SimulationSelectedProcessor; }
            set 
            {
                _Simulator.setProcessador( (int) value );
                _SimulationSelectedProcessor = value;
            }
        }

        public UCProgrammingDocument()
        {
            InitializeComponent();
            _Simulator.setProcessador( (int) _SimulationSelectedProcessor );

            using (StreamReader iStream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ProgrammingLanguagesResources\\CResources\\HighlightResources\\C.xshd"))
            {
                using (XmlTextReader iXmlTextReader = new XmlTextReader(iStream))
                {
                    _TextEditorSourceCode.SyntaxHighlighting = HighlightingLoader.Load(iXmlTextReader, HighlightingManager.Instance);
                }
            }
        }

        public UCProgrammingDocument(RibbonContextualTabGroup pSimulationContext)
        {
            InitializeComponent();
            _SimulationContext = pSimulationContext;
            _Simulator.setProcessador( (int) _SimulationSelectedProcessor );
        }

        private void _TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_TabItemProgramming.IsSelected)
            {
                _SimulationContext.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (_TabItemSimulation.IsSelected)
            {
                _SimulationContext.Visibility   = System.Windows.Visibility.Visible;
                _RibbonMain.SelectedItem        = _SimulationTab;
            }
        }
    }
}
