using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public class Erro
    {
        public int Linha { get; set; }
        public string Mensagem { get; set; }
        public enum eTipo { Error, Warning };
        public eTipo Tipo { get; set; }
        public Erro(int _linha, string _msg, eTipo _tipo)
        {
            Linha = _linha;
            Mensagem = _msg;
            Tipo = _tipo;
        }
        public Erro()
        {
            Linha = 0;
            Mensagem = "";
            Tipo = eTipo.Error;
        }
    }
}
