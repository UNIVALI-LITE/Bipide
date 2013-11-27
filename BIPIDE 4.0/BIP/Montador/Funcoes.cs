using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public static class Funcoes
    {
        /// <summary>
        /// Converte um numero binário em um numero inteiro
        /// </summary>
        /// <param name="strBin">String em binario, pode estar com prefixo 0b</param>
        /// <returns></returns>
        public static int BinToInt(string strBin, bool blnExtenderSinal)
        {
            if (strBin == "") return 0;
            while (strBin.Length < 16)
                if (blnExtenderSinal)
                    strBin = strBin[0] + strBin;
                else
                    strBin = '0' + strBin;

            long l = Convert.ToInt16(strBin.ToUpper().Replace("0B", ""), 2);
            int i = (int)l;
            return i;
        }
        /// <summary>
        /// Converte um numero inteiro para um binário de N bits
        /// </summary>
        /// <param name="intValor">Valor inteiro a ser convertido</param>
        /// <param name="intBits">Numero do bits do binario resultante</param>
        /// <returns></returns>
        public static string IntToBin(int intValor, int intBits)
        {
            string strBin = Convert.ToString(intValor, 2);

            if (strBin.Length > intBits)
                return strBin.Substring(strBin.Length - intBits);

            while (strBin.Length < intBits)
                strBin = '0' + strBin;
            return strBin;
        }
        /// <summary>
        /// Converte um numero inteiro para Hexadecimal
        /// </summary>
        /// <param name="intValor">Numero Inteiro</param>
        /// <param name="intValor">Numero de Digitos</param> 
        /// <returns></returns>
        public static string IntToHex(int intValor, int intCount)
        {
            string strHex = Convert.ToString(intValor, 16);
            if (strHex.Length > intCount)
                return strHex.Substring(strHex.Length - intCount);

            while (strHex.Length < intCount)
                strHex = '0' + strHex;
            return strHex.ToUpper();
        }
        /// <summary>
        /// Converte uma string hexadecimal para inteiro. Pode conter o prefixo 0x
        /// </summary>
        /// <param name="strHexa"></param>
        /// <returns></returns>
        public static int HexToInt(string strHexa)
        {
            return int.Parse(strHexa.ToUpper().Replace("0X", ""), System.Globalization.NumberStyles.HexNumber);
        }
    }
}
