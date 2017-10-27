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
            newProj = new ResearchProject(associatedName);
            this.DataContext = newProj;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newProj.LastModified = DateTime.Now;
            pa.AddProject(newProj);

            MainWindow main = new MainWindow(associatedName);
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(associatedName);
            App.Current.MainWindow = mw;
            this.Close();
            mw.Show();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pa.CloseStorage();
        }

        private void GetAccess(object sender, EventArgs e)
        {
            pa = new ProjectsAccess();
        }
    }
}
