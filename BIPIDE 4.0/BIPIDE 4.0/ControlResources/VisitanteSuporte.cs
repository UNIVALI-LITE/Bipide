using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.univali.portugol.integracao.asa;
using br.univali.portugol.integracao;

namespace BIPIDE_4._0
{
    
    [Serializable]
    public sealed class VisitanteSuporte : MarshalByRefObject, VisitanteASA
    {
        public NoBloco  noBloco;
        public NoBloco  RightNodeExpression;
        public Type     RightNodeExprType;
        public Boolean  _IsBitwiseExpression    = false;
        public Boolean GetRightExpression = false;
        
        private List<FunctionVariables> _Variables;
        public  List<FunctionVariables>  Variables
        {
            get { return _Variables; }
            set { _Variables = value; }
        }

        public VisitanteSuporte(Programa programa)
        {
            this._Variables             = new List<FunctionVariables>();
            ArvoreSintaticaAbstrataPrograma asa = programa.getArvoreSintaticaAbstrata();
            asa.aceitar(this);
        }

        public VisitanteSuporte()
        {
        }
        public object visitarArvoreSintaticaAbstrataPrograma(ArvoreSintaticaAbstrataPrograma asap)
        {
            foreach (NoDeclaracao declaracao in asap.getListaDeclaracoesGlobais())
                declaracao.aceitar(this);

            return null;
        }

        public object visitarNoCadeia(NoCadeia noCadeia)
        {
            return null;
        }

        public object visitarNoCaracter(NoCaracter noCaracter)
        {
            return null;
        }

        public object visitarNoCaso(NoCaso noCaso)
        {
            return null;
        }

        public object visitarNoChamadaFuncao(NoChamadaFuncao chamadaFuncao)
        {
            try
            {
                foreach (NoExpressao expressao in chamadaFuncao.getParametros())
                    expressao.aceitar(this);
            }
            catch
            {
            };

            return null;
        }

        public object visitarNoDeclaracaoFuncao(NoDeclaracaoFuncao declaracaoFuncao)
        {
            String nome_funcao = declaracaoFuncao.getNome();

            //Inicializa variáveis e salva parâmetros
            NoDeclaracaoParametro[] parametros  = declaracaoFuncao.getParametros();
            FunctionVariables       declaracao  = new FunctionVariables();
            declaracao.FunctionName             = nome_funcao;

            foreach (NoDeclaracaoParametro parametro in parametros)            
                declaracao.Variable.Add((String)parametro.aceitar(this));
            
            _Variables.Add(declaracao);

            foreach (NoBloco bloco in declaracaoFuncao.getBlocos())            
                bloco.aceitar(this);

            return null;
        }

        public object visitarNoDeclaracaoMatriz(NoDeclaracaoMatriz noDeclaracaoMatriz)
        {
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

            return null;
        }

        public object visitarNoDeclaracaoVetor(NoDeclaracaoVetor noDeclaracaoVetor)
        {
            if (noDeclaracaoVetor.getInicializacao() != null)
                noDeclaracaoVetor.getInicializacao().aceitar(this);

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
            return noInteiro.getValor(); 
        }

        public object visitarNoLogico(NoLogico noLogico)
        {
            return null;
        }

        public object visitarNoMatriz(NoMatriz noMatriz)
        {
            return null;
        }

        public object visitarNoMenosUnario(NoMenosUnario noMenosUnario)
        {
            return null;
        }

        public object visitarNoNao(NoNao noNao)
        {
            return null;
        }

        public object visitarNoOperacao(NoOperacao noOperacao)
        {      
            return null;
        }

        private void executa_atribuicao(NoOperacao noOperacao)
        {
            String op_esq = (String)noOperacao.getOperandoEsquerdo().aceitar(this);
            Object op_dir = noOperacao.getOperandoDireito().aceitar(this);
        
        }

        private void executa_operacao(NoOperacao noOperacao, String instr)
        {
            Object e = noOperacao.getOperandoEsquerdo().aceitar(this);
            Object d = noOperacao.getOperandoDireito().aceitar(this);
          
        }

