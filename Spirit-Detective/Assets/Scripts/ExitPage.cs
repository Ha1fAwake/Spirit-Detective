using UnityEngine;

public class ExitPage : MonoBehaviour {

    public void OnClickSure() {
        #if UNITY_EDITOR    //unity编辑器
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); //退出程序
        #endif
    }

}