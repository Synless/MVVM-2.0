using LTR_01.ViewModel;
using System.Windows.Controls;

namespace LTR_01.View.Controls
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : UserControl
    {
        public Password()
        {
            InitializeComponent();
            DataContext = new PasswordViewModel();
        }
    }
}
