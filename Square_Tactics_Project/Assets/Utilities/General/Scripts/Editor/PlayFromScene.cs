using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

[ExecuteInEditMode]
public class PlayFromScene : EditorWindow
{
    [SerializeField] string lastScene = "";
    [SerializeField] int targetScene = 0;
    [SerializeField] string waitScene = null;
    //[SerializeField] bool hasPlayed = false;

    [MenuItem("Edit/Play From Scene %0")]
    public static void Run()
    {
        GetWindow<PlayFromScene>();
    }

    static string[] sceneNames;
    static EditorBuildSettingsScene[] scenes;

    void OnEnable()
    {
        scenes = EditorBuildSettings.scenes;
        sceneNames = scenes.Select(x => AsSpacedCamelCase(Path.GetFileNameWithoutExtension(x.path))).ToArray();
    }

    void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            if (null == waitScene && !string.IsNullOrEmpty(lastScene))
            {
                EditorSceneManager.OpenScene(lastScene);
                lastScene = null;
            }
        }
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying)
        {
            if (EditorSceneManager.GetActiveScene().name == waitScene)
            {
                waitScene = null;
            }

            return;
        }

        if (EditorSceneManager.GetActiveScene().name == waitScene)
        {
            EditorApplication.isPlaying = true;
        }

        if (null == sceneNames) return;
        targetScene = EditorGUILayout.Popup(targetScene, sceneNames);
        if (GUILayout.Button("Play"))
        {
            lastScene = EditorSceneManager.GetActiveScene().name;
            waitScene = scenes[targetScene].path;
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(waitScene);
        }
    }

    public string AsSpacedCamelCase(string text)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(text.Length * 2);
        sb.Append(char.ToUpper(text[0]));
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                sb.Append(' ');
            sb.Append(text[i]);
        }

        return sb.ToString();
    }
}
