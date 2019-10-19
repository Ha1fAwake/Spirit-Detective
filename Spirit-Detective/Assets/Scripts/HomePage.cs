using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour {

    #region 变量
    //选项按钮
    public Button Play;
    public Button ReadData;
    public Button Setting;
    public Button Exit;

    //选项面板
    public Transform HomeButton;
    public Image ExitButton;

    //标题文字
    public Image Title;
    public Image AnyKey;

    //任意键
    private bool IsClick;       //是否已按任意键
    private bool IsFade;        //任意键字样是否在变淡
    private float Alpha;        //任意键字样的透明度
    private float LowAlpha;     //若隐若现最低值
    private float HighAlpha;    //若隐若现最高值
    private float FadeSpeed;    //字样显隐速度

    //颜色
    private Color White;    //白色
    private Color Empty;    //透明
    #endregion

    private void Start() {
        IsClick = false;
        IsFade = false;
        LowAlpha = 0.2f;
        Alpha = 0;
        HighAlpha = 0.9f;
        FadeSpeed = 0.01f;
        White = new Color(1, 1, 1, 1);
        Empty = new Color(1, 1, 1, 0);

        SetButton(false);
        SetAlphaAndPosition();
        ShowTitle();
    }

    private void Update() {
        ShowAnyKey();   //任意键若隐若现
        ShowOptions();  //点击触发选项
    }

    private void ShowOptions() {    //监听任意键后显示选项
        if (!IsClick) {
            if (Title.GetComponent<Image>().color.a == 1) {
                if (Input.anyKey || Input.GetMouseButtonDown(0)) {
                    IsClick = true;
                    Destroy(AnyKey);
                    Invoke("SetButton", 0.5f);
                    AnyKey.GetComponent<Image>().color = Empty;
                    Play.GetComponent<Image>().DOColor(White, 1.0f);
                    ReadData.GetComponent<Image>().DOColor(White, 1.0f);
                    Setting.GetComponent<Image>().DOColor(White, 1.0f);
                    Exit.GetComponent<Image>().DOColor(White, 1.0f);
                }
            }
        }
    }

    private void ShowAnyKey() { //任意按键提示字样的闪烁
        if (!IsClick) {
            if (Title.GetComponent<Image>().color.a >=0.8f) {
                if (!IsFade) Alpha += FadeSpeed;
                else Alpha -= FadeSpeed;
                if (Alpha >= HighAlpha) IsFade = true;
                if (Alpha <= LowAlpha) IsFade = false;
                AnyKey.GetComponent<Image>().color = new Color(1, 1, 1, Alpha);
            }
        }
    }

    #region 选项函数
    public void OnClickPlay() {     //按下开始游戏
        ClearOption();
        Invoke("LoadClassRoom", 0.8f);
    }

    public void OnClickReadData() { //按下读取存档
        ClearOption();
    }

    public void OnClickSetting() {  //按下设置选项
        ClearOption();
    }

    public void OnClickExit() {     //按下离开游戏
        ClearOption();
        //ExitButton.transform.DOMoveX(Screen.width / 2, 0.8f).SetEase(Ease.InOutExpo);
        ExitButton.transform.DOMoveX(0, 0.8f).SetEase(Ease.InOutExpo);
    }

    public void OnClickCancel() {
        ShowOption();
        //ExitButton.transform.DOMoveX(1300 + Screen.width / 2, 0.8f).SetEase(Ease.OutExpo);
        ExitButton.transform.DOMoveX(15, 0.8f).SetEase(Ease.OutExpo);
    }
    #endregion

    #region 基本操作函数

    private void SetButton() {    //设置按钮默认可以被按下
        Play.enabled = true;
        ReadData.enabled = true;
        Setting.enabled = true;
        Exit.enabled = true;
    }

    private void SetButton(bool x) {    //设置按钮是否可以被按下（重载）
        Play.enabled = x;
        ReadData.enabled = x;
        Setting.enabled = x;
        Exit.enabled = x;
    }

    private void SetAlphaAndPosition() {   //设置物体的透明度
        Play.GetComponent<Image>().color = Empty;
        ReadData.GetComponent<Image>().color = Empty;
        Setting.GetComponent<Image>().color = Empty;
        Exit.GetComponent<Image>().color = Empty;
        Title.GetComponent<Image>().color = Empty;
        AnyKey.GetComponent<Image>().color = Empty;
        //Title.transform.position = new Vector3(Screen.width / 2, 800 + Screen.height / 2, 0);
        Title.transform.position = new Vector3(0, 8, 0);
    }

    private void ClearOption() {    //按下选项后清空屏幕中的无关元素
        //Title.transform.DOMoveY(800 + Screen.height / 2, 0.5f).SetEase(Ease.InBack);
        //HomeButton.DOMoveY(-800 + Screen.height / 2, 0.5f).SetEase(Ease.InBack);
        Title.transform.DOMoveY(8, 0.5f).SetEase(Ease.InBack);
        HomeButton.DOMoveY(-8, 0.5f).SetEase(Ease.InBack);
    }

    private void ShowOption() {    //返回选项后还原屏幕中的主选项元素
        // Title.transform.DOMoveY(150 + Screen.height / 2, 0.5f).SetEase(Ease.OutBack);
        //HomeButton.DOMoveY(-300 + Screen.height / 2, 0.5f).SetEase(Ease.OutBack);
        Title.transform.DOMoveY(1.5f, 0.5f).SetEase(Ease.OutBack);
        HomeButton.DOMoveY(-3, 0.5f).SetEase(Ease.OutBack);
    }

    private void ShowTitle() {  //显示标题
        //Title.transform.DOMoveY(150 + Screen.height / 2, 1.5f).SetEase(Ease.OutQuart);
        Title.transform.DOMoveY(1.5f, 1.5f).SetEase(Ease.OutQuart);
        Title.GetComponent<Image>().DOColor(White, 2.0f).SetEase(Ease.Linear);
    }

    private void LoadClassRoom() {
        SceneManager.LoadScene("Classroom1");
    }
    #endregion

}