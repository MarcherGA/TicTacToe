using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

public class GameBundleCreatorWindow : EditorWindow
{
       
    public string assetBundleName;
    public List<Object> BundleAssets;

    private static GameTextures gameTextures;
    private static readonly string gameTexturesPath = "Assets/" + nameof(GameTextures) + ".asset";


    private void OnEnable()
    {
        CreateGameTexturesAssetIfNotExists();
    }

    private static void CreateGameTexturesAssetIfNotExists()
    {
        if (!gameTextures)
        {
            gameTextures = CreateInstance<GameTextures>();
            AssetDatabase.CreateAsset(gameTextures, gameTexturesPath);
            AssetDatabase.Refresh();
        }
    }

    private void OnDestroy()
    {
        AssetDatabase.DeleteAsset(gameTexturesPath);
    }

    private void OnGUI()
    {
        BundleAssets = new List<Object>();

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Enter Name:");
        assetBundleName = GUILayout.TextField(assetBundleName);
        
        GUILayout.Space(20);

        var gameTexturesSerializedObj = new SerializedObject(gameTextures);
        gameTexturesSerializedObj.Update();

        foreach (var field in GameTextures.GetResourcesFields())
        {
            EditorGUILayout.PropertyField(gameTexturesSerializedObj.FindProperty(field.Name), false);
        }

        gameTexturesSerializedObj.ApplyModifiedProperties();

        BundleAssets.Add(gameTextures);

        GUILayout.Space(40);
        if(GUILayout.Button("Create Asset Bundle"))
        {
            CreateAssetBundle();
        }


    }
    
    [MenuItem("Window/" + nameof(GameBundleCreatorWindow))]
    public static void ShowWindow() 
    {
        GetWindow<GameBundleCreatorWindow>();
    }

    private void CreateAssetBundle()
    {
        AssetBundleCreator.CreateAssetBundle(assetBundleName, BundleAssets, Application.streamingAssetsPath, EditorUserBuildSettings.activeBuildTarget);
    }

}
