using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    [RequireComponent(typeof(Button))]
    public class UndoButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            GameEventsManager.Instance.OnDisplayUndoButton += delegate { gameObject.SetActive(true); };
            _button.onClick.AddListener(GameEventsManager.Instance.UndoTurn);
            gameObject.SetActive(false);
        }

    }
}