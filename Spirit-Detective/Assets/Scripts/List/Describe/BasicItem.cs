using System;
using ObjectName;

namespace LudumDare.Model {

    [Serializable]
    public class BasicItem {
        public int id;
        public ConstStringChooser itemName;
        public string descriptions;
        public string ItemName => itemName.StringValue;
    }
}