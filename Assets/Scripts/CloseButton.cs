using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Закрывает игру.
/// </summary>
public class CloseButton : MonoBehaviour
{
    public void OnClick()
    {
        GameController.instance.CloseApp();
    }
}
