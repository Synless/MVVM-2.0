using System.Windows.Controls;
using LTR_01.ViewModel;

namespace LTR_01.View.Controls
{
    /// <summary>
    /// Interaction logic for HMIView.xaml
    /// </summary>
    public partial class HMIView : UserControl
    {
        public HMIView()
        {
            InitializeComponent();
            DataContext = new HMIViewModel();            
        }
    }
}
