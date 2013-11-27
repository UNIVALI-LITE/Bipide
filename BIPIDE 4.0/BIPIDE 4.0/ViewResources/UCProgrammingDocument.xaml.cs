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
using Xceed.Wpf.AvalonDock.Layout;

namespace BIPIDE_4._0.UIResources
{
    /// <summary>
    /// Interaction logic for ProgrammingDocument.xaml
    /// </summary>
    public partial class UCProgrammingDocument : UserControl
    {
        private static LineDebugASM _LineDebugASM;

        public static LineDebugASM LineDebugASM
        {
            get { return UCProgrammingDocument._LineDebugASM; }
            set { UCProgrammingDocument._LineDebugASM = value; }
        }
        private static int _LineDebugSourceCode;

        public static int LineDebugSourceCode
        {
            get { return UCProgrammingDocument._LineDebugSourceCode; }
            set { UCProgrammingDocument._LineDebugSourceCode = value; }
        }

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

        private LayoutDocument _LayoutDocument;
        public LayoutDocument LayoutDocument
        {
            set { _LayoutDocument = value; }
            get { return _LayoutDocument; }
        }

        private string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        private bool _Saved;
        public bool Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }

        private SimulationControl _SimulationControl;

        public UCProgrammingDocument(string pHighlightFile, string pProgrammingLanguage, SimulationControl pSimulationControl)
        {
            InitializeComponent();
            _Simulator.setProcessador( (int) _SimulationSelectedProcessor );
            _ProgrammingLanguage                    = pProgrammingLanguage;
            _LayoutAnchorableSourceCodeDebug.Title  = pProgrammingLanguage;
            _SimulationControl                      = pSimulationControl;

            UCProgrammingDocument.LineDebugASM          = new LineDebugASM();
            UCProgrammingDocument.LineDebugSourceCode   = 1;
            _Simulator.RequestEnderecoPrograma += new ucBip.Simulador.SetEnderecoPrograma(_Simulator_RequestEnderecoPrograma);

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

            HighlightCurrentLineBackgroundRenderer iCurrentLineBackgroundRenderer = new HighlightCurrentLineBackgroundRenderer(_TextEditorSourceCode);
            _TextEditorSourceCode.TextArea.TextView.BackgroundRenderers.Add(iCurrentLineBackgroundRenderer);

            HighlightDebugSurceCodeBackgroundRenderer iDebugLineBackgroundRendererSourceCode = new HighlightDebugSurceCodeBackgroundRenderer(_TextEditorSourceCodeDebug);
            _TextEditorSourceCodeDebug.TextArea.TextView.BackgroundRenderers.Add(iDebugLineBackgroundRendererSourceCode);

            HighlightDebugASMBackgroundRenderer iDebugMultiLinesBackgroundRendererASM = new HighlightDebugASMBackgroundRenderer(_TextEditorASM);
            _TextEditorASM.TextArea.TextView.BackgroundRenderers.Add(iDebugMultiLinesBackgroundRendererASM);
            
            #endregion Highlight Set

        }

        private void _Simulator_RequestFimPrograma()
        {
            _SimulationControl.Control = SimulationControl.SimulationControls.scStop;
        }

