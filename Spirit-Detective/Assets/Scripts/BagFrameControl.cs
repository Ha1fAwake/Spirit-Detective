using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using LudumDare.Model;

public class BagFrameControl : MonoBehaviour {

    public Transform Frame;
    public int pos; //位置序号
    public Image image;
    public Text name, describe;
    public Sprite nullSprite;
    public static int angle = 0;

    void Start() {
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(OnClick);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.PointerClick;
        myclick.callback.AddListener(click);
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Add(myclick);
    }

    public void OnClick(BaseEventData d) {
        BagData.FramePos = pos;
        UpdateDescription();
        Frame.DOMove(transform.position, 0.1f);
        angle += 90;
        angle %= 360;
        Frame.DORotate(new Vector3(0, 0, angle), 0.1f);
    }

    public void UpdateDescription() {
        if (BagData.BagContentId[BagData.FramePos] == -1) {
            image.sprite = nullSprite;
            name.text = null;
            describe.text = null;
        }
        else {
            image.sprite = ItemMgr.GetItem(BagData.BagContentId[BagData.FramePos]).sprite;
            name.text = ItemMgr.GetItem(BagData.BagContentId[BagData.FramePos]).ItemName;
            describe.text = ItemMgr.GetItem(BagData.BagContentId[BagData.FramePos]).descriptions;
        }
    }
}