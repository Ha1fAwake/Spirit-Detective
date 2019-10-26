using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace LudumDare.Model.Editor {

    [CustomEditor(typeof(ItemMgr))]
    public class ItemMgrEditor : UnityEditor.Editor {

        private int ListHeight = 4; //框的高度
        private Vector2 pos;
        private ReorderableList itemList;
        private SerializedProperty itemListProp;
        private ItemMgr mgr;

        private void OnEnable() {
            mgr = target as ItemMgr;
            itemListProp = serializedObject.FindProperty("itemInfos");
            itemList = new ReorderableList(serializedObject, itemListProp, false,
                true, true, true);
            itemList.elementHeight = ListHeight * EditorGUIUtility.singleLineHeight;
            itemList.drawElementCallback = (rect, index, x, y) => {
                var prop = itemListProp.GetArrayElementAtIndex(index++);
                EditorGUI.PropertyField(rect, prop);
            };
            itemList.onAddCallback = OnAddItem;
            itemList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "物品信息"); //标题栏
        }

        private void OnAddItem(ReorderableList list) {
            var size = itemListProp.arraySize;
            itemListProp.arraySize++;
            list.index = size;
            var itemProp = itemListProp.GetArrayElementAtIndex(size);
            itemProp.FindPropertyRelative("id").intValue = mgr.GetID();
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            pos = EditorGUILayout.BeginScrollView(pos);
            itemList.DoLayoutList();
            EditorGUILayout.EndScrollView();
            serializedObject.ApplyModifiedProperties();
        }
    }
}