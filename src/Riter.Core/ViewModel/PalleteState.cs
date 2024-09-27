using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.Main.ViewModel;

 public class PalleteState : INotifyPropertyChanged
    {
        private bool _isReleased = true;
        private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
        private string _buttonSelectedName = "ReleasedButton";

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsReleased
        {
            get => _isReleased;
            private set => SetProperty(ref _isReleased, value, nameof(IsReleased), () =>
            {
                InkEditingMode = _isReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
            });
        }

        public InkCanvasEditingMode InkEditingMode
        {
            get => _inkEditingMode;
            private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
        }

        public string ButtonSelectedName
        {
            get => _buttonSelectedName;
            private set => SetProperty(ref _buttonSelectedName, value, nameof(ButtonSelectedName));
        }

        public void Release(string buttonName)
        {
            IsReleased = true;
            ButtonSelectedName = buttonName;
        }

        public void StartDrawing(string buttonName)
        {
            IsReleased = false;
            InkEditingMode = InkCanvasEditingMode.Ink;
            ButtonSelectedName = buttonName;
        }

        public void StartErasing(string buttonName)
        {
            IsReleased = false;
            InkEditingMode = InkCanvasEditingMode.EraseByStroke;
            ButtonSelectedName = buttonName;
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool SetProperty<T>(ref T field, T newValue, string propertyName, Action onChangedAction = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                onChangedAction?.Invoke();
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
