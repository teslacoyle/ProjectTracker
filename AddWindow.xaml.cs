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
        private ProjectsAccess pa;
        ResearchProject newProj;
        public AddWindow()
        {
            pa = new ProjectsAccess();
            newProj = new ResearchProject();
            InitializeComponent();
            this.DataContext = newProj;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newProj.LastModified = DateTime.Now;
            pa.AddProject(newProj);
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pa.OnClosing();
        }
    }
}
