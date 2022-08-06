using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class SetActiveOnPress : MonoBehaviour
{
    [SerializeField] private bool value;
    [SerializeField] private GameObject ObjectTtoSetActive;

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ObjectTtoSetActive.SetActive(value));
    }

}
