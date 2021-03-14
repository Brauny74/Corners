using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Этот класс является основой для всех объектов, у которых есть координаты.
/// </summary>
public class Coordinates : MonoBehaviour
{
    public RectTransform Rect;
    protected int X;
    protected int Y;

    /// <summary>
    /// Задаёт координаты объекта на поле.
    /// </summary>
    /// <param name="x">Координата X</param>
    /// <param name="y">Координата Y</param>
    public virtual void SetCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Возвращает позицию X в поле
    /// </summary>
    /// <returns>Позиция X в поле</returns>
    public virtual int GetX()
    {
        return X;
    }

    /// <summary>
    /// Возвращает позицию Y в поле
    /// </summary>
    /// <returns>Позиция Y в поле</returns>
    public virtual int GetY()
    {
        return Y;
    }
}
