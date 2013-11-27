using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    public static class Arquitetura
    {
        /// <summary>Numero de Bits do Campo do Operando</summary>
        public static int BitsCampoOperando { get { return 11; } }
        /// <summary>Numero de Bits do Campo do OpCode</summary>
        public static int BitsCampoOpCode { get { return 5; } }
        /// <summary>Primeira posição de memória da memoria de dados. Caso queira deixar um espaço no inicio. Padrão 0</summary>
        public static int PrimeiraPosicaoMemDados { get { return 0; } }
        /// <summary>Numero de palavras para cada variavel. Padrão é 1, muda-se para 2 caso queira compatibilidade com o codigo gerado pelo ArchC</summary>
        public static int QtdWordVariavel { get { return 2; } }
        /// <summary>
        /// Retorna o endereco de um registrador
        /// </summary>
        /// <returns></returns>
        public static int GetEndRegistrador(string strReg)
        {
            strReg = strReg.Replace("$", "").ToLower();
            if (strReg == "port0_dir")  return 1024;         //Configuracao dos pinos de E/S
            if (strReg == "port0_data") return 1025;         //Valores dos pinos de E/S
            if (strReg == "port1_dir")  return 1026;         //Configuracao dos pinos de E/S
            if (strReg == "port1_data") return 1027;         //Valores dos pinos de E/S
            if (strReg == "tmr0_config")return 1040;         //Configuracao do Preecaler do Timer0
            if (strReg == "tmr0_value") return 1041;         //Valor do contador do Timer0	 
            if (strReg == "int_config") return 1056;         //Configuracao das Interrupcoes
            if (strReg == "int_status") return 1057;         //Indica quais interrupcoes estao ativas	 
            if (strReg == "mcu_config") return 1072;         //Opcoes
            if (strReg == "indr")       return 1073;		 //Auxiliar para Vetores
            if (strReg == "uart_data") return 1088;		 //Auxiliar para Vetores
            if (strReg == "uart_status") return 1089;		 //Auxiliar para Vetores
            if (strReg == "in_port") return 1024; //endereço de E/S BIP IV
            if (strReg == "out_port") return 1025;//endereço de E/S BIP IV
            return 0;
        }
        public static eClasseInstrucao GetClasseInstrucao(eInstrucao instrucao)
        {
            switch (instrucao)
            {
                case eInstrucao.STO:
                    return eClasseInstrucao.Armazenamento;
                case eInstrucao.LD:
                case eInstrucao.LDI:
                    return eClasseInstrucao.Carga;
                case eInstrucao.ADD:
                case eInstrucao.SUB:
                case eInstrucao.ADDI:
                case eInstrucao.SUBI:
                    return eClasseInstrucao.Aritmetica;
                case eInstrucao.AND:
                case eInstrucao.OR:
                case eInstrucao.XOR:
                case eInstrucao.ANDI:
                case eInstrucao.ORI:
                case eInstrucao.XORI:
                case eInstrucao.NOT:
                    return eClasseInstrucao.Logica_Booleana;
                case eInstrucao.HLT:
                    return eClasseInstrucao.Controle;
                case eInstrucao.BEQ:
                case eInstrucao.BNE:
                case eInstrucao.BGE:
                case eInstrucao.BGT:
                case eInstrucao.BLE:
                case eInstrucao.BLT:
                    return eClasseInstrucao.Desvio_Condicional;
                case eInstrucao.JMP:
                    return eClasseInstrucao.Desvio_Incondicional;
                case eInstrucao.SLL:
                case eInstrucao.SRL:
                    return eClasseInstrucao.Deslocamento_Logico;
                case eInstrucao.LDV:
                case eInstrucao.STOV:
                    return eClasseInstrucao.Manipulacao_de_Vetor;
                case eInstrucao.RETURN:
                case eInstrucao.RETINT:
                case eInstrucao.CALL:
                    return eClasseInstrucao.Suporte_a_Procedimentos;
                default:
                    return eClasseInstrucao.NaoDefinida;
            }
        }
        //=====================================================
        public static eTipoOperando GetTipoOperando(eInstrucao instrucao)
        {
            switch (instrucao)
            {
                case eInstrucao.HLT:
                case eInstrucao.NOT:
                case eInstrucao.RETURN:
                case eInstrucao.RETINT:
                    return eTipoOperando.SemOperando;
                case eInstrucao.STO:
                case eInstrucao.LD:
                case eInstrucao.ADD:
                case eInstrucao.SUB:
                case eInstrucao.AND:
                case eInstrucao.OR:
                case eInstrucao.XOR:
                case eInstrucao.STOV:
                case eInstrucao.LDV:
                    return eTipoOperando.EndMemoriaDados;
                case eInstrucao.BEQ:
                case eInstrucao.BNE:
                case eInstrucao.BGT:
                case eInstrucao.BGE:
                case eInstrucao.BLT:
                case eInstrucao.BLE:
                case eInstrucao.JMP:
                case eInstrucao.CALL:
                    return eTipoOperando.EndMemoriaInstrucao;
                case eInstrucao.LDI:
                case eInstrucao.ADDI:
                case eInstrucao.SUBI:
                case eInstrucao.ANDI:
                case eInstrucao.ORI:
                case eInstrucao.XORI:
                case eInstrucao.SLL:
                case eInstrucao.SRL:
                    return eTipoOperando.Imediato;
                default:
                    return eTipoOperando.NaoDefinido;
            }
        }

    }
    //=====================================================
    /// <summary>
    /// Lista de Instruções
    /// </summary>
    public enum eInstrucao
    {
        HLT = 0, //INSTRUCAO = OPCODE
        STO = 1,
        LD = 2,
        LDI = 3,
        ADD = 4,
        ADDI = 5,
        SUB = 6,
        SUBI = 7,
        BEQ = 8,
        BNE = 9,
        BGT = 10,
        BGE = 11,
        BLT = 12,
        BLE = 13,
        JMP = 14,
        NOT = 15,
        AND = 16,
        ANDI = 17,
        OR = 18,
        ORI = 19,
        XOR = 20,
        XORI = 21,
        SLL = 22,
        SRL = 23,
        STOV = 24,
        LDV = 25,
        RETURN = 26,
        RETINT = 27,
        CALL = 28,

        Rotulo = 100
    }
    public enum eArquitetura
    {
        BIP_I,
        BIP_II,
        BIP_III,
        BIP_IV,
        uBIP
    }
    public enum eClasseInstrucao
    {
        NaoDefinida,
        Armazenamento,
        Carga,
        Aritmetica,
        Logica_Booleana,
        Controle,
        Desvio_Condicional,
        Desvio_Incondicional,
        Deslocamento_Logico,
        Manipulacao_de_Vetor,
        Suporte_a_Procedimentos
    }
    public enum eTipoOperando
    {
        EndMemoriaDados,
        EndMemoriaInstrucao,
        Imediato,
        SemOperando,
        NaoDefinido
    }
}
