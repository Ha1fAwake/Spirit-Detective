using System;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace LudumDare.Model {

    public class ItemMgr : ScriptableObject {
        #region Editor

#if UNITY_EDITOR
        [MenuItem("Create/Describe")]
        public static void CreateAsset() {

            var path = "Assets/Resources";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            AssetDatabase.CreateAsset(CreateInstance<ItemMgr>(), path + "/Describe.asset");
            AssetDatabase.Refresh();

            Selection.activeObject = AssetDatabase.LoadAssetAtPath<ItemMgr>(path + "/Describe.asset");
        }
#endif

        #endregion

        #region 单例

        private static ItemMgr _instance;
        public static ItemMgr Instance {
            get {
                if (!_instance) {
   
                    _instance = Resources.Load<ItemMgr>("Describe");
                }
#if UNITY_EDITOR
                if (!_instance) {
                    var path = "Assets/Resources";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    _instance = CreateInstance<ItemMgr>();
                    AssetDatabase.CreateAsset(_instance, path + "/Describe.asset");
                }
#endif
                if (_instance == null)
                    throw new Exception("初始化失败");

                return _instance;
            }
        }

        #endregion

        public static BasicItem GetItem(int id) {
            foreach (var VARIABLE in ItemMgr.Instance.itemInfos) {
                if (VARIABLE.id == id)
                    return VARIABLE;
            }

            throw new Exception("没有这个ID的物体:" + id);
        }
        public static BasicItem GetItem(string name) {
            foreach (var VARIABLE in Instance.itemInfos) {
                if (VARIABLE.ItemName == name)
                    return VARIABLE;
            }

            throw new Exception("没有这个ID的物体：" + name);
        }

        public int GetID() {
            var index = 0;
            while (true) {
                var ok = true;
                foreach (var unit in itemInfos) {
                    if (index == unit.id) {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    break;
                index++;
            }

            return index;
        }
        public List<BasicItem> itemInfos = new List<BasicItem>();
        
    }
}