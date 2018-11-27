using System.ComponentModel;
using System.Windows.Input;
using LTR_01.Model;
using System.IO;
using System.Collections.Generic;

namespace LTR_01.ViewModel
{
    public class HMIViewModel : INotifyPropertyChanged
    {
        #region Variables
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
        private List<VanneTOR> tor = new List<VanneTOR>();
        public List<VanneTOR> TOR
        {
            get
            {
                return tor;
            }
            set
            {
                tor = value;
                OnPropertyChanged("TOR");
            }
        }
        private List<VanneREG> reg = new List<VanneREG>();
        public List<VanneREG> REG

        {
            get
            {
                return reg;
            }
            set
            {
                reg = value;
                OnPropertyChanged("REG");
            }
        }
        private XLApp ex = new XLApp();
        private string filePath = "Process1.xls";

        #region ICommand declaration
        public ICommand Vanne_Pushed { get; set; }
        public ICommand Hold_Pushed { get; set; }
        #endregion

        public HMIViewModel()
        {
            Vanne_Pushed = new Command(Vanne_pushed);
            Messaging.Messenger.Default.Register<object>(this, receivedMessage);
            for(int n =0;n<6;n++)
            {
                TOR.Add(new VanneTOR());
            }
            REG.Add(new VanneREG());        
            //Excel();          
              
        }
        private void Excel()
        {
            string path = "D:\\Documents\\LTR\\LTR01\\LTR_01\\LTR_01\\bin\\Debug";
            string[] fileEntries = Directory.GetFiles(path);
            foreach(string s in fileEntries)
            {
                string[] tmp = s.Split('.');
                if (tmp[1].Contains("xls"))
                {
                    path = s;
                    ex.OpenXL();
                    ex.OpenBook(path + filePath);
                    ex.SelectSheet("Main");
                    string s_tmp = "";
                    bool b_tmp = false;
                    b_tmp = ex.ReadString(ref s_tmp, 0, 0);
                    break;
                }
            }
            ;         
        }

        private void receivedMessage(object _message)
        {
            string message = _message.ToString();
            string[] headMessage = message.ToString().Split('=');

            if (headMessage.Length == 2)
            {
                if (headMessage[0].Contains("password"))
                {
                    if (headMessage[1].Contains("incorrect"))
                    {
                        Manual = false;
                    }
                    else if (headMessage[1].Contains("correct"))
                    {
                        Manual = true;
                    }
                }
            }
        }
        private void Vanne_pushed(object _parametre)
        {
            Messaging.Messenger.Default.Send("Button vanne " + _parametre + " pushed");
            int vanne = int.Parse(_parametre.ToString()) - 1;
            TOR[vanne].Open();
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
