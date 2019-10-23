using UnityEditor;
using UnityEngine;

public class Read : MonoBehaviour {

    void Start() {
        var idAsset = AssetDatabase.LoadAssetAtPath<ObjectId>("Assets/ObjectId.asset");
    }
}