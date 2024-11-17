﻿using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;

public class SettingsPanelStateHandler : BaseStateHandler, ISettingPanelStateHandler
{
    private static bool _settingButtonClicked;
    private static bool _isSettingPanelOpened;
    private static bool _isBrushPanelOpened;
    private static bool _isShapePanelOpened;
    private static bool _isColorPanelOpened;
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;

    static SettingsPanelStateHandler()
    {
        _isSettingPanelOpened = false;
    }

    public SettingsPanelStateHandler(IButtonSelectedStateHandler buttonSelectedStateHandler)
    {
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _isSettingPanelOpened = false;
    }

    public bool SettingPanelVisibility
    {
        get => _isSettingPanelOpened;
        protected set => SetProperty(ref _isSettingPanelOpened, value, nameof(SettingPanelVisibility));
    }

    public bool ColorPanelVisibility
    {
        get => _isColorPanelOpened;
        protected set => SetProperty(ref _isColorPanelOpened, value, nameof(ColorPanelVisibility));
    }

    public bool BrushPanelVisibility
    {
        get => _isBrushPanelOpened;
        protected set => SetProperty(ref _isBrushPanelOpened, value, nameof(BrushPanelVisibility));
    }

    public bool ShapePanelVisibility
    {
        get => _isShapePanelOpened;
        protected set => SetProperty(ref _isShapePanelOpened, value, nameof(ShapePanelVisibility));
    }

    public bool SettingButtonClicked
    {
        get => _settingButtonClicked;
        protected set => SetProperty(ref _settingButtonClicked, value, nameof(SettingButtonClicked));
    }

    public void HideAllPanels()
    {
        SettingPanelVisibility = false;
        BrushPanelVisibility = false;
        ShapePanelVisibility = false;
        SettingButtonClicked = false;
        ColorPanelVisibility = false;
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
    }

    public void SetSettingPanelVisibile() => SettingPanelVisibility = true;

    public void ToggleBrushSettingsPanel(string button)
    {
        if (BrushPanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeBrushSettingButton);
        BrushPanelVisibility = true;
    }

    public void ToggleShapePanel(string button)
    {
        if (ShapePanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeShapeSettingButton);
        ShapePanelVisibility = true;
    }

    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        SettingButtonClicked = true;
        SettingPanelVisibility = true;
    }

    public void ToggleColorPanel()
    {
        if (ColorPanelVisibility)
        {
            HideAllPanels();
            return;
        }

        HideAllPanels();
        ColorPanelVisibility = true;
    }
}
