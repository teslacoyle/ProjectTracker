using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectsAccess pa;
        int i = 0;
        CollectionViewSource researchProjectViewSource;

        public MainWindow()
        {
            InitializeComponent();

            pa = new ProjectsAccess();

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
            ResearchProject rp = new ResearchProject()
            {
                Investigators = "Eli Coyle",
                CoInvestigators = "Eli's Assistant",
                ProjectTitle = "Test Project"+i.ToString(),
                Objectives = "Some Objectives",
                Methods = "Some Methods",
                Results = "Some Results",
                Conclusions = "Some Conclusions",
                FutureStudies = "None Planned",
                LastModified = DateTime.Now
            };
            pa.AddProject(rp);
            i++;
            UpdateContext();
        }
    }
}
