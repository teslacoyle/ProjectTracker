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

        public MainWindow()
        {
            InitializeComponent();
            pa = new ProjectsAccess();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button) sender;
            /*ResearchProject rp = new ResearchProject()
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
            */
            switch (clicked.Name)
            {
                case "AddButton":
                    AddWindow aw = new AddWindow();
                    App.Current.MainWindow = aw;
                    this.Close();
                    aw.ShowDialog();
                    break;
                case "ListButton":
                    ProjectListWindow lw = new ProjectListWindow();
                    App.Current.MainWindow = lw;
                    this.Close();
                    lw.ShowDialog();
                    break;
                case "ReportButton":
                    /*      do I need to switch windows, or just open new? hmm 
                    ReportWindow rw = new ReportWindow();
                    App.Current.MainWindow = rw;
                    this.Close();
                    rw.ShowDialog();
                    */
                    break;
                case "clearButton":
                    //don't need to change windows, maybe need to make a new one 
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pa.OnClosing();
        }
    }
}
