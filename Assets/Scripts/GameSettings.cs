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
            if (!isInstance)
            {
                isInstance = true;
                _instance = CreateInstance<GameSettings>();
            }
            return _instance; 
        }
    }
    private static bool isInstance = false;
    #endregion

    public enum GameModeNames
    {
        vsPlayer,
        vsBot,
        Demo
    }

    public GameTextures GameTextures { get => _gameTextures;
        set
        {
            _gameTextures = value;
            GameEventsManager.Instance.GameTexturesChanged();
        }
    }
  
    public string GameTexturesBundleName
    {
        get => _gameTexturesBundleName != null ? _gameTexturesBundleName : _defaultGameTexturesBundleName;
        set => _gameTexturesBundleName = value;
    }

    public IGameMode GameMode { 
        get 
        {
            if (_isGameModeChanged || _gameMode == null)
            {
                _gameMode = GetGameMode(GameModeName);
                _isGameModeChanged = false;
            }
            return _gameMode;
        }
        set => _gameMode = value; }

    public GameModeNames GameModeName
    {
        get => _gameModeName;
        set
        {
            _isGameModeChanged = true;
            _gameModeName = value;
        }
    }
    public int TurnLength { get => _turnLength; set => _turnLength = value; }
    public BotPlayer.Difficulty Difficulty { get => difficulty; set => difficulty = value; }

    private bool isInitialized = false;
    private GameTextures _gameTextures;
    private string _gameTexturesBundleName;
    private readonly string _defaultGameTexturesBundleName = "defaultGameTextures";

    private IGameMode _gameMode;
    private GameModeNames _gameModeName = GameModeNames.vsPlayer;
    private BotPlayer.Difficulty difficulty = BotPlayer.Difficulty.Easy;
    private int _turnLength = 5;
    private bool _isGameModeChanged = false;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    public bool LoadGameTextures(string gameTexturesBundleName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, gameTexturesBundleName, "bundle", gameTexturesBundleName);

        if (!File.Exists(path))
            return false;

        if (gameTexturesBundleName == GameTexturesBundleName && GameTextures != null)
            return false;

        AssetBundle myLoadedAssetBundle;
        try
        {
            myLoadedAssetBundle = AssetBundle.LoadFromFile(path);

        }
        catch (System.Exception)
        {
            return false;
        }

        GameTextures = myLoadedAssetBundle.LoadAsset<GameTextures>(nameof(GameTextures));
        GameTexturesBundleName = gameTexturesBundleName;

        myLoadedAssetBundle.UnloadAsync(false);

        return true;

    }

    public void InitalizeSettings()
    {
        if (!isInitialized)
        {
            LoadGameTextures(GameTexturesBundleName);
            isInitialized = true;
        }
    }

    private IGameMode GetGameMode(GameModeNames gameMode)
    {
        switch (gameMode)
        {
            case GameModeNames.vsPlayer:
                return new LocalMultiplayerMode();
            case GameModeNames.vsBot:
                return new PlayerVsBotMode();
            case GameModeNames.Demo:
                return new BotVsBotMode();
            default:
                return new LocalMultiplayerMode();

        }
    }


}
