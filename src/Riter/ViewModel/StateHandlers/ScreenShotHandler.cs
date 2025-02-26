using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Riter.Core.IO;
using Riter.ViewModel.StateHandlers.Interfaces;

namespace Riter.ViewModel.StateHandlers;
public class ScreenShotHandler : BaseStateHandler, IScreenShotHandler
{
    public void Take()
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        try
        {
            mainWindow.MainPalette.Visibility = Visibility.Collapsed;
            mainWindow.Dispatcher.Invoke(() => { }, DispatcherPriority.Render);
            Rect bounds = VisualTreeHelper.GetDescendantBounds(mainWindow.MainInkCanvasControl.MainInkCanvas);
            RenderTargetBitmap renderBitmap = new((int)bounds.Width, (int)bounds.Height, 96d, 96d, PixelFormats.Pbgra32);

            DrawingVisual visual = new();
            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush brush = new(mainWindow.MainInkCanvasControl.MainInkCanvas);
                context.DrawRectangle(brush, null, new Rect(default, bounds.Size));
            }

            renderBitmap.Render(visual);
            FileStorage.SaveScreenshot(renderBitmap);
            Clipboard.SetImage(renderBitmap);
        }
        finally
        {
            mainWindow.MainPalette.Visibility = Visibility.Visible;
        }
    }
}
