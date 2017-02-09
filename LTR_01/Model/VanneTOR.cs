using System;
using System.Windows.Threading;

namespace LTR_01.Model
{
    public class VanneTOR : Instrument
    {
        private bool IsOpen;
        private int timeOut = 500;
        private DispatcherTimer dispatcherTimerTOR;
        private string color = "red";
        public string Color
        {
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
            get
            {
                return color;
            }
        }

        public VanneTOR()
        {
            //OUVERT OU PAS ?
            dispatcherTimerTOR = new DispatcherTimer();
            dispatcherTimerTOR.Tick += new EventHandler(DispatcherTimerTOR);
            dispatcherTimerTOR.Interval = new TimeSpan(0, 0, 1);
            IsOpen = false;
        }

        private void DispatcherTimerTOR(object sender, EventArgs e)
        {
            dispatcherTimerTOR.Stop();
            IsClickable = true;
            Color = "green";
        }

        public bool GetIsOpen()
        {
            return IsOpen;
        }
        public bool Open()
        {            
            IsOpen = true; //ACTION OPEN
            dispatcherTimerTOR.Start();
            Color = "yellow";
            return GetIsOpen();
        }
        public bool Close()
        {            
            IsOpen = false;//ACTION CLOSE
            return !GetIsOpen();
        } 
    }
}
