using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StickControlAddCheck : MonoBehaviour {

    public Transform Frame;
    public Transform Stick1, Stick2;
    public Image[] image = new Image[9];
    public Sprite[] sprite = new Sprite[9];
    private CubeData data = new CubeData();

    public void OnClickAdd1() {
        Stick1.DOLocalRotate(data.GetStick1Pos() * new Vector3(0, 0, -72), 0.5f).SetEase(Ease.OutBounce);
        data.AddStick1Pos();
    }
    
    public void OnClickAdd2() {
        Stick2.DOLocalRotate(data.GetStick2Pos() * new Vector3(0, 0, -72), 0.5f).SetEase(Ease.OutBounce);
        data.AddStick2Pos();
    }

    public void OnClickSure() {
        if (data.CheckTrue()) {
            data.AddClickNum();
            Frame.DORotate(data.GetClickNum() * new Vector3(0, 0, 90), 0.2f);
            image[data.GetFramePos()].sprite = sprite[data.GetFramePos()];
            image[data.GetFramePos()].DOColor(new Color(1, 1, 1, 1), 0.5f);
        }
    }
}