using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;
public interface IButtonSelectedStateHandler : INotifyPropertyChanged
{
    string ButtonSelectedName { get; }

    string ArrowButtonSelectedName { get; }

    void ResetArrowButtonSelected();

    void ResetPreviousButton();

    void SetArrowButtonSelected(string button);

    void SetButtonSelectedName(string button);

    void StoreCurrentButton();
}
