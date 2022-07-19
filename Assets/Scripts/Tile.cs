using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TicTacToe
{
    [RequireComponent(typeof(Button))]
    public class Tile : MonoBehaviour
    {
        public UnityAction TileClicked;
        public char Value { get => _value; }


        private Button _button;
        private char _value;

        private readonly static char _defaultValue = ' ';

        private void Start()
        {
            _value = _defaultValue;

            _button = GetComponent<Button>();
            _button.onClick.AddListener(TileClicked);

            _button.image.enabled = false;
        }

        public void UpdateTile(char value,Sprite sprite) 
        {
            _value = value;
            _button.image.sprite = sprite;
            _button.image.enabled = true;
        }

        public void ResetTile()
        {
            _value = _defaultValue;
            _button.image.enabled = false;
        }

    }
}