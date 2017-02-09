using System;
using System.Windows.Threading;

namespace LTR_01.Model
{
    public class VanneREG : Instrument
    {
        private static DispatcherTimer dispatcherTimerREG;
        private int apearture = 0;
        public int Apearture
        {
            get
            {
                return apearture;
            }
            set
            {
                apearture = value;
                feedback = true;
                OnPropertyChanged("Apearture");
            }
        }
        private int actualApearture = 10;
        public int ActualApearture
        {
            get
            {
                return actualApearture;
            }
            set
            {
                actualApearture = value;
                OnPropertyChanged("ActualApearture");
            }
        }
        private bool feedback = true;

        public VanneREG()
        {
            //OUVERT OU PAS ?
            dispatcherTimerREG = new DispatcherTimer();
            dispatcherTimerREG.Tick += new EventHandler(DispatcherTimerREG);
            dispatcherTimerREG.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimerREG.Start();
        }

        private void DispatcherTimerREG(object sender, EventArgs e)
        { 
            if(feedback)
            {                
                ActualApearture = (actualApearture + apearture + 1) / 2;
                if(actualApearture == apearture)
                    feedback = false;
            }            
            IsClickable = true;
        }
    }
}
