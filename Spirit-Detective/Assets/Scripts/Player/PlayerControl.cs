using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private Animator anim;
    private char faceDirection = 'f';   // 正面是f，背面是b，左面是l，右面是r
    private float last_xdre;            // 解决Blend Tree优先级问题
    private float last_ydre;
    [Range(0.5f, 10.0f)]
    public float moveSpeed = 3f;

    //摇杆
    [Range(100, 300)]
    public float pointRange = 200;
    public Image point;
    public Image ring;
    private Vector2 startPos, endPos;

    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        var xDre = Input.GetAxisRaw("Horizontal");
        var yDre = Input.GetAxisRaw("Vertical");
        MoveAnimation(xDre, yDre);
        Move();
        ChangeFace();
    }

    private void Move() {
        if (Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.x < Screen.width / 2) {
                BeginDrag();
            }
        }
        if (Input.GetMouseButton(0)) {
            if (Input.mousePosition.x < Screen.width / 2) {
                Drag();
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            EndDrag();
        }
    }

    private void MoveAnimation(float xDre, float yDre) {
        if (yDre > 0) faceDirection = 'b';
        if (yDre < 0) faceDirection = 'f';
        if (xDre > 0) faceDirection = 'r';
        if (xDre < 0) faceDirection = 'l';
        GetComponent<Rigidbody2D>().velocity = new Vector3(xDre, yDre, 0) * moveSpeed;
        if (Mathf.Abs(xDre) > Mathf.Epsilon || Mathf.Abs(yDre) > Mathf.Epsilon) {
            // 判断是否按下方向键
            // 避免Blend Tree的默认动画播放
            last_xdre = xDre;
            last_ydre = yDre;
            if (xDre == yDre) yDre += 0.01f;    //防止相等导致判断方向出错
            // 若按下方向键，即动画控制器中的move变量为true
            anim.SetBool("move", true);
        }
        else anim.SetBool("move", false);
        // 不论idle还是walk状态，都使用这两个值确定某方向的动画
        anim.SetFloat("xdre", last_xdre);
        anim.SetFloat("ydre", last_ydre);
    }

    //控制摇杆
    public void BeginDrag() {    //开始拖拽摇杆
        startPos = Input.mousePosition;
    }

    public void Drag() {    //拖拽摇杆
        endPos = Input.mousePosition;
        Vector3 Pos = endPos - startPos;
        if (Vector3.Distance(Pos, Vector3.zero) > pointRange) {
            Pos = Pos.normalized * pointRange;
        }
        point.transform.localPosition = ring.transform.localPosition + Pos;
        Pos /= 150.0f;
        MoveAnimation(Pos.x, Pos.y);

    }

    public void EndDrag() {
        point.transform.localPosition = ring.transform.localPosition;
    }

    //修改主角身上的box触发器的位置，便于靠近物体后的物体检测
    public void ChangeFace() {
        if (Mathf.Abs(last_ydre) > Mathf.Abs(last_xdre)) {
            if (last_ydre > 0) {
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.35f);
            }
            else {
                GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.35f);
            }
        }
        else if (Mathf.Abs(last_ydre) < Mathf.Abs(last_xdre)) {
            if (last_xdre > 0) {
                GetComponent<BoxCollider2D>().offset = new Vector2(0.35f, 0);
            }
            else {
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.35f, 0);
            }
        }
    }

}