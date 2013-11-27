using BIPIDE.Classes;
using BIPIDE_4._0.ControlResources;
using br.univali.portugol.integracao;
using br.univali.portugol.integracao.asa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIPIDE_4._0
{
    [Serializable]
    public sealed class Tradutor : MarshalByRefObject, VisitanteASA
    {
        
        private Stack<String>   _scope                      = new Stack<String>();
        private Stack<LabelCurrentScope> _StructureScope    = new Stack<LabelCurrentScope>();
        private int             _LabelPara                  = 0;
        private int             _LabelParaGreater           = 0;
        private int             _LabelElse                  = 0;
        private int             _LabelElseGreater           = 0;
        private int             _LabelEnquanto              = 0;
        private int             _LabelEnquantoGreater       = 0;
        private int             _LabelFacaEnquanto          = 0;
        private int             _LabelFacaEnquantoGreater   = 0;
        private int             _LabelTemp                  = 0;
        private int             _LabelTempBitwise           = 0;
        private int             _LabelTempBitwiseGreater    = 0;
        private int             _LabelTempOpLog             = 0;
        private int             _LabelTempOpLogGreater      = 0;
        private int             _LabelEscolhaGreater        = 0;
        private int             _LabelEscolha               = 0;
        private int             _LabelCasoGreater           = 0;
        private int             _LabelCaso                  = 0;
        private int             _LabelCasoAux               = 0;
        private String          _LabelRotuloCaso            = "PROXCASO";
        private String          _LabelRotuloCasoAux         = "PROXCASO";
        
        public int LabelParaGreater
        {
            get { return _LabelParaGreater; }
            set
            {
                _LabelParaGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "PARA", "FIMPARA"));
            }
        }

        public int LabelEnquantoGreater
        {
            get { return _LabelEnquantoGreater; }
            set
            {
                _LabelEnquantoGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "INI_ENQ", "FIM_ENQ"));
            }
        }

        public int LabelFacaEnquantoGreater
        {
            get { return _LabelFacaEnquantoGreater; }
            set
            {
                _LabelFacaEnquantoGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "INI_ENQ", "FIM_ENQ"));
            }
        }

        public int LabelEscolhaGreater
        {
            get { return _LabelEscolhaGreater; }
            set
            {
                _LabelEscolhaGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "", "FIMESCOLHA"));
            }
        }
       
        private Programa        _PortugolCode;
        private Boolean         _Jumped                 = false;
        private Boolean         _MenosUnario            = false;
        private Boolean         _MenosUnarioNumber      = false;

        //estruturas para armazenar código e unir depois
        private Codigo          _ObjectCode;
        private Codigo          _Object_ExpressionVectorCode;
        private Codigo          _Object_ExpressionCode;
        private Codigo          _Object_Interrupt;

        private List<FunctionVariables> _Variables;
        private List<ConstVariables>    _ConstVariablesList;

        //Atribuição Vetor
        private Object          _VectorIndex            = null;
        private Boolean         _FlagVector             = false;
        private List<Object>    _InicializationVector;
        private Boolean         _IsIndex                = false;

        //comportamentos
        private Boolean         _FACAENQUANTO           = false;
        private Boolean         _Conditional            = false;
        private Boolean         _FlagReversesOp         = false;
        private String          _RelationalOp           = "";
        private Boolean         _FlagOperation          = false;
        private Boolean         _FlagIncrement          = false;
        private Boolean         _WhenIncrementSeparate  = false;
        private List<String>    _OpBitwiseList;
        private Boolean         _Returned               = false;
        private Boolean         _GoTo                   = false;
        private Boolean         _SVExistsCasoContrario  = false;

        VisitanteSuporte        _SupportVisitor;
        Boolean                 _ThereIsInicio          = false;
        Boolean                 _ExpressionNeedsTemp    = false;
        Boolean                 _IsInterrupt            = false;
        Boolean                 _ThereIsInterrupt       = false;

        public String           _InitialFunction;
        public String           _ProgrammingLanguage;
        private String          _WriteFunction          = "";
        private String          _ReadFunction           = "";

        public Tradutor(Programa PortugolCode, String ProgrammingLanguage)
        {
            this._ProgrammingLanguage   = ProgrammingLanguage;
            this._PortugolCode          = PortugolCode;
            this._Variables             = new List<FunctionVariables>();
            this._ConstVariablesList    = new List<ConstVariables>();
            this._InicializationVector  = new List<Object>();

            this._OpBitwiseList         = new List<String>()
	            {
	                "OR",
	                "AND",
	                "XOR",
                    "SLL",
                    "SRL"
	            };

            _ObjectCode                     = new Codigo();
            _Object_ExpressionVectorCode    = new Codigo();
            _Object_ExpressionCode          = new Codigo();
            _Object_Interrupt               = new Codigo();

            _SupportVisitor = new VisitanteSuporte(PortugolCode);
            IsThereInicio(_SupportVisitor.Variables);
        }

     
        private void IsThereInicio(List<FunctionVariables> Variables)
        {
            if (_ProgrammingLanguage == "Portugol")
            {

                FunctionVariables VariaveisFuncao = Variables.Find(f => f.FunctionName.Equals("inicio"));
                if (VariaveisFuncao != null)
                {
                    _InitialFunction = "inicio";
                    _ThereIsInicio = true;

                    _WriteFunction = "escreva";
                    _ReadFunction = "leia";
                }
            }
            else
            {
                FunctionVariables VariaveisFuncao = Variables.Find(f => f.FunctionName.Equals("main"));
                if (VariaveisFuncao != null)
                {
                    _InitialFunction = "main";
                    _ThereIsInicio = true;

                    _WriteFunction = "printf";
                    _ReadFunction = "scanf";
                }
            }
        }

        public Codigo Convert(Programa programa)
        {
            ArvoreSintaticaAbstrataPrograma asa = programa.getArvoreSintaticaAbstrata();

            //_InitialFunction = programa.getFuncaoInicial().ToLower();

            _ObjectCode.AddInstrucaoASM(".DATA", "", BIPIDE.Classes.eTipo.Section,null);
            _ObjectCode.AddInstrucaoASM(".TEXT", "", BIPIDE.Classes.eTipo.Section, null);            


            asa.aceitar(this);


            List<InstrucaoASM> ia   = _ObjectCode.GetCodigoInstrucaoASM();
            Otimizacao iOptmize     = new Otimizacao();
            Codigo iNewSource       = new Codigo();
            iNewSource              = iOptmize.OptimizesSTOFollowedByLD(_ObjectCode);
            List<InstrucaoASM> iNS  = iNewSource.GetCodigoInstrucaoASM();


            //adicionar código interrupt imediatamente após .text quando C
            if (_Object_Interrupt.GetCodigoStringASM().Count() > 0)
            {
               List<InstrucaoASM> instr = _ObjectCode.PutCodigoTogether();
                
               int index = _ObjectCode.listaTextASM[0].IndexArquivo;

               if (index != null)
               {
                   index = _ObjectCode.listaTextASM[1].IndexArquivo;
                   _InjectInstruction(ref iNewSource, _Object_Interrupt, index);
               }
            }

            iNewSource.RedoProgramIndex();
            return iNewSource;
        }

        private void _InjectInstruction(ref Codigo iNewSource, Codigo _Object_Interrupt, int index)
        {
            iNewSource.InjectInstruction(_Object_Interrupt, index);
            _Object_Interrupt = new Codigo(false);
          
        }


        public object visitarArvoreSintaticaAbstrataPrograma(ArvoreSintaticaAbstrataPrograma asap)
        {
            foreach (NoDeclaracao declaracao in asap.getListaDeclaracoesGlobais())
            {

                declaracao.aceitar(this);
            }

            return null;
        }

        public object visitarNoChamadaFuncao(NoChamadaFuncao chamadaFuncao)
        {
            _WhenIncrementSeparate = true;
            String instr = "LD";
           
            FunctionVariables VariaveisFuncao = _Variables.Find( f => f.FunctionName == chamadaFuncao.getNome() );
            if (VariaveisFuncao != null)
                foreach (String variavel in VariaveisFuncao.Variable)
                {
                    Object valor = chamadaFuncao.getParametros()[VariaveisFuncao.Variable.IndexOf(variavel)].aceitar(this);
                    if (_MenosUnario && _FlagVector)
                    {
                        AppendInstruction("LDI", 0, chamadaFuncao.getTrechoCodigoFonte().getLinha());
                        instr = "SUB";
                        _MenosUnario = false;
                    }
                    if (valor != null)
                        AppendInstructionScope(instr, valor, chamadaFuncao.getTrechoCodigoFonte().getLinha());

                    AppendInstruction("STO", (chamadaFuncao.getNome().ToLower() == _InitialFunction) ? variavel.ToString() : chamadaFuncao.getNome() + "_" + variavel.ToString(), chamadaFuncao.getTrechoCodigoFonte().getLinha());
                }
            else
                foreach (NoDeclaracaoParametro parametro in chamadaFuncao.getParametros())
                {

                    Object valor = parametro.aceitar(this);
                    //leia
                    if (chamadaFuncao.getNome().Equals(_ReadFunction))
                    {
                        AppendInstruction("LD", "$in_port", chamadaFuncao.getTrechoCodigoFonte().getLinha());
                        instr = "STO";
                    }
                    if (_MenosUnario && _FlagVector)
                    {
                        AppendInstruction("LDI", 0, chamadaFuncao.getTrechoCodigoFonte().getLinha());
                        instr = "SUB";
                        _MenosUnario = false;
                    }
                    //leia-escreva
                    if (valor != null)
                        AppendInstructionScope((valor.GetType() == typeof(String)) ? instr : "LD", valor, chamadaFuncao.getTrechoCodigoFonte().getLinha());

                    //escreva
                    if (chamadaFuncao.getNome().Equals(_WriteFunction))
                        AppendInstruction("STO", "$out_port", chamadaFuncao.getTrechoCodigoFonte().getLinha());
                }
            //quando vetor dentro de chamada de funcao
            _FlagVector = false;
            _VectorIndex = null;


            if (!chamadaFuncao.getNome().Equals(_WriteFunction) &&
                !chamadaFuncao.getNome().Equals(_ReadFunction))
                AppendInstruction("CALL", "_" + chamadaFuncao.getNome().ToUpper(), chamadaFuncao.getTrechoCodigoFonte().getLinha());
            
            //AppendInstruction("STO", "a", chamadaFuncao.getTrechoCodigoFonte().getLinha());


            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoDeclaracaoFuncao(NoDeclaracaoFuncao declaracaoFuncao)
        {
            //inclui escopo
            _scope.Push(declaracaoFuncao.getNome());

            String nome_funcao = declaracaoFuncao.getNome();
            if (nome_funcao.ToLower().Equals(_InitialFunction))
                _Jumped = true;

            //trata interrupt no C
            if (_ProgrammingLanguage == "C")
                if (nome_funcao.ToLower() == "interrupt")
                {
                    _IsInterrupt = true;
                    _ThereIsInterrupt = true;
                    AppendInstruction("JMP", "_" + _InitialFunction.ToUpper(), declaracaoFuncao.getTrechoCodigoFonteNome().getLinha());
                }
                 

            //Inclui jump para inicio quando outra função
            if (nome_funcao.ToLower() != _InitialFunction && !_Jumped && _ThereIsInicio && !_ThereIsInterrupt)
            {
                AppendInstruction("JMP", "_" + _InitialFunction.ToUpper(), declaracaoFuncao.getTrechoCodigoFonteNome().getLinha());
                _Jumped = true;
            }

            //Inclui rótulo função
            AppendRotulo("_" + nome_funcao);


            if (_IsInterrupt)            
                AppendInstruction("STO", AddsTempInterrupt(), null);


            //Inicializa variáveis e salva parâmetros
            NoDeclaracaoParametro[] parametros = declaracaoFuncao.getParametros();
            FunctionVariables       declaracao = new FunctionVariables();
            declaracao.FunctionName            = nome_funcao;

            foreach (NoDeclaracaoParametro parametro in parametros)            
                declaracao.Variable.Add( (String)parametro.aceitar(this));            
            _Variables.Add(declaracao);


            foreach (NoBloco bloco in declaracaoFuncao.getBlocos())
            {
                bloco.aceitar(this);
                WriteExpressions();
            }

            if (_IsInterrupt)
            {
                AppendInstruction("LD", "tint", null);
                AppendInstruction("RETINT", "0", null);

                _IsInterrupt = false;                
            }
            else
                if (!_Returned && nome_funcao.ToLower() != _InitialFunction)                
                    AppendInstruction("RETURN", "0", null);
                

            if (nome_funcao.ToLower().Equals(_InitialFunction))
                AppendInstruction("HLT", 0, null);
            
            _Returned = false;
            _scope.Pop();
            return null;
        }

        public object visitarNoDeclaracaoParametro(NoDeclaracaoParametro noDeclaracaoParametro)
        {
            String nome_var = noDeclaracaoParametro.getNome();
            if (!_scope.Peek().Equals(_InitialFunction))
                nome_var = _scope.Peek() + "_" + nome_var;

            //Inclui declaracao de variável
            _ObjectCode.AddInstrucaoASM(nome_var, "0", BIPIDE.Classes.eTipo.Variavel, null);

            return noDeclaracaoParametro.getNome();
        }

        public object visitarNoDeclaracaoVariavel(NoDeclaracaoVariavel noDeclaracaoVariavel)
        {
            Object valor = 0;

            if (noDeclaracaoVariavel.getInicializacao() != null)
            {
                valor = noDeclaracaoVariavel.getInicializacao().aceitar(this);
            }

            String nome_var = noDeclaracaoVariavel.getNome();
            String nome_fun_var = nome_var;
            if (_scope.Count() > 0)
                if (!_scope.Peek().Equals(_InitialFunction))
                    nome_fun_var = _scope.Peek() + "_" + nome_var;

            if (valor != null)
                if (noDeclaracaoVariavel.constante())                
                    _ConstVariablesList.Add(new ConstVariables(nome_fun_var, System.Convert.ToInt32(valor)));                

            //Inclui declaracao de variável
            if (valor != null && !_FlagVector)            
                _ObjectCode.AddInstrucaoASM(nome_fun_var, valor.ToString(), BIPIDE.Classes.eTipo.Variavel, null);            
            else
                if (_FlagVector)
                {
                    _ObjectCode.AddInstrucaoASM(nome_fun_var, "0", BIPIDE.Classes.eTipo.Variavel, null);
                    AppendInstructionScope("LDV", "v", noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                    AppendInstructionScope("STO", nome_fun_var, noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                }
                else
                {
                    _ObjectCode.AddInstrucaoASM(nome_fun_var, "0", BIPIDE.Classes.eTipo.Variavel, null);
                    AppendInstructionScope("STO", nome_fun_var, noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                }

            return null;
        }

        public object visitarNoDeclaracaoVetor(NoDeclaracaoVetor noDeclaracaoVetor)
        {
            String nome = noDeclaracaoVetor.getNome();
            if (_scope.Count() > 0)
                if (!_scope.Peek().Equals(_InitialFunction))
                    nome = _scope.Peek() + "_" + nome;
            TipoDado tipoDado = noDeclaracaoVetor.getTipoDado();

            
            Object tamanho = (noDeclaracaoVetor.getTamanho() == null) ? 0 : noDeclaracaoVetor.getTamanho().aceitar(this);
            if (tamanho.GetType() != typeof(int))
                foreach(ConstVariables variavel in _ConstVariablesList)
                    if (variavel.VariableName.Equals(tamanho.ToString()))
                        tamanho = variavel.VariableValue;   

            String cod_declaracao = noDeclaracaoVetor.getNome() + "\t: 0";

            if (noDeclaracaoVetor.getInicializacao() != null)
            {
                noDeclaracaoVetor.getInicializacao().aceitar(this);

                foreach (Object iItemVetor in _InicializationVector)
                    _ObjectCode.AddInstrucaoASM(nome, iItemVetor.ToString(), BIPIDE.Classes.eTipo.Variavel, null, System.Convert.ToInt32(tamanho), _InicializationVector.IndexOf(iItemVetor));                
            }
            else
            {
                for (int i = 0; i < System.Convert.ToInt32(tamanho); i++)
                {
                    cod_declaracao += ", 0";
                    _ObjectCode.AddInstrucaoASM(nome, "0", BIPIDE.Classes.eTipo.Variavel, null, System.Convert.ToInt32(tamanho), i);
                }                
            }

            return null;
        }

        public object visitarNoEnquanto(NoEnquanto noEnquanto)
        {
            _LabelEnquanto = ++LabelEnquantoGreater;

            AppendRotulo("INI_ENQ" + _LabelEnquanto);

            VerifiesPreExpressionTemp(noEnquanto.getCondicao());
            noEnquanto.getCondicao().aceitar(this);
            PosExpressionTemp(noEnquanto.getCondicao());
            _FlagReversesOp = false;
            WriteExpressions();

            AppendInstruction(_RelationalOp, "FIM_ENQ" + _LabelEnquanto, noEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());


            foreach (NoBloco blocos in noEnquanto.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }

            AppendInstruction("JMP", "INI_ENQ" + _LabelEnquanto, noEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());
            AppendRotulo("FIM_ENQ" + _LabelEnquanto);
            
            _LabelEnquanto--;
            _StructureScope.Pop();
            return null;
        }

        public object visitarNoEscolha(NoEscolha noEscolha)
        {
            _SupportVisitor = new VisitanteSuporte();
            _SupportVisitor.noBloco = noEscolha;
            _SupportVisitor.noBloco.aceitar(_SupportVisitor);
            this._SVExistsCasoContrario = _SupportVisitor._ExistsCasoContrario;

            Object valorEscolha = noEscolha.getExpressao().aceitar(this);

            _LabelEscolha = ++LabelEscolhaGreater;

            if (valorEscolha != null)
                AppendInstructionScope("LD", valorEscolha, noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());
            AppendInstructionScope("STO", "t_escolha" + _LabelEscolha, noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());

            int count =0;
            foreach (NoCaso caso in noEscolha.getCasos())
            {
                ++count;
                if (_LabelCasoAux != 0)
                {
                    AppendRotulo(_LabelRotuloCasoAux + _LabelCasoAux);
                }

                if (_LabelRotuloCasoAux != "CASOCONTRARIO")
                    AppendInstructionScope("LD", "t_escolha" + _LabelEscolha, noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());
                               
                if (noEscolha.getCasos().Length == count)
                {
                    _LabelRotuloCasoAux = "FIMESCOLHA";
                    _LabelCasoAux       = _LabelEscolha;
                }

                //Para: BNE CASOCONTRARIO
                if (noEscolha.getCasos().Length - 1 == count &&
                    this._SVExistsCasoContrario)
                {
                    _LabelRotuloCasoAux = "CASOCONTRARIO";
                    //AppendInstructionScope("LD", "t_escolha" + _LabelEscolha, noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());
                }
                   

                caso.aceitar(this);

            }
            WriteExpressions();
            AppendRotulo("FIMESCOLHA" + _LabelEscolha);

            _LabelRotuloCasoAux = "PROXCASO";

            _StructureScope.Pop();
            return null;
        }

        public object visitarNoCaso(NoCaso noCaso)
        {
            _FlagReversesOp = true;
            NoExpressao iExpressao = noCaso.getExpressao();

            if (iExpressao != null)
            {
                if (_LabelRotuloCasoAux != "FIMESCOLHA")
                    _LabelCasoAux       = ++_LabelCasoGreater;

                Object e = iExpressao.aceitar(this);

                if (e != null)                
                    AppendInstructionScope("SUB", e, null);
                AppendInstructionScope("BNE", _LabelRotuloCasoAux + _LabelCasoAux, noCaso.getExpressao().getTrechoCodigoFonte().getLinha());
            }
            WriteExpressions();
            _FlagReversesOp = false;

            foreach (NoBloco bloco in noCaso.getBlocos())
            {
                bloco.aceitar(this);
                WriteExpressions();
            }

            return null;
        }

        public object visitarNoFacaEnquanto(NoFacaEnquanto noFacaEnquanto)
        {
            _FACAENQUANTO   = true;
            _LabelFacaEnquanto = ++LabelFacaEnquantoGreater;

            AppendRotulo("INI_ENQ" + _LabelFacaEnquanto);


            foreach (NoBloco blocos in noFacaEnquanto.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }


            VerifiesPreExpressionTemp(noFacaEnquanto.getCondicao());
            noFacaEnquanto.getCondicao().aceitar(this);
            PosExpressionTemp(noFacaEnquanto.getCondicao());
            WriteExpressions();

            AppendInstruction(_RelationalOp, "INI_ENQ" + _LabelFacaEnquanto,  noFacaEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());

            AppendRotulo("FIM_ENQ" + _LabelFacaEnquanto);

            _LabelFacaEnquanto--;
            _StructureScope.Pop();
            _FACAENQUANTO   = false;
            return null;
        }

        public object visitarNoInteiro(NoInteiro noInteiro)
        {
            return noInteiro.getValor();

        }
        
        public object visitarNoPara(NoPara noPara)
        {
            _LabelPara = ++LabelParaGreater;


            if (noPara.getInicializacao() != null)
                noPara.getInicializacao().aceitar(this);
            WriteExpressions();         

            AppendRotulo("PARA" + _LabelPara);

            VerifiesPreExpressionTemp(noPara.getCondicao());
            noPara.getCondicao().aceitar(this);
            PosExpressionTemp(noPara.getCondicao());            
            WriteExpressions();

            AppendInstruction(_RelationalOp, "FIMPARA" + _LabelPara, noPara.getCondicao().getTrechoCodigoFonte().getLinha());

            foreach (NoBloco blocos in noPara.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }

            noPara.getIncremento().aceitar(this);
            _FlagReversesOp = false;
            WriteExpressions();

            AppendInstruction("JMP", "PARA" + _LabelPara, noPara.getCondicao().getTrechoCodigoFonte().getLinha());            
            AppendRotulo("FIMPARA" + _LabelPara);


            _LabelPara--;
            _StructureScope.Pop();
            return null;
        }

        public object visitarNoPare(NoPare noPare)
        {
            if (_StructureScope.Count > 0)
                AppendInstruction("JMP", _StructureScope.Peek().End + _StructureScope.Peek().Value+"", null);

            return null;
        }
        
        public object visitarNoReferenciaVariavel(NoReferenciaVariavel noReferenciaVariavel)
        {
            return noReferenciaVariavel.getNome();
        }

        public object visitarNoReferenciaVetor(NoReferenciaVetor noReferenciaVetor)
        {
            _FlagVector = true;
            _IsIndex    = true;
            _VectorIndex = noReferenciaVetor.getIndice().aceitar(this);
            _IsIndex    = false;

            if (_VectorIndex != null)
                AppendInstruction(_VectorIndex.GetType() == typeof(String) ? "LD" : "LDI", _VectorIndex.ToString(), noReferenciaVetor.getTrechoCodigoFonte().getLinha());               
            else
                _VectorIndex = 0;

            AppendInstruction("STO", "$indr", noReferenciaVetor.getTrechoCodigoFonte().getLinha());

           return noReferenciaVetor.getNome();
        }

        public object visitarNoRetorne(NoRetorne noRetorne)
        {
            //Quando função principal não retorna 0 mesmo que explícito devido ao HLT.
            if (_scope.Peek().Equals(_InitialFunction))
                return null;

            _WhenIncrementSeparate = true;

            _Returned = true;

            Object e = noRetorne.getExpressao().aceitar(this);
            if (e != null)
                AppendInstructionScope("LD", e, noRetorne.getExpressao().getTrechoCodigoFonte().getLinha());

            AppendInstruction("RETURN", "0", noRetorne.getExpressao().getTrechoCodigoFonte().getLinha());

            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoSe(NoSe noSe)
        {
            //inclui escopo
            _LabelElse = ++_LabelElseGreater;

            VerifiesPreExpressionTemp(noSe.getCondicao());
            noSe.getCondicao().aceitar(this);
            PosExpressionTemp(noSe.getCondicao());           
            WriteExpressions();
            
            NoBloco[] b = noSe.getBlocosFalsos();
            if (noSe.getBlocosFalsos().Count() > 0)
                AppendInstruction(_RelationalOp, "ELSE" + _LabelElse, noSe.getCondicao().getTrechoCodigoFonte().getLinha());
            else                         
                AppendInstruction(_RelationalOp, "FIMSE" + _LabelElse, noSe.getCondicao().getTrechoCodigoFonte().getLinha());
            

            foreach (NoBloco blocoverdadeiro in noSe.getBlocosVerdadeiros())
            {
                blocoverdadeiro.aceitar(this);
                WriteExpressions();
            }
           
            int i = 0;

            foreach (NoBloco blocofalso in noSe.getBlocosFalsos())
            {
                if (i < 1)
                {
                    AppendInstruction("JMP", "FIMSE" + _LabelElse, noSe.getCondicao().getTrechoCodigoFonte().getLinha());                    
                    AppendRotulo("ELSE" + _LabelElse);
                    i++;
                }

                blocofalso.aceitar(this);
                WriteExpressions();

            }            

            AppendRotulo("FIMSE" + _LabelElse);

            _LabelElse--;
            return null;
        }

        public object visitarNoVetor(NoVetor noVetor)
        {
            List<Object> valores = new List<Object>();
            
            foreach (Object no in noVetor.getValores())
            {
                NoExpressao expr    = (NoExpressao) no;
                Object      o       = expr.aceitar(this);

                valores.Add(o);
            }
            _InicializationVector = valores;

            return null;

        }

        public object visitarNoBitwiseNao(NoBitwiseNao noBitwiseNao)
        {
            Object o = noBitwiseNao.getExpressao().aceitar(this);
            AppendInstructionScope("LD", o, noBitwiseNao.getTrechoCodigoFonte().getLinha());
            AppendInstructionScope("NOT", 0, noBitwiseNao.getTrechoCodigoFonte().getLinha());
            return null;
        }

        public object visitarNoMenosUnario(NoMenosUnario noMenosUnario)
        {
            Object valor = noMenosUnario.getExpressao().aceitar(this);
            _MenosUnario = true;


            if (!_FlagVector || _IsIndex)
            {
                AppendInstruction("LDI", 0, noMenosUnario.getTrechoCodigoFonte().getLinha());
                AppendInstructionScope("SUB", valor, noMenosUnario.getTrechoCodigoFonte().getLinha());
                _MenosUnario = false;
                _MenosUnarioNumber = true;

                return null;
            } else
                return valor;
        }
        
        public object visitarNoOperacaoAtribuicao(NoOperacaoAtribuicao noOperacaoAtribuicao)
        {
            Boolean _KeepsFlagRevOp = _FlagReversesOp;
           

            if (_WhenIncrementSeparate)
            {
                _FlagIncrement          = true;
                _FlagReversesOp         = false;
            }else
                _WhenIncrementSeparate  = true;

            string instr_sto = "STO";
            string instr_ld = "LD";


            #region Execução LADO DIREITO
            Object op_dir = noOperacaoAtribuicao.getOperandoDireito().aceitar(this);

            if (op_dir != null)
                instr_ld = "LD";

            //vetor em uma expressão
            if (_VectorIndex != null)
            {
                instr_ld = "LDV";
                _VectorIndex = null;
            }
            if (instr_ld == "LDV")
            {
                AppendInstructionScope(instr_ld, op_dir, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
                op_dir = null;
                AppendInstructionScope("STO", "t_vetor" + AddsTemp(), noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
                _FlagVector = false;
            }

            #endregion


            #region Execução LADO ESQUERDO
            String op_esq = (String)noOperacaoAtribuicao.getOperandoEsquerdo().aceitar(this);


            if (instr_ld == "LDV")
            {
                if (_MenosUnario)
                {
                    AppendInstruction("LDI", 0, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
                    instr_ld = "SUB";
                    _MenosUnario = false;
                }
                AppendInstructionScope("LD", "t_vetor" + _LabelTemp, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
            }


            //atribuição em vetor
            if (_VectorIndex != null)
            {
                instr_sto = "STOV";
                _VectorIndex = null;
                _FlagVector = false;
               
            }
            _MenosUnarioNumber = false;
            #endregion


            if (op_dir != null)
                AppendInstructionScope(instr_ld, op_dir, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
            AppendInstructionScope(instr_sto, op_esq, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());

            if (_FlagIncrement) 
            {
                _FlagReversesOp = (_KeepsFlagRevOp) ? _KeepsFlagRevOp : _FlagReversesOp;
                _FlagIncrement  = false;
                return op_esq;
            }

            _FlagIncrement         = false;
            return null;
        }

        public object visitarNoOperacaoBitwiseE(NoOperacaoBitwiseE noOperacaoBitwiseE)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoBitwiseE, "AND");
            return null;
        }

        public object visitarNoOperacaoBitwiseLeftShift(NoOperacaoBitwiseLeftShift noOperacaoBitwiseLeftShift)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoBitwiseLeftShift, "SLL");   
            return null;
        }

        public object visitarNoOperacaoBitwiseOu(NoOperacaoBitwiseOu noOperacaoBitwiseOu)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoBitwiseOu, "OR");
            return null;
        }

        public object visitarNoOperacaoBitwiseRightShift(NoOperacaoBitwiseRightShift noOperacaoBitwiseRightShift)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoBitwiseRightShift, "SRL");   
            return null;
        }

        public object visitarNoOperacaoBitwiseXOR(NoOperacaoBitwiseXOR noOperacaoBitwiseXOR)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoBitwiseXOR, "XOR");
            return null;
        }
        
        public object visitarNoOperacaoLogicaDiferenca(NoOperacaoLogicaDiferenca noOperacaoLogicaDiferenca)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO)? "BNE" : "BEQ";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaDiferenca, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoOperacaoLogicaIgualdade(NoOperacaoLogicaIgualdade noOperacaoLogicaIgualdade)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO) ? "BEQ" : "BNE";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaIgualdade, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoOperacaoLogicaMaior(NoOperacaoLogicaMaior noOperacaoLogicaMaior)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO) ? "BGT" : "BLE";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaMaior, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false; 
            return null;
        }

        public object visitarNoOperacaoLogicaMaiorIgual(NoOperacaoLogicaMaiorIgual noOperacaoLogicaMaiorIgual)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO) ? "BGE" : "BLT";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaMaiorIgual, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoOperacaoLogicaMenor(NoOperacaoLogicaMenor noOperacaoLogicaMenor)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO) ? "BLT" : "BGE";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaMenor, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoOperacaoLogicaMenorIgual(NoOperacaoLogicaMenorIgual noOperacaoLogicaMenorIgual)
        {
            _WhenIncrementSeparate = true;
            _FlagOperation  = true;
            _RelationalOp   = (_FACAENQUANTO) ? "BLE" : "BGT";
            _Conditional    = true;
            RunOperations(noOperacaoLogicaMenorIgual, (_ExpressionNeedsTemp) ? "LD" : "SUB");
            _FlagReversesOp = false;
            _WhenIncrementSeparate = false;
            return null;
        }

        public object visitarNoOperacaoSoma(NoOperacaoSoma noOperacaoSoma)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoSoma,_FlagReversesOp&&!_FlagVector ? "SUB" : "ADD");
            return null;
        }

        public object visitarNoOperacaoSubtracao(NoOperacaoSubtracao noOperacaoSubtracao)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoSubtracao, _FlagReversesOp && !_FlagVector ? "ADD" : "SUB");
            return null;
        }

        public object visitarNoContinue(NoContinue noContinue)
        {
            if (_StructureScope.Count > 0)
                if (_StructureScope.Peek().Ini != "")
                    AppendInstruction("JMP", _StructureScope.Peek().Ini + _StructureScope.Peek().Value + "", null);
            return null;
        }

        public object visitarNoTitulo(NoTitulo noTitulo)
        {
            if (!_GoTo)   
                AppendRotulo("_" + noTitulo.getNome().ToUpper());
            else
                return noTitulo.getNome();

            return null;
        }

        public object visitarNoVaPara(NoVaPara noVaPara)
        {
            _GoTo = true;
            Object o = noVaPara.getTitulo().aceitar(this);

            if (o != null)
                AppendInstruction("JMP", "_" + o.ToString().ToUpper(), null);

            _GoTo = false;
            return null;
        }
        
        private void RunOperations(NoOperacao noOperacao, String instr)
        {
            String instr_esq = "LD";
            String instr_esq_antes = "";
            String instr_antes = "";

            Boolean _NeedsTemp = false;

            if (_Conditional && _ExpressionNeedsTemp)
            {
                _NeedsTemp = true;
                _Conditional = false;
            }

           //inverte operacao somente para temp, variavel e numero
           if (_FlagReversesOp == true && !_FlagVector && !_ExpressionNeedsTemp)
               instr_esq = "SUB";

           #region Operando Esquerdo
               //1 - Visita operando
               Object e = noOperacao.getOperandoEsquerdo().aceitar(this);

               //2 - Verifica quando vetor em uma expressão
               if (_VectorIndex != null)
               {
                   instr_esq_antes = instr_esq;
                   instr_esq = "LDV";
                   _VectorIndex = null;
               }
              
               //3- Quando não é expressao, imprime:
               if (e != null)
                   AppendInstructionScope(instr_esq, e, noOperacao.getTrechoCodigoFonte().getLinha());
            
               //4- Se vetor, inclui STO em temporário e inclui var temp na expressão
               if (instr_esq == "LDV" )
               {
                   AppendInstructionScope("STO", "t_vetor" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                   _FlagVector = false;

                   if (_MenosUnario)
                   {
                       AppendInstruction("LDI", 0, noOperacao.getTrechoCodigoFonte().getLinha());
                       instr_esq_antes = "SUB";
                       _MenosUnario = false;
                   }
                   if (instr_esq_antes == "LDV")
                       instr_esq_antes = "LD";
                   AppendInstructionScope(instr_esq_antes, "t_vetor" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
               }

           #endregion
           //Em Operações Lógicas inverte a operação
            if (_Conditional && !_ExpressionNeedsTemp)
            {
                _FlagReversesOp = true;
                _Conditional = false;
            }

            if (_NeedsTemp)
               {
                   AppendInstructionScope("STO", "t_oplog" + AddsTempOpLog(), noOperacao.getTrechoCodigoFonte().getLinha());
                   _Conditional = false;
               }

            //para operações Bitwise
            if ((_OpBitwiseList.IndexOf(instr) != -1))
            {
                //Verifica se direita é expressão, se sim, cria temporário para esquerda
                _SupportVisitor.GetRightExpression = true;
                _SupportVisitor.noBloco = noOperacao;
                Object o = _SupportVisitor.noBloco.aceitar(_SupportVisitor);

                if (o == null)
                {
                    AppendInstructionScope("STO", "t_expr" + AddsTempBitwise(), noOperacao.getTrechoCodigoFonte().getLinha());
                    _Conditional = false;
                }

            }

            #region Operando Direito
                //1 - Visita operando
                Object d = noOperacao.getOperandoDireito().aceitar(this);
                           
                //2 - Verifica quando vetor em uma expressão
                if (_VectorIndex != null)
                {
                    instr_antes = instr;
                    instr = "LDV";
                    _VectorIndex = null;
                }

                //3- Quando não é expressao, imprime:
                if (d != null)
                    AppendInstructionScope(instr, d, noOperacao.getTrechoCodigoFonte().getLinha());

                //4- Se vetor, inclui STO em temporário e inclui var temp na expressão
                if (instr == "LDV")
                {
                    //inclui temporário de EXPR anterior
                    if ((_OpBitwiseList.IndexOf(instr) != -1))
                    {
                        AppendInstructionScope(instr_antes, "t_expr" + _LabelTempBitwise, noOperacao.getTrechoCodigoFonte().getLinha());
                        _FlagVector = false;
                    }
                    else
                    {
                        AppendInstructionScope("STO", "t_vetor" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                        _FlagVector = false;
                        if (_MenosUnario)
                        {
                            AppendInstructionScope("LD", 0, noOperacao.getTrechoCodigoFonte().getLinha());
                            instr_antes = "SUB";
                        }
                        if (instr_antes == "LDV")
                            instr_antes = "LD";
                        AppendInstructionScope(instr_antes, "t_vetor" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
                    }
                }
            #endregion

            //Quando EXPR BITWISE
            //E é uma expressão
            if (((_OpBitwiseList.IndexOf(instr) != -1)) && d == null)
                if (instr == "SLL" || instr == "SRL")
                {
                    AppendInstructionScope("STO", "t_expr" + AddsTempBitwise(), noOperacao.getTrechoCodigoFonte().getLinha());
                    int temp1 = _LabelTempBitwise--;
                    int temp2 = _LabelTempBitwise--;
                    AppendInstructionScope("LD", "t_expr" + temp2, noOperacao.getTrechoCodigoFonte().getLinha());
                    AppendInstructionScope(instr, "t_expr" + temp1, noOperacao.getTrechoCodigoFonte().getLinha());
                }
                else
                    AppendInstructionScope(instr, "t_expr" + _LabelTempBitwise--, noOperacao.getTrechoCodigoFonte().getLinha());
                 
        }


        private void VerifiesPreExpressionTemp(NoExpressao noExpressao)
        {
            _SupportVisitor = new VisitanteSuporte();
            _SupportVisitor.noBloco = noExpressao;
            _SupportVisitor.noBloco.aceitar(_SupportVisitor);
            
            this._ExpressionNeedsTemp = _SupportVisitor._ExpressionNeedsTemp;
        }

        private void PosExpressionTemp(NoExpressao noExpressao)
        {
            if (_ExpressionNeedsTemp)
            {
                AppendInstructionScope("STO", "t_oplog" + AddsTempOpLog(), noExpressao.getTrechoCodigoFonte().getLinha());
                int temp1 = _LabelTempOpLog--;
                int temp2 = _LabelTempOpLog--;
                AppendInstructionScope("LD", "t_oplog" + temp2, noExpressao.getTrechoCodigoFonte().getLinha());
                AppendInstructionScope("SUB", "t_oplog" + temp1, noExpressao.getTrechoCodigoFonte().getLinha());
            }
        }

        /* Método: adiciona_temp()
         * Inclui variável temporária nas declarações de variáveis
         */
        private string AddsTemp()
        {
            String nome = "t_vetor";
            //necessário para vetores em escopo de outras funções
            _IncludeScope(ref nome);
            
            _ObjectCode.AddInstrucaoASM(  nome + _LabelTemp.ToString(), "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTemp.ToString();
        }
        private string AddsTempBitwise()
        {
            String nome = "t_expr";
            //necessário para expressões bitwise em escopo de outras funções
            _IncludeScope(ref nome);
            _LabelTempBitwise = ++_LabelTempBitwiseGreater;
            _ObjectCode.AddInstrucaoASM(nome +_LabelTempBitwiseGreater, "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTempBitwise.ToString();
        }
        private string AddsTempOpLog()
        {
            String nome = "t_oplog";
            //necessário para operações lógicas em escopo de outras funções
            _IncludeScope(ref nome);
            _LabelTempOpLog = ++_LabelTempOpLogGreater;  
            _ObjectCode.AddInstrucaoASM(nome  +_LabelTempOpLog, "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTempOpLog.ToString();
        }

        private string AddsTempInterrupt()
        {
            String nome = "tint";
            _ObjectCode.AddInstrucaoASM(nome , "0", BIPIDE.Classes.eTipo.Variavel, null);
            return nome;
        }

        //Utilização na declaração de temporários
        private string _IncludeScope(ref String nome)
        {
            if (!_scope.Peek().Equals(_InitialFunction))
                nome = _scope.Peek() + "_" + nome;

            return nome;
        }
        
        public void AppendInstructionScope(String instrucao, Object valor, int? linha)
        {
            
            if (valor.GetType() != typeof(String) &&
                instrucao != "SLL" &&
                instrucao != "SRL" &&
                instrucao != "NOT")
                instrucao += "I";
            else
                if (!_scope.Peek().Equals(_InitialFunction) &&
                    valor.GetType() == typeof(String))
                    valor = _scope.Peek() + "_" + valor;

            AppendInstruction(instrucao, valor.ToString(), linha);

        }
        public void AppendRotulo(String nome_rotulo)
        {
            if (_IsInterrupt)
                _Object_Interrupt.AddInstrucaoASM( nome_rotulo, "", BIPIDE.Classes.eTipo.Rotulo, null);
            else
                _ObjectCode.AddInstrucaoASM(nome_rotulo, "", BIPIDE.Classes.eTipo.Rotulo, null);
        }

        public void AppendInstruction(String instrucao, Object valor, int? linha)
        {
            if (_IsInterrupt)
                _Object_Interrupt.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
            else
                if (_FlagVector && _FlagOperation)            
                    _Object_ExpressionVectorCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
                else
                    if (_FlagIncrement && _FlagOperation)                
                        _Object_ExpressionVectorCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
                    else
                        if (_FlagOperation)
                            _Object_ExpressionCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
                        else                
                            _ObjectCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);                

        }


        private void WriteExpressions()
        {
            _FlagOperation  = false;
            _WhenIncrementSeparate = false;

            //atribui código do bloco ao código do programa
            //zera geração de código para o bloco
            if (_Object_ExpressionVectorCode.GetCodigoStringASM().Count() >0)
            {
                _ObjectCode.AddInstrucaoASM(_Object_ExpressionVectorCode);
                _Object_ExpressionVectorCode    = new Codigo(false);
            }
            if (_Object_ExpressionCode.GetCodigoStringASM().Count() > 0)
            {
                _ObjectCode.AddInstrucaoASM(_Object_ExpressionCode);
                _Object_ExpressionCode          = new Codigo(false);
            }
        }

        #region NotSupported
        /* 
         * Not supported
         */
        public object visitarNoCadeia(NoCadeia noCadeia)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoCaracter(NoCaracter noCaracter)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoDeclaracaoMatriz(NoDeclaracaoMatriz noDeclaracaoMatriz)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoLogico(NoLogico noLogico)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoMatriz(NoMatriz noMatriz)
        {
            return null;
        }


        /* 
         * Not supported
         */
        public object visitarNoNao(NoNao noNao)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoReal(NoReal noReal)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoReferenciaMatriz(NoReferenciaMatriz noReferenciaMatriz)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoInclusaoBiblioteca(NoInclusaoBiblioteca noInclusaoBiblioteca)
        {
            return null;
        }
        /* 
         * Not supported
         */
        public object visitarNoOperacaoDivisao(NoOperacaoDivisao noOperacaoDivisao)
        {
            return null;
        }
        /* 
         * Not supported
         */
        public object visitarNoOperacaoLogicaE(NoOperacaoLogicaE noOperacaoLogicaE)
        {
            return null;
        }
        /* 
         * Not supported
         */
        public object visitarNoOperacaoLogicaOU(NoOperacaoLogicaOU noOperacaoLogicaOU)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoOperacaoModulo(NoOperacaoModulo noOperacaoModulo)
        {
            return null;
        }

        /* 
         * Not supported
         */
        public object visitarNoOperacaoMultiplicacao(NoOperacaoMultiplicacao noOperacaoMultiplicacao)
        {
            return null;
        }
        #endregion


    }
}
