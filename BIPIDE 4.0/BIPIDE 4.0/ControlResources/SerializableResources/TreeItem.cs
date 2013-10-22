using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ControlResources
{

    [XmlInclude(typeof(TreeItem))]
    public class TreeItem
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [XmlIgnore]
        private String _Uri { get; set; }

        [XmlAttribute("Uri")]
        public String UriString
        {
            get { return _Uri; }
            set { _Uri = value; }
        }

        private ArrayList _Items;

        public ArrayList Items
        {
            get { return _Items; }
            set { _Items = value; }
        }
    }
}
