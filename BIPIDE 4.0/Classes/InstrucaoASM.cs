using System;
using System.Collections.Generic;
using System.Text;

namespace BIPIDE.Classes
{
    public class InstrucaoASM
    {
        #region
        public InstrucaoASM()
        {
            this.IndexArquivo = intNrInstr;
            intNrInstr++;
            this.intTamanhoVetor = 0;
        }
        #endregion

        #region PRIVATE
        private string INDENTACAO_ASM = "   ";
        private string strInstrucao = "";
        private string strOperando = "";
        private int intEndRotulo = 0;
        private int intIndexMemoria = 0;
        private int intIndexArquivo = 0;
        private eTipo etipoTipo = eTipo.Instrucao;
        private bool blnIsVariavel = false;
        private bool blnIsSection = false;
        private int intTamanhoVetor = 0;
        private static int intNrInstMemoriaDados = 0;
        private static int intNrInstMemoriaProg = 0;
        private static int intNrInstr = 0;

        private int? intNrLinha = 0;
        #endregion

        #region PUBLIC
        public int Tamanho
        {
            get { return this.intTamanhoVetor; }
            set
            {
                this.intTamanhoVetor = value;
                //reserva posições de memória conforme o tamanho do vetor
                intNrInstMemoriaDados += value - 1;
            }
        }
        public string Instrucao
        {
            get { return this.strInstrucao; }
            set { this.strInstrucao = value.ToUpper(); }
        }
        public int? NrLinha
        {
            get { return this.intNrLinha; }
            set { this.intNrLinha = value; }
        }
        public string Operando
        {
            get { return this.strOperando; }
            set { this.strOperando = value; }
        }
        public int IndexMemoria
        {
            get { return this.intIndexMemoria; }
            set { this.intIndexMemoria = value; }
        }
        public int IndexArquivo
        {
            get { return this.intIndexArquivo; }
            set { this.intIndexArquivo = value; }
        }
        public bool IsDesvio
        {
            get
            {
                return ((this.Instrucao == "BEQ") ||
                        (this.Instrucao == "BNE") ||
                        (this.Instrucao == "BGE") ||
                        (this.Instrucao == "BGT") ||
                        (this.Instrucao == "BLE") ||
                        (this.Instrucao == "BLT") ||
                        (this.Instrucao == "JMP"));
            }
        }
        public bool isLogica
        {
            get
            {
                return ((this.Instrucao == "AND")  ||
                        (this.Instrucao == "OR")   ||
                        (this.Instrucao == "XOR")  ||
                        (this.Instrucao == "ANDI") ||
                        (this.Instrucao == "ORI")  ||
                        (this.Instrucao == "XORI") ||
                        (this.Instrucao == "NOT")  ||
                        (this.Instrucao == "SLL")  ||
                        (this.Instrucao == "SLR"));
            }
        }
        public bool isSuporteProcedimento
        {
            get
            {
                return ((this.Instrucao == "CALL") ||
                        (this.Instrucao == "RETURN"));
            }
        }
        public bool isES
        {
            get
            {
                return ((this.Operando == "$in_port") ||
                        (this.Operando == "$out_port"));
            }
        }

        public bool isVetor
        {
            get
            {
                return ((this.Instrucao == "LDV") ||
                        (this.Instrucao == "STOV"));
            }
        }
        /// <summary>
        /// Utilizado para instrucoes de desvios para indicar o endereco de memoria dos rotulos
        /// </summary>
        public int EndRotuloVariavel
        {
            get { return this.intEndRotulo; }
            set { this.intEndRotulo = value; }
        }
        public eTipo Tipo
        {
            get { return this.etipoTipo; }
            set
            {
                this.etipoTipo = value;
                if (this.intIndexMemoria == 0)
                {
                    if (value == eTipo.Instrucao)
                    {
                        this.intIndexMemoria = intNrInstMemoriaProg;
                        intNrInstMemoriaProg++;
                    }
                    else
                        if (value == eTipo.Variavel)
                        {
                            this.intIndexMemoria = intNrInstMemoriaDados;
                            intNrInstMemoriaDados++;
                        }
                        else
                            if (value == eTipo.Rotulo)
                            {
                                this.intIndexMemoria = intNrInstMemoriaProg;
                            }
                }
            }
        }
        #endregion

        #region METODOS
        public string GetInstrucaoASM()
        {
            if (this.Tipo == eTipo.Rotulo)
            {
                return this.Instrucao + ":";
            }
            else
                if (this.Tipo == eTipo.Variavel)
                {
                    return INDENTACAO_ASM + this.strInstrucao.ToLower() + " : " + this.strOperando;
                }
                else
                    if (this.Tipo == eTipo.Section)
                    {
                        return this.strInstrucao.ToLower();
                    }
                    else
                    {
                        string strEspacoIdentacaoOperando = "";
                        for (int i = this.strInstrucao.Length; i < 8; i++)
                        {
                            strEspacoIdentacaoOperando += " ";
                        }
                        return INDENTACAO_ASM + this.strInstrucao + strEspacoIdentacaoOperando + this.strOperando;
                    }
        }

        public static void Init()
        {
            intNrInstMemoriaDados = 0;
            intNrInstMemoriaProg = 0;
            intNrInstr = 0;
        }
        #endregion
    }
    public enum eTipo
    {
        Instrucao,
        Rotulo,
        Variavel,
        Section
    }
}
