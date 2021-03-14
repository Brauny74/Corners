using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс клетки, по которым перемещаются объекты.
/// </summary>
public class Cell : Coordinates
{
    //Клетки двумя цветами в шахматном порядке
    [Tooltip("Первый возможный цвет клетки")]
    public Color FirstColor;
    [Tooltip("Второй возможный цвет клетки")]
    public Color AltColor;

    Image cellSprite;

    public void Awake()
    {
        cellSprite = GetComponent<Image>();
    }

    private void setColor(Color c)
    {
        cellSprite.color = c;
    }

    /// <summary>
    /// Задаёт координаты клетки на поле.
    /// В отличие от стандартных координат, также её окрашивает в соответствующих цвет.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public override void SetCoordinates(int x, int y)
    {
        base.SetCoordinates(x, y);
        if (x % 2 == 0)
        {
            if (y % 2 == 0)
            {
                setColor(FirstColor);
            }
            else
            {
                setColor(AltColor);
            }
        } else
        {
            if (y % 2 == 0)
            {
                setColor(AltColor);
            }
            else
            {
                setColor(FirstColor);
            }
        }
    }

    /// <summary>
    /// Вызывается при клике по клетке
    /// </summary>
    public void OnClick()
    {
        if (GameController.instance.Selected == null)
            return;        
        GameController.instance.GameField.MoveSelectedChip(X, Y);
    }
}
