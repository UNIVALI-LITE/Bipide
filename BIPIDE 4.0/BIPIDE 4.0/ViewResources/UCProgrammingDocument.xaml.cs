using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;
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
        private string _ProgrammingLanguage;
        public string ProgrammingLanguage
        {
            get { return _ProgrammingLanguage; }
        }

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

        public UCProgrammingDocument(string pHighlightFile, string pProgrammingLanguage)
        {
            InitializeComponent();
            _Simulator.setProcessador( (int) _SimulationSelectedProcessor );
            _ProgrammingLanguage                    = pProgrammingLanguage;
            _LayoutAnchorableSourceCodeDebug.Title  = pProgrammingLanguage;

            #region Highlight Set

            // Load ASM Highlight
            using (StreamReader iStream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ProgrammingLanguagesResources\\ASMResources\\HighlightResources\\ASM.xshd"))
            {
                using (XmlTextReader iXmlTextReader = new XmlTextReader(iStream))
                {
                    _TextEditorASM.SyntaxHighlighting = HighlightingLoader.Load(iXmlTextReader, HighlightingManager.Instance);
                }
            }

            // Load Programming Language Highlight
            using (StreamReader iStream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + pHighlightFile))
            {
                using (XmlTextReader iXmlTextReader = new XmlTextReader(iStream))
                {
                    _TextEditorSourceCodeDebug.SyntaxHighlighting   = HighlightingLoader.Load(iXmlTextReader, HighlightingManager.Instance);
                }
            }

            // Load Programming Language Highlight
            using (StreamReader iStream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + pHighlightFile))
            {
                using (XmlTextReader iXmlTextReader = new XmlTextReader(iStream))
                {
                    _TextEditorSourceCode.SyntaxHighlighting        = HighlightingLoader.Load(iXmlTextReader, HighlightingManager.Instance);
                }
            }

            XBackgroundRenderer iBackgroundRendererASM          = new XBackgroundRenderer(_TextEditorASM);
            XBackgroundRenderer iBackgroundRendererSourceCode   = new XBackgroundRenderer(_TextEditorSourceCodeDebug);

            _TextEditorASM.TextArea.TextView.BackgroundRenderers.Add(iBackgroundRendererASM);
            _TextEditorSourceCodeDebug.TextArea.TextView.BackgroundRenderers.Add(iBackgroundRendererSourceCode);

            #endregion Highlight Set


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

    public class XBackgroundRenderer : IBackgroundRenderer
    {
        TextEditor editor;

        public XBackgroundRenderer(TextEditor e)
        {
            editor = e;
        }

        public KnownLayer Layer
        {
            get { return KnownLayer.Caret; }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            textView.EnsureVisualLines();
            foreach (Rect r in BackgroundGeometryBuilder.GetRectsForSegment(textView, new TextSegment() { StartOffset = editor.Document.GetLineByOffset(editor.CaretOffset).Offset }))
            {
                drawingContext.DrawRoundedRectangle(
                    new SolidColorBrush(Color.FromArgb(40, 0xff, 0x00, 0x15)),
                    new Pen(new SolidColorBrush(Color.FromArgb(60, 0xff, 0x00, 0x15)), 1),
                    new Rect(r.Location, new Size(textView.ActualWidth, r.Height)),
                    3, 3
                );
            }
        }
    }
}
