using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ControlText : MonoBehaviour {

    public bool clickAble = true;   //是否可通过点击屏幕提前显示下一句
    public Text text;               //显示语句的文本
    public string[] contents;       //语句内容
    [Range(0.02f, 0.5f)]
    public float playSpeed = 0.12f; //语句显示速度（0.12秒/字）
    [Range(1.0f, 5.0f)]
    public float shakeRange = 2.0f;    //震动幅度
    [Range(0.5f, 5.0f)]
    public float beginTime = 2.0f;  //加载场景后播放第一句的等待时间
    [Range(0.5f, 5.0f)]
    public float waitTime = 2.0f;   //语句结束后播放下一句的等待时间
    [Range(0.5f, 5.0f)]
    public float endTime = 2.0f;    //播放完最后第一句后等待时间

    private float countBeginTime = 0;
    private float countWaitTime = 0;
    private int textLength = 0;
    private bool isPause = false;
    private int currentNum = 0;
    private bool[] Showed = new bool[10];
    private float countIndex = 0;

    void Start() {
        for (int i = 0; i < contents.Length; i++) {
            if (contents[i] == "") {
                textLength = i;
                break;
            }
        }
        if (contents[contents.Length - 1] != "") {
            textLength = contents.Length;
        }
    }

    void Update() {
        if (countBeginTime <= beginTime) {
            countBeginTime += Time.deltaTime;
        }
        else {
            countWaitTime += Time.deltaTime;
            if (clickAble && Input.anyKey) {
                if (isPause) {
                    isPause = false;
                    currentNum++;
                }
            }
            if (countWaitTime >= waitTime && isPause) {
                isPause = false;
                currentNum++;
            }
            if (!isPause && !Showed[currentNum]) {
                Showed[currentNum] = true;
                Invoke("SetPause", contents[currentNum].Length * playSpeed);
                text.DOText(contents[currentNum], contents[currentNum].Length * playSpeed).SetEase(Ease.Linear);
                text.transform.DOShakePosition(contents[currentNum].Length * playSpeed + waitTime, new Vector3(shakeRange, shakeRange, 0), 60, 360, false, false);
            }
        }
    }

    private void SetPause() {
        isPause = true;
        countWaitTime = 0;
        if (currentNum == textLength - 1) { //结束，加载下一场景
            text.DOColor(new Color(1, 1, 1, 0), endTime);
            Invoke("LoadScene", endTime);
        }
    }

    private void LoadScene() {
        SceneManager.LoadScene("Classroom1");
    }
}