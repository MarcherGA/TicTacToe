using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TicTacToe;

public class GameSettings : ScriptableObject
{
    #region Singleton
    private static GameSettings _instance;
    private GameSettings()  { }
    public static GameSettings Instance 
    { 
        get 
        {
            if (!isInitialized)
            {
                isInitialized = true;
                _instance = CreateInstance<GameSettings>();
            }
            return _instance; 
        }
    }
    private static bool isInitialized = false;
    #endregion

    public GameTextures GameTextures { get => _gameTextures;
        set
        {
            GameEventsManager.Instance.GameTexturesChanged();
            _gameTextures = value;
        }
    }
    public string GameTexturesBundleName
    {
        get => _gameTexturesBundleName != null ? _gameTexturesBundleName : _defaultGameTexturesBundleName;
        set => _gameTexturesBundleName = value;
    }

    public IGameMode GameMode { get => _gameMode; set => _gameMode = value; }
    public int TurnLength { get => _turnLength; set => _turnLength = value; }

    private GameTextures _gameTextures;
    private string _gameTexturesBundleName;
    private readonly string _defaultGameTexturesBundleName = "defaultGameTextures";

    private IGameMode _gameMode = new LocalMultiplayerMode();
    private int _turnLength = 5;


    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadGameTextures()
    {
        string path = Path.Combine(Application.streamingAssetsPath, GameTexturesBundleName, "bundle", GameTexturesBundleName);
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(path);
        GameTextures = myLoadedAssetBundle.LoadAsset<GameTextures>(nameof(GameTextures));
    }

    public void InitalizeSettings()
    {
        LoadGameTextures();
    }

}
