using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0
{
    class Instrucoes
    {
        public enum eBipI
        {
            HLT,
            STO,
            LD,
            LDI,
            ADD,
            ADDI,
            SUB,
            SUBI
        }
        public enum eBipII
        {
            BEQ,
            BNE,
            BGT,
            BGE,
            BLT,
            BLE,
            JMP
        }
        public enum eBipIII
        {
            NOT,
            AND,
            ANDI,
            OR,
            ORI,
            XOR,
            XORI,
            SLL,
            SRL
        }
        public enum eBipIV
        {
            STOV,
            LDV,
            RETURN,
            CALL
        }
    }
}