        void _Simulator_RequestEnderecoPrograma(int intEndereco)
        {
            if (intEndereco >= 0)
            {
                InstrucaoASM instASM = _AssemblySource.GetInstrucaoAsmByEnderecoMemoria(intEndereco);

                int lineDocumentArquivoASM = 1;
                int lineDocumentSourceCode = (instASM.NrLinha == null) ? 1 : instASM.NrLinha.Value;

                List<InstrucaoASM> list = _AssemblySource.GetCodigoInstrucaoASM();

                UCProgrammingDocument.LineDebugASM.SourceCodeReferencedLine.Clear();
                UCProgrammingDocument.LineDebugSourceCode = lineDocumentSourceCode;

                foreach (InstrucaoASM instructions in list)
                {
                    if (instructions.Tamanho > 0)
                    {
                        foreach (InstrucaoASM instr in list)
                        {
                            if (instr.Instrucao == instructions.Instrucao)
                            {
                                instructions.IndexArquivo = instr.IndexArquivo;
                                continue;
                            }
                        }

                    }
                }


                foreach (InstrucaoASM instructions in list)
                {
                    if (instructions.NrLinha == lineDocumentSourceCode )
                    {
                        UCProgrammingDocument.LineDebugASM.SourceCodeReferencedLine.Add(instructions.IndexArquivo);
                        if (instructions == instASM)
                        {
                            UCProgrammingDocument.LineDebugASM.CurrentLine = instructions.IndexArquivo;
                            lineDocumentArquivoASM = instructions.IndexArquivo;
                        }
                    }
                    //tratar linhas sem código alto nível vinculado
                    else if (lineDocumentSourceCode == 1)
                    {
                        if (instructions == instASM)
                        {
                            UCProgrammingDocument.LineDebugASM.CurrentLine = instructions.IndexArquivo;
                            lineDocumentArquivoASM = instructions.IndexArquivo;
                            break;
                        }
                    }

                }

                _TextEditorASM.TextArea.TextView.Redraw();
                _TextEditorSourceCodeDebug.TextArea.TextView.Redraw();
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

        private void _TextEditorSourceCode_TextChanged(object sender, EventArgs e)
        {
            if (_Saved)
                _LayoutDocument.Title += "*";
            _Saved = false;
        }

        public FlowDocument GetFlowDocumentForEditor()
        {
            IHighlighter iHighlighter = _TextEditorSourceCode.TextArea.GetService(typeof(IHighlighter)) as IHighlighter;
            FlowDocument doc    = new FlowDocument(ConvertTextDocumentToBlock(_TextEditorSourceCode.Document, iHighlighter));
            doc.FontFamily      = _TextEditorSourceCode.FontFamily;
            doc.FontSize        = _TextEditorSourceCode.FontSize;
            return doc;
        }


        private Block ConvertTextDocumentToBlock(TextDocument document, IHighlighter highlighter)
        {
            if (document == null)
                throw new ArgumentNullException("Document");

            Paragraph p = new Paragraph();
            foreach (DocumentLine line in document.Lines)
            {
                int lineNumber = line.LineNumber;
                HighlightedInlineBuilder inlineBuilder = new HighlightedInlineBuilder(document.GetText(line));
                if (highlighter != null)
                {
                    HighlightedLine highlightedLine = highlighter.HighlightLine(lineNumber);
                    int lineStartOffset = line.Offset;
                    foreach (HighlightedSection section in highlightedLine.Sections)
                        inlineBuilder.SetHighlighting(section.Offset - lineStartOffset, section.Length, section.Color);
                }
                p.Inlines.AddRange(inlineBuilder.CreateRuns());
                p.Inlines.Add(new LineBreak());
            }
            return p;
        }
    }

    public class LineDebugASM
    {
        public LineDebugASM()
        {
            _SourceCodeReferencedLine = new List<int>();
            _CurrentLine = 1;
        }

        private int _CurrentLine;

        public int CurrentLine
        {
            get { return _CurrentLine; }
            set { _CurrentLine = value; }
        }
        private List<int> _SourceCodeReferencedLine; 

        public List<int> SourceCodeReferencedLine
        {
            get { return _SourceCodeReferencedLine; }
            set { _SourceCodeReferencedLine = value; }
        }
    }

    public class HighlightCurrentLineBackgroundRenderer : IBackgroundRenderer
    {
        private TextEditor _editor;

        public HighlightCurrentLineBackgroundRenderer(TextEditor editor)
        {
            _editor = editor;
        }

        public KnownLayer Layer
        {
            get { return KnownLayer.Caret; }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null)
                return;

            textView.EnsureVisualLines();
            var currentLine = _editor.Document.GetLineByOffset(_editor.CaretOffset);
            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                drawingContext.DrawRoundedRectangle(
                    new SolidColorBrush(Color.FromArgb(40, 0, 0, 0xFF)),
                    new Pen(new SolidColorBrush(Color.FromArgb(100, 0, 0, 0xFF)), 1),
                    new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)),
                    3, 3);
            }
        }
    }

    public class HighlightDebugSurceCodeBackgroundRenderer : IBackgroundRenderer
    {
        private TextEditor _editor;

        public HighlightDebugSurceCodeBackgroundRenderer(TextEditor editor)
        {
            _editor = editor;
            }

        public KnownLayer Layer
        {
            get { return KnownLayer.Background; }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null)
                return;

            textView.EnsureVisualLines();
            var currentLine = _editor.Document.Lines[UCProgrammingDocument.LineDebugSourceCode - 1];
            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                drawingContext.DrawRoundedRectangle(
                    new SolidColorBrush(Color.FromArgb(40, 255, 215, 0)),
                    new Pen(new SolidColorBrush(Color.FromArgb(100, 255, 215, 0)), 1),
                    new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)),
                    3, 3);
            }
        }
    }

    public class HighlightDebugASMBackgroundRenderer : IBackgroundRenderer
    {
        private TextEditor _editor;

        public HighlightDebugASMBackgroundRenderer(TextEditor editor)
        {
            _editor = editor;
        }

        public KnownLayer Layer
            {
            get { return KnownLayer.Background; }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null || UCProgrammingDocument.LineDebugASM == null)
                return;

            textView.EnsureVisualLines();

            foreach (int line in UCProgrammingDocument.LineDebugASM.SourceCodeReferencedLine)
            {
                if (UCProgrammingDocument.LineDebugASM.CurrentLine == line)
                    continue;

                var iCurrentLineSourceCodeReferenced = _editor.Document.Lines[line - 1];
                foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, iCurrentLineSourceCodeReferenced))
                {
                    drawingContext.DrawRectangle(
                        new SolidColorBrush(Color.FromArgb(0x40, 192, 192, 192)), null,
                        new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)));
                }
            }

            var iCurrentLineASM = _editor.Document.Lines[UCProgrammingDocument.LineDebugASM.CurrentLine - 1];
            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, iCurrentLineASM))
            {
                drawingContext.DrawRoundedRectangle(
                    new SolidColorBrush(Color.FromArgb(40, 255, 215, 0)),
                    new Pen(new SolidColorBrush(Color.FromArgb(100, 255, 215, 0)), 1),
                    new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)),
                    3,3);
            }
        }
    }
}
