using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Item.Model;

public class ToolsBar : MonoBehaviour {

    #region 变量
    public enum ShowMethod {
        Move,
        Rotate
    }
    [Header("【工具条】")]
    public GameObject toolsBar;
    public GameObject triangle;
    public ShowMethod showMethod = ShowMethod.Move;   //工具栏出现方式
    [Range(0.01f, 1.5f)]
    public float toolsBarShowTime = 0.3f;   //显示的过渡时间
    public bool AutoFold = true;    //是否自动折叠
    [Range(1.0f, 6.0f)]
    public float foldTime = 3.0f;  //自动折叠计时
    private bool isFold = true;
    private int toolsNum = 3;
    private float countTime = 0;

    [Header("【背包】")]
    public GameObject bag;
    public Image[] item = new Image[9];  //物品栏
    public Sprite nullItem;
    private bool showBag = false;   //包包是否已打开
    [Range(0.05f, 1.5f)]
    public float bagShowTime = 0.1f;    //背包出现过渡时间

    [Header("【禁止操作】")]
    public bool couldOperate = false;
    public Transform player;
    public Transform checkButton;
    #endregion

    private void Start() {
        BagAlpha(0);
        bag.transform.DOMoveY(Screen.height / 2 + 1000, 0);
        bag.transform.DORotate(new Vector3(0, 0, 180), 0);
    }

    private void Update() {
        if (!isFold && AutoFold) {
            countTime += Time.deltaTime;
            if (countTime >= foldTime) {
                countTime = 0;
                OnClickTriangle();
            }
        }
    }

    #region 其它函数
    private void BagAlpha(float a) {
        float time = bagShowTime;
        if (a == 0) time /= 2.0f;
        Color color;
        foreach (Transform g in bag.transform) {
            if (g.GetComponent<Text>()) {
                color = g.GetComponent<Text>().color;
                color.a = a;
                g.GetComponent<Text>().DOColor(color, time);
            }
            else if (g.GetComponent<Image>()) {
                color = g.GetComponent<Image>().color;
                color.a = a;
                g.GetComponent<Image>().DOColor(color, time);
            }
            foreach (Transform g1 in g) {
                if (g1.GetComponent<Text>()) {
                    color = g1.GetComponent<Text>().color;
                    color.a = a;
                    g1.GetComponent<Text>().DOColor(color, time);
                }
                else if (g1.GetComponent<Image>()) {
                    color = g1.GetComponent<Image>().color;
                    color.a = a;
                    g1.GetComponent<Image>().DOColor(color, time);
                }
            }
        }
    }

    public void UpdateBagView() {
        for (int i = 0; i < 9; i++) {
            if (BagData.BagContentId[i] != -1) {
                item[i].sprite = ItemMgr.GetItem(BagData.BagContentId[i]).sprite;
            }
            else {
                item[i].sprite = nullItem;
            }
        }
    }


    #endregion

    #region 按钮点击事件
    public void OnClickTriangle() {
        countTime = 0;
        if (showMethod == ShowMethod.Move) {
            if (isFold) {
                if (Screen.width < 1920) {
                    toolsBar.transform.DOMoveX((toolsNum * 100 + 50) * Screen.width / 1920.0f, toolsBarShowTime);
                }
                else if (Screen.height > 1080) {
                    toolsBar.transform.DOMoveX((toolsNum * 100 + 50) * Screen.height / 1080.0f, toolsBarShowTime);
                }
                else {
                    toolsBar.transform.DOMoveX((toolsNum * 100 + 50), toolsBarShowTime);
                }
                triangle.transform.DORotate(new Vector3(0, 0, 180), toolsBarShowTime);
                isFold = false;
            }
            else {
                if (Screen.width < 1920) {
                    toolsBar.transform.DOMoveX(50 * Screen.width / 1920.0f, toolsBarShowTime);
                }
                else if (Screen.height > 1080) {
                    toolsBar.transform.DOMoveX(50 * Screen.height / 1080.0f, toolsBarShowTime);
                }
                else {
                    toolsBar.transform.DOMoveX(50, toolsBarShowTime);
                }
                triangle.transform.DORotate(new Vector3(0, 0, 0), toolsBarShowTime);
                isFold = true;
            }
        }
        if (showMethod == ShowMethod.Rotate) {
            if (isFold) {
                toolsBar.transform.DORotate(new Vector3(0, 0, 180), toolsBarShowTime);
                isFold = false;
            }
            else {
                toolsBar.transform.DORotate(new Vector3(0, 0, 0), toolsBarShowTime);
                isFold = true;
            }
        }
    }

    public void OnClickBag() {
        OnClickTriangle();
        bag.transform.DOMoveY(Screen.height / 2, bagShowTime);
        bag.transform.DORotate(new Vector3(0, 0, 0), bagShowTime);
        BagAlpha(1);
        if (!couldOperate) {
            player.GetComponent<PlayerControl>().enabled = false;
            player.GetComponent<PlayerCheck>().enabled = false;
            triangle.GetComponent<Button>().enabled = false;
            checkButton.GetComponent<Button>().enabled = false;
        }
        UpdateBagView();
    }

    public void OnClickCloseBag() {
        bag.transform.DOMoveY(Screen.height / 2 + 1000, bagShowTime);
        bag.transform.DORotate(new Vector3(0, 0, 180), bagShowTime);
        BagAlpha(0);
        if (!couldOperate) {
            player.GetComponent<PlayerControl>().enabled = true;
            player.GetComponent<PlayerCheck>().enabled = true;
            triangle.GetComponent<Button>().enabled = true;
            checkButton.GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickMenu() {

    }
    #endregion

}