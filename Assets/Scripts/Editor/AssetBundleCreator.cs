using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public struct AssetWithNewName
{
    public AssetWithNewName(Object asset, string newName)
    {
        Asset = asset;
        NewName = newName;
    }
    public Object Asset;
    public string NewName;
}
public static class AssetBundleCreator
{
    public static void CreateAssetBundle(string assetBundleName, List<Object> assetList, string assetBundleRootSavePath, BuildTarget buildTarget)
    {
        string assetBundleSavePath = Path.Combine(assetBundleRootSavePath, assetBundleName, "bundle");

        Utilities.CreateDirectoryIfNotExists(assetBundleRootSavePath);
        Utilities.CreateDirectoryIfNotExists(assetBundleSavePath);

        foreach (var asset in assetList)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            var assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = assetBundleName;
        }
        BuildPipeline.BuildAssetBundles(assetBundleSavePath, BuildAssetBundleOptions.None, buildTarget);

    }
}
