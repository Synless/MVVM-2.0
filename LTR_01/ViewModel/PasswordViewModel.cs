using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
namespace LTR_01.ViewModel
{
    class PasswordViewModel : INotifyPropertyChanged
    {
        private string password = "";
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #region ICommand declaration
        public ICommand btnNum_Pushed { get; set; }
        public ICommand btnC_Pushed { get; set; }
        public ICommand btnV_Pushed { get; set; }
        #endregion

        public PasswordViewModel()
        {
            btnNum_Pushed = new Command(btnNum_pushed);
            btnC_Pushed = new Command(btnC_pushed);
            btnV_Pushed = new Command(btnV_pushed);
        }

        #region Commands
        private void btnNum_pushed(object parametre)
        {
            Password += parametre;
        }        
        private void btnC_pushed(object parametre)
        {
            Password = "";
        }
        private void btnV_pushed(object parametre)
        {            
            if(Password == Properties.Settings.Default.AdminPwd)
            {
                Messaging.Messenger.Default.Send("password=correct");
            }
            else
            {
                Messaging.Messenger.Default.Send("password=incorrect");
                Messaging.Messenger.Default.Send("Wrong password : \"" + Password + "\"");
                MessageBox.Show("Wrong password");
            }
            Password = "";
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
