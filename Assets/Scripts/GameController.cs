using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Игровой режим
/// Diagonal - перемещение по диагонали через одну фишку
/// Straight - перемещение по прямой через одну фишку
/// OneStep - перемещение в любую сторону на одну клетку
/// </summary>
public enum GameMode
{
    Diagonal,
    Straight,
    OneStep
}

/// <summary>
/// Цвета фишек и игроков
/// </summary>
public enum ChipColors
{
    Black,
    White
};

/// <summary>
/// Супер-класс, который является синглтоном и управляет в общем игрой.
/// </summary>
public class GameController : Singleton<GameController>
{
    public delegate void VictoryLevelOnLoadHandler(ChipColors victorColor);
    public static event VictoryLevelOnLoadHandler VictoryLevelOnLoad;

    public delegate void PlayerTurnOnSwitchHandler(ChipColors victorColor);
    public static event PlayerTurnOnSwitchHandler PlayerTurnOnSwitch;

    public delegate void PlayerTurnsOnUpdateHandler(ChipColors playerColor, int numOfTurns);
    public static event PlayerTurnsOnUpdateHandler PlayerTurnsOnUpdate;

    public delegate void MenuModeOnUpdateHandler(GameMode newMode);
    public static event MenuModeOnUpdateHandler MenuModeOnUpdate;

    [Header("Settings")]
    [Tooltip("Цвет игрока, который начинает игру.")]
    public ChipColors StartingPlayerColor;

    [Header("Auto Settings")]
    public Field GameField;
    public Selectable Selected;
    public GameMode Mode;
    public ChipColors CurrentPlayersColor;

    private ChipColors victorColor;

    private int playerBlackTurnNumber
    {
        get => w_playerBlackTurnNumber;
        set
        {
            w_playerBlackTurnNumber = value;
            PlayerTurnsOnUpdate?.Invoke(ChipColors.Black, w_playerBlackTurnNumber);
        }
    }
    private int w_playerBlackTurnNumber; //это настоящее значение
    private int playerWhiteTurnNumber
    {
        get => w_playerWhiteTurnNumber;
        set
        {
            w_playerWhiteTurnNumber = value;
            PlayerTurnsOnUpdate?.Invoke(ChipColors.White, w_playerWhiteTurnNumber);
        }
    }
    private int w_playerWhiteTurnNumber; //это настоящее значение

    private void Start()
    {
        SceneManager.sceneLoaded += OnGameLevelStart;
        SceneManager.sceneLoaded += OnVictoryLevelStart;
        SceneManager.sceneLoaded += OnMenuLevelStart;
    }

    /// <summary>
    /// Вызывается в начале уровня и выполняется только в начале уровня меню
    /// </summary>
    public void OnMenuLevelStart(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
        {
            MenuModeOnUpdate?.Invoke(Mode);
        }
    }

    /// <summary>
    /// Вызывается в начале уровня и выполняется только в начале игрового уровня
    /// </summary>
    public void OnGameLevelStart(Scene scene, LoadSceneMode mode)
    {        
        if (scene.name == "GameScene")
        {
            playerBlackTurnNumber = 1;
            playerWhiteTurnNumber = 0;
            CurrentPlayersColor = StartingPlayerColor;
            PlayerTurnOnSwitch?.Invoke(CurrentPlayersColor);
            InitializeField();
        }
    }

    /// <summary>
    /// Вызывается в начале уровня и выполняется только в начале уровня победы
    /// </summary>
    public void OnVictoryLevelStart(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "VictoryScene")
        {
            VictoryLevelOnLoad?.Invoke(victorColor);
        }
    }

    /// <summary>
    /// Если мы в игровой сцене - нарисовать игровое поле и разместить на нём фишки.
    /// Также, сбрасываем игрока на игрока чёрными.
    /// </summary>
    private void InitializeField()
    {
        GameField = FindObjectOfType<Field>();
        GameField.DrawField();
        GameField.PlaceChips();
    }
    
    /// <summary>
    /// Заканчиваем ход и проверям, победил ли один из игроков.
    /// </summary>
    public void EndTurn()
    {
        Selected?.Select(false);
        if (CurrentPlayersColor == ChipColors.Black)
        {
            CurrentPlayersColor = ChipColors.White;
            playerWhiteTurnNumber++;
        }
        else
        {
            CurrentPlayersColor = ChipColors.Black;
            playerBlackTurnNumber++;
        }
        PlayerTurnOnSwitch?.Invoke(CurrentPlayersColor);
        bool victory = GameField.CheckVictor(out victorColor);
        if (victory)
        {
            MoveToScene("VictoryScene");
        }
    }

    /// <summary>
    /// Закрывает игру
    /// </summary>
    public void CloseApp()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// Переключить режим игры
    /// </summary>
    /// <param name="gameMode">Новый режим</param>
    public void SwitchMode(GameMode newMode)
    {
        Mode = newMode;
        MenuModeOnUpdate?.Invoke(Mode);
    }

    /// <summary>
    /// Загрузить сцену по имени
    /// </summary>
    /// <param name="sceneName">Имя сцены</param>
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
