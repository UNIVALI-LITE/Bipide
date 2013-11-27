using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    //=====================================================
    /// <summary>
    /// Representa uma instrução assembly
    /// </summary>
    public class InstrASM : Base
    {
        #region PROPRIEDADES
        //--------------------------------------------------------------
        ///<summary>Tipo da Instrução. Ex: ADD ou Variavel</summary>
        public eInstrucao Instrucao
        {
            get
            {
                return _instucao;
            }
            set
            {
                _instucao = value;

                this.SetEndArquivo();

                #region DEFINE O ENDERECO DA INSTRUCAO NA MEMORIA
                if (!this._blnEndMemJaDefinido) //Se ainda não foi definido
                {
                    if (value == eInstrucao.Rotulo)
                    {
                        _intEndMemoria = EndGeralMemInstr;
                        _blnEndMemJaDefinido = true;
                    }
                    else
                        {
                            _intEndMemoria = EndGeralMemInstr++;
                            _blnEndMemJaDefinido = true;
                        }
                }
                #endregion
            }
        }
        private eInstrucao _instucao;
        //--------------------------------------------------------------
        ///<summary>Tipo do Operando no arquivo. Ex: Int, Hex...</summary>
        public eOperando TipoOperando { get { return _tipoOperando; } set { _tipoOperando = value; } }
        private eOperando _tipoOperando = eOperando.NaoDefinido;
        //--------------------------------------------------------------
        ///<summary>Armazena o nome da Section, Variavel ou Rotulo. Ex: __FIMSE, x, data</summary>
        public string Auxiliar { get { return _strAuxiliar; } set { _strAuxiliar = value; } }
        private string _strAuxiliar = "";
        //--------------------------------------------------------------
        ///<summary>Armazena o endereço da variavel se operando for VAR ou endereco do Rotulo</summary>
        public int EndRotVar { get { return _intEndRotVar; } set { _intEndRotVar = value; } }
        private int _intEndRotVar = 0;
        //--------------------------------------------------------------
        ///<summary>Utilizado para gerar a sequencia de endereco de Memoria de Instrucao</summary>
        private static int EndGeralMemInstr = 0;
        //--------------------------------------------------------------
        #endregion
        //=====================================================
        #region METODOS
        /// <summary>
        /// Seta o enum de instrucao pelo parametro string
        /// </summary>
        /// <param name="strInstr">String contendo o mneumonico da instrucao</param>
        public bool SetInstrucaoByString(string strInstr)
        {
            try
            {
                this.Instrucao = (eInstrucao)Enum.Parse(typeof(eInstrucao), strInstr, true);
                return true;
            }
            catch { return false; }
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Seta o enum do tipo do operando pelo parametro string
        /// </summary>
        /// <param name="strTipo">String contento o tipo do operando</param>
        public bool SetTipoOperandoByString(string strTipo)
        {
            try
            {
                this.TipoOperando = (eOperando)Enum.Parse(typeof(eOperando), strTipo, true);
                return true;
            }
            catch { return false; }
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Retorna a Classe de Instrução desta.
        /// </summary>
        /// <returns></returns>
        public eClasseInstrucao GetClasseInstrucao()
        {
            return Arquitetura.GetClasseInstrucao(this.Instrucao);
        }
        #region GERA SAIDAS
        //--------------------------------------------------------------
        /// <summary>
        /// Converte a Instrução atual no formato VHDL
        /// </summary>
        /// <returns></returns>
        public string ToVHDL()
        {
            string strHex = this.ToHex(false);
            if (strHex != "")
                return "X\"" + strHex + "\"";
            else return "";
        }
        /// <summary>
        /// Converte a instrução para formato MIF (Memory Initialization File)
        /// </summary>
        /// <returns></returns>
        public string ToMif()
        {
            return this.ToHex(true) + ";";
        }
        /// <summary>
        /// Converte a instrução atual no formato Hexadecimal
        /// </summary>
        /// <returns></returns>
        public string ToHex(bool blnIncluirEnd)
        {
            if (blnIncluirEnd)
                return Funcoes.IntToHex(this.EndMemoria, 4) + ":" + Funcoes.IntToHex(Funcoes.BinToInt(this.ToBin(), false), 4);
            else
                return Funcoes.IntToHex(Funcoes.BinToInt(this.ToBin(), false), 4);
        }
        /// <summary>
        /// Converte a Instrução atual para Binário
        /// </summary>
        /// <returns></returns>
        public string ToBin()
        {
            if (this.Instrucao != eInstrucao.Rotulo)
            {
                if ((this.TipoOperando == eOperando.ROTULO) || (this.TipoOperando == eOperando.VAR))
                    return Funcoes.IntToBin(this.GetOpCode(), BIP.Montador.Arquitetura.BitsCampoOpCode) + Funcoes.IntToBin(this.EndRotVar, BIP.Montador.Arquitetura.BitsCampoOperando);
                else
                    return Funcoes.IntToBin(this.GetOpCode(), BIP.Montador.Arquitetura.BitsCampoOpCode) + Funcoes.IntToBin(this.GetOperandoToInt(), BIP.Montador.Arquitetura.BitsCampoOperando);
            }
            return "";
        }
        /// <summary>
        /// Converte a Instrução Atual para Inteiro
        /// </summary>
        /// <returns></returns>
        public int ToInt()
        {
            string strBinario = this.ToBin();
            if (strBinario != "")
                return Funcoes.BinToInt(strBinario, true);
            return 0;
        }
        /// <summary>
        /// Converte a instrução para string conforme entrada.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Instrucao == eInstrucao.Rotulo)
                return this.Auxiliar + ":";

            string strInstr = this.Instrucao.ToString();
            while (strInstr.Length < 4)
                strInstr = strInstr + " ";

            return strEspaco + strInstr + strEspaco + this.Operando;
        }
        #endregion
        /// <summary>
        /// Retorna o valor do Operando como inteiro
        /// </summary>
        /// <returns></returns>
        public int GetOperandoToInt()
        {
            if (this.Instrucao != eInstrucao.Rotulo)
            {
                if (this.TipoOperando == eOperando.INT)
                    return int.Parse(this.Operando);
                else
                    if (this.TipoOperando == eOperando.HEX)
                        return Funcoes.HexToInt(this.Operando);
                    else
                        if (this.TipoOperando == eOperando.CIF)
                            return int.Parse(this.Operando.Replace("$", ""));
                        else
                            if (this.TipoOperando == eOperando.BIN)
                                return Funcoes.BinToInt(this.Operando, true);
                            else
                                if (this.TipoOperando == eOperando.REG)
                                    return Arquitetura.GetEndRegistrador(this.Operando.ToLower());
                                else
                                    if (this.TipoOperando == eOperando.CHAR)
                                        return (int)(this.Operando.Replace("'", "")[0]);
            }
            return 0;
        }
        /// <summary>
        /// Retorna o código do OpCode da instrução
        /// </summary>
        /// <returns></returns>
        public int GetOpCode()
        {
            return (int)this.Instrucao;
        }

        public static void StartNewAnaliser()
        {
            EndGeralArquivo = 0;
            EndGeralMemInstr = 0;
        }
        #endregion

    }
    //=====================================================
    public enum eOperando
    {
        INT, //Numero Inteiro
        HEX, //Numero em hexadecimal com prefixo 0x
        BIN, //Numero Binário com prefixo 0b
        VAR, //Uma variavel (texto)
        CIF, //Numero com prefix $
        REG, //Nome registrador com prefixo $
        ROTULO, //Um texto referente a um rótulo
        VETOR, //Usado em inicialização de variaveis
        CHAR, //Caracter
        NaoDefinido
    }
}
