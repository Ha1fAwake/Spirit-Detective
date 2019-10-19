using UnityEngine;

public class Point : MonoBehaviour {

    private Searchs S = new Searchs();
    private float CountTime=0;
    void Start() {
        
    }

    void Update() {
        CountTime += Time.deltaTime;
        if (CountTime >= S.GetPointTime()) GameObject.Destroy(gameObject);
    }

}