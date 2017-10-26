using System;
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

namespace ProjectTracker
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly string associatedName;
        private ProjectsAccess pa;
        ResearchProject currentProj;
        public EditWindow(string facultyName, ResearchProject project)
        {
            associatedName = facultyName;
            currentProj = project;
            pa = new ProjectsAccess();
            InitializeComponent();
            this.DataContext = currentProj;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pa.UpdateProject(currentProj);
            pa.CloseStorage();

            ProjectListWindow main = new ProjectListWindow(associatedName);
            App.Current.MainWindow = main;
            main.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            pa.CloseStorage();
            ProjectListWindow mw = new ProjectListWindow(associatedName);
            App.Current.MainWindow = mw;
            mw.Show();
            this.Close();
        }
    }
}
