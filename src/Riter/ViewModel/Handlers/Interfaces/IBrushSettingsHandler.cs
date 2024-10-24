﻿using System.ComponentModel;

namespace Riter.ViewModel.Handlers;

public interface IBrushSettingsHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value of InkColor.
    /// </summary>
    string InkColor { get; }

    /// <summary>
    /// Gets a value of InkColor.
    /// </summary>
    string ColorSelected { get; }

    /// <summary>
    /// Gets a value of Size.
    /// </summary>
    double SizeOfBrush { get; }

    /// <summary>
    /// set InkColor from settings.
    /// </summary>
    /// <param name="color">The color user selected.</param>
    void SetInkColor(string color);

    /// <summary>
    /// Set Size of brush from settins.
    /// </summary>
    /// <param name="size">type of brush size enum.</param>
    void SetSizeOfBrush(string size);
}
