using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    /// <summary>
    /// Representa um programa ASM
    /// </summary>
    public class Programa
    {
        //=====================================================
        public Programa()
        {
            Instrucoes = new List<InstrASM>();
            Variaveis = new List<Variavel>();
        }
        //=====================================================
        public List<Variavel> Variaveis;
        //=====================================================
        public List<InstrASM> Instrucoes;
        //=====================================================
        public InstrASM UltimaInstrucao
        {
            get
            {
                if (Instrucoes.Count > 0)
                    return Instrucoes.Last();
                else
                    return null;
            }
            set
            {
                if (Instrucoes.Count > 0)
                    Instrucoes[Instrucoes.Count - 1] = value;
            }
        }
        //=====================================================
    }
}
