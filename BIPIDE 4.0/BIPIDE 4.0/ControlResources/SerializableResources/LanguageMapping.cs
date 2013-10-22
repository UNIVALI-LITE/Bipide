using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ControlResources
{
    [XmlInclude(typeof(LanguageMapping))]
    public class LanguageMapping
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Icon;

        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        private string _HelpMapping;

        public string HelpMapping
        {
            get { return _HelpMapping; }
            set { _HelpMapping = value; }
        }

        private string _InterfaceMapping;

        public string InterfaceMapping
        {
            get { return _InterfaceMapping; }
            set { _InterfaceMapping = value; }
        }

        private string _TipsMapping;

        public string TipsMapping
        {
            get { return _TipsMapping; }
            set { _TipsMapping = value; }
        }
    }
}
