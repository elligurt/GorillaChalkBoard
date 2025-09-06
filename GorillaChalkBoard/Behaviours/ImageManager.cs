using System;
using System.IO;
using System.Linq;
using UnityEngine;
using BepInEx;

namespace GorillaChalkBoard
{
    public static class ImageManager
    {
        private static Texture2D[] loadedImages;
        private static string imageFolderPath;

        public static void CreateImageFolder()
        {
            imageFolderPath = Path.Combine(Paths.PluginPath, "ChalkboardImages");

            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
                Debug.Log($"[GorillaChalkBoard] Created image folder: {imageFolderPath}");
            }
        }

        public static void LoadAllImages()
        {
            string[] files = Directory.GetFiles(imageFolderPath, "*.*")
                .Where(s => s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            loadedImages = new Texture2D[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                byte[] fileData = File.ReadAllBytes(files[i]);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
                loadedImages[i] = tex;
            }
        }

        public static void ApplyImagesToPhotos(GameObject parentObject)
        {
            if (parentObject == null || loadedImages == null || loadedImages.Length == 0)
            {
                return;
            }

            string[] photoNames = { "Photo1", "Photo2", "Photo3", "Photo4", "Photo5" };

            for (int i = 0; i < photoNames.Length; i++)
            {
                Transform photoTransform = parentObject.transform.Find(photoNames[i]);
                if (photoTransform != null)
                {
                    Renderer rend = photoTransform.GetComponentInChildren<Renderer>();
                    if (rend != null)
                    {
                        Texture2D tex = loadedImages[i % loadedImages.Length];
                        rend.material.mainTexture = tex;
                    }
                }
            }
        }

        public static int GetLoadedImageCount()
        {
            return loadedImages != null ? loadedImages.Length : 0;
        }
    }
}
