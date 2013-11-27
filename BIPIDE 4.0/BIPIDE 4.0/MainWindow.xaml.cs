using BIPIDE.Classes;
using BIPIDE_4._0.ControlResources;
using BIPIDE_4._0.UIResources;
using BIPIDE_4._0.ViewResources;
using br.univali.portugol.integracao;
using br.univali.portugol.integracao.analise;
using br.univali.portugol.integracao.mensagens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml;
using System.Xml.Serialization;
using Xceed.Wpf.AvalonDock.Layout;

namespace BIPIDE_4._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private static SimulationControl   _SimulationControl;
        private ArrayList           _LanguagesMapping;
        private CorbaController     _CorbaController;
        private Portugol            _ProgrammingLanguageInstance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadLanguageResources()
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(LanguageMapping) });

            XmlReader Reader = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + @"LanguageResources\LanguageMapping.xml");
            _LanguagesMapping = (ArrayList)SerializerObj.Deserialize(Reader);

            foreach (LanguageMapping iLanguage in _LanguagesMapping)
            {
                RibbonMenuItem iMenuItem = new RibbonMenuItem()
                    {
                        Header      = (String)FindResource(iLanguage.Name),
                        ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + iLanguage.Icon)),
                        Tag         = iLanguage
                    };
                iMenuItem.Click += iMenuItem_Click;
                _RibbonMenuButtonLanguages.Items.Add(iMenuItem);

                if (_RibbonMenuButtonLanguages.Tag == null)
                    iMenuItem_Click(iMenuItem, null);
            }
        }

        void iMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _RibbonMenuButtonLanguages.Tag              = (sender as RibbonMenuItem).Tag;
            _RibbonMenuButtonLanguages.SmallImageSource = 
                new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).Icon));

            Resources.MergedDictionaries.Clear();
            Application.Current.Resources.Clear();

            Resources.MergedDictionaries.Add(
                new ResourceDictionary() 
                { 
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).InterfaceMapping) 
                });
            Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).TipsMapping)
                });
            
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).NotSuportedErrors)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroSimboloNaoInicializado)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroAoAtribuirEmVetor)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroAoInicializarVetor)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroAtribuirEmChamadaFuncao)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroAtribuirEmConstante)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroDefinirTipoDadoVetorLiteral)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroInicializacaoConstante)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroInicializacaoInvalida)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroNumeroParametrosFuncao)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroOperacaoComExpressaoConstante)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroOperandoEsquerdoAtribuicaoConstante)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroParaSemExpressaoComparacao)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroParametroExcedente)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroParametroRedeclarado)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroPassagemParametroInvalida)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroQuantidadeElementosInicializacaoVetor)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroQuantificadorParametroFuncao)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroReferenciaInvalida)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroSimboloNaoDeclarado)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroSimboloRedeclarado)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroTamanhoVetorMatriz)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroTipoParametroIncompativel)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroTiposIncompativeis)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroVaParaSemTitulo)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroVetorSemElementos)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroComandoEsperado)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroEscopo)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroExpressaoEsperada)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroExpressaoIncompleta)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroExpressaoInesperada)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroExpressoesForaEscopoPrograma)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroFaltaDoisPontos)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroNomeIncompativel)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroNomeSimboloEstaFaltando)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroPalavraReservadaEstaFaltando)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroParaEsperaCondicao)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroParentesis)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroTipoDeDadoEstaFaltando)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).ErroTokenFaltando)
                });

            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + ((sender as RibbonMenuItem).Tag as LanguageMapping).Success)
                });

            SetDimanycResources();
        }

        private void SetDimanycResources()
        {
            foreach (RibbonMenuItem iMenuItem in _RibbonMenuButtonLanguages.Items)
                iMenuItem.Header                = (String)FindResource((iMenuItem.Tag as LanguageMapping).Name);

            foreach (LayoutDocument iLayoutDocument in _DocumentPane.Children)
            {
                if (iLayoutDocument.Content.GetType() == typeof(UCHelpFundamentals))
                {
                    iLayoutDocument.Title = (String)FindResource("ButtonFundamentals");
                    (iLayoutDocument.Content as UCHelpFundamentals).ReloadFundamentals((_RibbonMenuButtonLanguages.Tag as LanguageMapping).HelpMapping);
                }
                else if (iLayoutDocument.Content.GetType() == typeof(UCHelpPractice))
                {
                    iLayoutDocument.Title = (String)FindResource("ButtonPractice");
                }
                else if (iLayoutDocument.Content.GetType() == typeof(UCProgrammingDocument))
                {
                    (iLayoutDocument.Content as UCProgrammingDocument)._TabItemProgramming.Header   =
                        (String)FindResource("TabProgramming");
                    (iLayoutDocument.Content as UCProgrammingDocument)._TabItemSimulation.Header    =
                        (String)FindResource("TabSimulation");
                }
            }

            _LayoutAnchorableErrorList.Title    = (String)FindResource("GridMessageName");
            _ErrorLine.Header                   = (String)FindResource("GridErrorColumnLines");
            _ErrorColumn.Header                 = (String)FindResource("GridErrorColumnColumns");
            _ErrorDescription.Header            = (String)FindResource("GridErrorColumnDescription");
        }


        private void LoadCorbaConnection(SplashScreen iSScreen)
        {
            iSScreen.SplashScreenLabel.Content = "Inicializando CORBA";            
            _CorbaController = new CorbaController();
            _CorbaController.Start();

            _CorbaController.RaiseJavaProcess(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ProgrammingLanguagesResources\\PortugolResources\\CompilerResources\\portugol-integracao.jar");
            _ProgrammingLanguageInstance = (Portugol)_CorbaController.ResolveProcess("Portugol");

        }

        public void Build(UCProgrammingDocument pSelectedDocument)
        {
            List<CompilationError>  iErrorList = new List<CompilationError>(); 
            try
            {
                br.univali.portugol.integracao.Programa iProgram = (br.univali.portugol.integracao.Programa)_ProgrammingLanguageInstance.compilar(pSelectedDocument._TextEditorSourceCode.Text, pSelectedDocument.ProgrammingLanguage);


                Restricoes iRestrictions = new Restricoes(iErrorList, pSelectedDocument.ProgrammingLanguage);
                iErrorList = iRestrictions.Executar(iProgram);

                ArchitectureCheck();

                if (iErrorList.Count == 0)
                {
                    iErrorList.Add(new CompilationError("", "", (string)Application.Current.FindResource("Success.Message"), 0));

                    Tradutor reg = new Tradutor(iProgram, pSelectedDocument.ProgrammingLanguage);
                    Codigo iAssembly = reg.Convert(iProgram);

                    pSelectedDocument._Simulator.SetMemoriaDados(iAssembly.GetMemoriaDados());
                    pSelectedDocument._Simulator.SetMemoriaPrograma(iAssembly.GetMemoriaInstrucao());
                    pSelectedDocument._Simulator.SetRotulosPrograma(iAssembly.GetListaRotulos());

                    pSelectedDocument._AssemblySource       = iAssembly;
                    pSelectedDocument.AssemblyText          = iAssembly.GetCodigoStringASM();
                    pSelectedDocument.SourceCodeDebugText   = pSelectedDocument._TextEditorSourceCode.Text;
                }

            }
            catch (br.univali.portugol.integracao.ErroCompilacao ec)
            {
                
                ResultadoAnalise resultado = ec.resultadoAnalise;

                if (resultado.getNumeroTotalErros() > 0) {

                    foreach (ErroSintatico erro in resultado.getErrosSintaticos())  
                        iErrorList.Add(new CompilationError(erro));

                    foreach (ErroSemantico erro in resultado.getErrosSemanticos())                    
                        iErrorList.Add(new CompilationError(erro));
                }
            }
            
            if (iErrorList != null)
                dataGridErrorList.ItemsSource = iErrorList;
        }

        private void ArchitectureCheck()
        {
            switch (Restricoes.Processador)
            {
                case Restricoes.Processadores.BIPI:
                    _RadioButtonSimulationBipI.IsEnabled    = true;
                    _RadioButtonSimulationBipII.IsEnabled   = true;
                    _RadioButtonSimulationBipIII.IsEnabled  = true;
                    _RadioButtonSimulationBipIV.IsEnabled   = true;
                    break;

                case Restricoes.Processadores.BIPII:
                    _RadioButtonSimulationBipI.IsEnabled    = false;
                    _RadioButtonSimulationBipII.IsEnabled   = true;
                    _RadioButtonSimulationBipIII.IsEnabled  = true;
                    _RadioButtonSimulationBipIV.IsEnabled   = true;
                    break;

                case Restricoes.Processadores.BIPIII:
                    _RadioButtonSimulationBipI.IsEnabled    = false;
                    _RadioButtonSimulationBipII.IsEnabled   = false;
                    _RadioButtonSimulationBipIII.IsEnabled  = true;
                    _RadioButtonSimulationBipIV.IsEnabled   = true;
                    break;

                case Restricoes.Processadores.BIPIV:
                    _RadioButtonSimulationBipI.IsEnabled    = false;
                    _RadioButtonSimulationBipII.IsEnabled   = false;
                    _RadioButtonSimulationBipIII.IsEnabled  = false;
                    _RadioButtonSimulationBipIV.IsEnabled   = true;
                    break;
            }
        }

        #region Programar

        private void _MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            FormNewDocument iFormNewDocument = new FormNewDocument(Resources);
            if (iFormNewDocument.ShowDialog() == true)
            {
                UCProgrammingDocument iProgrammingDocument          = new UCProgrammingDocument(iFormNewDocument.HighlightFile, iFormNewDocument.ProgrammingLanguage);
                iProgrammingDocument.SimulationContext              = _ContextualTabGroupSimulation;
                iProgrammingDocument.SimulationTab                  = _TabSimulation;
                iProgrammingDocument.RibbonMain                     = _RibbonMain;
                iProgrammingDocument._TabItemProgramming.Header     = (String)FindResource("TabProgramming");
                iProgrammingDocument._TabItemSimulation.Header      = (String)FindResource("TabSimulation");
                iProgrammingDocument.SimulationSelectedProcessor    = SimulationControl.Processors.psBipIV;

                if (iFormNewDocument.IsFile)
                    iProgrammingDocument._TextEditorSourceCode.Text = File.ReadAllText(iFormNewDocument.Content);
                else if (iFormNewDocument.IsNewProject)
                    iProgrammingDocument._TextEditorSourceCode.Text = iFormNewDocument.Content;

                LayoutDocument iLayoutDocument  = new LayoutDocument();
                iLayoutDocument.Title           = iFormNewDocument.ProjectName;
                iLayoutDocument.Content         = iProgrammingDocument;
                iLayoutDocument.IsSelectedChanged += iProgrammingDocument_IsSelectedChanged;

                _DocumentPane.Children.Add(iLayoutDocument);

                iLayoutDocument.IsActive = true;
            }
        }

        void iProgrammingDocument_IsSelectedChanged(object sender, EventArgs e)
        {
            if (((sender as LayoutDocument).Content as UCProgrammingDocument)._TabItemProgramming.IsSelected)
                _ContextualTabGroupSimulation.Visibility = System.Windows.Visibility.Collapsed;

            if (((sender as LayoutDocument).Content as UCProgrammingDocument)._TabItemSimulation.IsSelected)
                _ContextualTabGroupSimulation.Visibility = System.Windows.Visibility.Visible;

            if (!(sender as LayoutDocument).IsSelected)
                _ContextualTabGroupSimulation.Visibility = System.Windows.Visibility.Collapsed;

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

        private void _ButtonBuild_Click(object sender, RoutedEventArgs e)
        {
            if (_DocumentPane.SelectedContent != null)
            {
                UCProgrammingDocument iSelectedDocument =
                    ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

                Build(iSelectedDocument);
            }
        }

        #endregion Programar

        #region Simular

        private void _ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.ExecutaSimulacao();

            _SimulationControl.Control = SimulationControl.SimulationControls.scStart;
        }

        private void _ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.Pause();

            _SimulationControl.Control = SimulationControl.SimulationControls.scPause;
        }

        private void _ButtonRepeat_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.RepetePasso();

            _SimulationControl.Control = SimulationControl.SimulationControls.scRepeat;
        }

        private void _ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.PassoProx();

            _SimulationControl.Control = SimulationControl.SimulationControls.scNext;
        }

        private void _ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.Restart();

            _SimulationControl.Control = SimulationControl.SimulationControls.scContinue;
        }

        private void _ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument._Simulator.Stop();

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

        private void _RadioButtonSimulationBipV_Checked(object sender, RoutedEventArgs e)
        {
            UCProgrammingDocument iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psUBip;
        }

        #endregion Simular

        #region Aprender

        private void _ButtonFundamentals_Click(object sender, RoutedEventArgs e)
        {
            UCHelpFundamentals iHelp = new UCHelpFundamentals((_RibbonMenuButtonLanguages.Tag as LanguageMapping).HelpMapping);

            LayoutDocument iLayoutDocument = new LayoutDocument();
            iLayoutDocument.Title   = (String)FindResource("ButtonFundamentals");
            iLayoutDocument.Content = iHelp;

            if (_DocumentPane.Children.Count(x => x.Content.GetType() == typeof(UCHelpFundamentals)) == 0)
            {
                _DocumentPane.Children.Add(iLayoutDocument);
                iLayoutDocument.IsActive = true;
        }
            else
            {
                foreach (LayoutDocument iDocument in _DocumentPane.Children)
                {
                    if (iDocument.Content.GetType() == typeof(UCHelpFundamentals))
                        iDocument.IsActive = true;
                }
            }

        }

        private void _ButtonPractice_Click(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iHelpPractice = new UCHelpPractice();
            iHelpPractice.SimulationSelectedProcessor = SimulationControl.Processors.psBipIV;

            LayoutDocument iLayoutDocument = new LayoutDocument();
            iLayoutDocument.Title   = (String)FindResource("ButtonPractice");
            iLayoutDocument.Content = iHelpPractice;
            iLayoutDocument.IsSelectedChanged += iHelpPractice_IsSelectedChanged;
            

            if (_DocumentPane.Children.Count(x => x.Content.GetType() == typeof(UCHelpPractice)) == 0)
            {
                _DocumentPane.Children.Add(iLayoutDocument);
                iLayoutDocument.IsActive = true;
            }
            else
            {
                foreach (LayoutDocument iDocument in _DocumentPane.Children)
                {
                    if (iDocument.Content.GetType() == typeof(UCHelpPractice))
                        iDocument.IsActive = true;
                }
            }

            _RibbonMain.SelectedItem = _TabPractice;
        }

        void iHelpPractice_IsSelectedChanged(object sender, EventArgs e)
        {
            if ((sender as LayoutDocument).IsSelected)
                _ContextualTabGroupPractice.Visibility = System.Windows.Visibility.Visible;
            else
                _ContextualTabGroupPractice.Visibility = System.Windows.Visibility.Collapsed;

            switch (((sender as LayoutDocument).Content as UCHelpPractice).SimulationSelectedProcessor)
            {
                case SimulationControl.Processors.psBipI:
                    _RadioButtonPracticeBipI.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipII:
                    _RadioButtonPracticeBipII.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipIII:
                    _RadioButtonPracticeBipIII.IsChecked = true;
                    break;

                case SimulationControl.Processors.psBipIV:
                    _RadioButtonPracticeBipIV.IsChecked = true;
                    break;
            }
        }

        private void _RadioButtonPracticeBipI_Checked(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            VerifyComboBoxPracticeValue(iSelectedDocument, SimulationControl.Processors.psBipI);
            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipI;
            
            _GroupBIPI.Visibility   = System.Windows.Visibility.Visible;
            _GroupBIPII.Visibility  = System.Windows.Visibility.Collapsed;
            _GroupBIPIII.Visibility = System.Windows.Visibility.Collapsed;
            _GroupBIPIV.Visibility  = System.Windows.Visibility.Collapsed;
        }

        private void _RadioButtonPracticeBipII_Checked(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            VerifyComboBoxPracticeValue(iSelectedDocument, SimulationControl.Processors.psBipII);
            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipII;

            _GroupBIPI.Visibility   = System.Windows.Visibility.Visible;
            _GroupBIPII.Visibility  = System.Windows.Visibility.Visible;
            _GroupBIPIII.Visibility = System.Windows.Visibility.Collapsed;
            _GroupBIPIV.Visibility  = System.Windows.Visibility.Collapsed;
        }

        private void _RadioButtonPracticeBipIII_Checked(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            VerifyComboBoxPracticeValue(iSelectedDocument, SimulationControl.Processors.psBipIII);
            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipIII;

            _GroupBIPI.Visibility   = System.Windows.Visibility.Visible;
            _GroupBIPII.Visibility  = System.Windows.Visibility.Visible;
            _GroupBIPIII.Visibility = System.Windows.Visibility.Visible;
            _GroupBIPIV.Visibility  = System.Windows.Visibility.Collapsed;

        }

        private void _RadioButtonPracticeBipIV_Checked(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            VerifyComboBoxPracticeValue(iSelectedDocument, SimulationControl.Processors.psBipIV);
            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psBipIV;

            _GroupBIPI.Visibility   = System.Windows.Visibility.Visible;
            _GroupBIPII.Visibility  = System.Windows.Visibility.Visible;
            _GroupBIPIII.Visibility = System.Windows.Visibility.Visible;
            _GroupBIPIV.Visibility  = System.Windows.Visibility.Visible;
        }

        private void _RadioButtonPracticeBipV_Checked(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            VerifyComboBoxPracticeValue(iSelectedDocument, SimulationControl.Processors.psUBip);
            iSelectedDocument.SimulationSelectedProcessor = SimulationControl.Processors.psUBip;

            _GroupBIPI.Visibility = System.Windows.Visibility.Visible;
            _GroupBIPII.Visibility = System.Windows.Visibility.Visible;
            _GroupBIPIII.Visibility = System.Windows.Visibility.Visible;
            _GroupBIPIV.Visibility = System.Windows.Visibility.Visible;
        }

        private void _ButtonPracticeStart_Click(object sender, RoutedEventArgs e)
        {
            UCHelpPractice iSelectedDocument =
                ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

            switch ((_GalleryInstructions.SelectedItem as RibbonGalleryItem).Content as String)
            {
                case "HLT":
                    iSelectedDocument._Simulator.HLT( Convert.ToInt32(_TextBoxPracticeValue.Text) );
                    break;

                case "STO":
                    iSelectedDocument._Simulator.STO(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "LD":
                    iSelectedDocument._Simulator.LD(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "LDI":
                    iSelectedDocument._Simulator.LDI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "ADD":
                    iSelectedDocument._Simulator.ADD(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "ADDI":
                    iSelectedDocument._Simulator.ADDI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "SUB":
                    iSelectedDocument._Simulator.SUB(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "SUBI":
                    iSelectedDocument._Simulator.SUBI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BEQ":
                    iSelectedDocument._Simulator.BEQ(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BNE":
                    iSelectedDocument._Simulator.BNE(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BGT":
                    iSelectedDocument._Simulator.BGT(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BGE":
                    iSelectedDocument._Simulator.BGE(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BLT":
                    iSelectedDocument._Simulator.BLT(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "BLE":
                    iSelectedDocument._Simulator.BLE(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "JMP":
                    iSelectedDocument._Simulator.JMP(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "NOT":
                    iSelectedDocument._Simulator.NOT();
                    break;

                case "AND":
                    iSelectedDocument._Simulator.AND(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "ANDI":
                    iSelectedDocument._Simulator.ANDI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "OR":
                    iSelectedDocument._Simulator.OR(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "ORI":
                    iSelectedDocument._Simulator.ORI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "XOR":
                    iSelectedDocument._Simulator.XOR(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "XORI":
                    iSelectedDocument._Simulator.XORI(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "SLL":
                    iSelectedDocument._Simulator.SLL(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "SLR":
                    iSelectedDocument._Simulator.SRL(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "STOV":
                    iSelectedDocument._Simulator.STOV(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

                case "LDV":
                    iSelectedDocument._Simulator.LDV(Convert.ToInt32(_TextBoxPracticeValue.Text));
                    break;

            }
        }

        private void VerifyComboBoxPracticeValue(UCHelpPractice pSelectedDocument, SimulationControl.Processors pCurrentProcessor )
        {
            if (pSelectedDocument.SimulationSelectedProcessor > pCurrentProcessor)
            {
                if (Convert.ToInt32((_GalleryInstructions.SelectedItem as RibbonGalleryItem).Tag) > (int) pCurrentProcessor)
                {
                    switch (pCurrentProcessor)
                    {
                        case SimulationControl.Processors.psBipI:
                            _GalleryInstructions.SelectedItem = _GroupBIPIDefoultValue;
                            break;

                        case SimulationControl.Processors.psBipII:
                            _GalleryInstructions.SelectedItem = _GroupBIPIIDefoultValue;
                            break;
                        
                        case SimulationControl.Processors.psBipIII:
                            _GalleryInstructions.SelectedItem = _GroupBIPIIIDefoultValue;
                            break;
                        
                        case SimulationControl.Processors.psBipIV:
                            _GalleryInstructions.SelectedItem = _GroupBIPIVDefoultValue;
                            break;
                    }
                }
            }
        }

        private void _TextBoxPracticeValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                case Key.Tab:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        #endregion Aprender

        private void _ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bipide_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ProgrammingLanguageInstance.encerrar();
        }

        private void Bipide_Initialized(object sender, EventArgs e)
        {
            SplashScreen iSplashScreen = new SplashScreen();
            iSplashScreen.Show();

            SetDimanycResources();
            _SimulationControl = new SimulationControl(_ButtonStart,
                                                        _ButtonPause,
                                                        _ButtonRepeat,
                                                        _ButtonNext,
                                                        _ButtonContinue,
                                                        _ButtonStop);

            LoadLanguageResources();

            LoadCorbaConnection(iSplashScreen);

            iSplashScreen.Close();
        }

        private void _GallerySpeedPractice_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((_DocumentPane.SelectedContent as LayoutDocument).Content.GetType() == typeof(UCProgrammingDocument))
            {

                UCProgrammingDocument iSelectedDocument =
                    ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCProgrammingDocument);

                iSelectedDocument._Simulator.Velocidade(Convert.ToInt32(((sender as RibbonGallery).SelectedItem as RibbonGalleryItem).Tag));
            }

            if ((_DocumentPane.SelectedContent as LayoutDocument).Content.GetType() == typeof(UCHelpPractice))
            {

                UCHelpPractice iSelectedDocument =
                    ((_DocumentPane.SelectedContent as LayoutDocument).Content as UCHelpPractice);

                iSelectedDocument._Simulator.Velocidade(Convert.ToInt32(((sender as RibbonGallery).SelectedItem as RibbonGalleryItem).Tag));
            }
        }
    }

    public enum TipoErro 
    { 
        teSucesso = 0,
        teAlerta = 1,
        teErro = 2
    }

    public class CompilationError : System.ComponentModel.INotifyPropertyChanged
    {
        private TipoErro _TipoErro;
        private string _Line;
        private string _Column;
        private string _Message;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
       
        public CompilationError(){
        }
        public CompilationError(int line, int column, String message, TipoErro errorType)
        {
            Line = line.ToString();
            Column = column.ToString();
            Message = message;
            TipoErro = errorType;
        }
        public CompilationError(int line, int column, String message)
        {
            Line = line.ToString();
            Column = column.ToString();
            Message = message;
            TipoErro = TipoErro.teErro;
        }
        public CompilationError(String line, String column, String message)
        {
            Line = line;
            Column = column;
            Message = message;
            TipoErro = TipoErro.teErro;
        }

        public CompilationError(String line, String column, String message, TipoErro errorType)
        {
            Line = line;
            Column = column;
            Message = message;
            TipoErro = errorType;
        }
        public CompilationError(br.univali.portugol.integracao.mensagens.ErroSintatico erro)
        {
            Line = erro.linha.ToString();
            Column = erro.coluna.ToString();
            Message = _TreatingMessages(erro.codigo, erro.mensagem);
            TipoErro = TipoErro.teErro;
        }
        public CompilationError(br.univali.portugol.integracao.mensagens.ErroSemantico erro)
        {
            Line = erro.linha.ToString();
            Column = erro.coluna.ToString();
            Message = _TreatingMessages(erro.codigo, erro.mensagem);
            TipoErro = TipoErro.teErro;
        }

        public string Line
        {
            get { return _Line; }
            set { _Line = value;
            OnPropertyChanged("Line");
            }
        }
        public string Column
        {
            get { return _Column; }
            set { _Column = value;
            OnPropertyChanged("Column");
            }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value;
            OnPropertyChanged("Message");
            }
        }

        public TipoErro TipoErro
        {
            get { return _TipoErro; }
            set { _TipoErro = value; }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _TreatingMessages(String erro, String msgDefault)
        {
            String msg ="";
            try
            {
                msg = (string)Application.Current.FindResource(erro);
            }catch{
                msg = msgDefault;
            };

            return msg;

        }
    }


    
}
