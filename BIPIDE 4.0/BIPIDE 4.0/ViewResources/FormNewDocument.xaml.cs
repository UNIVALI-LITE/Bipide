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
using System.Xml;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ViewResources
{
    /// <summary>
    /// Interaction logic for FormNewDocument.xaml
    /// </summary>
    public partial class FormNewDocument : Window
    {
        private ArrayList _ProgrammingLanguages;

        public string ProjectName
        {
            get { return _TextBoxFile.Text; }
        }

        private string _HighlightFile;

        public string HighlightFile
        {
            get { return _HighlightFile; }
        }

        private string _ProgrammingLanguage;

        public string ProgrammingLanguage
        {
            get { return _ProgrammingLanguage; }
        }

        private Boolean _IsNewProject = true;

        public Boolean IsNewProject
        {
            get { return _IsNewProject; }
        }

        public FormNewDocument(ResourceDictionary pResource)
        {
            Resources = pResource;
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(ProgrammingLanguageMapping), typeof(TreeItem) });

            XmlReader Reader = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + @"ProgrammingLanguagesResources\ProgrammingLanguagesMapping.xml");
            _ProgrammingLanguages = (ArrayList)SerializerObj.Deserialize(Reader);
            
            InitializeComponent();
            _TextBoxFile.Text = (String)FindResource("NDDefaultFileName");
            GenerateTreeView();
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void _ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        void TreeViewItemProject_Selected(object sender, RoutedEventArgs e)
        {
            string iNewFileContent = string.Empty;
            TreeViewItem iTreeViewItem = e.OriginalSource as TreeViewItem;
            if (iTreeViewItem != null)
            {
                ItemsControl iItemsControl = ItemsControl.ItemsControlFromItemContainer(iTreeViewItem);
                if (iItemsControl != null)
                    iNewFileContent = (iItemsControl.Tag as ProgrammingLanguageMapping).NewProject;
            }

            _LabelDescription.Content = (sender as TreeViewItem).Name;
            _IsNewProject = true;
            _ListView.Items.Clear();

            _ListView.Items.Add(new TreeItem() { Name = (String)FindResource("NDListItemNewDocument"), UriString = iNewFileContent });
            _ListView.Items.Add(new TreeItem() { Name = (String)FindResource("NDListItemEmptyDocument") } );
        }

        void TreeViewItemExamples_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem iTreeViewItem = e.OriginalSource as TreeViewItem;
            if (iTreeViewItem != null)
            {
                ItemsControl iItemsControl = ItemsControl.ItemsControlFromItemContainer(iTreeViewItem);
                if (iItemsControl != null)
                {
                    _HighlightFile = iItemsControl.Tag as String;
                }
            }

            _LabelDescription.Content = (sender as TreeViewItem).Name;
            _IsNewProject = false;
            _ListView.Items.Clear();

            foreach (TreeItem iTreeItem in ((sender as TreeViewItem).Tag as ArrayList))
            {
                _ListView.Items.Add(iTreeItem);
            }
        }

        void TreeItemiProgrammingLanguage_Selected(object sender, RoutedEventArgs e)
        {
            ChangeFileNameExtension(((sender as TreeViewItem).Tag as ProgrammingLanguageMapping).FileExtension);
            _ProgrammingLanguage        = ((sender as TreeViewItem).Tag as ProgrammingLanguageMapping).Name;
            _HighlightFile              = ((sender as TreeViewItem).Tag as ProgrammingLanguageMapping).HighlightMapping;
            _LabelDescription.Content   = ((sender as TreeViewItem).Tag as ProgrammingLanguageMapping).Name;
        }

        private void ChangeFileNameExtension(string iExtension)
        {
            if (_TextBoxFile.Text != String.Empty)
            {
                string[] iFileName = _TextBoxFile.Text.Split('.');
                if (iFileName.Length > 1)
                    _TextBoxFile.Text = _TextBoxFile.Text.Replace(iFileName[iFileName.Length - 1], iExtension);
                else
                    _TextBoxFile.Text = _TextBoxFile.Text + "." + iExtension;
            }
            else
            {
                _TextBoxFile.Text = (String)FindResource("NDDefaultFileName") + "." + iExtension;
            }
        }

        private void GenerateTreeView()
        {
            foreach (ProgrammingLanguageMapping iProgrammingLanguage in _ProgrammingLanguages)
            {
                TreeViewItem iTreeItemiProgrammingLanguage  = GetTreeView(iProgrammingLanguage.Name, "sourceCode.png");
                iTreeItemiProgrammingLanguage.Tag           = iProgrammingLanguage;
                iTreeItemiProgrammingLanguage.Selected      += TreeItemiProgrammingLanguage_Selected;

                if (_TreeView.Items.Count == 0)
                    iTreeItemiProgrammingLanguage.IsSelected = true;
                  

                TreeViewItem iTreeItemiProject              = GetTreeView((String)FindResource("NDTreeViewItemProject"), "new16.png");
                iTreeItemiProject.Tag                       = iProgrammingLanguage.NewProject;
                iTreeItemiProject.Selected                  += TreeViewItemProject_Selected; 

                TreeViewItem iTreeItemiExamples             = GetTreeView((String)FindResource("NDTreeViewItemExamples"), "examples16.png");
                iTreeItemiExamples.Tag                      = iProgrammingLanguage.Examples;
                iTreeItemiExamples.Selected                 += TreeViewItemExamples_Selected;

                iTreeItemiProgrammingLanguage.Items.Add(iTreeItemiProject);
                iTreeItemiProgrammingLanguage.Items.Add(iTreeItemiExamples);                

                _TreeView.Items.Add(iTreeItemiProgrammingLanguage);
            }
        }

        private TreeViewItem GetTreeView(string pLabel, string pImage)
        {
            TreeViewItem iTreeViewItem = new TreeViewItem();

            iTreeViewItem.IsExpanded    = true;

            StackPanel iStackPanel      = new StackPanel();
            iStackPanel.Orientation     = Orientation.Horizontal;
            iStackPanel.Width           = 150;
            iStackPanel.Height          = 20;

            Image iImage                = new Image();
            iImage.Width                = 16;
            iImage.Height               = 16;
            iImage.Source               = new BitmapImage
                (new Uri("pack://application:,,/ImageResources/" + pImage));

            iStackPanel.Children.Add(iImage);

            TextBlock iTextBlock    = new TextBlock();
            iTextBlock.Text         = pLabel;

            iStackPanel.Children.Add(iTextBlock);

            iTreeViewItem.Header = iStackPanel;
            return iTreeViewItem;
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

        private string _FileExtension;

        public string FileExtension
        {
            get { return _FileExtension; }
            set { _FileExtension = value; }
        }

        private string _HighlightMapping;

        public string HighlightMapping
        {
            get { return _HighlightMapping; }
            set { _HighlightMapping = value; }
        }

        private string _NewProject;

        public string NewProject
        {
            get { return _NewProject; }
            set { _NewProject = value; }
        }

        private ArrayList _Examples;

        public ArrayList Examples
        {
            get { return _Examples; }
            set { _Examples = value; }
        }

    } 
}
