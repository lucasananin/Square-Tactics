#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utilities.Managers
{
    [CreateAssetMenu(fileName = "Resetter_Main", menuName = "SO/Managers/Resetter")]
    public class ResetterSO : ScriptableObject
    {
        //[Title("// Messages")]
        //[SerializeField, TextArea] string _logMessage = "Remove the \"ENABLE_LOG\" from the Player Settings.";
        //[SerializeField, TextArea] string _localizationMessage = "Build the Addressables before building.";

        private const string SAVE_PATH = "/saveFile";

        [Button(ButtonHeight = 40)]
        public void ResetValues()
        {
            ClearSave();
            Debug.Log($"// {name}.ResetValues()");
        }

        [Button(ButtonHeight = 40)]
        public void ClearSave()
        {
            PlayerPrefs.DeleteAll();
            File.Delete($"{Application.persistentDataPath}{SAVE_PATH}");
        }
    }
}
#endif
