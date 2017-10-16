using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectTracker
{
    /// <summary>
    /// Interaction logic for ProjectListWindow.xaml
    /// </summary>
    public partial class ProjectListWindow : Window
    {
        private ProjectsAccess pa;
        CollectionViewSource researchProjectViewSource;

        public ProjectListWindow()
        {
            pa = new ProjectsAccess();
            InitializeComponent();
        }

        internal void UpdateContext()
        {
            researchProjectViewSource.Source = new ObservableCollection<ResearchProject>(pa.GetProjectsList());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            researchProjectViewSource = ((CollectionViewSource)(FindResource("researchProjectViewSource")));
            //load data
            UpdateContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
