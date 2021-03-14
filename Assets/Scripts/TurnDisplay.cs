using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отображение текущего игрока, можно прикрепить к любому объекту, кроме синглтонов.
/// </summary>
public class TurnDisplay : MonoBehaviour
{
    public Text CurrentPlayerText;
    public Image CurrentPlayerMarker;

    public Text BlackTurnNumberText;
    public Text WhiteTurnNumberText;

    [Tooltip("Название игрока белыми (красными)")]
    public string PlayerWhiteName;
    [Tooltip("Цвета маркера для игрока белыми (красными)")]
    public Color PlayerWhiteColor;

    [Tooltip("Название игрока чёрными")]
    public string PlayerBlackName;
    [Tooltip("Цвет маркера для игрока чёрными")]
    public Color PlayerBlackColor;

    private void OnEnable()
    {
        GameController.PlayerTurnOnSwitch += SetPlayer;
        GameController.PlayerTurnsOnUpdate += SetPlayerTurn;
    }

    private void OnDisable()
    {
        GameController.PlayerTurnOnSwitch -= SetPlayer;
        GameController.PlayerTurnsOnUpdate -= SetPlayerTurn;
    }

    /// <summary>
    /// Переключает отображение в цвет соответствующего игрока.
    /// </summary>
    /// <param name="newPlayerColor">Цвет игрока</param>
    public void SetPlayer(ChipColors newPlayerColor)
    {
        switch (newPlayerColor)
        {
            case ChipColors.White:
                CurrentPlayerText.text = PlayerWhiteName;
                CurrentPlayerMarker.color = PlayerWhiteColor;
                break;
            case ChipColors.Black:
                CurrentPlayerText.text = PlayerBlackName;
                CurrentPlayerMarker.color = PlayerBlackColor;
                break;
        }
    }

    /// <summary>
    /// Обновляет текст с номером текущего хода
    /// </summary>
    /// <param name="playerColor">Цвет игрока, ход которого обновляется</param>
    /// <param name="playerTurn">Номер хода</param>
    public void SetPlayerTurn(ChipColors playerColor, int playerTurn)
    {
        switch (playerColor)
        {
            case ChipColors.White:
                WhiteTurnNumberText.text = playerTurn.ToString();
                break;
            case ChipColors.Black:
                BlackTurnNumberText.text = playerTurn.ToString();
                break;
        }
    }
}
