using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TicTacToe;

public class GameOverPanel : MonoBehaviour
{

    [SerializeField] private TMP_Text ResultLabel;

    private void Start()
    {
        GameEventsManager.Instance.OnDisplayGameOver += ShowPanel;
        GameEventsManager.Instance.OnRestartGame += HidePanel;
        HidePanel();
    }


    private void ShowPanel(TicTacToeGrid.Result result) 
    {
        gameObject.SetActive(true);
        string winText = Utilities.FromCamelCase(result.ToString());
        ResultLabel.text = winText;

        SoundManager.Instance.PlayWinSound();
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
