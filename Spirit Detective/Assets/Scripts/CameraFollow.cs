using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Player;

    void LateUpdate() {
        Vector3 Pos = Player.position;
        Pos.z = -10;
        transform.position = Pos;
    }
}