using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ObjectName {
    public class ConstStringAsset : ScriptableObject {
        private static ConstStringAsset _instance;
        public static ConstStringAsset Instance {
            get {
                if (!_instance) {
                    _instance = Resources.Load<ConstStringAsset>("ObjectName");
                }
#if UNITY_EDITOR
                if (!_instance) {
                    _instance = CreateInstance<ConstStringAsset>();
                    var path = "Assets/Resources";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    AssetDatabase.CreateAsset(_instance, path + "/ObjectName.asset");
                }
#endif
                if (_instance == null)
                    throw new System.Exception("初始化失败");

                return _instance;
            }
        }

        public List<string> constStrings = new List<string>();
    }
}