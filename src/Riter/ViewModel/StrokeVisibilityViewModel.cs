using System.ComponentModel;
using Riter.Core;
using Riter.ViewModel.Handlers;

namespace Riter.ViewModel;
public sealed class StrokeVisibilityViewModel : BaseViewModel
{
    private readonly IStrokeVisibilityHandler _strokeVisibilityHandler;

    public StrokeVisibilityViewModel(IStrokeVisibilityHandler strokeVisibilityHandler)
    {
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    public ICommand HideAllCommand => new RelayCommand(_strokeVisibilityHandler.HideAll);
}
