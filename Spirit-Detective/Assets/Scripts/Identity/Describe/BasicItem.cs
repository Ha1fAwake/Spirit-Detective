using System;
using ObjectName;
using UnityEngine;

namespace LudumDare.Model {

    [Serializable]
    public class BasicItem {
        public int id;
        public ConstStringChooser itemName;
        public string descriptions;
        public string ItemName => itemName.StringValue;
        public Sprite sprite;
        public bool pickAble = false;
    }
}