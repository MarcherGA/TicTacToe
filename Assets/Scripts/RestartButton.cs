using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GameEventsManager.Instance.RestartGame);
    }

}
