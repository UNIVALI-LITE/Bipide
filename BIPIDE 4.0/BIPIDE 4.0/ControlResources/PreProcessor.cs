using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0.ControlResources
{
    class PreProcessor
    {
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

                

            }
            return "";
        }
    }
}
