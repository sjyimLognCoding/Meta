using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR_WIN
public static class asdf
{
    [MenuItem("Assets/Save Prefab Thumbnail")]
    public static void asdfasdf()
    {
        Object[] selectedObject = Selection.gameObjects;


        foreach (var obj in selectedObject)
        {
            if (selectedObject != null && PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.MissingAsset)
            {
                GameObject prefab = (GameObject)obj;

                //Generate the prefab thumbnail texture
                Texture2D thumbnailTexture = AssetPreview.GetAssetPreview(prefab);
                if (thumbnailTexture != null)
                {
                    //Create a new texture with transparent background
                    Texture2D transparentTexture = new Texture2D(thumbnailTexture.width, thumbnailTexture.height, TextureFormat.RGBA32, false);
                    Color[] pixels = thumbnailTexture.GetPixels();

                    //Get the background color
                    Color backgroundColor = pixels[0];
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] == backgroundColor) pixels[i].a = 0; //If this pixel is exactly the background color, make it transparent
                    }
                    transparentTexture.SetPixels(pixels);
                    transparentTexture.Apply();

                    //Save the texture to a user specified folder
                    // string filePath = EditorUtility.SaveFilePanel("Save Prefab Thumbnail", "", prefab.name + ".png", "png");
                    string filePath = Path.Combine(Application.dataPath, "Thumbnails", obj.name);
                    filePath += ".png";
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        byte[] pngData = transparentTexture.EncodeToPNG();
                        File.WriteAllBytes(filePath, pngData);
                    }
                }
                else
                {
                    Debug.LogWarning("Could not generate thumbnail for the selected prefab.");
                }
            }
        }
    }
}

public class Temp_ThumbnailGenerator : MonoBehaviour
{

    public void SaveTextureToPNG()
    {
        string dirPath = Path.Combine(Application.dataPath, "Thumbnails");

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        // Object selectedObject = Selection.activeObject;

        // if (selectedObject != null && PrefabUtility.GetPrefabAssetType(selectedObject) != PrefabAssetType.MissingAsset)
        // {
        //     GameObject prefab = (GameObject)selectedObject;

        //     //Generate the prefab thumbnail texture
        //     Texture2D thumbnailTexture = AssetPreview.GetAssetPreview(prefab);
        //     if (thumbnailTexture != null)
        //     {
        //         //Create a new texture with transparent background
        //         Texture2D transparentTexture = new Texture2D(thumbnailTexture.width, thumbnailTexture.height, TextureFormat.RGBA32, false);
        //         Color[] pixels = thumbnailTexture.GetPixels();

        //         //Get the background color
        //         Color backgroundColor = pixels[0];
        //         for (int i = 0; i < pixels.Length; i++)
        //         {
        //             if (pixels[i] == backgroundColor) pixels[i].a = 0; //If this pixel is exactly the background color, make it transparent
        //         }
        //         transparentTexture.SetPixels(pixels);
        //         transparentTexture.Apply();

        //         //Save the texture to a user specified folder
        //         string savePath = dirPath + selectedObject.name + ".png";
        //         if (!string.IsNullOrEmpty(savePath))
        //         {
        //             byte[] pngData = transparentTexture.EncodeToPNG();
        //             File.WriteAllBytes(savePath, pngData);

        //             Debug.Log("Prefab thumbnail saved: " + savePath);
        //         }
        //     }
        // }

        foreach (GameObject go in AssetPool.instance.RoomAssets)
        {
            Texture2D texture = AssetPreview.GetAssetPreview(go);
            if (texture != null)
            {
                Texture2D transparentTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
                Color[] pixels = transparentTexture.GetPixels();
                Color bg = pixels[0];

                for (int i = 0; i < pixels.Length; i++)
                {
                    if (pixels[i] == bg) pixels[i].a = 0;
                }

                transparentTexture.SetPixels(pixels);
                transparentTexture.Apply();

                string filePath = Path.Combine(dirPath, go.name);
                filePath += ".png";

                byte[] textureByte = transparentTexture.EncodeToPNG();

                File.WriteAllBytes(filePath, textureByte);
            }
        }
    }
}
#endif