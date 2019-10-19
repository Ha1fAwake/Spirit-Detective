using UnityEngine;

public class Back : MonoBehaviour {

    private Searchs S = new Searchs();
    bool IsBack = false;

    private void Start() {
        
    }

    private void Update() {
        if (S.IsReturn()) IsBack = true;

        if(IsBack) {
            transform.localScale -= S.GetAddScale();
            if (transform.localScale.x <= 0) {
                S.InitNum();
                GameObject.Destroy(gameObject);
            }
        }
        else transform.localScale += S.GetAddScale();

        if (S.GetKeyDown2()) {
            S.InitNum();
            GameObject.Destroy(gameObject);
        }
        S.SetBackScale(transform.localScale.x);
    }


}