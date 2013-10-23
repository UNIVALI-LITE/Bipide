using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0.ControlResources
{
    class LabelCurrentScope
    {
        public LabelCurrentScope(int value, String ini, String end)
        {
            this._Value = value;
            this._Ini   = ini;
            this._End   = end;
        }
        private int _Value;
        public  int  Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private String _Ini;
        public  String  Ini
        {
            get { return _Ini; }
            set { _Ini = value; }
        }

        private String _End;
        public  String  End
        {
            get { return _End; }
            set { _End = value; }
        }
    }
}
