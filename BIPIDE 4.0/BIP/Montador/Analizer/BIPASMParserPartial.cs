using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using System.Collections;
namespace BIP.Montador.Analizer
{
    public partial class BIPASMParser : Parser
    {
        #region PROPRIEDADES
        /// <summary>Armazena o programa convertido e analisado</summary>
        public BIP.Montador.Programa Programa = new Programa();
        /// <summary>Armazena o tipo do operando da instrucao processada</summary>
        private string strTipoOperando = "";
        /// <summary>Utilizado no Gerenciamento de Erros</summary>
        public Stack paraphrases = new Stack();
        ///<summary>Usado quando o operando é um vetor</summary>
        private List<ItemVetor> lstVetor = new List<ItemVetor>();
        #endregion
        //=================================================================
        #region TRATAMENTO DE ERROS

        public delegate void HandlerMsgRequest(string _msg, int _linha);
        public event HandlerMsgRequest MessageRequest;

        public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
        {
            this.ShowError(base.GetErrorMessage(e, tokenNames), e.Line);
            return "";
        }
        public void ShowError(string msg, int line)
        {
            msg = msg.Replace("extraneous input", "valor encontrado");
            msg = msg.Replace("expecting", "valor esperado");
            msg = msg.Replace("missing ", "valor esperado ");
            msg = msg.Replace("at ", "valor encontrado ");
            if (paraphrases.Count > 0)
            {
                String paraphrase = (String)paraphrases.Peek();
                msg = /*paraphrase +*/ "Erro na linha " + line.ToString() + ": " + msg;
            }
            if (this.MessageRequest != null)
                this.MessageRequest(msg, line);

            //this.AppendErro(msg);

            //return msg;
        }

        #endregion
        //=================================================================
        /// <summary>
        /// Adiciona uma instrucao ao programa. Obs. o tipo da instrucao é recuperado por uma propriedade desta classe.
        /// </summary>
        /// <param name="strInst">Mneumonico da Instrucao</param>
        /// <param name="strOperando">Operando da Instrucao</param>
        public void AddInstrucao(string strInst, string strOperando)
        {
            //Escreve o resultado no console
            System.Console.WriteLine("Instrucao: " + strInst + "  " + strOperando + "   " + strTipoOperando);
            //Cria uma nova instrucao
            InstrASM instrucao = new InstrASM();
            instrucao.SetInstrucaoByString(strInst);
            instrucao.Operando = strOperando;
            instrucao.SetTipoOperandoByString(strTipoOperando);
            //Adiciona ao programa
            this.Programa.Instrucoes.Add(instrucao);
            //Limpa o tipo de operando
            strTipoOperando = "";
        }
        //=================================================================
        /// <summary>
        /// Adiciona uma variavel da secao .data ao programa.
        /// </summary>
        /// <param name="strVar">Nome da variavel</param>
        /// <param name="strValor">Valor inicial</param>
        public void AddVariavel(string strVar, string strValor)
        {
            if (strValor == "") strValor = "0";

            //Escreve o resultado no console
            System.Console.WriteLine("Variavel: " + strVar + "  " + strValor + "   " + strTipoOperando);
            //Cria uma nova variavel
            Variavel objVariavel = new Variavel();
            objVariavel.Nome = strVar;
            objVariavel.Operando = strValor;

            if (lstVetor.Count == 0)
            {
                this.AddItemVetor(strValor);
            }
            objVariavel.Vetor = lstVetor;

            //Nao pode dar .Clear() por causa da referencia.
            LimpaVetor();
            //Adiciona ao programa
            this.Programa.Variaveis.Add(objVariavel);
            //Limpa o tipo de operando
            strTipoOperando = "";
        }
        //=================================================================
        /// <summary>
        /// Adiciona um rotulo ao programa
        /// </summary>
        /// <param name="strRotulo">Nome do Rotulo</param>
        public void AddRotulo(string strRotulo)
        {
            //Escreve o resultado no console
            System.Console.WriteLine("Rotulo: " + strRotulo );
            //Cria uma nova instrucao
            InstrASM instrucao = new InstrASM();
            instrucao.Instrucao = eInstrucao.Rotulo;
            instrucao.TipoOperando = eOperando.NaoDefinido;
            instrucao.Operando = "";
            instrucao.Auxiliar = strRotulo;

            //Adiciona ao programa
            this.Programa.Instrucoes.Add(instrucao);
            //Limpa o tipo de operando
            strTipoOperando = "";
        }
        //=================================================================
        public void AddItemVetor(string strValor)
        {
            ItemVetor obj = new ItemVetor();
            obj.Operando = strValor;
            obj.SetTipoOperandoByString(strTipoOperando);
            lstVetor.Add(obj);           
        }
        //=================================================================
        public void LimpaVetor()
        {
            this.lstVetor = new List<ItemVetor>();
        }
    }
}