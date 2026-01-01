using UnityEngine;
using UnityEngine.SceneManagement;

namespace RockPaper.StaticHelper
{
    public class SceneName
    {
        public const string MENU = "Menu";
        public const string GAME = "Game";
    }
    
    public static class SceneLoader
    {
        public static void LoadSingle(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public static void LoadAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public static void Unload(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}