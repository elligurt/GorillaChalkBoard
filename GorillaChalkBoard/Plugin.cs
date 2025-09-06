using System;
using System.Threading.Tasks;
using BepInEx;
using GorillaChalkBoard.Tools;
using UnityEngine;

namespace GorillaChalkBoard
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        private GameObject _boardPrefab;

        private void Awake()
        {
            Instance = this;
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        private async void OnGameInitialized(object sender, EventArgs e)
        {
            await SetupBoard();
        }

        private async Task SetupBoard()
        {
            try
            {
                ImageManager.CreateImageFolder();
                ImageManager.LoadAllImages();

                _boardPrefab = await AssetLoader.LoadAsset<GameObject>("GorillaChalkBoard");
                if (_boardPrefab == null)
                {
                    Debug.LogError("[GorillaChalkBoard] Failed to load prefab.");
                    return;
                }

                GameObject boardInstance = Instantiate(_boardPrefab);
                boardInstance.SetActive(true);
                boardInstance.transform.position = new Vector3(-69.2846f, 12.1246f, -81.8547f);
                boardInstance.transform.rotation = Quaternion.Euler(0f, 108.5379f, 0f);

                ImageManager.ApplyImagesToPhotos(boardInstance);

                ErrorManager.CheckAndShowError(boardInstance);

            }
            catch (Exception ex)
            {
                Debug.LogError("[GorillaChalkboard] Error setting up board: " + ex);
            }
        }
    }
}
