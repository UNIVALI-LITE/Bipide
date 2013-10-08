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
    public partial class UCHelpFundamentals : UserControl
    {
        private ArrayList _DeviceGroups;

        public ArrayList DeviceGroups
        {
            get { return _DeviceGroups; }
            set { _DeviceGroups = value; }
        }

        public UCHelpFundamentals()
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(TreeItem) });

            XmlReader Reader = XmlReader.Create(@"D:\Users\Nereu\Documents\Visual Studio 2012\Projects\BIPIDE 4.0\BIPIDE 4.0\LanguageResources\Help\PortugueseHelpMapping.xml");
            DeviceGroups = (ArrayList)SerializerObj.Deserialize(Reader);

            InitializeComponent();
        }

        private void _TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            String iPartialUri = ( ( TreeItem ) _TreeView.SelectedItem ).UriString;
            Uri iHelpFileUri = GetUri( iPartialUri );
            
            if ( iPartialUri == String.Empty )
                _WebBrowser.Navigate("about:blank");
            else
                _WebBrowser.Navigate( iHelpFileUri );
        }

        private Uri GetUri(String pPartialUri)
        {
            String iAssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            iAssemblyPath = iAssemblyPath.Replace('\\', '/');
            iAssemblyPath = "file://127.0.0.1/" + iAssemblyPath[0] + "$/" + iAssemblyPath.Substring(3) + pPartialUri;

            return new Uri(iAssemblyPath);
        }
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
