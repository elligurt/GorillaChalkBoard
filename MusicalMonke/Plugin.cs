using BepInEx;

namespace GorillaChalkBoard
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            GorillaTagger.OnPlayerSpawned(AssetLoader.OnGameInitialized);
        }
    }
}
