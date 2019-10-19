using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowTag : MonoBehaviour {

    public Transform Player;                //玩家
    public GameObject Tag;                  //标签组件
    public string Content;                  //物品的描述（初始赋值时用）
    public Vector3 TagPos = new Vector3(0, 0, 0);   //Tag的偏移位置

    private GameObject Tag1;                //标签实例化组件
    private readonly float FadeTime = 0.2f; //玩家离开时标签慢慢消失消耗的时间

    private void OnCollisionEnter2D(Collision2D c) {
        if (c.transform == Player) {
            Tag.GetComponentInChildren<Text>().text = Content;
            Tag1 = Instantiate(Tag, Vector3.zero, new Quaternion(0, 0, 0, 0)); //生成对象
            Tag1.GetComponentInChildren<Image>().DOColor(new Color(1, 1, 1, 1), FadeTime).SetEase(Ease.Linear);
            Tag1.GetComponentInChildren<Text>().DOColor(new Color(1, 1, 1, 1), FadeTime).SetEase(Ease.Linear);

            Tag1.GetComponentInChildren<Image>().transform.localPosition = Camera.main.WorldToScreenPoint(transform.position + TagPos) - new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Tag1.GetComponentInChildren<Text>().transform.localPosition = Camera.main.WorldToScreenPoint(transform.position + TagPos) - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D c) {
        if (c.transform == Player) {
            Destroy(Tag1, 0.2f);
            Tag1.GetComponentInChildren<Image>().DOColor(new Color(1, 1, 1, 0), FadeTime).SetEase(Ease.Linear);
            Tag1.GetComponentInChildren<Text>().DOColor(new Color(1, 1, 1, 0), FadeTime).SetEase(Ease.Linear);
        }
    }
}