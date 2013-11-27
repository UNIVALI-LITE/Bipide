using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public class ItemVetor
    {
        //--------------------------------------------------------------
        ///<summary>Operando da Instrução. Ex: 4, x...</summary>
        public string Operando { get { return _strOperando; } set { _strOperando = value; } }
        private string _strOperando = "";
        //--------------------------------------------------------------
        ///<summary>Tipo do Operando no arquivo. Ex: Int, Hex...</summary>
        public eOperando TipoOperando { get { return _tipoOperando; } set { _tipoOperando = value; } }
        private eOperando _tipoOperando = eOperando.NaoDefinido;
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
        /// <summary>
        /// Retorna o valor do Operando como inteiro
        /// </summary>
        /// <returns></returns>
        public int GetOperandoToInt()
        {
                if (this.TipoOperando == eOperando.INT)
                    return int.Parse(this.Operando);
                else
                    if (this.TipoOperando == eOperando.HEX)
                        return Funcoes.HexToInt(this.Operando);
                    else
                        if (this.TipoOperando == eOperando.BIN)
                            return Funcoes.BinToInt(this.Operando, true);
                        else
                            if (this.TipoOperando == eOperando.CHAR)
                                return (int)(this.Operando.Replace("'", "")[0]);

                return 0;
        }
        //--------------------------------------------------------------
        #region GERA SAIDAS
        /// <summary>
        /// Converte a instrução atual no formato Hexadecimal
        /// </summary>
        /// <returns></returns>
        public string ToHex()
        {
            return Funcoes.IntToHex(Funcoes.BinToInt(this.ToBin(), true), 4);
        }
        /// <summary>
        /// Converte a Instrução atual para Binário
        /// </summary>
        /// <returns></returns>
        public string ToBin()
        {
            return Funcoes.IntToBin(this.GetOperandoToInt(), BIP.Montador.Arquitetura.BitsCampoOpCode+BIP.Montador.Arquitetura.BitsCampoOperando);
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
            return this.Operando;
        }
        #endregion
    }
}
