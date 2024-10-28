using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;
public interface IButtonSelectedStateHandler : INotifyPropertyChanged
{
    string ButtonSelectedName { get; }

    void ResetPreviousButton();

    void SetButtonSelectedName(string button);

    void StoreCurrentButton();
}
