using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class WindowFactory
    {
        private readonly Canvas _canvas;
        private readonly AssetDataBase _assetDataBase;

        public WindowFactory(AssetDataBase assetDataBase)
        {
            _assetDataBase = assetDataBase;
            var canvasPrefab = _assetDataBase.GetAsset<Canvas>(Constants.CanvasID);
            _canvas = Object.Instantiate(canvasPrefab);
            Object.DontDestroyOnLoad(_canvas.gameObject);
        }

        public MainMenuView CreateMainMenu()
        {
            var prefab = _assetDataBase.GetAsset<MainMenuView>(Constants.MasterViewID);
            var instance = Object.Instantiate(prefab, _canvas.transform);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public InSelectingLevelView CreateSelectingView()
        {
            var prefab = _assetDataBase.GetAsset<InSelectingLevelView>(Constants.InSelectingViewID);
            var instance = Object.Instantiate(prefab, _canvas.transform);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public InGameView CreateInGameView()
        {
            var prefab = _assetDataBase.GetAsset<InGameView>(Constants.InGameViewID);
            var instance = Object.Instantiate(prefab, _canvas.transform);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public LoadGameView CreateLoadGameView()
        {
            var prefab = _assetDataBase.GetAsset<LoadGameView>(Constants.LoadGameViewID);
            var instance = Object.Instantiate(prefab, _canvas.transform);
            instance.gameObject.SetActive(false);
            return instance;
        }
    }
}