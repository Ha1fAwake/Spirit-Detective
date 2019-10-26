using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCheck : MonoBehaviour {

    public KeyCode check = KeyCode.Space;
    private bool triggerEnter = false;
    private Collider2D TriggerObject;
    public Text triggerName;
    public Text triggerDescribe;
    public Image backGround;
    [Range(0,2.0f)]
    public float showTime = 0.3f;
    private bool isShowing = false;


    void Update() {
        if (Input.GetKeyDown(check) && triggerEnter) {
            if (isShowing) {
                HideDescribe();
            }
            else {
                ShowDescribe(TriggerObject.GetComponent<ObjectIdentity>().objectName, TriggerObject.GetComponent<ObjectIdentity>().describe);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.transform.tag == "DescribeAble") {
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
        isShowing = true;
        triggerName.text = name;
        triggerDescribe.text = describe;
        triggerName.DOColor(new Color(1, 1, 1, 1), showTime);
        triggerDescribe.DOColor(new Color(1, 1, 1, 1), showTime);
        backGround.DOColor(new Color(1, 1, 1, 1), showTime);
    }

    private void HideDescribe() {
        isShowing = false;
        triggerName.DOColor(new Color(1, 1, 1, 0), showTime);
        triggerDescribe.DOColor(new Color(1, 1, 1, 0), showTime);
        backGround.DOColor(new Color(1, 1, 1, 0), showTime);
    }

}