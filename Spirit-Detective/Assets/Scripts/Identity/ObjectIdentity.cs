using Item.Model;
using ObjectName;
using UnityEngine;

public class ObjectIdentity : MonoBehaviour {

    public ConstStringChooser ObjectName;
    public virtual BasicItem ItemInfo => ItemMgr.GetItem(ObjectName.StringValue);

    [HideInInspector]
    public int id;
    [HideInInspector]
    public string objectName;
    [HideInInspector]
    public string describe;
    [HideInInspector]
    public Sprite sprite;
    [HideInInspector]
    public bool pickAble;

    private void Start() {
        id = ItemInfo.id;
        objectName = ItemInfo.ItemName;
        describe = ItemInfo.descriptions;
        sprite= ItemInfo.sprite;
        pickAble = ItemInfo.pickAble;
    }

}