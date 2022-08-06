using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TicTacToe;
using System;

[RequireComponent(typeof(TMP_Dropdown))]
public class GameModeDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
        _dropdown.onValueChanged.AddListener((int gameMode) => { GameSettings.Instance.GameModeName = (GameSettings.GameModeNames)gameMode; });
        _dropdown.value = (int)GameSettings.Instance.GameModeName;
    }


    
    private void PopulateDropdown() 
    {
        _dropdown.ClearOptions();
        var gameModeList = Enum.GetNames(typeof(GameSettings.GameModeNames));
        var gameModeNames = new List<string>();
        foreach (var gameMode in gameModeList)
        {
            gameModeNames.Add(Utilities.FromCamelCase(gameMode));
        }
        _dropdown.AddOptions(gameModeNames);
    }

}
