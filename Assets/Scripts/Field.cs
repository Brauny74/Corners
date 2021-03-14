using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс поля, можно прикрепить к любому объекту в сцене, который уже существует, кроме синглтонов.
/// </summary>
public class Field : MonoBehaviour
{
    [Tooltip("Позиция на канвасе, откуда начинается рисоваться поле.")]
    public Vector2 StartDrawingPosition;
    [Tooltip("Размер шага между объектами.")]
    public Vector2 DrawingStep;
    [Tooltip("Трансформ канваса, где будут размещаться объекты.")]
    public RectTransform Content;    

    [Tooltip("Размер поля")]
    public int FieldSize;
    [Tooltip("Имя, по которому префаб клетки загружается из ресурсов.")]
    public string CellPrefabName;
    [Tooltip("Имя, по которому префаб фишки загружается из ресурсов.")]
    public string ChipPrefabName;

    List<List<Chip>> chips;

    /// <summary>
    /// Размещает клетки на поле
    /// </summary>
    public void DrawField()
    {
        GameObject cellObject = Resources.Load(CellPrefabName) as GameObject;
        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                Cell cell = Instantiate(cellObject, Content).GetComponent<Cell>();
                cell.Rect.anchoredPosition = new Vector2(StartDrawingPosition.x + DrawingStep.x * x, StartDrawingPosition.y + DrawingStep.y * y);
                cell.SetCoordinates(x, y);
            }
        }
    }

    /// <summary>
    /// Размещает фишки в сцене
    /// </summary>
    public void PlaceChips()
    {
        GameObject chipObject = Resources.Load(ChipPrefabName) as GameObject;
        chips = new List<List<Chip>>();
        for (int x = 0; x < FieldSize; x++)
        {
            chips.Add(new List<Chip>());
            for (int y = 0; y < FieldSize; y++)
            {
                chips[x].Add(null);
            }
        }
        for (int x = FieldSize - 3; x < FieldSize; x++)
        {            
            for (int y = FieldSize - 3; y < FieldSize; y++)
            {
                Chip chip = Instantiate(chipObject, Content).GetComponent<Chip>();
                chip.Rect.anchoredPosition = new Vector2(StartDrawingPosition.x + DrawingStep.x * x, StartDrawingPosition.y + DrawingStep.y * y);
                chip.SetColor(ChipColors.Black);
                chip.SetCoordinates(x, y);
                chips[x][y] = chip;
            }
        }
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Chip chip = Instantiate(chipObject, Content).GetComponent<Chip>();
                chip.Rect.anchoredPosition = new Vector2(StartDrawingPosition.x + DrawingStep.x * x, StartDrawingPosition.y + DrawingStep.y * y);
                chip.SetColor(ChipColors.White);
                chip.SetCoordinates(x, y);
                chips[x][y] = chip;
            }
        }
    }

    bool CanMoveDiagonal(int chipX, int chipY, int x, int y)
    {
        if (chipX == x - 2 && chipY == y - 2)
        {
            if (chips[x - 1][y - 1] != null)
            {
                return true;
            }
        }
        if (chipX == x + 2 && chipY == y - 2)
        {
            if (chips[x + 1][y - 1] != null)
            {
                return true;
            }
        }
        if (chipX == x - 2 && chipY == y + 2)
        {
            if (chips[x - 1][y + 1] != null)
            {
                return true;
            }
        }
        if (chipX == x + 2 && chipY == y + 2)
        {
            if (chips[x + 1][y + 1] != null)
            {
                return true;
            }
        }
        return false;
    }

    bool CanMoveStraight(int chipX, int chipY, int x, int y)
    {
        if (chipX == x - 2 && chipY == y)
        {
            if (chips[x - 1][y] != null)
            {
                return true;
            }
        }
        if (chipX == x + 2 && chipY == y)
        {
            if (chips[x + 1][y] != null)
            {
                return true;
            }
        }
        if (chipX == x && chipY == y + 2)
        {
            if (chips[x][y + 1] != null)
            {
                return true;
            }
        }
        if (chipX == x && chipY == y - 2)
        {
            if (chips[x][y - 1] != null)
            {
                return true;
            }
        }
        return false;
    }

    bool CanMoveOneStep(int chipX, int chipY, int x, int y)
    {
        if (chipX == x + 1 && chipY == y + 1 && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x - 1 && chipY == y + 1 && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x - 1 && chipY == y - 1 && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x + 1 && chipY == y - 1 && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x + 1 && chipY == y && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x - 1 && chipY == y && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x && chipY == y - 1 && chips[x][y] == null)
        {
            return true;
        }
        if (chipX == x && chipY == y + 1 && chips[x][y] == null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Проверяет, можно ли переместить фишку в координаты на поле.
    /// </summary>
    /// <param name="chipToMove">Фишку, возможность перемещения которой, которой надо проверить</param>
    /// <param name="x">Координата X, куда надо переместить фишку.</param>
    /// <param name="y">Координата Y, куда надо переместить фишку.</param>
    /// <returns></returns>
    public bool CanMoveChip(Chip chipToMove, int x, int y)
    {
        var chipX = chipToMove.GetX();
        var chipY = chipToMove.GetY();
        if (chips[x][y] != null)
            return false;
        switch (GameController.instance.Mode)
        {
            case GameMode.Diagonal:
                return CanMoveDiagonal(chipX, chipY, x, y);
            case GameMode.Straight:
                return CanMoveStraight(chipX, chipY, x, y);
            case GameMode.OneStep:
                return CanMoveOneStep(chipX, chipY, x, y);
            default:
                return false;
        }
    }

    /// <summary>
    /// Передвигает выбранную фишку в заданные координаты
    /// </summary>
    /// <param name="x">Координата X, куда переместить выбранную фишку</param>
    /// <param name="y">Координата Y, куда переместить выбранную фишку</param>
    public void MoveSelectedChip(int x, int y)
    {
        Chip chip = GameController.instance.Selected.GetComponent<Chip>();
        if (CanMoveChip(chip, x, y))
        {
            chips[chip.GetX()][chip.GetY()] = null;
            chip.Move(x, y);
            chips[x][y] = chip;
            GameController.instance.EndTurn();
        }
    }

    /// <summary>
    /// Эта функция проверяет, победил ли один из игроков.
    /// </summary>
    /// <param name="victorColor">Цвет игрока. Изменяется, чтобы соответствовать игроку, который победил. Если такого нет - просто текущего игрока.</param>
    /// <returns>true, если один из игроков победил.</returns>
    public bool CheckVictor(out ChipColors victorColor)
    {
        bool victory = true;
        for (int x = FieldSize - 3; x < FieldSize; x++)
        {
            for (int y = FieldSize - 3; y < FieldSize; y++)
            {
                if (chips[x][y] == null || chips[x][y].GetColor() != ChipColors.White)
                {
                    victory = false;
                }
            }
        }
        if (victory)
        {
            victorColor = ChipColors.White;
            return true;
        }
        victory = true;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (chips[x][y] == null || chips[x][y].GetColor() != ChipColors.Black)
                {
                    victory = false;
                }
            }
        }
        if (victory)
        {
            victorColor = ChipColors.Black;
            return true;
        }
        victorColor = GameController.instance.CurrentPlayersColor;
        return false;
    }
}
