using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Этот класс позволяет переключать между сценами.
/// </summary>
public class LoadSceneButton : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        GameController.instance.MoveToScene(sceneName);
    }
}
