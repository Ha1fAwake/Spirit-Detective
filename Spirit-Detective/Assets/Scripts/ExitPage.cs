using UnityEngine;

public class ExitPage : MonoBehaviour {

    public void OnClickSure() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); //退出游戏
        #endif
    }

}