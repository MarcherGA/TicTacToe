using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TicTacToe;
using System;
using System.Linq;

[RequireComponent(typeof(TMP_Dropdown))]
public class DifficultyDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
        _dropdown.onValueChanged.AddListener((int difficulty) => { GameSettings.Instance.Difficulty = (BotPlayer.Difficulty)difficulty; });
        _dropdown.value = (int)GameSettings.Instance.Difficulty;
    }



    private void PopulateDropdown()
    {
        _dropdown.ClearOptions();
        var difficulties = Enum.GetNames(typeof(BotPlayer.Difficulty));
        _dropdown.AddOptions(difficulties.ToList());
    }

}
