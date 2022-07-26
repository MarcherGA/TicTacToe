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
        public UnityAction OnTileClicked;

        private Button _button;
        private Coroutine currentFlicker;

        private void Awake()
        {
            
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnTileClicked);
            SetImgAlpha(0f);
        }

        public void SetActiveButton(bool isOn)
        {
            _button.enabled = isOn;
        }

        public void UpdateTile(TicTacToeGrid.Sign sign) 
        {
            StopFlicker();
            _button.image.sprite = GameSettings.Instance.GameTextures.GetSignImage(sign);
            SetImgAlpha(1f);
            _button.enabled = false;
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
            currentFlicker = StartCoroutine(flicker(length));
        }

        private void StopFlicker()
        {
            if (currentFlicker != null)
            {
                StopCoroutine(currentFlicker);
                currentFlicker = null;
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
        }

    }
}