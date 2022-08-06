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
        public bool isFlicker { get => _isFlicker; }

        public UnityAction OnTileClicked;

        private Button _button;
        private Coroutine currentFlicker;
        private bool _isFlicker = false;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnTileClicked);
        }

        public void SetActiveButton(bool isOn)
        {
            _button.enabled = isOn;
        }

        public void UpdateTile(TicTacToeGrid.Sign sign) 
        {
            StopFlicker();
            _button.image.sprite = GameSettings.Instance.GameTextures.GetSignImage(sign);
            SetImgAlpha(GameSettings.Instance.GameTextures.GetSignImageAlpha(sign));
            if(sign != TicTacToeGrid.Sign.Empty)
            {
                SoundManager.Instance.PlayTileClickedSound();
                _button.enabled = false;
            }
            else 
            {
                _button.enabled = true;
            }
            
        }

        public void ResetTile()
        {
            _button.image.sprite = null;
            SetImgAlpha(0f);
        }

        private void SetImgAlpha(float alpha)
        {
            Color color = _button.image.color;
            color.a = alpha;
            _button.image.color = color;
        }

        public void Flicker(int length)
        {
            _isFlicker = true;
            currentFlicker = StartCoroutine(flicker(length));
        }

        private void StopFlicker()
        {
            if (isFlicker)
            {
                StopCoroutine(currentFlicker);
            }
        }

        private IEnumerator flicker(int length)
        {
            float timeLeft = length;
            while (timeLeft > 0)
            {
                SetImgAlpha((Mathf.Sin(Time.time * 4) + 1) / 4);
                timeLeft -= Time.deltaTime;
                yield return null;
            }
            SetImgAlpha(0f);
            _isFlicker = false;
        }

    }
}