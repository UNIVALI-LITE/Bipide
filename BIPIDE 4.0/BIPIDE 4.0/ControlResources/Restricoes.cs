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
    class Restricoes: MarshalByRefObject, VisitanteASA
    {
        private static  Processadores _Processador;
        public bool     unsupported             = false;
        public String   unsupported_message     = "";

        public String   _CurrentFunction        = "";

        //Atribuição Vetor
        private Boolean _IsVector               = false;

        internal static Processadores Processador
        {
            get { return Restricoes._Processador; }
            set 
            {
                if (_Processador < value)
                    Restricoes._Processador = value; 
            }
        }

        public Restricoes(){
            Processador = Processadores.BIPI;
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

        internal void Executar(Programa programa)
        {
            _Processador = Processadores.BIPI;
            ArvoreSintaticaAbstrataPrograma asa = programa.getArvoreSintaticaAbstrata();
            asa.aceitar(this);

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
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoCadeia";

            return null;
        }

        public object visitarNoCaracter(NoCaracter noCaracter)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoCaracter";

            return null;
        }

        public object visitarNoCaso(NoCaso noCaso)
        {
            return null;
        }

        public object visitarNoChamadaFuncao(NoChamadaFuncao chamadaFuncao)
        { //SuporteProcedimento + E/S
           
            Processador = Processadores.BIPIV;

            String nome = chamadaFuncao.getNome();

            if (nome == _CurrentFunction)
            {
                unsupported = true;
                unsupported_message += Environment.NewLine + "O BIP  não possui suporte à recursividade";
            }
            _CurrentFunction = nome;

            Object valor;

            try
            {
                foreach (NoDeclaracaoParametro parametro in chamadaFuncao.getParametros())
                {
                    valor = parametro.aceitar(this);
                    if (nome.Equals("leia"))
                    {
                        if (valor != null)
                        {
                            if (valor.GetType() != typeof(string))
                            {
                                unsupported = true;
                                unsupported_message += Environment.NewLine + "O comando leia somente permite variáveis";
                            }

                        }
                        else
                        {//expressao dentro de leia
                            unsupported = true;
                            unsupported_message += Environment.NewLine + "O comando leia  não permite expressões";
                        }

                    }
                }
            }
            catch { };//Quando não há parâmetros
                  
            


             return null;
        }

        public object visitarNoDeclaracaoFuncao(NoDeclaracaoFuncao declaracaoFuncao)
        {
            _CurrentFunction = declaracaoFuncao.getNome();

            foreach (NoBloco bloco in declaracaoFuncao.getBlocos())
            {
                bloco.aceitar(this);
            }
            return null;
        }

        public object visitarNoDeclaracaoMatriz(NoDeclaracaoMatriz noDeclaracaoMatriz)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Matriz";
            return null;
        }

        public object visitarNoDeclaracaoParametro(NoDeclaracaoParametro noDeclaracaoParametro)
        {
            return null;
        }

        public object visitarNoDeclaracaoVariavel(NoDeclaracaoVariavel noDeclaracaoVariavel)
        {

            if (noDeclaracaoVariavel.getInicializacao() != null)
                noDeclaracaoVariavel.getInicializacao().aceitar(this);
            else
            {
                TipoDado tipoDadoVariavel = noDeclaracaoVariavel.getTipoDado();
                if (tipoDadoVariavel == TipoDado.REAL)
                {
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado REAL";
                }
                if (tipoDadoVariavel == TipoDado.CADEIA)
                {
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado CADEIA";
                }
                if (tipoDadoVariavel == TipoDado.CARACTER)
                {
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado CARACTER";
                }
                if (tipoDadoVariavel == TipoDado.LOGICO)
                {
                    unsupported = true;
                    unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tipo de dado LOGICO";
                }
            }

            return null;
        }

        public object visitarNoDeclaracaoVetor(NoDeclaracaoVetor noDeclaracaoVetor)
        {

            Processador = Processadores.BIPIV;


            int tamanho = (noDeclaracaoVetor.getTamanho() == null) ? 0 : (System.Int32) noDeclaracaoVetor.getTamanho().aceitar(this);
            if (tamanho > 1024)
            {
                unsupported = true;
                unsupported_message += Environment.NewLine + "O BIP  não possui suporte à tamanho de vetores acima de 1024";
            }

            return null;
        }

        public object visitarNoEnquanto(NoEnquanto noEnquanto)
        {
            return null;
        }

        public object visitarNoEscolha(NoEscolha noEscolha)
        {
            return null;
        }

        public object visitarNoFacaEnquanto(NoFacaEnquanto noFacaEnquanto)
        {
            return null;
        }

        public object visitarNoInteiro(NoInteiro noInteiro)
        {
            //retorna tamanho de vetor
            return noInteiro.getValor();
        }

        public object visitarNoLogico(NoLogico noLogico)
        {

            Processador = Processadores.BIPIII;
            Processador = Processadores.BIPIV;

            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoLogico";

            return null;
        }


        public object visitarNoMatriz(NoMatriz noMatriz)
        {
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

            //carrega inicialização como uma operação
            if (noPara.getInicializacao() != null)
                noPara.getInicializacao().aceitar(this);


            //substituir visitacao condicao
            noPara.getCondicao().aceitar(this);

            //faz blocos
            foreach (NoBloco blocos in noPara.getBlocos())            
                blocos.aceitar(this);
            
            //faz operação atribuição do incremento
            noPara.getIncremento().aceitar(this);

            return null;
        }

        public object visitarNoPare(NoPare noPare)
        {
            return null;
        }

        public object visitarNoReal(NoReal noReal)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à NoReal";

            return null;
        }

        public object visitarNoReferenciaMatriz(NoReferenciaMatriz noReferenciaMatriz)
        {
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
            if (_IsVector)
            {
                unsupported = true;
                unsupported_message += Environment.NewLine + "Não suportado vetor dentro de índice de vetor";
            }
            else
                _IsVector = true;

            noReferenciaVetor.getIndice().aceitar(this);

            _IsVector = false;

            return noReferenciaVetor.getNome();
        }

        public object visitarNoRetorne(NoRetorne noRetorne)
        {
            return null;
        }

        public object visitarNoSe(NoSe noSe)
        {
            Processador = Processadores.BIPII;


            noSe.getCondicao().aceitar(this);

            Boolean bloco_else = false;
            //ocorre erro corba se blocos falsos = null quando não existe
            try
            {
                NoBloco[] b = noSe.getBlocosFalsos();
                if (noSe.getBlocosFalsos() != null)
                bloco_else = true;
            }
            catch
            {
            }

            foreach (NoBloco blocoverdadeiro in noSe.getBlocosVerdadeiros())           
                blocoverdadeiro.aceitar(this);
             
            
            if (bloco_else)            
                foreach (NoBloco blocofalso in noSe.getBlocosFalsos())
                    blocofalso.aceitar(this);

                
            

            return null;
        }

        public object visitarNoVetor(NoVetor noVetor)
        {
            return null;
        }
        
        public object visitarNoBitwiseNao(NoBitwiseNao noBitwiseNao)
        {
            Object e = noBitwiseNao.getExpressao().aceitar(this);
            return null;
        }

        public object visitarNoInclusaoBiblioteca(NoInclusaoBiblioteca noInclusaoBiblioteca)
        {
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
            RunOperations(noOperacaoBitwiseE);
            return null;
        }

        public object visitarNoOperacaoBitwiseLeftShift(NoOperacaoBitwiseLeftShift noOperacaoBitwiseLeftShift)
        {
            RunOperations(noOperacaoBitwiseLeftShift);
            return null;
        }

        public object visitarNoOperacaoBitwiseOu(NoOperacaoBitwiseOu noOperacaoBitwiseOu)
        {
            RunOperations(noOperacaoBitwiseOu);
            return null;
        }

        public object visitarNoOperacaoBitwiseRightShift(NoOperacaoBitwiseRightShift noOperacaoBitwiseRightShift)
        {
            RunOperations(noOperacaoBitwiseRightShift);
            return null;
        }

        public object visitarNoOperacaoBitwiseXOR(NoOperacaoBitwiseXOR noOperacaoBitwiseXOR)
        {
            RunOperations(noOperacaoBitwiseXOR);
            return null;
        }

        public object visitarNoOperacaoDivisao(NoOperacaoDivisao noOperacaoDivisao)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Divisão";
            return null;
        }

        public object visitarNoOperacaoLogicaDiferenca(NoOperacaoLogicaDiferenca noOperacaoLogicaDiferenca)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica Diferença";
            return null;
        }

        public object visitarNoOperacaoLogicaE(NoOperacaoLogicaE noOperacaoLogicaE)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica E";
            return null;
        }

        public object visitarNoOperacaoLogicaIgualdade(NoOperacaoLogicaIgualdade noOperacaoLogicaIgualdade)
        {
            RunOperations(noOperacaoLogicaIgualdade);
            return null;
        }

        public object visitarNoOperacaoLogicaMaior(NoOperacaoLogicaMaior noOperacaoLogicaMaior)
        {
            RunOperations(noOperacaoLogicaMaior);
            return null;
        }

        public object visitarNoOperacaoLogicaMaiorIgual(NoOperacaoLogicaMaiorIgual noOperacaoLogicaMaiorIgual)
        {
            RunOperations(noOperacaoLogicaMaiorIgual);
            return null;
        }

        public object visitarNoOperacaoLogicaMenor(NoOperacaoLogicaMenor noOperacaoLogicaMenor)
        {
            RunOperations(noOperacaoLogicaMenor);
            return null;
        }

        public object visitarNoOperacaoLogicaMenorIgual(NoOperacaoLogicaMenorIgual noOperacaoLogicaMenorIgual)
        {
            RunOperations(noOperacaoLogicaMenorIgual);
            return null;
        }

        public object visitarNoOperacaoLogicaOU(NoOperacaoLogicaOU noOperacaoLogicaOU)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Operação Lógica OU";
            return null;
        }

        public object visitarNoOperacaoModulo(NoOperacaoModulo noOperacaoModulo)
        {
            unsupported = true;
            unsupported_message += Environment.NewLine + "O BIP  não possui suporte à Módulo";
            return null;
        }

        public object visitarNoOperacaoMultiplicacao(NoOperacaoMultiplicacao noOperacaoMultiplicacao)
        {
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
      
    }
}
