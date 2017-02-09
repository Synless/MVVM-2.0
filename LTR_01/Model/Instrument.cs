using System.ComponentModel;

namespace LTR_01.Model
{
    public class Instrument : ModelBase
    {
        

        private bool isClickable = true;
        public bool IsClickable
        {
            set
            {
                isClickable = value;
            }
            get
            {
                return isClickable;
            }
        }


        public Instrument()
        {

        }
    }
}
