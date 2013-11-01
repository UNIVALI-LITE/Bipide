using br.univali.portugol.integracao.asa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0
{
    public class FunctionVariables
    {
        public FunctionVariables()
        {
            Variable = new List<String>();
        }



        private String _FunctionName;

        public String FunctionName
        {
            get { return _FunctionName; }
            set { _FunctionName = value; }
        }



        private List<String> _Variable;

        public List<String> Variable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }
    }

    public class ConstVariables
    {
        public ConstVariables(String name, int value)
        {
            VariableName    = name;
            VariableValue   = value;
        }

        private String _VariableName;

        public String VariableName
        {
            get { return _VariableName; }
            set { _VariableName = value; }
        }

        private int _VariableValue;

        public int VariableValue
        {
            get { return _VariableValue; }
            set { _VariableValue = value; }
        }
    }
}
