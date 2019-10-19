using UnityEngine;

public class Wave : MonoBehaviour {

    private Searchs S = new Searchs();
    private int Num;    //波纹的编号
    private float WaveSpeed = 1.0f; //波纹超过的速度（超出边界然后消失）

    private SpriteRenderer Fade;
    private float CountTime = 0;
    private float MoveCountTime = 0;
    private float FadeTime1 = 2.0f; //击中或未击中时变淡时长
    private float FadeTime2 = 0.2f; //超出范围时变淡时长
    private float MoveTime = 0.3f; //移动时长
    private bool FadeMode1 = false;
    private bool FadeMode2 = false;
    private bool MoveMode = false;
    private bool IsBack = false;

    private void Start() {
        Fade = GetComponent<SpriteRenderer>();
        Num = S.GetWaveNum();
    }

    void Update() {
        if (S.IsReturn()) IsBack = true;

        if (IsBack) {
            transform.localScale -= S.GetAddScale();
            if (transform.localScale.x <= 0) {
                S.MinusWaveNum();
                GameObject.Destroy(gameObject);
            }
        }
        else {
            if (!FadeMode1) transform.localScale += S.GetAddScale() * WaveSpeed;
        }

        if (S.GetKeyDown2()) {
            FadeMode1 = true;
            MoveMode = true;
            GameObject.Destroy(gameObject, FadeTime1);
        }
        if (!S.GetKeyDown1()) {
            FadeMode1 = true;
            MoveMode = true;
            GameObject.Destroy(gameObject, FadeTime1);
        }
        if (transform.localScale.x > S.GetBackScale()) {
            FadeMode2 = true;
            GameObject.Destroy(gameObject, FadeTime2);
        }

        if (FadeMode1) {
            CountTime += Time.deltaTime;
            SlowFade(FadeTime1);
            transform.localScale -= S.GetAddScale();
        }
        else if (FadeMode2) {
            CountTime += Time.deltaTime;
            SlowFade(FadeTime2);
        }


        if (MoveMode && S.GetTargetPointState()) {
            MoveCountTime += Time.deltaTime;
            MoveToPoint();
        }

        #region 波纹花样方案

        float i = 2.0f;   //方案1(平面交错旋转)
        if (Num % 3 == 0) transform.Rotate(0, 0, i);
        else if (Num % 3 == 1) transform.Rotate(0, 0, -i);


        //float i = 3.0f;    //方案2（立体十字旋转）
        //if (Num % 2 == 0) transform.Rotate(0, i, 0);
        //else if (Num % 2 == 1) transform.Rotate(i, 0, 0);


        //float i = 3.0f;    //方案3（立体斜十字螺旋）
        //float j = 1.0f;
        //if (Num % 2 == 0) transform.Rotate(i, 0, j);
        //else transform.Rotate(0, i, j);


        //float i = 3.0f;    //方案4（平面抖动涡轮）
        //float j = 10.0f;
        //if (Num % 2 == 0) transform.Rotate(i, 0, j);
        //else transform.Rotate(0, i, j);

        //float i = 2.0f;    //方案5（规则的眼花缭乱）
        //if (Num % 4 == 0) transform.Rotate(i, 0, 0);
        //else if (Num % 4 == 1) transform.Rotate(i, i, 0);
        //else if (Num % 4 == 2) transform.Rotate(-i, i, 0);
        //else if (Num % 4 == 3) transform.Rotate(0, i, 0);

        //float i = 1.0f;    //方案6（规则的眼花缭乱2）
        //float j = 1.0f;
        //if (Num % 4 == 0) transform.Rotate(i, 0, j);
        //else if (Num % 4 == 1) transform.Rotate(i, i, -j);
        //else if (Num % 4 == 2) transform.Rotate(-i, -i, j);
        //else if (Num % 4 == 3) transform.Rotate(0, i, -j);

        #endregion

    }

    private void SlowFade(float t) {   //变淡消失函数

        #region 变淡方案

        //float x = 200 / 255.0f - CountTime / t;    //方案1（普通变暗）
        //if (x < 0) x = 0;
        //Fade.color = new Color(1, 1, 1, x);

        if (Num % 2 == 0) {   //方案2（奇偶交错变暗）
            float x = 200 / 255.0f - CountTime / t;
            if (x < 0) x = 0;
            Fade.color = new Color(1, 1, 1, x);
        }
        else {
            float x = 200 / 255.0f - CountTime * 1.3f / t;
            if (x < 0) x = 0;
            Fade.color = new Color(1, 1, 1, x);
        }

        #endregion

    }

    private void MoveToPoint() {    //成功命中后朝目标点移动
        
        if (MoveCountTime <= MoveTime) {
            Vector2 v3 = S.GetPlayerPos() + (S.GetPointPos() - S.GetPlayerPos()) * (MoveCountTime /MoveTime);
            transform.position = new Vector3(v3.x, v3.y, transform.position.z);
        }
        
    }

}