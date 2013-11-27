using BIPIDE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIPIDE_4._0.ControlResources
{
    class PreProcessor
    {
        private InstrucaoASM _InstrucaoASM;
        private Codigo _Assembly;
        
        private List<Codigo> _Assemblys;
        private List<DefineStructure> _Defines;
        private int _LineCount = 1;

        private Boolean _ASM;
        private Boolean _DEFINE;
        private Boolean _INCLUDE;
        
        private Boolean _IsTokenFound;
        private TokenParser _Parser;

        public PreProcessor()
        {
            _Parser         = new TokenParser();
            _InstrucaoASM   = new InstrucaoASM();
            _Assembly       = new Codigo();
            _Assemblys      = new List<Codigo>();
            _Defines        = new List<DefineStructure>();

        }

        public string RunPreProcessor(string pCommandText)
        {
            _Parser = new TokenParser();
            _LineCount = 0;
            _Parser.InputString         = pCommandText;
            DefineStructure iDefine     = new DefineStructure();
            bool iFoundDefineSignature  = false;
            bool iDefineExpression      = false;
            bool iDefineASM             = false;
            string iASM                 = string.Empty;

            while (true)
            {
                Token iToken = _Parser.GetToken();
                
                if (iToken == null)
                    break;

                switch (iToken.TokenName)
                {
                    case TokenParser.Tokens.ASM:
                        _ASM        = true;
                        break;
                    case TokenParser.Tokens.ENDASM:
                        if (iDefineASM) iDefine.IsIdentifierASM = true;
                        _ASM        = false;
                        break;
                    case TokenParser.Tokens.INCLUDE:
                        _INCLUDE    = true;
                        break;
                    case TokenParser.Tokens.DEFINE:
                        _DEFINE     = true;
                        break;
                    case TokenParser.Tokens.NEWLINE:
                        _INCLUDE                = false;
                        _DEFINE                 = false;
                        iFoundDefineSignature   = false;
                        _LineCount++;
                        break;
                }

                if (iToken.TokenName == TokenParser.Tokens.NEWLINE)
                {
                    if (iDefine.IsIdentifierASM == null && iDefine.MethodName != string.Empty && iDefine.MethodName != null)
                    {
                        // Procurar ASM;
                        iDefineASM = true;
                    }

                    if (iDefine.IsIdentifierASM == false)
                    {
                        // Adicionar a lista
                        _Defines.Add(iDefine);
                        iDefine = new DefineStructure();
                    }

                    if (iDefine.IsIdentifierASM == true)
                    {
                        // Adicionar a lista
                        iASM                += "#endasm";
                        iDefine.Identifier  = iASM;
                        _Defines.Add(iDefine);
                        iASM                = string.Empty;
                        iDefine             = new DefineStructure();
                    }
                }

                if (_ASM)
                {
                    if (iDefineASM)
                        iASM += iToken.TokenValue;
                }

                if (_DEFINE)
                {
                    if (iDefineExpression)
                    {
                        iDefine.Identifier += iToken.TokenValue;
                        iDefine.IsIdentifierASM = false;
                    }

                    if (iFoundDefineSignature && iToken.TokenName == TokenParser.Tokens.WHITESPACE)
                    {
                        iDefineExpression = true;
                    }

                    if (!iDefineExpression && iFoundDefineSignature && iToken.TokenName == TokenParser.Tokens.IDENTIFIER)
                    {
                        iDefine.Params.Add(iToken.TokenValue);
                    }

                    if (!iFoundDefineSignature && iToken.TokenName == TokenParser.Tokens.IDENTIFIER)
                    {
                        iDefine.MethodName      = iToken.TokenValue;
                        iFoundDefineSignature   = true;
                    }
                }

                if (_INCLUDE)
                { }
            }

            foreach (DefineStructure iDefineStructure in _Defines.Where(x => x.MethodName != null))
            {
                if (iDefineStructure.Params.Count > 0)
                {
                    string iStringRegex = "((?<!#define[ \t]+)" + iDefineStructure.MethodName + "\\(";
                    for (int i = 0; i < iDefineStructure.Params.Count; i++)
                    {
                        if (i == 0)
                            iStringRegex += "[0-9A-Za-z$_ \t]*";
                        else
                            iStringRegex += "\\,[0-9A-Za-z$_ \t]*";
                    }
                    iStringRegex += "\\))";

                    Regex iRegex = new Regex(iStringRegex);
                    MatchCollection iMatchCalls = iRegex.Matches(pCommandText);

                    foreach (Match iMatchCall in iMatchCalls)
                    {
                        string iParams = Regex.Replace(iMatchCall.ToString(), @"([a-zA-Z_][a-zA-Z0-9_]*\()", "");
                        iParams = iParams.Replace(")", "");

                        string iStringASM = iDefineStructure.Identifier;

                        foreach (string iParam in iDefineStructure.Params)
                        {
                            iStringASM = Regex.Replace(iStringASM, @"\b" + iParam + @"\b", iParams.Split(',')[iDefineStructure.Params.IndexOf(iParam)].Trim());
                        }

                        pCommandText = iRegex.Replace(pCommandText, iStringASM, 1);
                    }
                }
                else
                {
                    string iStringRegex = "((?<!#define[ \t]+)" + iDefineStructure.MethodName + "\\))";
                    pCommandText = Regex.Replace(pCommandText, iStringRegex, iDefineStructure.Identifier + "\n");
                }
            }
            pCommandText = Regex.Replace(pCommandText, @"(?:#endasm;)", "#endasm");

            return pCommandText;
        }

        public List<Codigo> RunPosProcessor(string pCommandText)
        {
            _Parser = new TokenParser();
            _LineCount = 0;
            _Parser.InputString = pCommandText;
            bool iWaitDefineExpression  = false;
            bool iFoundDefineSignature  = false;
            bool iIgnoreASM             = false;

            while (true)
            {
                Token iToken = _Parser.GetToken();

                if (iToken == null)
                    break;

                switch (iToken.TokenName)
                {
                    case TokenParser.Tokens.ASM:
                        if (!iIgnoreASM)
                            _ASM = true;
                        break;
                    case TokenParser.Tokens.ENDASM:
                        if (_ASM)
                        {
                            _ASM = false;
                            _Assemblys.Add(_Assembly);
                        }
                        iIgnoreASM  = false;
                        _Assembly   = new Codigo();
                        break;
                    case TokenParser.Tokens.INCLUDE:
                        _INCLUDE = true;
                        break;
                    case TokenParser.Tokens.DEFINE:
                        _DEFINE = true;
                        break;
                    case TokenParser.Tokens.NEWLINE:
                        if (iWaitDefineExpression)
                            iIgnoreASM          = true;
                        _INCLUDE                = false;
                        _DEFINE                 = false;
                        iWaitDefineExpression   = false;
                        iFoundDefineSignature   = false;
                        _LineCount++;
                        break;
                }

                if (_DEFINE)
                {
                    if (iWaitDefineExpression)
                    {
                        iIgnoreASM              = false;
                        iWaitDefineExpression   = false;
                        iFoundDefineSignature   = false;
                    }

                    if (iFoundDefineSignature && iToken.TokenName == TokenParser.Tokens.WHITESPACE)
                        iWaitDefineExpression   = true;

                    if (!iFoundDefineSignature && iToken.TokenName == TokenParser.Tokens.IDENTIFIER)
                        iFoundDefineSignature   = true;
                }

                if (_ASM)
                {
                    if (iToken.TokenName != TokenParser.Tokens.WHITESPACE)
                    {
                        if (_IsTokenFound)
                        {
                            _InstrucaoASM.Operando = iToken.TokenValue;
                            _InstrucaoASM.NrLinha = _LineCount;

                            _Assembly.AddInstrucaoASM(_InstrucaoASM.Instrucao,
                                                       _InstrucaoASM.Operando,
                                                       BIPIDE.Classes.eTipo.Instrucao,
                                                       _InstrucaoASM.NrLinha);

                            _InstrucaoASM = new InstrucaoASM();
                            _IsTokenFound = false;
                        }
                        else
                        {
                            switch (iToken.TokenName)
                            {
                                #region BIP I

                                case TokenParser.Tokens.INSTRUCTION_STO:
                                case TokenParser.Tokens.INSTRUCTION_LD:
                                case TokenParser.Tokens.INSTRUCTION_LDI:
                                case TokenParser.Tokens.INSTRUCTION_ADD:
                                case TokenParser.Tokens.INSTRUCTION_ADDI:
                                case TokenParser.Tokens.INSTRUCTION_SUB:
                                case TokenParser.Tokens.INSTRUCTION_SUBI:
                                case TokenParser.Tokens.INSTRUCTION_HLT:
                                    _InstrucaoASM.Instrucao = iToken.TokenValue.ToUpper();
                                    _IsTokenFound = true;
                                    break;

                                #endregion BIP I

                                #region BIP II

                                case TokenParser.Tokens.INSTRUCTION_BEQ:
                                case TokenParser.Tokens.INSTRUCTION_BNE:
                                case TokenParser.Tokens.INSTRUCTION_BGT:
                                case TokenParser.Tokens.INSTRUCTION_BGE:
                                case TokenParser.Tokens.INSTRUCTION_BLT:
                                case TokenParser.Tokens.INSTRUCTION_BLE:
                                case TokenParser.Tokens.INSTRUCTION_JMP:
                                    _InstrucaoASM.Instrucao = iToken.TokenValue.ToUpper();
                                    _IsTokenFound = true;
                                    break;

                                #endregion BIP II

                                #region BIP III

                                case TokenParser.Tokens.INSTRUCTION_AND:
                                case TokenParser.Tokens.INSTRUCTION_OR:
                                case TokenParser.Tokens.INSTRUCTION_XOR:
                                case TokenParser.Tokens.INSTRUCTION_ANDI:
                                case TokenParser.Tokens.INSTRUCTION_ORI:
                                case TokenParser.Tokens.INSTRUCTION_XORI:
                                case TokenParser.Tokens.INSTRUCTION_NOT:
                                case TokenParser.Tokens.INSTRUCTION_SLL:
                                case TokenParser.Tokens.INSTRUCTION_SRL:
                                    _InstrucaoASM.Instrucao = iToken.TokenValue.ToUpper();
                                    _IsTokenFound = true;
                                    break;

                                #endregion BIP III

                                #region BIP IV

                                case TokenParser.Tokens.INSTRUCTION_LDV:
                                case TokenParser.Tokens.INSTRUCTION_STOV:
                                case TokenParser.Tokens.INSTRUCTION_RETURN:
                                case TokenParser.Tokens.INSTRUCTION_CALL:
                                    _InstrucaoASM.Instrucao = iToken.TokenValue.ToUpper();
                                    _IsTokenFound = true;
                                    break;

                                #endregion BIP IV

                                #region uBIP

                                case TokenParser.Tokens.INSTRUCTION_RETINT:
                                    _InstrucaoASM.Instrucao = iToken.TokenValue.ToUpper();
                                    _IsTokenFound = true;
                                    break;

                                #endregion uBIP
                            }
                        }
                    }
                }
            }
            return _Assemblys;
        }
    }

    public class DefineStructure
    {
        public DefineStructure()
        {
            _Params = new List<string>();
        }

        private string _MethodName;

        public string MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        private List<string> _Params;

        public List<string> Params
        {
            get { return _Params; }
            set { _Params = value; }
        }
        private string _Identifier;

        public string Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; }
        }
        private bool? _IsIdentifierASM;

        public bool? IsIdentifierASM
        {
            get { return _IsIdentifierASM; }
            set { _IsIdentifierASM = value; }
        }
    }
}
