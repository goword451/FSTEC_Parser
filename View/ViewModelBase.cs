using System;
using System.ComponentModel;

namespace FSTEC
{
    public abstract class ViewModelBase : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase()
        {

        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public virtual void OnDispose()
        {

        }
        public void Dispose()
        {
            this.OnDispose();
        }
    }
}
