using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Это класс фишки, которыми играют игроки
/// </summary>
public class Chip : Coordinates
{

    Selectable selectable;
    ChipColors playColor;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        selectable = GetComponent<Selectable>();
    }

    /// <summary>
    /// Установить цвет фишки
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(ChipColors color)
    {
        playColor = color;
        switch (color)
        {
            case ChipColors.White:
                animator.SetBool("White", true);
                break;
            case ChipColors.Black:
                animator.SetBool("White", false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Возвращает цвет фишки
    /// </summary>
    /// <returns>Цвет фишки</returns>
    public ChipColors GetColor()
    {
        return playColor;
    }

    /// <summary>
    /// Выбирает фишку как ту, которой игрок хочет ходить.
    /// Если игрок кликает на фишку чужого цвета, ничего не происходит.
    /// </summary>
    public void Select()
    {
        if (GameController.instance.CurrentPlayersColor == playColor)
        {
            selectable.Select(true);
        }
    }

    /// <summary>
    /// Перемещает фишку на позицию, соответствующую координатам в поле.
    /// </summary>
    /// <param name="x">X в поле</param>
    /// <param name="y">Y в поле</param>
    public void Move(int x, int y)
    {
        Field gField = GameController.instance.GameField;
        Rect.anchoredPosition = new Vector2(gField.StartDrawingPosition.x + gField.DrawingStep.x * x, gField.StartDrawingPosition.y + gField.DrawingStep.y * y);
        SetCoordinates(x, y);
    }

    public override string ToString()
    {
        return "Chip at " + X.ToString() + "," + Y.ToString();
    }
}
