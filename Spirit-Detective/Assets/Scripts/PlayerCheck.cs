using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCheck : MonoBehaviour {

    public KeyCode check = KeyCode.Space;
    public bool MoveWhenCheck = false;
    [Range(0, 2.0f)]
    public float showTime = 0.3f;
    public Text triggerName;
    public Text triggerDescribe;
    public Image backGround;

    private Collider2D TriggerObject;
    private bool triggerEnter = false;
    private bool isShowing = false;

    void Update() {
        InputChecking();    //输入检测
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.transform.tag == "DescribeAble") {    //所有可交互物体应属于此tag
            TriggerObject = c;
            triggerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D c) {
        if (c.transform.tag == "DescribeAble") {
            HideDescribe();
            triggerEnter = false;
        }
    }

    private void ShowDescribe(string name, string describe) {
        if (!MoveWhenCheck) {
            GetComponent<PlayerControl>().enabled = false;
        }
        isShowing = true;
        triggerName.text = name;
        triggerDescribe.text = describe;
        triggerName.DOColor(new Color(1, 1, 1, 1), showTime);
        triggerDescribe.DOColor(new Color(1, 1, 1, 1), showTime);
        backGround.DOColor(new Color(1, 1, 1, 1), showTime);
    }

    private void HideDescribe() {
        if (!MoveWhenCheck) {
            GetComponent<PlayerControl>().enabled = true;
        }
        isShowing = false;
        triggerName.DOColor(new Color(1, 1, 1, 0), showTime);
        triggerDescribe.DOColor(new Color(1, 1, 1, 0), showTime);
        backGround.DOColor(new Color(1, 1, 1, 0), showTime);
    }

    public void InputChecking() {
        if (Input.GetKeyDown(check)) {
            OnClickCheck();
        }
    }

    public void OnClickCheck() {
        if (triggerEnter) {
            if (isShowing) {
                HideDescribe();
            }
            else {
                string name, describe;
                if (TriggerObject.GetComponent<ObjectIdentity>()) {
                    name = TriggerObject.GetComponent<ObjectIdentity>().objectName;
                    describe = TriggerObject.GetComponent<ObjectIdentity>().describe;
                }
                else {
                    print("该物体没有Identity脚本");
                    return;
                }
                ShowDescribe(name, describe);
            }
        }
    }

}