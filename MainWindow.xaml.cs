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
        private readonly string associatedName;
        public MainWindow(string facultyName)
        {
            associatedName = facultyName;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button) sender;
            switch (clicked.Name)
            {
                case "AddButton":
                    AddWindow aw = new AddWindow(associatedName);
                    App.Current.MainWindow = aw;
                    aw.Show();
                    this.Close();
                    break;
                case "ListButton":
                    ProjectListWindow lw = new ProjectListWindow(associatedName);
                    App.Current.MainWindow = lw;
                    lw.Show();
                    this.Close();
                    break;
                case "ReportButton":
                    ReportCreator rc = new ReportCreator();
                    rc.CreateReport(true);
                    break;
                case "ClearButton":
                    //don't need to change windows, maybe need to make a new one 
                    ProjectsAccess pa = new ProjectsAccess();
                    pa.RemoveAllProjects();
                    pa.CloseStorage();
                    break;
            }
        }
    }
}
