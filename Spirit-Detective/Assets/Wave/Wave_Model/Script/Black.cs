using UnityEngine;

public class Black : MonoBehaviour {

    private Searchs S = new Searchs();
    private SpriteRenderer Fade;
    private float CountTime = 0;    //结束时计时
    private float FadeTime = 0.8f;  //变淡时长
    private float ShowTime = 0.8f;  //出现时长
    private bool FadeMode = false;  //是否开始变淡
    private float CountStartTime = 0;   //出现时计时

    private void Start() {
        Fade = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        CountStartTime += Time.deltaTime;
        StartSlow(ShowTime);

        if (S.IsReturn() && transform.localScale.x <= 0) GameObject.Destroy(gameObject);
        //if (S.GetKeyDown2()) GameObject.Destroy(gameObject);
        if (!S.GetKeyDown1()|| S.GetKeyDown2()) {
            FadeMode = true;
            GameObject.Destroy(gameObject, FadeTime);
        }

        if (FadeMode) {
            CountTime += Time.deltaTime;
            SlowFade(FadeTime);
        }
    }

    private void SlowFade(float t) {   //变淡消失函数
        if (CountTime <= t) {
            float x = 1 - CountTime / t;
            Fade.color = new Color(1, 1, 1, x);
        }
    }

    private void StartSlow(float t) {   //慢慢出现函数
        if (CountStartTime <= t) {
            float x = CountStartTime / t;
            Fade.color = new Color(1, 1, 1, x);
        }
    }

}