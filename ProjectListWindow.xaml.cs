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
        private readonly string associatedName;
        private ProjectsAccess pa;
        CollectionViewSource researchProjectViewSource;

        public ProjectListWindow(string facultyName)
        {
            associatedName = facultyName;
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
            Button clicked = (Button)sender;
            switch (clicked.Name)
            {
                case "AddButton":
                    AddWindow aw = new AddWindow(associatedName);
                    App.Current.MainWindow = aw;
                    aw.Show();
                    this.Close();
                    break;
                case "EditButton":
                    break;
                case "RemoveButton":
                    DataGrid dg = (DataGrid)FindName("researchProjectDataGrid");
                    pa.RemoveProject((ResearchProject)dg.SelectedItem);
                    UpdateContext();
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(associatedName);
            App.Current.MainWindow = mw;
            mw.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pa.CloseStorage();
        }

    }
}
