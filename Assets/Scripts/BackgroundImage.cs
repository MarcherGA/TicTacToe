using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TicTacToe;

[RequireComponent(typeof(Image))]
public class BackgroundImage : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        GameEventsManager.Instance.OnGameTexturesChanged += Set;
        Set();
    }
    private void Set()
    {
        _image.sprite = GameSettings.Instance.GameTextures.BackgroundImage;
    }
}
