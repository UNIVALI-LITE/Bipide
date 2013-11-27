using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public class Base
    {
        //--------------------------------------------------------------
        ///<summary>Posição da Instrução no Arquivo</summary>
        public int EndArquivo { get { return _intEndArquivo; } set { this._intEndArquivo = value; } }
        protected int _intEndArquivo = 0;
        //--------------------------------------------------------------
        ///<summary>Utilizado para gerar a sequencia de endereco de Memoria de dados</summary>
        protected static int EndGeralArquivo = 0;
        //--------------------------------------------------------------
        protected void SetEndArquivo()
        {
            #region DEFINE O ENDERECO DA INSTRUCAO NO ARQUIVO
            if (this._intEndArquivo == 0)//Se ainda não foi definido
            {
                this._intEndArquivo = ++EndGeralArquivo;
            }
            #endregion
        }
        //--------------------------------------------------------------
        ///<summary>Posição da Instrução na Memória de Dados/Instrução</summary>
        public int EndMemoria { get { return _intEndMemoria; } set { this._intEndMemoria = value; } }
        protected int _intEndMemoria = 0;
        //--------------------------------------------------------------
        ///<summary>Apenas indica se o endereco de memora desta instrucao já foi definido</summary>
        protected bool _blnEndMemJaDefinido = false;
        //--------------------------------------------------------------
        ///<summary>Operando da Instrução. Ex: 4, x...</summary>
        public string Operando { get { return _strOperando; } set { _strOperando = value; } }
        protected string _strOperando = "";
        //--------------------------------------------------------------
        ///<summary>Usuado na geracao de arquivos</summary>
        protected string strEspaco = "   ";

    }
}
