using LudumDare.Model;
using ObjectName;
using UnityEngine;

public class ObjectIdentity : MonoBehaviour {

    public ConstStringChooser ObjectName;
    public virtual BasicItem ItemInfo => ItemMgr.GetItem(ObjectName.StringValue);

    [HideInInspector]
    public string objectName;
    [HideInInspector]
    public string describe;

    private void Start() {
        objectName = ItemInfo.ItemName;
        describe = ItemInfo.descriptions;
    }

}