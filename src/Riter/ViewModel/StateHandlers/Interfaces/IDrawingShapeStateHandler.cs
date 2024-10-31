using System.ComponentModel;
using Riter.Core.Enum;

namespace Riter.ViewModel.StateHandlers;
public interface IDrawingShapeStateHandler : INotifyPropertyChanged
{
    public DrawingShape CurrentShape { get; }
}
