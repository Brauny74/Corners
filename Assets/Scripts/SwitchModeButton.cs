using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Этот класс позволяет переключать режим игры.
/// </summary>
public class SwitchModeButton : MonoBehaviour
{
    public GameMode NewMode;

    private Button _button;

    private void OnEnable()
    {
        GameController.MenuModeOnUpdate += OnModeUpdate;
        _button = GetComponent<Button>();
    }

    private void OnDisable()
    {
        GameController.MenuModeOnUpdate -= OnModeUpdate;
    }

    void OnModeUpdate(GameMode newMode)
    {
        if (newMode == NewMode)
            _button.interactable = false;
        else
            _button.interactable = true;
    }

    public void OnClick()
    {
        GameController.instance.SwitchMode(NewMode);
    }
}
