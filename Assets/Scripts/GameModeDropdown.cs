using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TicTacToe;
using System;
using System.Linq;

[RequireComponent(typeof(TMP_Dropdown))]
public class GameModeDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    public enum GameMode
    {
        vsPlayer,
        vsBot,
    }

    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
        _dropdown.onValueChanged.AddListener((int gameMode) => { GameSettings.Instance.GameMode = GetGameMode((GameMode)gameMode); });
    }

    private IGameMode GetGameMode(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.vsPlayer:
                return new LocalMultiplayerMode();
            case GameMode.vsBot:
                break;
            default:
                return new LocalMultiplayerMode();
                
        }
        return new LocalMultiplayerMode();
    }
    
    private void PopulateDropdown() 
    {
        _dropdown.ClearOptions();
        var gameModeList = Enum.GetNames(typeof(GameMode));
        var gameModeNames = new List<string>();
        foreach (var gameMode in gameModeList)
        {
            gameModeNames.Add(Utilities.FromCamelCase(gameMode));
        }
        _dropdown.AddOptions(gameModeNames);
    }

}
