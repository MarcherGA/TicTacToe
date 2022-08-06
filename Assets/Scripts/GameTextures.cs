using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

[System.Serializable]
public class GameTextures : ScriptableObject
{
    public Sprite XImage;
    public Sprite OImage;
    public Sprite BackgroundImage;

    public static List<FieldInfo> GetResourcesFields()
    {
        List<FieldInfo> resourcesFields = new List<FieldInfo>();
        var publicFields = typeof(GameTextures).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        foreach (var field in publicFields)
        {
            if (typeof(Object).IsAssignableFrom(field.FieldType))
                resourcesFields.Add(field);
        }
        return resourcesFields;
    }

    public Sprite GetSignImage(TicTacToe.TicTacToeGrid.Sign sign)
    {
        switch (sign)
        {
            case TicTacToe.TicTacToeGrid.Sign.X:
                return XImage;
            case TicTacToe.TicTacToeGrid.Sign.O:
                return OImage;
            default:
                return null;
        }
    }

    public float GetSignImageAlpha(TicTacToe.TicTacToeGrid.Sign sign)
    {
        switch (sign)
        {
            case TicTacToe.TicTacToeGrid.Sign.X:
                return 1f;
            case TicTacToe.TicTacToeGrid.Sign.O:
                return 1f;
            default:
                return 0f;
        }
    }
}
