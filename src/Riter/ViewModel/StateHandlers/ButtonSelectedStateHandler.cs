using Riter.Core.Consts;

namespace Riter.ViewModel.StateHandlers;
public class ButtonSelectedStateHandler : BaseStateHandler, IButtonSelectedStateHandler
{
    private static string _arrowButtonSelectedName;
    private static string _buttonSelectedName;
    private static string _previousButtonSelectedName = string.Empty;

    static ButtonSelectedStateHandler()
    {
        _buttonSelectedName = ButtonNames.DefaultButtonSelectedName;
    }

    public string ButtonSelectedName
    {
        get => _buttonSelectedName;
        protected set => SetProperty(ref _buttonSelectedName, value, nameof(ButtonSelectedName));
    }

    public string ArrowButtonSelectedName
    {
        get => _arrowButtonSelectedName;
        protected set => SetProperty(ref _arrowButtonSelectedName, value, nameof(ArrowButtonSelectedName));
    }

    public void SetButtonSelectedName(string button) => ButtonSelectedName = button;

    public void ResetPreviousButton()
    {
        if (!string.IsNullOrEmpty(_previousButtonSelectedName))
        {
            ButtonSelectedName = _previousButtonSelectedName;
        }

        _previousButtonSelectedName = string.Empty;
    }

    public void StoreCurrentButton()
    {
        if (string.IsNullOrEmpty(_previousButtonSelectedName))
        {
            _previousButtonSelectedName = ButtonSelectedName;
        }
    }

    public void ResetArrowButtonSelected() => ArrowButtonSelectedName = string.Empty;

    public void SetArrowButtonSelected(string button) => ArrowButtonSelectedName = button;
}
