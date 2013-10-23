using BIPIDE.Classes;
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
        public Codigo _AssemblySource;

        public String AssemblyText
        {
            set { _TextEditorASM.Text = value; }
        }

        public String SourceCodeDebugText
        {
            set { _TextEditorSourceCodeDebug.Text = value; }
        }

        XBackgroundRenderer iBackgroundRendererASM;

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

        private void _Simulator_RequestFimPrograma()
        {
            _TextEditorASM.CaretOffset = _TextEditorASM.Document.Lines[_TextEditorASM.LineCount-1].Offset;
            _TextEditorSourceCodeDebug.CaretOffset = _TextEditorSourceCodeDebug.Document.Lines[_TextEditorASM.LineCount-1].Offset;
        }

        void _Simulator_RequestEnderecoPrograma(int intEndereco)
        {
            //_LayoutDocumentSimulation.Title = Convert.ToString(str);
            if (intEndereco >= 0)
            {

                InstrucaoASM instASM = _AssemblySource.GetInstrucaoAsmByEnderecoMemoria(intEndereco);

                int lineDocumentArquivoASM = 0;
                int lineDocumentSourceCode = instASM.NrLinha.Value;

                List<InstrucaoASM> list = _AssemblySource.GetCodigoInstrucaoASM();

                foreach (InstrucaoASM instructions in list)
                {
                    if (instructions.NrLinha == lineDocumentSourceCode)
                    {
                        if (instructions == instASM)
                            lineDocumentArquivoASM = list.IndexOf(instructions);
                    }

                }

                _TextEditorASM.CaretOffset = _TextEditorASM.Document.Lines[lineDocumentArquivoASM].Offset;
                _TextEditorSourceCodeDebug.CaretOffset = _TextEditorSourceCodeDebug.Document.Lines[lineDocumentSourceCode].Offset; 
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
        public void Draw(TextView textView, /*DrawingContext drawingContext,*/ int linha)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            textView.EnsureVisualLines();
            foreach (Rect r in BackgroundGeometryBuilder.GetRectsForSegment(textView, new TextSegment() { StartOffset = linha }))
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

    class LineColorizer : DocumentColorizingTransformer
    {
        int lineNumber;

        public LineColorizer(int lineNumber)
        {
            if (lineNumber < 1)
                throw new ArgumentOutOfRangeException("lineNumber", lineNumber, "Line numbers are 1-based.");
            this.lineNumber = lineNumber;
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", value, "Line numbers are 1-based.");
                lineNumber = value;
            }
        }

        protected override void ColorizeLine(ICSharpCode.AvalonEdit.Document.DocumentLine line)
        {
            if (!line.IsDeleted && line.LineNumber == lineNumber)
            {
                ChangeLinePart(line.Offset, line.EndOffset, ApplyChanges);
            }
        }

        void ApplyChanges(VisualLineElement element)
        {
            // apply changes here
            element.TextRunProperties.SetBackgroundBrush(Brushes.Red);
        }
    }
}
