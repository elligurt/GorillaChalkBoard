using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace GorillaChalkBoard
{
    public static class AssetLoader
    {
        public static GameObject chalkboard;

        public static void OnGameInitialized()
        {
            try
            {
                if (chalkboard != null)
                    return;

                var assetBundle = LoadFromResource("GorillaChalkBoard.Content.chalkboard");
                if (assetBundle == null)
                    return;

                chalkboard = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>("GorillaChalkBoard"));
                chalkboard.transform.localPosition = new Vector3(-69.2846f, 12.1246f, - 81.8547f);
                chalkboard.transform.eulerAngles = new Vector3(0f, 108.5379f, 0f);

                ImageManager.CreateImageFolder();
                ImageManager.LoadAllImages();

                ImageManager.ApplyImagesToPhotos(chalkboard);

                ErrorManager.CheckAndShowError(chalkboard);

                Debug.Log("[GorillaChalkBoard] Chalkboard initialized");
            }
            catch (Exception e)
            {
                Debug.LogError($"[GorillaChalkBoard] Error while initialzing: {e}");
            }
        }

        public static AssetBundle LoadFromResource(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    Debug.LogError($"[GorillaChalkBoard] Resource not found: {resourcePath}");
                    return null;
                }

                return AssetBundle.LoadFromStream(stream);
            }
        }
    }
}
