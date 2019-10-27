using UnityEngine;
using DG.Tweening;

public class ToolsBar : MonoBehaviour {

    public enum ShowMethod {
        Move,
        Rotate
    }
    public GameObject toolsBar;
    public GameObject triangle;
    public ShowMethod showMethod = ShowMethod.Rotate;   //工具栏出现方式
    [Range(0.01f, 1.5f)]
    public float ShowTime = 0.3f;
    private bool isFold = true;

    public void OnClickTriangle() {
        if (showMethod == ShowMethod.Move) {
            if (isFold) {
                if (Screen.width < 1920) {
                    toolsBar.transform.DOMoveX(450 * Screen.width / 1920.0f, ShowTime);
                }
                else if (Screen.height > 1080) {
                    toolsBar.transform.DOMoveX(450 * Screen.height / 1080.0f, ShowTime);
                }
                else {
                    toolsBar.transform.DOMoveX(450, ShowTime);
                }
                triangle.transform.DORotate(new Vector3(0, 0, 180), ShowTime);
                isFold = false;
            }
            else {
                if (Screen.width < 1920) {
                    toolsBar.transform.DOMoveX(50 * Screen.width / 1920.0f, ShowTime);
                }
                else if (Screen.height > 1080) {
                    toolsBar.transform.DOMoveX(50 * Screen.height / 1080.0f, ShowTime);
                }
                else {
                    toolsBar.transform.DOMoveX(50, ShowTime);
                }
                triangle.transform.DORotate(new Vector3(0, 0, 0), ShowTime);
                isFold = true;
            }
        }
        if (showMethod == ShowMethod.Rotate) {
            if (isFold) {
                toolsBar.transform.DORotate(new Vector3(0, 0, 180), ShowTime);
                isFold = false;
            }
            else {
                toolsBar.transform.DORotate(new Vector3(0, 0, 0), ShowTime);
                isFold = true;
            }
        }
    }

    public void OnClickBag() {

    }
}