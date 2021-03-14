using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Класс объектов, которые можно выбирать.
/// </summary>
public class Selectable : MonoBehaviour
{
    public GameObject SelectionBrackets;
    public bool Selected;

    public void Awake()
    {
        Select(false);
    }

    public void Select(bool select)
    {
        if (select)
        {
            GameController.instance.Selected?.Select(false);
            GameController.instance.Selected = this;
        }
        else
        {
            GameController.instance.Selected = null;
        }
        Selected = select;
        SelectionBrackets.SetActive(select);
    }
}
