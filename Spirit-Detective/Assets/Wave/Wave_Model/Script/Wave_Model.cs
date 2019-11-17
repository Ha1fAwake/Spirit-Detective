using UnityEngine;

public class Wave_Model : MonoBehaviour {

    private Searchs S = new Searchs();
    public GameObject Player;   //玩家
    public GameObject Back, Wave, Black, RedPoint;  //背景圆盘，波纹，黑背景，红点
    private Vector3 PlayerPos;
    private float SkillFreezeTime = 2.0f;
    private float SkillTime = 2.0f;

    private void Start() {
        PlayerPos = Player.transform.position + new Vector3(0, 0.3f, 0);
        S.SetPointPos(new Vector3(4, 1, 0));    //调试用代码,正式使用时可删去
    }

    private void Update() {
        SkillTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A)&& S.GetKeyAble()) {
            if (SkillTime > SkillFreezeTime) {
                SkillTime = 0;
                S.SetKeyDown();
                S.SetPlayerPos(PlayerPos);
            }
        }
        if (S.GetKeyDown1()) {
            S.AddCountTime(Time.deltaTime);
            S.CreateBlack(Black, PlayerPos);
            S.CreateBack(Back, PlayerPos);
            S.CreateWave(Wave, PlayerPos);
            if (!S.GetShowPoint()) S.CreateRedPoint(RedPoint, S.GetPointPos());
            if (S.GetWaveNum() == 0) S.SetKeyDown();
        }
        if (S.GetKeyDown2()) {
            if (S.InRange()) {
                S.SetTargetPointState(true);
                S.CreateRedPoint2(RedPoint, S.GetPointPos());
            }
        }
    }

}