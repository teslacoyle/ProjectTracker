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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly string associatedName;
        private ProjectsAccess pa;
        ResearchProject newProj;
        public AddWindow(string facultyName)
        {
            associatedName = facultyName;

            pa = new ProjectsAccess();
            newProj = new ResearchProject(associatedName);
            InitializeComponent();
            this.DataContext = newProj;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newProj.LastModified = DateTime.Now;
            pa.AddProject(newProj);
            pa.CloseStorage();

            MainWindow main = new MainWindow(associatedName);
            App.Current.MainWindow = main;
            main.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(associatedName);
            App.Current.MainWindow = mw;
            mw.Show();
            this.Close();
        }
    }
}
