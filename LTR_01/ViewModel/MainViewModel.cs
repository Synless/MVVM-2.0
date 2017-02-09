using System.ComponentModel;

namespace LTR_01.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Variables
        private string statustext = "";
        public string StatusText
        {
            get
            {
                return statustext;
            }
            set
            {
                statustext = value;
                OnPropertyChanged("StatusText");
            }
        }
        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        private bool manual = false;
        public bool Manual
        {
            get
            {
                return manual;
            }
            set
            {
                manual = value;
                OnPropertyChanged("Manual");
            }
        }

        #endregion
        public MainViewModel()
        {            
            Messaging.Messenger.Default.Register<object>(this, receivedMessage);
        }
        private void receivedMessage(object _message)
        {
            StatusText = _message.ToString();
            string[] headMessage = StatusText.ToString().Split('=');

            if (headMessage.Length == 2)
            {
                if (headMessage[0].Contains("password"))
                {
                    if (headMessage[1].Contains("incorrect"))
                    {
                    }
                    else if (headMessage[1].Contains("correct"))
                    {
                        SelectedIndex = 2;
                    }
                }
            }
        }
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
