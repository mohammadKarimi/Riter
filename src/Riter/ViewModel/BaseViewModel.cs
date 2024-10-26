using System.ComponentModel;

namespace Riter.ViewModel;
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
     => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnStateChanged(object sender, PropertyChangedEventArgs e)
     => OnPropertyChanged(e.PropertyName);
}
