using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FrameControl : MonoBehaviour {

    public int Position;
    public Transform Frame;
    private CubeData data = new CubeData();

    void Start() {
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(OnClick);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.PointerClick;
        myclick.callback.AddListener(click);
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Add(myclick);
    }

    public void OnClick(BaseEventData d) {
        data.AddClickNum();
        Frame.DOMove(transform.position, 0.2f);
        Frame.DORotate(data.GetClickNum() * new Vector3(0, 0, 90), 0.2f);
        data.SetFramePos(Position);
    }
}