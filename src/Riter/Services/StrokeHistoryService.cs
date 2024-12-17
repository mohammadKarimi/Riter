using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using Riter.Core.Drawing;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Services;

/// <inheritdoc/>
public class StrokeHistoryService : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];
    private readonly Dictionary<StrokesHistoryNode, DispatcherTimer> _fadeTimers = [];

    private bool _ignoreStrokesChange;

    /// <summary>
    /// Gets or sets a value indicating whether return ignore strokeChnage backing field.
    /// </summary>
    public bool IgnoreStrokesChange
    {
        get => _ignoreStrokesChange;
        set => _ignoreStrokesChange = value;
    }

    private InkCanvas InkCanvas { get; set; }

    /// <inheritdoc/>
    public void SetMainElementToRedoAndUndo(InkCanvas canvas) => InkCanvas = canvas;

    /// <inheritdoc/>
    public bool CanRedo() => _redoHistory.Count != 0;

    /// <inheritdoc/>
    public bool CanUndo() => _history.Count != 0;

    /// <inheritdoc/>
    public void Clear()
    {
        _history.Clear();
        foreach (var timer in _fadeTimers.Values)
        {
            timer.Stop();
        }

        _fadeTimers.Clear();
        _redoHistory.Clear();
        InkCanvas.Strokes.Clear();
    }

    /// <inheritdoc/>
    public void ClearRedoHistory() => _redoHistory.Clear();

    /// <inheritdoc/>
    public StrokesHistoryNode Pop() => _history.Count == 0 ? null : _history.Pop();

    /// <inheritdoc/>
    public void Push(StrokesHistoryNode node)
    {
        if (node.EnableTimer)
        {
            StartFadeAnimation(node);
        }
        else
        {
            _history.Push(node);
        }
    }

    /// <inheritdoc/>
    public void Redo()
    {
        if (!CanRedo())
        {
            return;
        }

        _ignoreStrokesChange = true;
        var lastItem = _redoHistory.Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Removed)
        {
            InkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            InkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _ignoreStrokesChange = false;
        Push(lastItem);
    }

    /// <inheritdoc/>
    public void Undo()
    {
        if (!CanUndo())
        {
            return;
        }

        _ignoreStrokesChange = true;
        var lastItem = Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Added)
        {
            InkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            InkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _ignoreStrokesChange = false;
        _redoHistory.Push(lastItem);
    }

    private void StartFadeAnimation(StrokesHistoryNode node)
    {
        foreach (var stroke in node.Strokes)
        {
            var drawingAttributes = stroke.DrawingAttributes;
            var initialColor = drawingAttributes.Color;
            var duration = TimeSpan.FromMilliseconds(node.TimerMiliSecond);
            var steps = 30;
            var interval = duration.TotalMilliseconds / steps;
            var opacityStep = initialColor.A / (float)steps;

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(interval),
            };

            var currentStep = 0;

            timer.Tick += (s, e) =>
            {
                if (currentStep >= steps)
                {
                    timer.Stop();
                    InkCanvas.Strokes.Remove(stroke);
                    return;
                }

                currentStep++;
                var newAlpha = (byte)Math.Max(0, initialColor.A - (opacityStep * currentStep));
                drawingAttributes.Color = Color.FromArgb(newAlpha, initialColor.R, initialColor.G, initialColor.B);
            };

            timer.Start();
        }
    }
}
