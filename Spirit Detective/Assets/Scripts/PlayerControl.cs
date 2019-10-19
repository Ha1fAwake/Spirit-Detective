using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private Animator anim;
    private char faceDirection = 'f';   // 正面是f，背面是b，左面是l，右面是r
    private float last_xdre;            // 解决Blend Tree优先级问题
    private float last_ydre;

    [SerializeField]
    [Range(0.5f, 10.0f)]
    private float moveSpeed = 3f;

    //摇杆
    public Image Point;
    public Image Ring;
    private Vector2 StartPos, EndPos;
    [Range(50.0f, 500.0f)]
    public float PointRange = 200;

    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        var xDre = Input.GetAxisRaw("Horizontal");
        var yDre = Input.GetAxisRaw("Vertical");
        Move(xDre, yDre);

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

    private void Move(float xDre, float yDre) {
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
            // 若按下方向键，即动画控制器中的move变量为true
            anim.SetBool("move", true);
        }
        else anim.SetBool("move", false);
        // 不论idle还是walk状态，都使用这两个值确定某方向的动画
        anim.SetFloat("xdre", last_xdre);
        anim.SetFloat("ydre", last_ydre);
    }

    public void BeginDrag() {    //开始拖拽摇杆
        StartPos = Input.mousePosition;
    }

    public void Drag() {    //拖拽摇杆
        EndPos = Input.mousePosition;
        Vector3 Pos = EndPos - StartPos;
        if (Vector3.Distance(Pos, Vector3.zero) > PointRange) {
            Pos = Pos.normalized * PointRange;
        }
        Point.transform.localPosition = Ring.transform.localPosition + Pos;
        Pos /= 150.0f;
        Move(Pos.x, Pos.y);
        
    }
    public void EndDrag() {
        Point.transform.localPosition = Ring.transform.localPosition;
    }
   
}