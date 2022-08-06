using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DestroyOnPressButton : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToDestroy;

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => Destroy(ObjectToDestroy));
    }
}
