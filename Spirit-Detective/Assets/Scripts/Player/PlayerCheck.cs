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
    [Range(0.1f, 1.5f)]
    public float effectTime = 0.5f;
    [Range(0.5f, 4.0f)]
    public float effectDistence = 1.5f;

    private Collider2D TriggerObject;
    private bool triggerEnter = false;
    private bool isShowing = false;


    void Update() {
        if (Input.GetKeyDown(check)) //输入检测
            OnClickCheck();
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

    public void OnClickCheck() {
        if (triggerEnter) {
            bool pickAble = TriggerObject.GetComponent<ObjectIdentity>().pickAble;
            if (pickAble) { //捡起物品
                BagData.AddItem(TriggerObject.GetComponent<ObjectIdentity>().id);   //添加物品
                PickEffect();   //捡起效果
            }
            else {  //查看物品
                if (isShowing) {    //收起描述
                    HideDescribe();
                }
                else {  //展开描述
                    string name, describe;
                    if (TriggerObject.GetComponent<ObjectIdentity>()) { //挂了ID脚本
                        name = TriggerObject.GetComponent<ObjectIdentity>().objectName;
                        describe = TriggerObject.GetComponent<ObjectIdentity>().describe;
                        ShowDescribe(name, describe);
                    }
                    else {  //没挂脚本
                        Debug.Log("该物体没有Identity脚本");
                    }
                }
            }
        }
    }

    public void PickEffect() {
        float y = TriggerObject.transform.position.y;
        if (TriggerObject.GetComponent<CircleCollider2D>())
            TriggerObject.GetComponent<CircleCollider2D>().enabled = false;
        if (TriggerObject.GetComponent<BoxCollider2D>())
            TriggerObject.GetComponent<BoxCollider2D>().enabled = false;
        if(TriggerObject.GetComponent<Animation>())
            TriggerObject.GetComponent<Animation>().enabled = false;
        TriggerObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        TriggerObject.transform.DOMoveY(y + effectDistence, effectTime);
        TriggerObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), effectTime);
        Destroy(TriggerObject.gameObject, effectTime);  //销毁物体
    }
}