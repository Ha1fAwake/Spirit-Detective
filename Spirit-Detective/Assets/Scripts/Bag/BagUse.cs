using UnityEngine;

public class BagUse : MonoBehaviour {

    public void OnClickUse() {
        if (BagData.SelectedItemId != -1) {
            BagData.UseItem();
        }
    }
}