        private void executa_operacao_condicional(NoOperacao noOperacao, String instr)
        {
            Object e = noOperacao.getOperandoEsquerdo().aceitar(this);           
            Object d = noOperacao.getOperandoDireito().aceitar(this);          

        }

        public object visitarNoPara(NoPara noPara)
        {          
            if (noPara.getInicializacao() != null)            
                noPara.getInicializacao().aceitar(this);

            //substituir visitacao condicao
            noPara.getCondicao().aceitar(this);
         
            foreach (NoBloco blocos in noPara.getBlocos())            
                blocos.aceitar(this);

            noPara.getIncremento().aceitar(this);
                      
            return null;
        }

        public object visitarNoPare(NoPare noPare)
        {
            return null;
        }

        public object visitarNoReal(NoReal noReal)
        {
            return null;
        }

        public object visitarNoReferenciaMatriz(NoReferenciaMatriz noReferenciaMatriz)
        {
            return null;
        }

        public object visitarNoReferenciaVariavel(NoReferenciaVariavel noReferenciaVariavel)
        {
            return noReferenciaVariavel.getNome();
        }

        public object visitarNoReferenciaVetor(NoReferenciaVetor noReferenciaVetor)
        {
            noReferenciaVetor.getIndice().aceitar(this);

            return noReferenciaVetor.getNome();
        }

        public object visitarNoRetorne(NoRetorne noRetorne)
        {
            Object e = noRetorne.getExpressao().aceitar(this);        

            return null;
        }

        public object visitarNoSe(NoSe noSe)
        {
            noSe.getCondicao().aceitar(this);

            foreach (NoBloco blocoverdadeiro in noSe.getBlocosVerdadeiros())
                blocoverdadeiro.aceitar(this);

            foreach (NoBloco blocofalso in noSe.getBlocosFalsos())
                blocofalso.aceitar(this);

            return null;
        }

        public object visitarNoVetor(NoVetor noVetor)
        {
            foreach (Object no in noVetor.getValores())
            {
                NoExpressao expr = (NoExpressao)no;
                Object o = expr.aceitar(this);
            }

            return null;
        }

        public object visitarNoBitwiseNao(NoBitwiseNao noBitwiseNao)
        {
            noBitwiseNao.getExpressao().aceitar(this);
            //trata-se de uma expressão
            return null;
        }

        public object visitarNoInclusaoBiblioteca(NoInclusaoBiblioteca noInclusaoBiblioteca)
        {
            return null;
        }

        public object visitarNoOperacaoAtribuicao(NoOperacaoAtribuicao noOperacaoAtribuicao)
        {
            String op_esq = (String)noOperacaoAtribuicao.getOperandoEsquerdo().aceitar(this);           
            Object op_dir = noOperacaoAtribuicao.getOperandoDireito().aceitar(this);           

            return null;
        }

        public object visitarNoOperacaoBitwiseE(NoOperacaoBitwiseE noOperacaoBitwiseE)
        {
            _IsBitwiseExpression = true;

            if (GetRightExpression)
            {
                GetRightExpression = false;
                return noOperacaoBitwiseE.getOperandoDireito().aceitar(this);
            }
            else
            {
                noOperacaoBitwiseE.getOperandoEsquerdo().aceitar(this);
                noOperacaoBitwiseE.getOperandoDireito().aceitar(this);
            }

            return null;
        }

        public object visitarNoOperacaoBitwiseLeftShift(NoOperacaoBitwiseLeftShift noOperacaoBitwiseLeftShift)
        {
            _IsBitwiseExpression = true;

            if (GetRightExpression)
            {
                GetRightExpression = false;
                return noOperacaoBitwiseLeftShift.getOperandoDireito().aceitar(this);
            }
            else
            {
                noOperacaoBitwiseLeftShift.getOperandoEsquerdo().aceitar(this);
                noOperacaoBitwiseLeftShift.getOperandoDireito().aceitar(this);
            }

            return null;
        }

        public object visitarNoOperacaoBitwiseOu(NoOperacaoBitwiseOu noOperacaoBitwiseOu)
        {
            _IsBitwiseExpression = true;

