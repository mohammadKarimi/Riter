﻿using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core;
using Riter.Core.Extensions;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    private readonly Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> _hotkies;
    private readonly IStrokeHistoryService _strokeHistoryService;
    private GlobalHotkeyManager _globalHotkeyManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// Sets up the UI, binds the <see cref="PalleteStateViewModel"/> to the DataContext,
    /// and applies various UI configurations such as setting event listeners,
    /// making the window top-most, setting the default color, and brush size.
    /// </summary>
    /// <param name="pallateStateViewModel">
    /// The view model that manages the palette state, which is used to bind data to the UI.
    /// </param>
    /// <param name="strokeHistoryService">
    /// Manage the history of drawing history.
    /// </param>
    public MainWindow(PalleteStateViewModel pallateStateViewModel, IStrokeHistoryService strokeHistoryService)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        _strokeHistoryService = strokeHistoryService;
        _strokeHistoryService.SetMainElementToRedoAndUndo(MainInkCanvasControl.MainInkCanvas);
        MainInkCanvasControl.MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;

        _hotkies = new Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)>
         {
                { HotKey.CTRL_R, (GlobalHotkeyManager.CTRL, 0x52, pallateStateViewModel.HandleHotkey) }, // CTRL + R
                { HotKey.CTRL_H, (GlobalHotkeyManager.CTRL, 0x48, pallateStateViewModel.HandleHotkey) }, // CTRL + H
         };

        this.EnableDragging(MainPallete)
            .SetTopMost(true);

        Loaded += MainWindow_Loaded;
    }

    /// <inheritdoc/>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _globalHotkeyManager = new GlobalHotkeyManager(this);
        foreach (var hotkey in _hotkies)
        {
            _globalHotkeyManager.RegisterHotkey(hotkey.Key, hotkey.Value.modifiers, hotkey.Value.key, hotkey.Value.callback);
        }
    }

    /// <inheritdoc/>
    protected override void OnClosed(EventArgs e)
    {
        _globalHotkeyManager.Dispose();
        base.OnClosed(e);
    }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_strokeHistoryService.IgnoreStrokesChange)
        {
            return;
        }

        if (e.Added.Count != 0)
        {
            Console.WriteLine(e.ToString());
            _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(e.Added));
        }

        if (e.Removed.Count != 0)
        {
            _strokeHistoryService.Push(StrokesHistoryNode.CreateRemovedType(e.Removed));
        }
    }

    /// <summary>
    /// Load the window in bottom center of screen.
    /// </summary>
    /// <param name="e">Contains the data of routed event.</param>
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var canvasWidth = Layout.ActualWidth;
        var canvasHeight = Layout.ActualHeight;
        var palleteWidth = MainPallete.ActualWidth;
        var palleteHeight = MainPallete.ActualHeight;

        Canvas.SetLeft(MainPallete, (canvasWidth - palleteWidth) / 2);
        Canvas.SetTop(MainPallete, canvasHeight - palleteHeight - 75);
    }
}
