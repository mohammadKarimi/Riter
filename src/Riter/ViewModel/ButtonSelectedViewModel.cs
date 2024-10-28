namespace Riter.ViewModel;
public class ButtonSelectedViewModel : BaseViewModel
{
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;

    public ButtonSelectedViewModel(IButtonSelectedStateHandler buttonSelectedStateHandler)
    {
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _buttonSelectedStateHandler.PropertyChanged += OnStateChanged;
    }

    public string ButtonSelectedName => _buttonSelectedStateHandler.ButtonSelectedName;
}