            if (GetRightExpression)
            {
                GetRightExpression = false;
                return noOperacaoBitwiseOu.getOperandoDireito().aceitar(this);
            }
            else
            {
                noOperacaoBitwiseOu.getOperandoEsquerdo().aceitar(this);
                if (noOperacaoBitwiseOu.getOperandoDireito() != null)
                    noOperacaoBitwiseOu.getOperandoDireito().aceitar(this);
            }

            return null;
        }

        public object visitarNoOperacaoBitwiseRightShift(NoOperacaoBitwiseRightShift noOperacaoBitwiseRightShift)
        {
            _IsBitwiseExpression = true;

            if (GetRightExpression)
            {
                GetRightExpression = false;
                return noOperacaoBitwiseRightShift.getOperandoDireito().aceitar(this);
            }
            else
            {
                noOperacaoBitwiseRightShift.getOperandoEsquerdo().aceitar(this);
                noOperacaoBitwiseRightShift.getOperandoDireito().aceitar(this);
            }

            return null;
        }

        public object visitarNoOperacaoBitwiseXOR(NoOperacaoBitwiseXOR noOperacaoBitwiseXOR)
        {
            _IsBitwiseExpression = true;

            if (GetRightExpression)
            {
                GetRightExpression = false;
                return  noOperacaoBitwiseXOR.getOperandoDireito().aceitar(this);
            }
            else
            {
                noOperacaoBitwiseXOR.getOperandoEsquerdo().aceitar(this);
                noOperacaoBitwiseXOR.getOperandoDireito().aceitar(this);
            }
            
            return null;
        }

        public object visitarNoOperacaoDivisao(NoOperacaoDivisao noOperacaoDivisao)
        {
            return null;
        }

        public object visitarNoOperacaoLogicaDiferenca(NoOperacaoLogicaDiferenca noOperacaoLogicaDiferenca)
        {
                executa_operacao_condicional(noOperacaoLogicaDiferenca, "SUB");    

            return null;
        }

        public object visitarNoOperacaoLogicaE(NoOperacaoLogicaE noOperacaoLogicaE)
        {
            return null;
        }

        public object visitarNoOperacaoLogicaIgualdade(NoOperacaoLogicaIgualdade noOperacaoLogicaIgualdade)
        {
                executa_operacao_condicional(noOperacaoLogicaIgualdade, "SUB");        
            return null;
        }

        public object visitarNoOperacaoLogicaMaior(NoOperacaoLogicaMaior noOperacaoLogicaMaior)
        {
                executa_operacao_condicional(noOperacaoLogicaMaior, "SUB");   
            return null;
        }

        public object visitarNoOperacaoLogicaMaiorIgual(NoOperacaoLogicaMaiorIgual noOperacaoLogicaMaiorIgual)
        {
                executa_operacao_condicional(noOperacaoLogicaMaiorIgual, "SUB");    
            return null;
        }

        public object visitarNoOperacaoLogicaMenor(NoOperacaoLogicaMenor noOperacaoLogicaMenor)
        {
                executa_operacao_condicional(noOperacaoLogicaMenor, "SUB");  
            return null;
        }

        public object visitarNoOperacaoLogicaMenorIgual(NoOperacaoLogicaMenorIgual noOperacaoLogicaMenorIgual)
        {
                executa_operacao_condicional(noOperacaoLogicaMenorIgual, "SUB");         
            return null;
        }

        public object visitarNoOperacaoLogicaOU(NoOperacaoLogicaOU noOperacaoLogicaOU)
        {
            return null;
        }

        public object visitarNoOperacaoModulo(NoOperacaoModulo noOperacaoModulo)
        {
            return null;
        }

        public object visitarNoOperacaoMultiplicacao(NoOperacaoMultiplicacao noOperacaoMultiplicacao)
        {
            return null;
        }

        public object visitarNoOperacaoSoma(NoOperacaoSoma noOperacaoSoma)
        {
            executa_operacao(noOperacaoSoma, "ADD");
            return null;
        }

        public object visitarNoOperacaoSubtracao(NoOperacaoSubtracao noOperacaoSubtracao)
        {
            executa_operacao(noOperacaoSubtracao, "SUB");
            return null;
        }
    }
}
