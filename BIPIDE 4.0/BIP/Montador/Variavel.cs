using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public class Variavel : Base
    {
        /// <summary>
        /// Construtor, setar enderecos de memoria e arquivo
        /// </summary>
        public Variavel()
        {
            //O endereco final da variavel é redefinido dentro do método CheckVariaveis na classe Montador
            _intEndMemoria = EndGeralMemDados++;
            _blnEndMemJaDefinido = true;
            this.SetEndArquivo();
        }
        //--------------------------------------------------------------
        ///<summary>Operando da Instrução. Ex: 4, x...</summary>
        public string Nome { get { return _strNome; } set { _strNome = value; } }
        private string _strNome = "";
        //--------------------------------------------------------------
        ///<summary>Tamanho da Variavel. Usado para variaveis do tipo vetor</summary>
        public int TamanhoVariavel { get { return Vetor.Count; } }
        //--------------------------------------------------------------
        ///<summary>Usado quando o operando é um vetor</summary>
        public List<ItemVetor> Vetor = new List<ItemVetor>();
        //--------------------------------------------------------------
        ///<summary>Utilizado para gerar a sequencia de endereco de Memoria de dados</summary>
        private static int EndGeralMemDados = Arquitetura.PrimeiraPosicaoMemDados;
        /// <summary>
        /// Inicializa variaveis globais
        /// </summary>
        public static void StartNewAnaliser()
        {
            EndGeralArquivo = 0;
            EndGeralMemDados = Arquitetura.PrimeiraPosicaoMemDados;
        }
        //--------------------------------------------------------------
        #region GERA SAIDAS
        /// <summary>
        /// Converte a Instrução atual no formato VHDL
        /// </summary>
        /// <returns></returns>
        public List<string> ToVHDL()
        {
            List<string> lstSaida = new List<string>();
            foreach (ItemVetor item in Vetor)
            {
                lstSaida.Add("X\"" + item.ToHex() + "\"");
            }
            return lstSaida;
        }
        /// <summary>
        /// Converte a instrução para formato MIF (Memory Initialization File)
        /// </summary>
        /// <returns></returns>
        public List<string> ToMif(bool blnIncludeSourceAsComment)
        {
            List<string> lstSaida = new List<string>();
            int iCount = 0;
            foreach (ItemVetor item in Vetor)
            {
                string strLine = Funcoes.IntToHex(this.EndMemoria+iCount, 4) + ":" + item.ToHex() + ";";
                if (blnIncludeSourceAsComment)
                {
                    if (this.Vetor.Count > 1)
                        strLine += " -- " + strEspaco + this.Nome + "[" + iCount.ToString() + "] : " + item.Operando;
                    else
                        strLine += " -- " + strEspaco + this.Nome + " : " + this.Operando;
                }
                lstSaida.Add(strLine);
                iCount += Arquitetura.QtdWordVariavel;
            }
            return lstSaida;
        }
        /// <summary>
        /// Converte a instrução atual no formato Hexadecimal
        /// </summary>
        /// <returns></returns>
        public List<string> ToHex(bool blnIncluirAddr, bool blnIncludeSourceAsComment)
        {
            List<string> lstSaida = new List<string>();
            int iCount = 0;
            foreach (ItemVetor item in Vetor)
            {
                if (blnIncluirAddr)
                {
                    string strLine = Funcoes.IntToHex(this.EndMemoria + iCount, 4) + ":" + item.ToHex();
                    if (blnIncludeSourceAsComment)
                    {
                        if (this.Vetor.Count > 1)
                            strLine += " -- " + strEspaco + this.Nome + "[" + iCount.ToString() + "] : " + item.Operando;
                        else
                            strLine += " -- " + strEspaco + this.Nome + " : " + this.Operando;
                    }
                    lstSaida.Add(strLine);
                    iCount += Arquitetura.QtdWordVariavel;
                }
                else
                    lstSaida.Add(item.ToHex());
            }
            return lstSaida;
        }
        /// <summary>
        /// Converte a Instrução atual para Binário
        /// </summary>
        /// <returns></returns>
        public List<string> ToBin(bool blnIncluirAddr, bool blnIncludeSourceAsComment)
        {
            List<string> lstSaida = new List<string>();
            int iCount = 0;
            foreach (ItemVetor item in Vetor)
            {
                if (blnIncluirAddr)
                {
                    string strLine = Funcoes.IntToHex(this.EndMemoria + iCount, 4) + ":" + item.ToBin();
                    if (blnIncludeSourceAsComment)
                    {
                        if (this.Vetor.Count > 1)
                            strLine += " -- " + strEspaco + this.Nome + "[" + iCount.ToString() + "] : " + item.Operando;
                        else
                            strLine += " -- " + strEspaco + this.Nome + " : " + this.Operando;
                    }
                    lstSaida.Add(strLine);
                    iCount += Arquitetura.QtdWordVariavel;
                }
                else
                    lstSaida.Add(item.ToBin());
            }
            return lstSaida;
        }
        /// <summary>
        /// Converte a Instrução Atual para Inteiro
        /// </summary>
        /// <returns></returns>
        public List<int> ToInt()
        {
            List<int> lstSaida = new List<int>();
            foreach (ItemVetor item in Vetor)
            {
                lstSaida.Add(item.ToInt());
            }
            return lstSaida;
        }
        /// <summary>
        /// Converte a instrução para string conforme entrada.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return strEspaco + this.Nome + strEspaco + ": " + this.Operando;
        }
        #endregion
    }
}
