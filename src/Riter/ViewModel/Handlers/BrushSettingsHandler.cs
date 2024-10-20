using Riter.Core;
using Riter.ViewModel.Handlers;

namespace Riter.ViewModel;

public class BrushSettingsHandler : BaseHandler, IBrushSettingsHandler
{
    private string _inkColor;
    private string _colorSelected;
    private double _sizeOfBrush;

    public BrushSettingsHandler()
    {
        SetInkColor(AppSettings.InkDefaultColor);
        SizeOfBrush = AppSettings.BrushSize;
    }

    public string ColorSelected
    {
        get => _colorSelected;
        private set => SetProperty(ref _colorSelected, value, nameof(ColorSelected));
    }

    public string InkColor
    {
        get => _inkColor;
        private set => SetProperty(ref _inkColor, value, "InkDrawingAttributes");
    }

    public double SizeOfBrush
    {
        get => _sizeOfBrush;
        private set => SetProperty(ref _sizeOfBrush, value, nameof(SizeOfBrush), () =>
        {
            OnPropertyChanged("InkDrawingAttributes");
        });
    }

    public void SetInkColor(string color)
    {
        InkColor = color;
        ColorSelected = color;
        ResetPreviousButton();
    }

    public void SetSizeOfBrush(string size)
    {
        SizeOfBrush = double.Parse(size);
        ResetPreviousButton();
    }
}
