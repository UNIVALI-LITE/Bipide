﻿using BIPIDE.Classes;
using BIPIDE_4._0.ControlResources;
using br.univali.portugol.integracao;
using br.univali.portugol.integracao.asa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0
{
    [Serializable]
    public sealed class Tradutor : MarshalByRefObject, VisitanteASA
    {

        private Stack<String>               _scope                      = new Stack<String>();
        private Stack<LabelCurrentScope>    _StructureScope             = new Stack<LabelCurrentScope>();
        private int                         _LabelPara                  = 0;
        private int                         _LabelParaGreater           = 0;
        private int                         _LabelElse                  = 0;
        private int                         _LabelElseGreater           = 0;
        private int                         _LabelEnquanto              = 0;
        private int                         _LabelEnquantoGreater       = 0;
        private int                         _LabelFacaEnquanto          = 0;
        private int                         _LabelFacaEnquantoGreater   = 0;
        private int                         _LabelTemp                  = 0;
        private int                         _LabelTempBitwise           = 0;
        private int                         _LabelTempBitwiseGreater    = 0;
        private int                         _LabelTempOpLog             = 0;
        private int                         _LabelCasoGreater           = 0;
        private int                         _LabelCaso                  = 0;
        private String                      _LabelRotuloCaso            = "PROXCASO";

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
                _StructureScope.Push(new LabelCurrentScope(value, "INI_ENQ", "FIMFACA"));
            }
        }

        public int LabelFacaEnquantoGreater
        {
            get { return _LabelFacaEnquantoGreater; }
            set
            {
                _LabelFacaEnquantoGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "INI_ENQ", "FIMFACA"));
            }
        }

        public int LabelCasoGreater
        {
            get { return _LabelCasoGreater; }
            set
            {
                _LabelCasoGreater = value;
                _StructureScope.Push(new LabelCurrentScope(value, "", "FIMESCOLHA"));
            }
        }

        private StringBuilder       _Asemblycode;
        private StringBuilder       _DataCode;
        private StringBuilder       _TextCode;
        private Programa            _PortugolCode;
        private String              _InitialFunction;
        private Boolean             _Jumped                 = false;
        private Boolean             _MenosUnario            = false;
        private Boolean             _MenosUnarioNumber      = false;

        private Codigo              _ObjectCode;
        private Codigo              _Object_ExpressionVectorCode;
        private Codigo              _Object_ExpressionCode;

        private List<FunctionVariables> _Variables;

        //Atribuição Vetor
        private Object              _VectorIndex            = null;
        private Boolean             _FlagVector             = false;
        private StringBuilder       _ExpressionVectorCode;
        private StringBuilder       _ExpressionCode;
        private List<Object>        _InicializationVector;

        //comportamentos
        private Boolean             _FACAENQUANTO           = false;
        private Boolean             _Conditional            = false;
        private Boolean             _FlagReversesOp         = false;
        private String              _RelationalOp           = "";
        private Boolean             _FlagOperation          = false;
        private List<String>        _OpBitwiseList;
        private Boolean             _Returned               = false;

        VisitanteSuporte            _SupportVisitor;
        Boolean                     _ThereIsInicio          = false;
        Boolean                     _IsBitwiseExpression    = false;


        public Tradutor(Programa PortugolCode)
        {

            this._PortugolCode          = PortugolCode;
            this._Asemblycode           = new StringBuilder();
            this._DataCode              = new StringBuilder();
            this._TextCode              = new StringBuilder();
            this._ExpressionCode        = new StringBuilder();
            this._ExpressionVectorCode  = new StringBuilder();
            this._Variables             = new List<FunctionVariables>();
            this._InicializationVector  = new List<Object>();

            this._OpBitwiseList = new List<String>()
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
            _SupportVisitor                 = new VisitanteSuporte(PortugolCode);

            IsThereInicio(_SupportVisitor.Variables);
        }


        private void IsThereInicio(List<FunctionVariables> Variables)
        {
            FunctionVariables VariaveisFuncao = Variables.Find(f => f.FunctionName.Equals("inicio"));
            if (VariaveisFuncao != null)
                _ThereIsInicio = true;
        }

        public Codigo Convert(Programa programa)
        {
            ArvoreSintaticaAbstrataPrograma asa = programa.getArvoreSintaticaAbstrata();

            _InitialFunction = programa.getFuncaoInicial().ToLower();

            _ObjectCode.AddInstrucaoASM(".DATA", "", BIPIDE.Classes.eTipo.Section, null);
            _ObjectCode.AddInstrucaoASM(".TEXT", "", BIPIDE.Classes.eTipo.Section, null);

            _DataCode.AppendLine(".data");
            _TextCode.AppendLine(".text");
            _ExpressionCode.Append("");
            _ExpressionVectorCode.Append("");



            asa.aceitar(this);


            AppendInstruction("HLT", 0, null);
            _Asemblycode.AppendLine(_DataCode.ToString());
            _Asemblycode.AppendLine(_TextCode.ToString());
            // _ObjectCode.AddInstrucaoASM("HLT", "0", BIPIDE.Classes.eTipo.Instrucao,2);

            // String sp = DisplayMembers(_ObjectCode.GetCodigo()).ToString();
            //String sa = DisplayMembers(_ObjectCode.GetCodigoStringASM()).ToString();
            String sa = DisplayMembers(_ObjectCode.GetCodigoStringASM()).ToString();
            List<InstrucaoASM> ia = _ObjectCode.GetCodigoInstrucaoASM();
            Otimizacao iOptmize = new Otimizacao();
            Codigo iNewSource = new Codigo();
            iNewSource = iOptmize.OptimizesSTOFollowedByLD(_ObjectCode);
            String iDMNS = DisplayMembers(iNewSource.GetCodigoStringASM()).ToString();
            List<InstrucaoASM> iNS = iNewSource.GetCodigoInstrucaoASM();

            return iNewSource;
            //return _Asemblycode.ToString();
        }


        public object visitarArvoreSintaticaAbstrataPrograma(ArvoreSintaticaAbstrataPrograma asap)
        {
            foreach (NoDeclaracao declaracao in asap.getListaDeclaracoesGlobais())
            {

                declaracao.aceitar(this);
            }

            return null;
        }


        public object visitarNoCaso(NoCaso noCaso)
        {
            _FlagReversesOp = true;

            NoExpressao iExpressao = noCaso.getExpressao();

            if (iExpressao != null)
            {
                _LabelCaso = ++LabelCasoGreater;
                Object e = iExpressao.aceitar(this);
                AppendInstructionScope("SUB", e, null);
                AppendInstructionScope("BNE", _LabelRotuloCaso + _LabelCaso, noCaso.getExpressao().getTrechoCodigoFonte().getLinha());
            }

            _FlagReversesOp = false;

            foreach (NoBloco bloco in noCaso.getBlocos())
            {
                bloco.aceitar(this);
                WriteExpressions();
            }

            return null;
        }

        public object visitarNoChamadaFuncao(NoChamadaFuncao chamadaFuncao)
        {
            String instr = "LD";

            FunctionVariables VariaveisFuncao = _Variables.Find(f => f.FunctionName == chamadaFuncao.getNome());
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

                    AppendInstruction("STO", (chamadaFuncao.getNome() == "inicio") ? variavel.ToString() : chamadaFuncao.getNome() + "_" + variavel.ToString(), chamadaFuncao.getTrechoCodigoFonte().getLinha());
                }
            else
                foreach (NoDeclaracaoParametro parametro in chamadaFuncao.getParametros())
                {

                    Object valor = parametro.aceitar(this);
                    //leia
                    if (chamadaFuncao.getNome().Equals("leia"))
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
                        AppendInstructionScope((valor.GetType() == typeof(String)) ? instr : "LDI", valor.ToString(), chamadaFuncao.getTrechoCodigoFonte().getLinha());

                    //escreva
                    if (chamadaFuncao.getNome().Equals("escreva"))
                        AppendInstruction("STO", "$out_port", chamadaFuncao.getTrechoCodigoFonte().getLinha());
                }


            if (!chamadaFuncao.getNome().Equals("escreva") &&
                !chamadaFuncao.getNome().Equals("leia"))
                AppendInstruction("CALL", "_" + chamadaFuncao.getNome().ToUpper(), chamadaFuncao.getTrechoCodigoFonte().getLinha());

            return null;
        }

        public object visitarNoDeclaracaoFuncao(NoDeclaracaoFuncao declaracaoFuncao)
        {
            //inclui escopo
            _scope.Push(declaracaoFuncao.getNome());

            String nome_funcao = declaracaoFuncao.getNome();
            if (nome_funcao.Equals("inicio"))
                _Jumped = true;
            //Inclui jump para inicio quando outra função
            if (nome_funcao.ToUpper() != "INICIO" && !_Jumped && _ThereIsInicio)
            {
                AppendInstruction("JMP", "_INICIO", declaracaoFuncao.getTrechoCodigoFonteNome().getLinha());
                _Jumped = true;
            }

            //Inclui rótulo função
            _ObjectCode.AddInstrucaoASM("_" + nome_funcao, "", BIPIDE.Classes.eTipo.Rotulo, null);
            _TextCode.AppendLine("_" + nome_funcao.ToUpper() + ":");

            //Inicializa variáveis e salva parâmetros
            NoDeclaracaoParametro[] parametros = declaracaoFuncao.getParametros();
            FunctionVariables declaracao = new FunctionVariables();
            declaracao.FunctionName = nome_funcao;
            foreach (NoDeclaracaoParametro parametro in parametros)
            {
                declaracao.Variable.Add((String)parametro.aceitar(this));
            }
            _Variables.Add(declaracao);

            foreach (NoBloco bloco in declaracaoFuncao.getBlocos())
            {
                bloco.aceitar(this);
                WriteExpressions();
            }
            if (!_Returned && nome_funcao.ToUpper() != "INICIO")
            {
                AppendInstruction("RETURN", "0", null);
                _Returned = false;
            }


            _scope.Pop();
            return null;
        }

        public object visitarNoDeclaracaoParametro(NoDeclaracaoParametro noDeclaracaoParametro)
        {
            String nome_var = noDeclaracaoParametro.getNome();
            if (!_scope.Peek().Equals(_InitialFunction))
                nome_var = _scope.Peek() + "_" + nome_var;

            _DataCode.AppendLine("\t" + nome_var + ": " + 0);
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
            if (!_scope.Peek().Equals(_InitialFunction))
                nome_var = _scope.Peek() + "_" + nome_var;

            //Inclui declaracao de variável
            if (valor != null && !_FlagVector)
            {
                _ObjectCode.AddInstrucaoASM(nome_var, valor.ToString(), BIPIDE.Classes.eTipo.Variavel, null);
                _DataCode.AppendLine("\t" + nome_var + ": " + valor);
            }
            else
                if (_FlagVector)
                {
                    _ObjectCode.AddInstrucaoASM(nome_var, "0", BIPIDE.Classes.eTipo.Variavel, null);
                    _DataCode.AppendLine("\t" + nome_var + ": " + 0);
                    AppendInstructionScope("LDV", "v", noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                    AppendInstructionScope("STO", nome_var, noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                }
                else
                {
                    _ObjectCode.AddInstrucaoASM(nome_var, "0", BIPIDE.Classes.eTipo.Variavel, null);
                    _DataCode.AppendLine("\t" + nome_var + ": " + 0);
                    AppendInstructionScope("STO", nome_var, noDeclaracaoVariavel.getTrechoCodigoFonteNome().getLinha());
                }


            return null;

        }

        public object visitarNoDeclaracaoVetor(NoDeclaracaoVetor noDeclaracaoVetor)
        {
            String nome = noDeclaracaoVetor.getNome();
            TipoDado tipoDado = noDeclaracaoVetor.getTipoDado();

            int tamanho = (noDeclaracaoVetor.getTamanho() == null) ? 0 : (System.Int32)noDeclaracaoVetor.getTamanho().aceitar(this);


            String cod_declaracao = noDeclaracaoVetor.getNome() + "\t: 0";

            if (noDeclaracaoVetor.getInicializacao() != null)
            {
                noDeclaracaoVetor.getInicializacao().aceitar(this);
                _DataCode.AppendLine("\t" + cod_declaracao);

                foreach (Object iItemVetor in _InicializationVector)
                {
                    _ObjectCode.AddInstrucaoASM(noDeclaracaoVetor.getNome(), iItemVetor.ToString(), BIPIDE.Classes.eTipo.Variavel, null, tamanho);
                }
            }
            else
            {
                for (int i = 0; i < tamanho; i++)
                {
                    cod_declaracao += ", 0";
                    _ObjectCode.AddInstrucaoASM(noDeclaracaoVetor.getNome(), "0", BIPIDE.Classes.eTipo.Variavel, null, tamanho);
                }


                _DataCode.AppendLine("\t" + cod_declaracao);
                

            }

            return null;
        }

        public object visitarNoEnquanto(NoEnquanto noEnquanto)
        {
            _LabelEnquanto = ++LabelEnquantoGreater;

            _TextCode.AppendLine("INI_ENQ" + _LabelEnquanto + ":");
            _ObjectCode.AddInstrucaoASM("INI_ENQ" + _LabelEnquanto, "", BIPIDE.Classes.eTipo.Rotulo, null);

            noEnquanto.getCondicao().aceitar(this);
            WriteExpressions();
            _FlagReversesOp = false;

            AppendInstruction(_RelationalOp, "FIMFACA" + _LabelEnquanto, noEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());


            foreach (NoBloco blocos in noEnquanto.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }

            AppendInstruction("JMP", "INI_ENQ" + _LabelEnquanto, noEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());
            _TextCode.AppendLine("FIMFACA" + _LabelEnquanto + ":");
            _ObjectCode.AddInstrucaoASM("FIMFACA" + _LabelEnquanto, "", BIPIDE.Classes.eTipo.Rotulo, null);

            _LabelEnquanto--;
            _StructureScope.Pop();
            return null;
        }

        public object visitarNoEscolha(NoEscolha noEscolha)
        {


            Object valorEscolha = noEscolha.getExpressao().aceitar(this);

            AppendInstructionScope("LD", valorEscolha, noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());
            AppendInstructionScope("STO", "t_escolha", noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());

            NoCaso[] casos = noEscolha.getCasos();
            for (int i = 0; i < casos.Length - 1; i++)
            {

                if (i > 0)
                {
                    _TextCode.AppendLine(_LabelRotuloCaso + _LabelCaso + ":");
                    _ObjectCode.AddInstrucaoASM(_LabelRotuloCaso + _LabelCaso, "", BIPIDE.Classes.eTipo.Rotulo, null);
                }

                if (i == casos.Length - 2)
                    _LabelRotuloCaso = "CASOCONTRARIO";

                AppendInstructionScope("LD", "t_escolha", noEscolha.getExpressao().getTrechoCodigoFonte().getLinha());
                casos[i].aceitar(this);
            }

            _TextCode.AppendLine(_LabelRotuloCaso + _LabelCaso + ":");
            _ObjectCode.AddInstrucaoASM(_LabelRotuloCaso + _LabelRotuloCaso, "", BIPIDE.Classes.eTipo.Rotulo, null);
            casos[casos.Length - 1].aceitar(this);


            _TextCode.AppendLine("FIMESCOLHA:");
            _ObjectCode.AddInstrucaoASM("FIMESCOLHA", "", BIPIDE.Classes.eTipo.Rotulo, null);



            _LabelRotuloCaso = "PROXCASO";
            _StructureScope.Pop();
            return null;
        }

        public object visitarNoFacaEnquanto(NoFacaEnquanto noFacaEnquanto)
        {
            _FACAENQUANTO = true;
            _LabelFacaEnquanto = ++LabelFacaEnquantoGreater;

            _TextCode.AppendLine("INI_FACA" + _LabelFacaEnquanto + ":");
            _ObjectCode.AddInstrucaoASM("INI_FACA" + _LabelFacaEnquanto, "", BIPIDE.Classes.eTipo.Rotulo, null);


            foreach (NoBloco blocos in noFacaEnquanto.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }

            noFacaEnquanto.getCondicao().aceitar(this);
            WriteExpressions();
            _FlagReversesOp = false;

            AppendInstruction(_RelationalOp, "INI_FACA" + _LabelFacaEnquanto, noFacaEnquanto.getCondicao().getTrechoCodigoFonte().getLinha());

            _TextCode.AppendLine("FIMFACA" + _LabelFacaEnquanto + ":");
            _ObjectCode.AddInstrucaoASM("FIMFACA" + _LabelFacaEnquanto, "", BIPIDE.Classes.eTipo.Rotulo, null);

            _LabelFacaEnquanto--;
            _StructureScope.Pop();
            _FACAENQUANTO = false;
            return null;
        }

        public object visitarNoInteiro(NoInteiro noInteiro)
        {
            return noInteiro.getValor();

        }

        public object visitarNoPara(NoPara noPara)
        {
            _LabelPara = ++LabelParaGreater;

            //carrega inicialização como uma operação
            if (noPara.getInicializacao() != null)
                noPara.getInicializacao().aceitar(this);

            //label PARA1
            _TextCode.AppendLine("PARA" + _LabelPara + ":");
            _ObjectCode.AddInstrucaoASM("PARA" + _LabelPara, "", BIPIDE.Classes.eTipo.Rotulo, null);

            //substituir visitacao condicao
            noPara.getCondicao().aceitar(this);
            WriteExpressions();

            //flag para trocar operações lado direito da expressão
            _FlagReversesOp = false;

            //faz BGT FIMPARA1
            AppendInstruction(_RelationalOp, "FIMPARA" + _LabelPara, noPara.getCondicao().getTrechoCodigoFonte().getLinha());

            //faz blocos
            foreach (NoBloco blocos in noPara.getBlocos())
            {
                blocos.aceitar(this);
                WriteExpressions();
            }
            //faz operação atribuição do incremento
            noPara.getIncremento().aceitar(this);
            WriteExpressions();

            //JMP PARA1
            AppendInstruction("JMP", "PARA" + _LabelPara, noPara.getCondicao().getTrechoCodigoFonte().getLinha());
            //LABEL FIMPARA1:
            _TextCode.AppendLine("FIMPARA" + _LabelPara + ":");
            _ObjectCode.AddInstrucaoASM("FIMPARA" + _LabelPara, "", BIPIDE.Classes.eTipo.Rotulo, null);


            _LabelPara--;
            _StructureScope.Pop();
            return null;
        }

        public object visitarNoPare(NoPare noPare)
        {
            AppendInstruction("JMP", _StructureScope.Peek().End + ((_StructureScope.Peek().Ini == "") ? "" : _StructureScope.Peek().Value + ""), null);
            return null;
        }

        public object visitarNoReferenciaVariavel(NoReferenciaVariavel noReferenciaVariavel)
        {
            return noReferenciaVariavel.getNome();
        }

        public object visitarNoReferenciaVetor(NoReferenciaVetor noReferenciaVetor)
        {
            _FlagVector = true;

            //Em atribuições
            //Quando esquerda vetor
            //Direita unário numérico
            if (_MenosUnarioNumber)
                AppendInstructionScope("STO", "t" + AddsTemp(), noReferenciaVetor.getTrechoCodigoFonte().getLinha());

            _VectorIndex = noReferenciaVetor.getIndice().aceitar(this);

            if (_VectorIndex != null)
                AppendInstruction(_VectorIndex.GetType() == typeof(String) ? "LD" : "LDI", _VectorIndex.ToString(), noReferenciaVetor.getTrechoCodigoFonte().getLinha());
            else
                _VectorIndex = 0;

            AppendInstruction("STO", "$indr", noReferenciaVetor.getTrechoCodigoFonte().getLinha());

            return noReferenciaVetor.getNome();
        }

        public object visitarNoRetorne(NoRetorne noRetorne)
        {
            _Returned = true;
            Object e = noRetorne.getExpressao().aceitar(this);

            if (e != null)
                AppendInstructionScope("LD", e, noRetorne.getExpressao().getTrechoCodigoFonte().getLinha());
            
            AppendInstruction("RETURN", "0", noRetorne.getExpressao().getTrechoCodigoFonte().getLinha());

            return null;
        }

        public object visitarNoSe(NoSe noSe)
        {
            //inclui escopo
            _LabelElse = ++_LabelElseGreater;

            VerifiesBitwiseExpression(noSe.getCondicao());
            noSe.getCondicao().aceitar(this);
            WriteExpressions();

            //flag para trocar operações lado direito da expressão
            _FlagReversesOp = false;


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
                    _TextCode.AppendLine("ELSE" + _LabelElse + ":");
                    _ObjectCode.AddInstrucaoASM("ELSE" + _LabelElse, "", BIPIDE.Classes.eTipo.Rotulo, null);
                    i++;
                }

                blocofalso.aceitar(this);
                WriteExpressions();

            }


            _TextCode.AppendLine("FIMSE" + _LabelElse + ":");
            _ObjectCode.AddInstrucaoASM("FIMSE" + _LabelElse, "", BIPIDE.Classes.eTipo.Rotulo, null);


            _LabelElse--;
            return null;
        }

        public object visitarNoVetor(NoVetor noVetor)
        {
            List<Object> valores = new List<Object>();

            foreach (Object no in noVetor.getValores())
            {
                NoExpressao expr = (NoExpressao)no;
                Object      o    = expr.aceitar(this);

                valores.Add(o);                
            }
            _InicializationVector = valores;

            String cod_declaracao = "";
            foreach (Object v in valores)
            {
                cod_declaracao += ", " + v.ToString();
            }
            _DataCode.AppendLine("\t" + cod_declaracao);


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


            if (!_FlagVector)
            {
                AppendInstruction("LDI", 0, noMenosUnario.getTrechoCodigoFonte().getLinha());
                AppendInstructionScope("SUB", valor, noMenosUnario.getTrechoCodigoFonte().getLinha());
                _MenosUnario = false;
                _MenosUnarioNumber = true;

                return null;
            }
            else
                return valor;
        }

        public object visitarNoOperacaoAtribuicao(NoOperacaoAtribuicao noOperacaoAtribuicao)
        {
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
                AppendInstructionScope("STO", "t" + AddsTemp(), noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
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
                AppendInstructionScope(instr_ld, "t" + _LabelTemp, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
            }


            //atribuição em vetor
            if (_VectorIndex != null)
            {
                instr_sto = "STOV";
                _VectorIndex = null;
                _FlagVector = false;

                if (_MenosUnarioNumber)
                    AppendInstructionScope("LDI", "t" + _LabelTemp, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());

            }
            _MenosUnarioNumber = false;
            #endregion


            if (op_dir != null)
                AppendInstructionScope(instr_ld, op_dir, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());
            AppendInstructionScope(instr_sto, op_esq, noOperacaoAtribuicao.getTrechoCodigoFonte().getLinha());



            return null;
        }

        public object visitarNoOperacaoBitwiseE(NoOperacaoBitwiseE noOperacaoBitwiseE)
        {
            RunOperations(noOperacaoBitwiseE, "AND");
            return null;
        }

        public object visitarNoOperacaoBitwiseLeftShift(NoOperacaoBitwiseLeftShift noOperacaoBitwiseLeftShift)
        {
            RunOperations(noOperacaoBitwiseLeftShift, "SLL"); 
            return null;
        }

        public object visitarNoOperacaoBitwiseOu(NoOperacaoBitwiseOu noOperacaoBitwiseOu)
        {
            RunOperations(noOperacaoBitwiseOu, "OR");
            return null;
        }

        public object visitarNoOperacaoBitwiseRightShift(NoOperacaoBitwiseRightShift noOperacaoBitwiseRightShift)
        {
            RunOperations(noOperacaoBitwiseRightShift, "SRL");   
            return null;
        }

        public object visitarNoOperacaoBitwiseXOR(NoOperacaoBitwiseXOR noOperacaoBitwiseXOR)
        {
            RunOperations(noOperacaoBitwiseXOR, "XOR");
            return null;
        }

        public object visitarNoOperacaoLogicaDiferenca(NoOperacaoLogicaDiferenca noOperacaoLogicaDiferenca)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BNE" : "BEQ";
            _Conditional = true;
            RunOperations(noOperacaoLogicaDiferenca, "SUB");

            return null;
        }

        public object visitarNoOperacaoLogicaIgualdade(NoOperacaoLogicaIgualdade noOperacaoLogicaIgualdade)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BEQ" : "BNE";
            _Conditional = true;
            RunOperations(noOperacaoLogicaIgualdade, "SUB");
            return null;
        }

        public object visitarNoOperacaoLogicaMaior(NoOperacaoLogicaMaior noOperacaoLogicaMaior)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BGT" : "BLE";
            _Conditional = true;
            RunOperations(noOperacaoLogicaMaior, "SUB");
            return null;
        }

        public object visitarNoOperacaoLogicaMaiorIgual(NoOperacaoLogicaMaiorIgual noOperacaoLogicaMaiorIgual)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BGE" : "BLT";
            _Conditional = true;
            RunOperations(noOperacaoLogicaMaiorIgual, "SUB");
            return null;
        }

        public object visitarNoOperacaoLogicaMenor(NoOperacaoLogicaMenor noOperacaoLogicaMenor)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BLT" : "BGE";
            _Conditional = true;
            RunOperations(noOperacaoLogicaMenor, "SUB");
            return null;
        }

        public object visitarNoOperacaoLogicaMenorIgual(NoOperacaoLogicaMenorIgual noOperacaoLogicaMenorIgual)
        {
            _FlagOperation = true;
            _RelationalOp = (_FACAENQUANTO) ? "BLE" : "BGT";
            _Conditional = true;
            RunOperations(noOperacaoLogicaMenorIgual, "SUB");
            return null;
        }

        public object visitarNoOperacaoSoma(NoOperacaoSoma noOperacaoSoma)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoSoma, _FlagReversesOp && !_FlagVector ? "SUB" : "ADD");
            return null;
        }

        public object visitarNoOperacaoSubtracao(NoOperacaoSubtracao noOperacaoSubtracao)
        {
            _FlagOperation = true;
            RunOperations(noOperacaoSubtracao, _FlagReversesOp && !_FlagVector ? "ADD" : "SUB");
            return null;
        }

        private void RunOperations(NoOperacao noOperacao, String instr)
        {
            String instr_esq = "LD";
            String instr_esq_antes = "";
            String instr_antes = "";

            //inverte operacao somente para temp, variavel e numero
            if (_FlagReversesOp == true && !_FlagVector && !_IsBitwiseExpression)
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
            if (instr_esq == "LDV" &&
                 !((_OpBitwiseList.IndexOf(instr) != -1) && (e != null)))
            {
                AppendInstructionScope("STO", "t" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                _FlagVector = false;

                if (_MenosUnario)
                {
                    AppendInstruction("LDI", 0, noOperacao.getTrechoCodigoFonte().getLinha());
                    instr_esq_antes = "SUB";
                    _MenosUnario = false;
                }
                AppendInstructionScope(instr_esq_antes, "t" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
            }
            else
                if ((_OpBitwiseList.IndexOf(instr) != -1) && (e != null))
                    _FlagVector = false;
            #endregion
            //Em Operações Lógicas inverte a operação
            if (_Conditional && !_IsBitwiseExpression)
            {
                _FlagReversesOp = true;
                _Conditional = false;
            }
            else
                if (_Conditional && _IsBitwiseExpression)
                {
                    AppendInstructionScope("STO", "t_oplog" + AddsTempOpLog(), noOperacao.getTrechoCodigoFonte().getLinha());
                    _Conditional = false;
                }
                else
                    _Conditional = false;
                
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
                        AppendInstructionScope("STO", "t" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                        _FlagVector = false;
                        AppendInstructionScope(instr_antes, "t" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
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

                if (_IsBitwiseExpression)
                {
                    AppendInstructionScope("STO", "t_oplog" + AddsTempOpLog(), noOperacao.getTrechoCodigoFonte().getLinha());
                }
            }            

        }

        private void RunBitwiseOperations(NoOperacao noOperacao, String instr)
        {
            String instr_esq = "LD";
            String instr_esq_antes = "";
            String instr_antes = "";


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
            if (instr_esq == "LDV" &&
                 !((_OpBitwiseList.IndexOf(instr) != -1) && (e != null)))
            {
                AppendInstructionScope("STO", "t" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                _FlagVector = false;

                if (_MenosUnario)
                {
                    AppendInstruction("LDI", 0, noOperacao.getTrechoCodigoFonte().getLinha());
                    instr_esq_antes = "SUB";
                    _MenosUnario = false;
                }
                AppendInstructionScope(instr_esq_antes, "t" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
            }
            else
                if ((_OpBitwiseList.IndexOf(instr) != -1) && (e != null))
                    _FlagVector = false;
            #endregion


            if ((_OpBitwiseList.IndexOf(instr) != -1))
            {
                //Verifica se direita é expressão, se sim, cria temporário para esquerda
                VisitanteSuporte v = new VisitanteSuporte();
                Object o;
                v.GetRightExpression = true;
                v.noBloco = noOperacao;
                o = v.noBloco.aceitar(v);

                if (o == null)
                    AppendInstructionScope("STO", "t_expr" + AddsTempBitwise(), noOperacao.getTrechoCodigoFonte().getLinha());

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
                    AppendInstructionScope("STO", "t" + AddsTemp(), noOperacao.getTrechoCodigoFonte().getLinha());
                    _FlagVector = false;
                    AppendInstructionScope(instr_antes, "t" + _LabelTemp, noOperacao.getTrechoCodigoFonte().getLinha());
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

        private void VerifiesBitwiseExpression(NoExpressao noExpressao)
        {
            _SupportVisitor = new VisitanteSuporte();
            _SupportVisitor.noBloco = noExpressao;
            _SupportVisitor.noBloco.aceitar(_SupportVisitor);

            this._IsBitwiseExpression = _SupportVisitor._IsBitwiseExpression;
        }

        /* Método: adiciona_temp()
         * Inclui variável temporária nas declarações de variáveis
         */
        private string AddsTemp()
        {
            _DataCode.AppendLine("\ttemp" + ++_LabelTemp + ": " + 0);
            _ObjectCode.AddInstrucaoASM("t" + _LabelTemp.ToString(), "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTemp.ToString();
        }
        private string AddsTempBitwise()
        {
            _LabelTempBitwise = ++_LabelTempBitwiseGreater;
            _DataCode.AppendLine("\ttemp_expr" + _LabelTempBitwise + ": " + 0);
            _ObjectCode.AddInstrucaoASM("t_expr" + _LabelTempBitwiseGreater, "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTempBitwise.ToString();
        }
        private string AddsTempOpLog()
        {
            _DataCode.AppendLine("\ttemp_oplog" + _LabelTempOpLog + ": " + 0);
            _ObjectCode.AddInstrucaoASM("t_oplog" + _LabelTempOpLog, "0", BIPIDE.Classes.eTipo.Variavel, null);
            return _LabelTempBitwise.ToString();
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

        public void AppendInstruction(String instrucao, Object valor, int? linha)
        {
            if (_FlagVector && _FlagOperation)
            {
                _ExpressionVectorCode.Append("\t");
                _ExpressionVectorCode.Append(instrucao);
                _ExpressionVectorCode.Append("\t");
                _ExpressionVectorCode.Append(valor);
                _ExpressionVectorCode.AppendLine("\r");
                _Object_ExpressionVectorCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
            }
            else
                if (_FlagOperation)
                {
                    _ExpressionCode.Append("\t");
                    _ExpressionCode.Append(instrucao);
                    _ExpressionCode.Append("\t");
                    _ExpressionCode.Append(valor);
                    _ExpressionCode.AppendLine("\r");
                    _Object_ExpressionCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
                }
                else
                {
                    _TextCode.Append("\t");
                    _TextCode.Append(instrucao);
                    _TextCode.Append("\t");
                    _TextCode.Append(valor);
                    _TextCode.AppendLine("\r");
                    _ObjectCode.AddInstrucaoASM(instrucao, valor.ToString(), BIPIDE.Classes.eTipo.Instrucao, linha);
                }

        }

        public StringBuilder DisplayMembers(List<String> list)
        {
            StringBuilder codigo = new StringBuilder();

            foreach (String s in list)
            {
                codigo.AppendLine(s.ToString());
            }
            return codigo;
        }

        private void WriteExpressions()
        {

            _FlagOperation = false;
            //atribui código do bloco ao código do programa
            //zera geração de código para o bloco
            if (_ExpressionVectorCode != null)
            {
                _TextCode.Append(_ExpressionVectorCode.ToString());
                _ObjectCode.AddInstrucaoASM(_Object_ExpressionVectorCode);

                _Object_ExpressionVectorCode    = new Codigo(false);
                _ExpressionVectorCode           = new StringBuilder();
            }
            if (_ExpressionCode != null)
            {
                _TextCode.Append(_ExpressionCode.ToString());
                _ObjectCode.AddInstrucaoASM(_Object_ExpressionCode);

                _Object_ExpressionCode          = new Codigo(false);
                _ExpressionCode                 = new StringBuilder();
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