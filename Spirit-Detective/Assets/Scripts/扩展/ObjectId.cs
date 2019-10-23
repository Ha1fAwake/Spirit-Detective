using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(menuName = "ObjectId/Create Asset")]
public class ObjectId : ScriptableObject {


    [MenuItem("ObjectId/Create Asset")]
    static void CreateExampleAssetInstance() {
        var exampleAsset = CreateInstance<ObjectId>();
        AssetDatabase.CreateAsset(exampleAsset, "Assets/ObjectId.asset");
        AssetDatabase.Refresh();
    }
}