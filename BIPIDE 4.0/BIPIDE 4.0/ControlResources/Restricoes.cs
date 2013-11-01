using br.univali.portugol.integracao;
using br.univali.portugol.integracao.asa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIPIDE_4._0
{
  
    class Restricoes: MarshalByRefObject, VisitanteASA
    {
        private static  Processadores _Processador;
        public bool     unsupported             = false;
        public String   unsupported_message     = "";

        public String   _CurrentFunction        = "";
        public Boolean  _IsLoop                 = false;
        public Boolean  _IsConditional          = false;
        //Atribuição em Vetor
        private Boolean _IsVector               = false;

        List<CompilationError>      _ErrorList;
        ResourceDictionary          _Resources;

        private String _WriteFunction           = "";
        private String _ReadFunction            = "";

        internal static Processadores Processador
        {
            get { return Restricoes._Processador; }
            set { if (_Processador < value)
                    Restricoes._Processador = value; 
            }
        }

        public Restricoes(List<CompilationError> iErrorList, String ProgrammingLanguage)
        {
            Processador = Processadores.BIPI;
            _ErrorList  = iErrorList;

            if (ProgrammingLanguage == "Portugol")
            {
                _WriteFunction  = "escreva";
                _ReadFunction   = "leia";
            }
            else
            {
                _WriteFunction  = "printf";
                _ReadFunction   = "scanf";
            }
        }

        public enum Processadores
        {
            [Description("BIP I")]
            BIPI = 1,
            [Description("BIP II")]
            BIPII = 2,
            [Description("BIP III")]
            BIPIII = 3,
            [Description("BIP IV")]
            BIPIV = 4
        }

        public static string GetEnumDescription()
        {
            Enum value = Processador;

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        internal List<CompilationError> Executar(Programa programa)
        {


            _Processador = Processadores.BIPI;
            ArvoreSintaticaAbstrataPrograma asa = programa.getArvoreSintaticaAbstrata();
            asa.aceitar(this);

            return _ErrorList;

        }

        public object visitarArvoreSintaticaAbstrataPrograma(ArvoreSintaticaAbstrataPrograma asap)
        {
            foreach (NoDeclaracao declaracao in asap.getListaDeclaracoesGlobais())
            {

                declaracao.aceitar(this);
            }
            return null;
        }

        public object visitarNoCadeia(NoCadeia noCadeia)
        {
            int linha = noCadeia.getTrechoCodigoFonte().getLinha();
            int coluna = noCadeia.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à NoCadeia";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoCadeia";

            return null;
        }

        public object visitarNoCaracter(NoCaracter noCaracter)
        {
            int linha = noCaracter.getTrechoCodigoFonte().getLinha();
            int coluna = noCaracter.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à NoCaracter";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoCaracter";

            return null;
        }

        public object visitarNoCaso(NoCaso noCaso)
        {
            Processador = Processadores.BIPII;
            noCaso.getExpressao();

            foreach (NoBloco bloco in noCaso.getBlocos())
                bloco.aceitar(this);

            return null;
        }

        public object visitarNoChamadaFuncao(NoChamadaFuncao chamadaFuncao)
        { //SuporteProcedimento + E/S
           
            Processador = Processadores.BIPIV;

            String nome = chamadaFuncao.getNome();
     
            if (nome == _CurrentFunction)
            {
                int linha = chamadaFuncao.getTrechoCodigoFonteNome().getLinha();
                int coluna = chamadaFuncao.getTrechoCodigoFonteNome().getLinha();
                String mensagem = "O BIP  não possui suporte à recursividade";

                //_Resources.FindResource("NotSupportedErrors_English.3");
                   // mensagem = (String)FrameworkElement.FindResource("NotSupportedErrors_English.3");
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "O BIP  não possui suporte à recursividade";
            }

            if (nome != _ReadFunction &&
                nome != _WriteFunction
            )
            _CurrentFunction = nome;

            Object valor;

            foreach (NoDeclaracaoParametro parametro in chamadaFuncao.getParametros())
            {

                valor = parametro.aceitar(this);
                if (nome.Equals(_ReadFunction))
                {
                    if (valor != null)
                    {
                        if (valor.GetType() != typeof(string))
                        {

                            int linha = parametro.getTrechoCodigoFonteNome().getLinha();
                            int coluna = parametro.getTrechoCodigoFonteNome().getLinha();
                            String mensagem = "O comando leia somente permite variáveis";
                            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                            unsupported = true;
                            unsupported_message += Environment.NewLine + "O comando leia somente permite variáveis";
                        }

                    }
                    else
                    {//expressao dentro de leia

                        int linha = parametro.getTrechoCodigoFonteNome().getLinha();
                        int coluna = parametro.getTrechoCodigoFonteNome().getLinha();
                        String mensagem = "O comando leia não permite expressões";
                        _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                        unsupported = true;
                        unsupported_message += Environment.NewLine + "O comando leia  não permite expressões";
                    }

                }
            }
                  
             return null;
        }

        public object visitarNoDeclaracaoFuncao(NoDeclaracaoFuncao declaracaoFuncao)
        {

            _CurrentFunction = declaracaoFuncao.getNome();


            foreach (NoDeclaracaoParametro parametro in declaracaoFuncao.getParametros())
            {
                parametro.aceitar(this);
            }

            foreach (NoBloco bloco in declaracaoFuncao.getBlocos())
            {
                _CurrentFunction = declaracaoFuncao.getNome();
                bloco.aceitar(this);
            }
            return null;
        }

        public object visitarNoDeclaracaoParametro(NoDeclaracaoParametro noDeclaracaoParametro)
        {
            if (noDeclaracaoParametro.getModoAcesso() == ModoAcesso.POR_REFERENCIA)
            {
                int linha = noDeclaracaoParametro.getTrechoCodigoFonteNome().getLinha();
                int coluna = noDeclaracaoParametro.getTrechoCodigoFonteNome().getLinha();
                String mensagem = "O BIP  não possui suporte à modo de acesso por referência";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "O BIP  não possui suporte à modo de acesso por referência";
            }

            return null;
        }

        public object visitarNoDeclaracaoMatriz(NoDeclaracaoMatriz noDeclaracaoMatriz)
        {
            int linha = noDeclaracaoMatriz.getTrechoCodigoFonteTipoDado().getLinha();
            int coluna = noDeclaracaoMatriz.getTrechoCodigoFonteTipoDado().getLinha();
            String mensagem = "O BIP  não possui suporte à Matriz";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Matriz";
            return null;
        }


        public object visitarNoDeclaracaoVariavel(NoDeclaracaoVariavel noDeclaracaoVariavel)
        {

            if (noDeclaracaoVariavel.getInicializacao() != null)
                noDeclaracaoVariavel.getInicializacao().aceitar(this);
            else
            {
                int linha;
                int coluna;
                String mensagem = "";

                TipoDado tipoDadoVariavel = noDeclaracaoVariavel.getTipoDado();
                if (tipoDadoVariavel == TipoDado.REAL)
                {
                    mensagem = "REAL";
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado REAL";
                }
                if (tipoDadoVariavel == TipoDado.CADEIA)
                {
                    mensagem =  "CADEIA";
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado CADEIA";
                }
                if (tipoDadoVariavel == TipoDado.CARACTER)
                {
                    mensagem =  "CARACTER";
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado CARACTER";
                }
                if (tipoDadoVariavel == TipoDado.LOGICO)
                {
                    mensagem =  "LOGICO";
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado LOGICO";
                }
                if (mensagem != "")
                { 
                    mensagem = "O BIP  não possui suporte ao tipo de dado " + mensagem;
                    linha = noDeclaracaoVariavel.getTrechoCodigoFonteTipoDado().getLinha();
                    coluna = noDeclaracaoVariavel.getTrechoCodigoFonteTipoDado().getLinha();
                    _ErrorList.Add(new CompilationError(linha, coluna, mensagem));
                }
            }

            return null;
        }

        public object visitarNoDeclaracaoVetor(NoDeclaracaoVetor noDeclaracaoVetor)
        {

            Processador = Processadores.BIPIV;

            Object tamanho = (noDeclaracaoVetor.getTamanho() == null) ? 0 : noDeclaracaoVetor.getTamanho().aceitar(this);
            if (tamanho.GetType() == typeof(int))
                if (Convert.ToInt32(tamanho) > 1024)
                {
                    int linha = noDeclaracaoVetor.getTrechoCodigoFonteNome().getLinha();
                    int coluna = noDeclaracaoVetor.getTrechoCodigoFonteNome().getLinha();
                    String mensagem = "O BIP  não possui suporte à tamanho de vetores acima de 1024";
                    _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tamanho de vetores acima de 1024";
                }

            return null;
        }

        public object visitarNoEnquanto(NoEnquanto noEnquanto)
        {
            Processador = Processadores.BIPII;

            _IsLoop = true;
            _IsConditional = true;
            noEnquanto.getCondicao().aceitar(this);
            _IsConditional = false;

            foreach (NoBloco blocos in noEnquanto.getBlocos())            
                blocos.aceitar(this);              
            
            _IsLoop = false;
          
            return null;
        }

        public object visitarNoEscolha(NoEscolha noEscolha)
        {

            foreach (NoCaso caso in noEscolha.getCasos())
                caso.aceitar(this);

            return null;
        }

        public object visitarNoFacaEnquanto(NoFacaEnquanto noFacaEnquanto)
        {

            Processador = Processadores.BIPII;

            _IsLoop = true;
          
            foreach (NoBloco blocos in noFacaEnquanto.getBlocos())            
                blocos.aceitar(this);

            _IsConditional = true;
            noFacaEnquanto.getCondicao().aceitar(this);
            _IsConditional = false;

            _IsLoop = false;
            return null;
        }

        public object visitarNoInteiro(NoInteiro noInteiro)
        {
            //retorna tamanho de vetor
            return noInteiro.getValor();
        }

        public object visitarNoLogico(NoLogico noLogico)
        {


            int linha = noLogico.getTrechoCodigoFonte().getLinha();
            int coluna = noLogico.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à NoLogico";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoLogico";

            return null;
        }


        public object visitarNoMatriz(NoMatriz noMatriz)
        {

            int linha = noMatriz.getTrechoCodigoFonte().getLinha();
            int coluna = noMatriz.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Matriz";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Matriz";
            return null;
        }

        public object visitarNoMenosUnario(NoMenosUnario noMenosUnario)
        {
            return null;
        }

        public object visitarNoNao(NoNao noNao)
        {

            int linha = noNao.getTrechoCodigoFonte().getLinha();
            int coluna = noNao.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Operação Lógica NAO";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica NAO";
            return null;
        }

        public object visitarNoOperacao(NoOperacao noOperacao)
        {
            noOperacao.getOperandoDireito().aceitar(this); 
            noOperacao.getOperandoEsquerdo().aceitar(this);
            return null;
        }

        public object visitarNoPara(NoPara noPara)
        {

            Processador = Processadores.BIPII;

            _IsLoop = true;

            if (noPara.getInicializacao() != null)
                noPara.getInicializacao().aceitar(this);

            _IsConditional = true;
            noPara.getCondicao().aceitar(this);
            _IsConditional = false;

            foreach (NoBloco blocos in noPara.getBlocos())            
                blocos.aceitar(this);
            
            noPara.getIncremento().aceitar(this);

            _IsLoop = false;
            return null;
        }

        public object visitarNoPare(NoPare noPare)
        {

            Processador = Processadores.BIPII;
            return null;
        }

        public object visitarNoReal(NoReal noReal)
        {

            int linha = noReal.getTrechoCodigoFonte().getLinha();
            int coluna = noReal.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à NoReal";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoReal";

            return null;
        }

        public object visitarNoReferenciaMatriz(NoReferenciaMatriz noReferenciaMatriz)
        {

            int linha = noReferenciaMatriz.getTrechoCodigoFonte().getLinha();
            int coluna = noReferenciaMatriz.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Matriz";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Matriz";
            return null;
        }

        public object visitarNoReferenciaVariavel(NoReferenciaVariavel noReferenciaVariavel)
        {
            return noReferenciaVariavel.getNome();
        }

        public object visitarNoReferenciaVetor(NoReferenciaVetor noReferenciaVetor)
        {

            Processador = Processadores.BIPIV;
            if (_IsVector)
            {
                int linha = noReferenciaVetor.getTrechoCodigoFonte().getLinha();
                int coluna = noReferenciaVetor.getTrechoCodigoFonte().getLinha();
                String mensagem = "Não suportado vetor dentro de índice de vetor";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Não suportado vetor dentro de índice de vetor";
            }
            else
                _IsVector = true;


            Object o = noReferenciaVetor.getIndice().aceitar(this);

            _IsVector = false;

            return noReferenciaVetor.getNome();
        }

        public object visitarNoRetorne(NoRetorne noRetorne)
        {
            Processador = Processadores.BIPIV;
            return null;
        }

        public object visitarNoSe(NoSe noSe)
        {
            Processador = Processadores.BIPII;

            _IsConditional = true;
            noSe.getCondicao().aceitar(this);
            _IsConditional = false;

            NoBloco[] b = noSe.getBlocosFalsos();
           
            foreach (NoBloco blocoverdadeiro in noSe.getBlocosVerdadeiros())            
                blocoverdadeiro.aceitar(this);    

            foreach (NoBloco blocofalso in noSe.getBlocosFalsos())            
                blocofalso.aceitar(this);

            return null;
        }

        public object visitarNoVetor(NoVetor noVetor)
        {

            Processador = Processadores.BIPIV;
            return null;
        }
        
        public object visitarNoBitwiseNao(NoBitwiseNao noBitwiseNao)
        {
            Processador = Processadores.BIPIII;
            Object e = noBitwiseNao.getExpressao().aceitar(this);
            return null;
        }

        public object visitarNoInclusaoBiblioteca(NoInclusaoBiblioteca noInclusaoBiblioteca)
        {

            int linha = noInclusaoBiblioteca.getTrechoCodigoFonte().getLinha();
            int coluna = noInclusaoBiblioteca.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Inclusão de Biblioteca";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Inclusão de Biblioteca";
            return null;
        }

        public object visitarNoOperacaoAtribuicao(NoOperacaoAtribuicao noOperacaoAtribuicao)
        {
            RunOperations(noOperacaoAtribuicao);

            return null;
        }

        public object visitarNoOperacaoBitwiseE(NoOperacaoBitwiseE noOperacaoBitwiseE)
        {
            Processador = Processadores.BIPIII;
            RunOperations(noOperacaoBitwiseE);
            return null;
        }

        public object visitarNoOperacaoBitwiseLeftShift(NoOperacaoBitwiseLeftShift noOperacaoBitwiseLeftShift)
        {
            Processador = Processadores.BIPIII;
            RunOperations(noOperacaoBitwiseLeftShift);
            return null;
        }

        public object visitarNoOperacaoBitwiseOu(NoOperacaoBitwiseOu noOperacaoBitwiseOu)
        {
            Processador = Processadores.BIPIII;
            RunOperations(noOperacaoBitwiseOu);
            return null;
        }

        public object visitarNoOperacaoBitwiseRightShift(NoOperacaoBitwiseRightShift noOperacaoBitwiseRightShift)
        {
            Processador = Processadores.BIPIII;
            RunOperations(noOperacaoBitwiseRightShift);
            return null;
        }

        public object visitarNoOperacaoBitwiseXOR(NoOperacaoBitwiseXOR noOperacaoBitwiseXOR)
        {
            Processador = Processadores.BIPIII;
            RunOperations(noOperacaoBitwiseXOR);
            return null;
        }

        public object visitarNoOperacaoDivisao(NoOperacaoDivisao noOperacaoDivisao)
        {
            int linha = noOperacaoDivisao.getTrechoCodigoFonte().getLinha();
            int coluna = noOperacaoDivisao.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Operação Divisão";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Divisão";
            return null;
        }

        public object visitarNoOperacaoLogicaDiferenca(NoOperacaoLogicaDiferenca noOperacaoLogicaDiferenca)
        {

            Processador = Processadores.BIPII;

            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaDiferenca.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaDiferenca.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma";
            }
            RunOperations(noOperacaoLogicaDiferenca);

            return null;
        }

        public object visitarNoOperacaoLogicaE(NoOperacaoLogicaE noOperacaoLogicaE)
        {
            int linha = noOperacaoLogicaE.getTrechoCodigoFonte().getLinha();
            int coluna = noOperacaoLogicaE.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Operação Lógica E";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica E";
            return null;
        }

        public object visitarNoOperacaoLogicaIgualdade(NoOperacaoLogicaIgualdade noOperacaoLogicaIgualdade)
        {

            Processador = Processadores.BIPII;

            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaIgualdade.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaIgualdade.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma";
            }
            RunOperations(noOperacaoLogicaIgualdade);
            return null;
        }

        public object visitarNoOperacaoLogicaMaior(NoOperacaoLogicaMaior noOperacaoLogicaMaior)
        {

            Processador = Processadores.BIPII;
            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaMaior.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaMaior.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma.";
            }
            RunOperations(noOperacaoLogicaMaior);
            return null;
        }

        public object visitarNoOperacaoLogicaMaiorIgual(NoOperacaoLogicaMaiorIgual noOperacaoLogicaMaiorIgual)
        {

            Processador = Processadores.BIPII;
            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaMaiorIgual.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaMaiorIgual.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma.";
            }
            RunOperations(noOperacaoLogicaMaiorIgual);
            return null;
        }

        public object visitarNoOperacaoLogicaMenor(NoOperacaoLogicaMenor noOperacaoLogicaMenor)
        {

            Processador = Processadores.BIPII;
            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaMenor.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaMenor.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma.";
            }
            RunOperations(noOperacaoLogicaMenor);
            return null;
        }

        public object visitarNoOperacaoLogicaMenorIgual(NoOperacaoLogicaMenorIgual noOperacaoLogicaMenorIgual)
        {

            Processador = Processadores.BIPII;
            if (!_IsConditional)
            {
                int linha = noOperacaoLogicaMenorIgual.getTrechoCodigoFonte().getLinha();
                int coluna = noOperacaoLogicaMenorIgual.getTrechoCodigoFonte().getLinha();
                String mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma.";
            }
            RunOperations(noOperacaoLogicaMenorIgual);
            return null;
        }

        public object visitarNoOperacaoLogicaOU(NoOperacaoLogicaOU noOperacaoLogicaOU)
        {
            int linha;
            int coluna;
            String mensagem;

            if (!_IsConditional)
            {
                linha = noOperacaoLogicaOU.getTrechoCodigoFonte().getLinha();
                coluna = noOperacaoLogicaOU.getTrechoCodigoFonte().getLinha();
                mensagem = "Operação Lógica não pode ser utilizada desta forma";
                _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Operação Lógica não pode ser utilizada desta forma.";
            }

            linha = noOperacaoLogicaOU.getTrechoCodigoFonte().getLinha();
            coluna = noOperacaoLogicaOU.getTrechoCodigoFonte().getLinha();
            mensagem = "O BIP  não possui suporte à Operação Lógica OU";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica OU";
            return null;
        }

        public object visitarNoOperacaoModulo(NoOperacaoModulo noOperacaoModulo)
        {
            int linha = noOperacaoModulo.getTrechoCodigoFonte().getLinha();
            int coluna = noOperacaoModulo.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Módulo";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Módulo";
            return null;
        }

        public object visitarNoOperacaoMultiplicacao(NoOperacaoMultiplicacao noOperacaoMultiplicacao)
        {
            int linha = noOperacaoMultiplicacao.getTrechoCodigoFonte().getLinha();
            int coluna = noOperacaoMultiplicacao.getTrechoCodigoFonte().getLinha();
            String mensagem = "O BIP  não possui suporte à Multiplicação";
            _ErrorList.Add(new CompilationError(linha, coluna, mensagem));

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Multiplicação";
            return null;
        }

        public object visitarNoOperacaoSoma(NoOperacaoSoma noOperacaoSoma)
        {
            RunOperations(noOperacaoSoma);
            return null;
        }

        public object visitarNoOperacaoSubtracao(NoOperacaoSubtracao noOperacaoSubtracao)
        {
            RunOperations(noOperacaoSubtracao);
            return null;
        }

        private void RunOperations(NoOperacao noOperacao)
        {           
            Object e = noOperacao.getOperandoEsquerdo().aceitar(this);

            if (noOperacao.getOperandoDireito() != null)
                noOperacao.getOperandoDireito().aceitar(this);
        }

        public object visitarNoContinue(NoContinue noContinue)
        {

            Processador = Processadores.BIPII;

            if (!_IsLoop)
            {
                //int linha = noContinue.getTrechoCodigoFonte().getLinha();
                //int coluna = noContinue.getTrechoCodigoFonte().getLinha();
                String mensagem = "Continue não pode ser utilizado fora de laços de repetição";
                _ErrorList.Add(new CompilationError(0, 0, mensagem));

                unsupported = true;
                unsupported_message += Environment.NewLine + "Continue não pode ser utilizado fora de laços de repetição";
            }
            return null;
        }

        public object visitarNoTitulo(NoTitulo noTitulo)
        {
            return null;
        }

        public object visitarNoVaPara(NoVaPara noVaPara)
        {
            noVaPara.getTitulo().aceitar(this);

            return null;
        }
    }
}
