using BIPIDE_4._0.ControlResources;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ViewResources
{
    /// <summary>
    /// Interaction logic for FormNewDocument.xaml
    /// </summary>
    public partial class FormNewDocument : Window
    {
        private ArrayList _ProgrammingLanguages;

        public ArrayList ProgrammingLanguages
        {
            get { return _ProgrammingLanguages; }
            set { _ProgrammingLanguages = value; }
        }

        public FormNewDocument()
        {
            InitializeComponent();
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void _ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProgrammingLanguages = new ArrayList();
            ProgrammingLanguages.Add(
                new ProgrammingLanguageMapping()
                {
                    Name = "Portugol",
                    HighlightMapping = @"ProgrammingLanguagesResources\PortugolResources\HighlightResources\Portugol.xshd",
                    NewProjectMapping = "/*\r\n * Programa Portugol\r\n */\r\nprograma\r\n{\r\n\tfuncao inicio()\r\n\t{\r\n\t\t// TODO\r\n\t}\r\n}\r\n"
                });
        }


    }

    [XmlInclude(typeof(ProgrammingLanguageMapping))]
    public class ProgrammingLanguageMapping
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _HighlightMapping;

        public string HighlightMapping
        {
            get { return _HighlightMapping; }
            set { _HighlightMapping = value; }
        }

        private string _NewProjectMapping;

        public string NewProjectMapping
        {
            get { return _NewProjectMapping; }
            set { _NewProjectMapping = value; }
        }

        private List<TreeItem> _Examples;

        public List<TreeItem> Examples
        {
            get { return _Examples; }
            set { _Examples = value; }
        }

    } 
}
