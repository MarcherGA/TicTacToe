using Newtonsoft.Json.Linq;
using System.Collections;
using TMPro;
using System.IO;
using UnityEngine;

public class ReskinWindow: MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    private TMP_InputField _skinNameInput;

    private static string _errorMessage;
    private static string _successMessage;

    private static readonly string _resultStringFileName = "ReskinResult";

    private void Start()
    {
        LoadResultStrings();
        _skinNameInput = GetComponentInChildren<TMP_InputField>();
    }

    public void TryReskin()
    {
        string skinBundleName = _skinNameInput.text;
        bool isSuccess = GameSettings.Instance.LoadGameTextures(skinBundleName);

        if (isSuccess)
        {
            DisplayResultPopUp(_successMessage);
        }
        else
        {
            DisplayResultPopUp(_errorMessage);
        }
    }

    private static void LoadResultStrings() 
    {
        JObject jsonObj = JObject.Parse(File.ReadAllText(Path.Combine(Application.dataPath, "TextFiles", _resultStringFileName + ".json")));
        _errorMessage = jsonObj.Property("Error").Value.ToString();
        _successMessage = jsonObj.Property("Success").Value.ToString();
    }

    private void DisplayResultPopUp(string text)
    {
        GameObject popUp = Instantiate(popUpPrefab, GetComponentInParent<Canvas>().transform);
        TMP_Text popUpLbl = popUp.GetComponentInChildren<TMP_Text>();
        popUpLbl.text = text;

        _skinNameInput.text = "";
        gameObject.SetActive(false);
    }
}
