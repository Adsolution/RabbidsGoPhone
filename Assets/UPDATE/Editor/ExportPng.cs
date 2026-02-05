using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportFontTextures
{
    [MenuItem("UPDATE/Export Selected Textures to PNG")]
    static void Export()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is Texture2D tex)
            {
                var path = AssetDatabase.GetAssetPath(tex);

                var png = tex.EncodeToPNG();
                File.WriteAllBytes(obj.name + ".png", png);
            }
        }

        AssetDatabase.Refresh();
    }
}
