using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        GameSettings.Instance.InitalizeSettings();
    }
}
