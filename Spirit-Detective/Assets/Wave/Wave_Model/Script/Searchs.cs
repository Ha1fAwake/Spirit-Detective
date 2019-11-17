using UnityEngine;

public class Searchs {

    #region 变量

    //模块内部使用变量
    private static bool ShowedPoint = false;   //是否已显示过红点
    private static float CountTime = 0;    //计时
    private static int WaveNum = 0; //波纹个数
    private static bool KeyDown1 = false;  //两次按键是否按下
    private static bool KeyDown2 = false;
    private static float BackScale; //圆盘尺寸（用于判断是否命中）

    private readonly static int PicTureSize = 1000;  //波纹素材尺寸（像素）
    private readonly static float DeltaTime = 0.5f; //产生波纹的时间间隔
    private readonly static float ReturnTime = 4;   //波纹收缩的时间点
    private readonly static float PointTime = 0.5f;    //红点出现时长
    private readonly static float Speed = 0.01f;    //波纹速度
    private readonly static float ASpeed = 0.002f;  //波纹加速度
    private readonly static float ErrorNum = 80f;  //命中误差范围


    //模块外部使用变量
    private static Vector3 PointPos;    //目标点的位置
    private static bool PressKey = true;    //搜索技能键是否被激活（按下是否有效）
    private static bool HitTargetPoint = false;  //是否已命中目标点（是否解锁目标）
    private static Vector3 PlayerPos;   //玩家的位置

    #endregion

    public Searchs() {

    }

    public Searchs(Vector3 x) {
        PointPos = x;
    }

    public void AddCountTime(float x) {
        CountTime += x;
    }

    public void SetCountTime(float x) {
        CountTime = x;
    }

    public float GetCountTime() {
        return CountTime;
    }

    public float GetDeltaTime() {
        return DeltaTime;
    }

    public float GetReturnTime() {
        return ReturnTime;
    }

    public void AddWaveNum() {
        WaveNum++;
    }

    public void MinusWaveNum() {
        WaveNum--;
    }

    public int GetWaveNum() {
        return WaveNum;
    }

    public void SetKeyAble(bool x) {
        PressKey = x;
    }

    public bool GetKeyAble() {
        return PressKey;
    }

    public void SetTargetPointState(bool x) {
        HitTargetPoint = x;
    }

    public bool GetTargetPointState() {
        return HitTargetPoint;
    }

    public void CreateBlack(GameObject g, Vector3 v) {
        v.z = 0;
        if (WaveNum == 0) {
            GameObject.Instantiate(g, v + new Vector3(0, 0, PlayerPos.z + 0.4f), new Quaternion(0, 0, 0, 0));
        }
    }

    public void CreateBack(GameObject g, Vector3 v) {
        v.z = 0;
        if (WaveNum == 0) {
            GameObject.Instantiate(g, v + new Vector3(0, 0, PlayerPos.z + 0.3f), new Quaternion(0, 0, 0, 0));
        }
    }

    public void CreateWave(GameObject g, Vector3 v) {
        v.z = 0;
        if (CountTime < ReturnTime && CountTime >= WaveNum * DeltaTime) {
            GameObject.Instantiate(g, v + new Vector3(0, 0, PlayerPos.z + 0.1f), new Quaternion(0, 0, 0, 0));
            WaveNum++;
        }
    }

    public void CreateRedPoint(GameObject g, Vector3 v) {
        v.z = 0;
        if (GetCircleR() > GetDis() && CountTime > 0.1f) {
            GameObject.Instantiate(g, v + new Vector3(0, 0, PlayerPos.z + 0.2f), new Quaternion(0, 0, 0, 0));
            ShowedPoint = true;
        }
    }

    public void CreateRedPoint2(GameObject g, Vector3 v) {
        v.z = 0;
        GameObject.Instantiate(g, v + new Vector3(0, 0, PlayerPos.z + 0.1f), new Quaternion(0, 0, 0, 0));
    }

    public Vector3 GetAddScale() {
        return new Vector3(Speed, Speed, 0);
    }

    public void SetKeyDown() {
        if (!KeyDown1 && !KeyDown2) {
            InitNum();
            KeyDown1 = true;
            return;
        }
        if (CountTime >= ReturnTime) {
            KeyDown1 = false;
            KeyDown2 = true;
            return;
        }

    }

    public bool GetKeyDown1() {
        return KeyDown1;
    }

    public bool GetKeyDown2() {
        return KeyDown2;
    }

    public bool IsReturn() {
        if (CountTime >= ReturnTime) return true;
        return false;
    }

    public void InitNum() {
        CountTime = 0;
        WaveNum = 0;
        //HitTargetPoint = false;
        //PressKey = true;
        KeyDown1 = false;
        KeyDown2 = false;
        ShowedPoint = false;
    }

    public void SetBackScale(float x) {
        BackScale = x;
    }

    public float GetBackScale() {
        return BackScale;
    }

    public float GetDis() {    //计算玩家和目标点的距离
        Vector2 Point = Camera.main.WorldToScreenPoint(PointPos);
        Vector2 Player = Camera.main.WorldToScreenPoint(PlayerPos);
        return Vector2.Distance(Point, Player);
    }

    public void SetPlayerPos(Vector3 Pos) {
        PlayerPos = Pos;
    }

    public Vector3 GetPlayerPos() {
        return PlayerPos;
    }

    public float GetCircleR() {
        return PicTureSize / 2 * BackScale;
    }

    public bool InRange() {
        if (GetCircleR() >= GetDis() - ErrorNum && GetCircleR() <= GetDis() + ErrorNum) return true;
        return false;
    }

    public void SetPointPos(Vector3 x) {
        PointPos = x;
    }

    public Vector3 GetPointPos() {
        return PointPos;
    }

    public float GetPointTime() {
        return PointTime;
    }

    public bool GetShowPoint() {
        return ShowedPoint;
    }

}