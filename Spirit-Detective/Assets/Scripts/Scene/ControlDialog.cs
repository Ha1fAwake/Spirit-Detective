using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ControlDialog : MonoBehaviour {
    
    public bool clickAble = true;   //是否可通过点击屏幕提前显示下一句
    public bool Lerp = true;    //是否过渡显示文字
    public bool forcibleFastForward = false;    //强行快进
    public bool IsShake = false;    //是否晃动
    public bool clear = false;  //上下文之间是否清空
    public Text text;               //显示语句的文本
    public Text name;               //说话人
    public string[] contents, contents1;       //语句内容
    [Range(0.02f, 0.5f)]
    public float playSpeed = 0.12f; //语句显示速度（0.12秒/字）
    [Range(0.0f, 5.0f)]
    public float shakeRange = 2.0f; //震动幅度
    [Range(0.5f, 5.0f)]
    public float beginTime = 2.0f;  //加载场景后播放第一句的等待时间
    [Range(0.5f, 50000.0f)]
    public float waitTime = 2.0f;   //语句结束后播放下一句的等待时间
    [Range(0.5f, 5.0f)]
    public float endTime = 2.0f;    //播放完最后第一句后等待时间
    public string nextScene;

    private float countBeginTime = 0;
    private float countWaitTime = 0;
    private int textLength = 0;
    private bool isPause = false;
    private int currentNum = 0;
    private bool[] Showed = new bool[50];
    private float countIndex = 0;

    void Start() {
        for (int i = contents.Length - 1; i >= 0; i--) {
            if (contents[i] != "") {
                textLength = i + 1;
                break;
            }
        }
    }

    void Update() {
        if (countBeginTime <= beginTime) {
            countBeginTime += Time.deltaTime;
        }
        else {
            countWaitTime += Time.deltaTime;
            if (clickAble && Input.GetMouseButtonDown(0)) {
                if (isPause || forcibleFastForward) {
                    isPause = false;
                    if (currentNum < textLength - 1) currentNum++;
                }
            }
            if (countWaitTime >= waitTime && isPause) {
                isPause = false;
                if (currentNum < textLength - 1) currentNum++;
            }
            if (!isPause && !Showed[currentNum]) {
                Showed[currentNum] = true;

                if (Lerp) {
                    Invoke("SetPause", contents[currentNum].Length * playSpeed);
                    if (clear) {
                        text.text = "";
                        name.text = "";
                    }
                    text.DOText(contents[currentNum], contents[currentNum].Length * playSpeed).SetEase(Ease.Linear);
                    name.DOText(contents1[currentNum], 0.1f);
                }
                else {
                    SetPause();
                    text.text = contents[currentNum];
                    name.text = contents1[currentNum];
                }
                if (IsShake) text.transform.DOShakePosition(contents[currentNum].Length * playSpeed + waitTime, new Vector3(shakeRange, shakeRange, 0), 60, 360, false, false);
            }
        }
    }

    private void SetPause() {
        isPause = true;
        countWaitTime = 0;
        if (currentNum == textLength - 1) { //结束，加载下一场景
            text.DOColor(new Color(1, 1, 1, 0), endTime);
            name.DOColor(new Color(1, 1, 1, 0), endTime);
            Invoke("LoadScene", endTime);
        }
    }

    private void LoadScene() {
        if (nextScene != "")
            SceneManager.LoadScene(nextScene);
    }
}