using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BIPIDE_4._0.ControlResources
{
    public class TokenParser
    {
        private readonly Dictionary<Tokens, string> _tokens;
        private readonly Dictionary<Tokens, MatchCollection> _regExMatchCollection;
        private string _inputString;
        private int _index;

        public enum Tokens
        {
            UNDEFINED = 0,
            INCLUDE = 1,
            INCLUDEFILE_STR = 2,
            INCLUDEFILE = 3,
            DEFINE = 4,
            ASM = 5,
            ENDASM = 6,
            REGISTER_PORT0_DIR = 7,
            REGISTER_PORT0_DATA = 8,
            REGISTER_PORT1_DIR = 9,
            REGISTER_PORT1_DATA = 10,
            REGISTER_TMR0_CONFIG = 11,
            REGISTER_TMR0_VALUE = 12,
            REGISTER_INT_CONFIG = 13,
            REGISTER_INT_STATUS = 14,
            REGISTER_MCU_CONFIG = 15,
            REGISTER_INDR = 16,
            REGISTER_STATUS = 17,
            INSTRUCTION_HLT = 18,
            INSTRUCTION_STOV = 19,
            INSTRUCTION_STO = 20,
            INSTRUCTION_LDV = 21,
            INSTRUCTION_LDI = 22,
            INSTRUCTION_LD = 23,
            INSTRUCTION_ADDI = 24,
            INSTRUCTION_ADD = 25,
            INSTRUCTION_SUBI = 26,
            INSTRUCTION_SUB = 27,
            INSTRUCTION_BEQ = 28,
            INSTRUCTION_BNE = 29,
            INSTRUCTION_BGT = 30,
            INSTRUCTION_BGE = 31,
            INSTRUCTION_BLT = 32,
            INSTRUCTION_BLE = 33,
            INSTRUCTION_JMP = 34,
            INSTRUCTION_NOT = 35,
            INSTRUCTION_ANDI = 36,
            INSTRUCTION_AND = 37,
            INSTRUCTION_ORI = 38,
            INSTRUCTION_OR = 39,
            INSTRUCTION_XORI = 40,
            INSTRUCTION_XOR = 41,
            INSTRUCTION_SLL = 42,
            INSTRUCTION_SRL = 43,
            INSTRUCTION_RETURN = 44,
            INSTRUCTION_RETINT = 45,
            INSTRUCTION_CALL = 46,
            WHITESPACE = 47,
            NEWLINE = 48,
            NUMBER = 49,
            IDENTIFIER = 50
        }

        public string InputString
        {
            set
            {
                _inputString = value;
                PrepareRegex();
            }
        }

        public TokenParser()
        {
            _tokens = new Dictionary<Tokens, string>();
            _regExMatchCollection = new Dictionary<Tokens, MatchCollection>();
            _index = 0;
            _inputString = string.Empty;

            _tokens.Add(Tokens.INCLUDE, "#[Ii][Nn][Cc][Ll][Uu][Dd][Ee]");
            _tokens.Add(Tokens.INCLUDEFILE_STR, "\"[a-zA-Z_][a-zA-Z0-9_]*?\\.c\"");
            _tokens.Add(Tokens.INCLUDEFILE, "\\<.[a-zA-Z_][a-zA-Z0-9_]*\\.h\\>");
            _tokens.Add(Tokens.DEFINE, "#[Dd][Ee][Ff][Ii][Nn][Ee]");
            _tokens.Add(Tokens.ASM, "#[Aa][Ss][Mm]");
            _tokens.Add(Tokens.ENDASM, "#[Ee][Nn][Dd][Aa][Ss][Mm]");
            _tokens.Add(Tokens.REGISTER_PORT0_DIR, "\\$[Pp][Oo][Rr][Tt]0_[Dd][Ii][Rr]");
            _tokens.Add(Tokens.REGISTER_PORT0_DATA, "\\$[Pp][Oo][Rr][Tt]0_[Dd][Aa][Tt][Aa]");
            _tokens.Add(Tokens.REGISTER_PORT1_DIR, "\\$[Pp][Oo][Rr][Tt]1_[Dd][Ii][Rr]");
            _tokens.Add(Tokens.REGISTER_PORT1_DATA, "\\$[Pp][Oo][Rr][Tt]1_[Dd][Aa][Tt][Aa]");
            _tokens.Add(Tokens.REGISTER_TMR0_CONFIG, "\\$[Tt][Mm][Rr]0_[Cc][Oo][Nn][Ff][Ii][Gg]");
            _tokens.Add(Tokens.REGISTER_TMR0_VALUE, "\\$[Tt][Mm][Rr]0_[Vv][Aa][Ll][Uu][Ee]");
            _tokens.Add(Tokens.REGISTER_INT_CONFIG, "\\$[Ii][Nn][Tt]_[Cc][Oo][Nn][Ff][Ii][Gg]");
            _tokens.Add(Tokens.REGISTER_INT_STATUS, "\\$[Ii][Nn][Tt]_[Ss][Tt][Aa][Tt][Uu][Ss]");
            _tokens.Add(Tokens.REGISTER_MCU_CONFIG, "\\$[Mm][Cc][Uu]_[Cc][Oo][Nn][Ff][Ii][Gg]");
            _tokens.Add(Tokens.REGISTER_INDR, "\\$[Ii][Nn][Dd][Rr]");
            _tokens.Add(Tokens.REGISTER_STATUS, "\\$[Ss][Tt][Aa][Tt][Uu][Ss]");
            _tokens.Add(Tokens.INSTRUCTION_HLT, "[Hh][Ll][Tt]");
            _tokens.Add(Tokens.INSTRUCTION_STOV, "[Ss][Tt][Oo][Vv]");
            _tokens.Add(Tokens.INSTRUCTION_STO, "[Ss][Tt][Oo]");
            _tokens.Add(Tokens.INSTRUCTION_LDV, "[Ll][Dd][Vv]");
            _tokens.Add(Tokens.INSTRUCTION_LDI, "[Ll][Dd][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_LD, "[Ll][Dd]");
            _tokens.Add(Tokens.INSTRUCTION_ADDI, "[Aa][Dd][Dd][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_ADD, "[Aa][Dd][Dd]");
            _tokens.Add(Tokens.INSTRUCTION_SUBI, "[Ss][Uu][Bb][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_SUB, "[Ss][Uu][Bb]");
            _tokens.Add(Tokens.INSTRUCTION_BEQ, "[Bb][Ee][Qq]");
            _tokens.Add(Tokens.INSTRUCTION_BNE, "[Bb][Nn][Ee]");
            _tokens.Add(Tokens.INSTRUCTION_BGT, "[Bb][Gg][Tt]");
            _tokens.Add(Tokens.INSTRUCTION_BGE, "[Bb][Gg][Ee]");
            _tokens.Add(Tokens.INSTRUCTION_BLT, "[Bb][Ll][Tt]");
            _tokens.Add(Tokens.INSTRUCTION_BLE, "[Bb][Ll][Ee]");
            _tokens.Add(Tokens.INSTRUCTION_JMP, "[Jj][Mm][Pp]");
            _tokens.Add(Tokens.INSTRUCTION_NOT, "[Nn][Oo][Tt]");
            _tokens.Add(Tokens.INSTRUCTION_ANDI, "[Aa][Nn][Dd][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_AND, "[Aa][Nn][Dd]");
            _tokens.Add(Tokens.INSTRUCTION_ORI, "[Oo][Rr][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_OR, "[Oo][Rr]");
            _tokens.Add(Tokens.INSTRUCTION_XORI, "[Xx][Oo][Rr][Ii]");
            _tokens.Add(Tokens.INSTRUCTION_XOR, "[Xx][Oo][Rr]");
            _tokens.Add(Tokens.INSTRUCTION_SLL, "[Ss][Ll][Ll]");
            _tokens.Add(Tokens.INSTRUCTION_SRL, "[Ss][Rr][Ll]");
            _tokens.Add(Tokens.INSTRUCTION_RETURN, "[Rr][Ee][Tt][Uu][Rr][Nn]");
            _tokens.Add(Tokens.INSTRUCTION_RETINT, "[Rr][Ee][Tt][Ii][Nn][Tt]");
            _tokens.Add(Tokens.INSTRUCTION_CALL, "[Cc][Aa][Ll][Ll]");
            _tokens.Add(Tokens.WHITESPACE, "[ \\t]+");
            _tokens.Add(Tokens.NEWLINE, "[\\r\\n]+");
            _tokens.Add(Tokens.NUMBER, "[0-9]*");
            _tokens.Add(Tokens.IDENTIFIER, "[a-zA-Z_][a-zA-Z0-9_]*");
        }

        private void PrepareRegex()
        {
            _regExMatchCollection.Clear();
            foreach (KeyValuePair<Tokens, string> pair in _tokens)
            {
                _regExMatchCollection.Add(pair.Key, Regex.Matches(_inputString, pair.Value));
            }
        }

        public void ResetParser()
        {
            _index = 0;
            _inputString = string.Empty;
            _regExMatchCollection.Clear();
        }

        public Token GetToken()
        {
            if (_index >= _inputString.Length)
                return null;

            foreach (KeyValuePair<Tokens, MatchCollection> pair in _regExMatchCollection)
            {
                foreach (Match match in pair.Value)
                {
                    if (match.Index == _index)
                    {
                        _index += match.Length;
                        return new Token(pair.Key, match.Value);
                    }

                    if (match.Index > _index)
                    {
                        break;
                    }
                }
            }
            _index++;
            return new Token(Tokens.UNDEFINED, string.Empty);
        }

        public PeekToken Peek()
        {
            return Peek(new PeekToken(_index, new Token(Tokens.UNDEFINED, string.Empty)));
        }

        public PeekToken Peek(PeekToken peekToken)
        {
            int oldIndex = _index;

            _index = peekToken.TokenIndex;

            if (_index >= _inputString.Length)
            {
                _index = oldIndex;
                return null;
            }

            foreach (KeyValuePair<Tokens, string> pair in _tokens)
            {
                Regex r = new Regex(pair.Value);
                Match m = r.Match(_inputString, _index);

                if (m.Success && m.Index == _index)
                {
                    _index += m.Length;
                    PeekToken pt = new PeekToken(_index, new Token(pair.Key, m.Value));
                    _index = oldIndex;
                    return pt;
                }
            }
            PeekToken pt2 = new PeekToken(_index + 1, new Token(Tokens.UNDEFINED, string.Empty));
            _index = oldIndex;
            return pt2;
        }
    }

    public class PeekToken
    {
        public int TokenIndex { get; set; }

        public Token TokenPeek { get; set; }

        public PeekToken(int index, Token value)
        {
            TokenIndex = index;
            TokenPeek = value;
        }
    }

    public class Token
    {
        public TokenParser.Tokens TokenName { get; set; }

        public string TokenValue { get; set; }

        public Token(TokenParser.Tokens name, string value)
        {
            TokenName = name;
            TokenValue = value;
        }
    }
}

