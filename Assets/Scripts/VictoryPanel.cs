using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отображения победителя игры.
/// </summary>
public class VictoryPanel : MonoBehaviour
{
    public Text VictorNameText;
    public Image ColorBackImage;

    [Tooltip("Имя для игрока белыми (красными)")]
    public string PlayerWhiteName;
    [Tooltip("Цвет маркера для игрока белыми (красными)")]
    public Color PlayerWhiteColor;

    [Tooltip("Имя для игрока чёрными")]
    public string PlayerBlackName;
    [Tooltip("Цвет маркера для игрока чёрными")]
    public Color PlayerBlackColor;

    private void OnEnable()
    {
        GameController.VictoryLevelOnLoad += Init;
    }

    private void OnDisable()
    {
        GameController.VictoryLevelOnLoad -= Init;
    }

    /// <summary>
    /// Обозначить победу конкретного игрока
    /// </summary>
    /// <param name="victorColor">Цвет игрока победителя</param>
    public void Init(ChipColors victorColor)
    {
        switch(victorColor)
        {
            case ChipColors.White:
                VictorNameText.text = PlayerWhiteName;
                ColorBackImage.color = PlayerWhiteColor;
                break;
            case ChipColors.Black:
                VictorNameText.text = PlayerBlackName;
                ColorBackImage.color = PlayerBlackColor;
                break;
        }
    }
}
