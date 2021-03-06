//using System.IO;
//using UnityEngine;
using UnityEditor;

namespace Assets.Editor.FlexibleUI
{
    [CanEditMultipleObjects]
    public class FlexibleUIEditor : UnityEditor.Editor
    {
        // functions that might be useful in the future, but don't really need them at the moment
        /*
        public string[] GetDataFilePaths(string dataPath, string searchPattern)
        {
            var path = dataPath;
            var pattern = searchPattern;
            return Directory.GetFiles(path, pattern);
        }

        public string[] GetDataOptions(string[] dataPaths)
        {
            var paths = dataPaths;
            var dataOptions = new string[paths.Length];

            for (var i = 0; i < dataOptions.Length; i++)
            {
                dataOptions[i] = Path.GetFileNameWithoutExtension(paths[i]);
            }

            return dataOptions;
        }

        // Function used to find all data of type <T> (Flexible UI) in the project.
        public T[] GetDataPresets<T>(string[] dataPaths) where T : ScriptableObject
        {
            var paths = dataPaths;
            var dataPresets = new T[paths.Length];

            for (var i = 0; i < dataPresets.Length; i++)
            {
                var targetData = AssetDatabase.LoadAssetAtPath<T>(paths[i]);
                dataPresets[i] = targetData;
            }

            return dataPresets;
        }
        */

        // Helper function to get inspected objects 
        protected T[] GetInspected<T>() where T : Scripts.FlexibleUI.FlexibleUI
        {
            var objects = targets;
            // initialize _Buttons array
            var inspected = new T[objects.Length];
            // get each object as FlexibleUI type and Call OnSkinUI();
            for (var i = 0; i < inspected.Length; i++)
            {
                inspected[i] = objects[i] as T;
            }

            return inspected;
        }
    }
}
