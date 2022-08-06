using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TicTacToe;


[RequireComponent(typeof(Button))]
public class HintButton : MonoBehaviour
{
    public int HintLength = 2;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { GameEventsManager.Instance.DisplayHint(HintLength); });
    }
}
