using Riter.Core.Interfaces;

namespace Riter.ViewModel;
public sealed class StrokeHistoryViewModel(IStrokeHistoryService strokeHistoryService) : BaseViewModel
{
    private readonly IStrokeHistoryService _strokeHistoryService = strokeHistoryService;

    public ICommand UndoCommand => new RelayCommand(_strokeHistoryService.Undo);

    public ICommand RedoCommand => new RelayCommand(_strokeHistoryService.Redo);

    public ICommand ClearCommand => new RelayCommand(_strokeHistoryService.Clear);
}
