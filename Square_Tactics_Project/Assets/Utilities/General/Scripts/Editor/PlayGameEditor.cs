using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Utilities.Managers;

namespace Utilities.Editor
{
    public class PlayGameEditor
    {
        const string INITIAL_SCENE_PATH = "Assets/Utilities/Scenes/Test_Audio.unity";

        [MenuItem("Utilities/Play Game")]
        public static void PlayGame()
        {
            if (EditorApplication.isPlaying == true)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(INITIAL_SCENE_PATH);
            EditorApplication.isPlaying = true;
        }

        [MenuItem("Utilities/Clear Save and Play Game")]
        public static void ClearSaveAndPlayGame()
        {
            var _resetters = FindAssetsByType<ResetterSO>();

            foreach (var _item in _resetters)
            {
                _item.ResetValues();
            }

            PlayGame();
        }

        public static IEnumerable<T> FindAssetsByType<T>() where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

            foreach (var t in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(t);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset != null)
                {
                    yield return asset;
                }
            }
        }
    }
}
