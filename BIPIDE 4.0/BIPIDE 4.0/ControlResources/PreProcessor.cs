using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0.ControlResources
{
    class PreProcessor
    {
        private Boolean _ASM;
        private Boolean _DEFINE;
        private Boolean _INCLUDE;

        TokenParser _Parser = new TokenParser();

        public PreProcessor()
        {

        }

        public string RunPreProcessor(string pCommandText)
        {
            _Parser.InputString = pCommandText;

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
                        _ASM        = false;
                        break;
                    case TokenParser.Tokens.INCLUDE:
                        _INCLUDE    = true;
                        break;
                    case TokenParser.Tokens.DEFINE:
                        _DEFINE     = true;
                        break;
                    case TokenParser.Tokens.NEWLINE:
                        _INCLUDE    = false;
                        _DEFINE     = false;
                        break;
                }

                if (_ASM)
                { }

                if (_DEFINE)
                { }

                if (_INCLUDE)
                { }
            }
            return "";
        }
    }
}
