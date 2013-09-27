using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ViewResources
{
    /// <summary>
    /// Interaction logic for UCHelp.xaml
    /// </summary>
    public partial class UCHelp : UserControl
    {
        private ArrayList _DeviceGroups;

        public ArrayList DeviceGroups
        {
            get { return _DeviceGroups; }
            set { _DeviceGroups = value; }
        }

        public UCHelp()
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(TreeItem) });

            XmlReader Reader = XmlReader.Create(@"D:\test.xml");
            DeviceGroups = (ArrayList)SerializerObj.Deserialize(Reader);

            InitializeComponent();
            _WebBrowser.Navigate(new Uri(@"file://127.0.0.1/d$/Users/Nereu/Documents/Visual Studio 2012/Projects/BIPIDE 4.0/BIPIDE 4.0/LanguageResources/Help/HelpProgrammingLanguages/LanguagePortugol/PortugueseProgramStructure.html"));
        }

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
            private Uri _Uri { get; set; }

            [XmlAttribute("Uri")]
            public string UriString
            {
                get { return (_Uri == null ? null : _Uri.ToString()); }
                set { _Uri = (value == null ? null : new Uri(value)); }
            }

            private ArrayList _Items;

            public ArrayList Items
            {
                get { return _Items; }
                set { _Items = value; }
            }
        }
    }

}
