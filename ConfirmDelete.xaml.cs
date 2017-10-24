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
    /// Interaction logic for ConfirmDelete.xaml
    /// </summary>
    public partial class ConfirmDelete : Window
    {
        private readonly string pass = "adimin_pass";

        public ConfirmDelete()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox userPass = (TextBox)FindName("passwordText");
            if (userPass.Text == pass)
            {
                ProjectsAccess pa = new ProjectsAccess();
                pa.RemoveAllProjects();
                pa.CloseStorage();
            }
            else
            {
                //display window saying pw incorrect
                IncorrectPassWindow pw = new IncorrectPassWindow();
                pw.Show();
            }
            this.Close();
        }
    }
}
