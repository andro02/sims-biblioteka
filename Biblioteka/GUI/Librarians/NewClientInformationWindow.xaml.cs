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

namespace Biblioteka.GUI.Librarians
{
    /// <summary>
    /// Interaction logic for NewClientInformationWindow.xaml
    /// </summary>
    public partial class NewClientInformationWindow : Window
    {
        public NewClientInformationWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
