using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CoreDomain.Scripts.Editor.DefaultSceneSelector
{
    [InitializeOnLoad]
    public static class DefaultSceneSelector
    {
        private const string DEFAULT_SCENE_PATH_KEY = "DefaultScenePathKey";
        private const string HAS_OPENED_PROJECT_BEFORE_KEY = "HasOpenedProjectBeforeKey";
        
        private const string CORE_SCENE_FILE = "Assets/Scenes/Core.unity";
    
        static DefaultSceneSelector()
        {
            EditorApplication.delayCall += OnLoad;
        }
        
        private static void OnLoad()
        {
            OpenCoreSceneIfHaventBefore();
            SetSavedSceneAsStarting();
        }

        private static void SetSavedSceneAsStarting()
        {
            var path = EditorPrefs.GetString(DEFAULT_SCENE_PATH_KEY, CORE_SCENE_FILE);
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }

        private static void OpenCoreSceneIfHaventBefore()
        {
            var didOpenProjectBefore = EditorPrefs.HasKey(HAS_OPENED_PROJECT_BEFORE_KEY);
            if (didOpenProjectBefore)
            {
                return;
            }
            
            EditorSceneManager.OpenScene(CORE_SCENE_FILE);
            EditorPrefs.SetBool(HAS_OPENED_PROJECT_BEFORE_KEY, true);
        }

        [MenuItem("Tools/Scene/Select Default Scene", false, 1)]
        private static void SelectDefaultScene()
        {
            var absolutePath = EditorUtility.OpenFilePanel("Select default scene", GetSelectedFolder(), "unity");
            if (string.IsNullOrEmpty(absolutePath))
            {
                return;
            }
            
            var path = GetProjectRelativePath(absolutePath);
            EditorPrefs.SetString(DEFAULT_SCENE_PATH_KEY, path);
            SetSavedSceneAsStarting();
        }
    
        private static string GetSelectedFolder()
        {
            var obj = Selection.activeObject;
            return obj == null ? "Assets" : AssetDatabase.GetAssetPath(obj.GetInstanceID());;
        }

        private static string GetProjectRelativePath(string absolutePath)
        {
            if (absolutePath.StartsWith(Application.dataPath))
            {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            }

            Debug.LogError("Selected file is not within the project's Assets folder.");
            return null;
        }
    }
}