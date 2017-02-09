using System.ComponentModel;

namespace LTR_01.Model
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        protected ModelBase() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}