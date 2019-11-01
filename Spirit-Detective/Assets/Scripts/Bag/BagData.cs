public static class BagData {

    private static int[] bagContentId = new int[9] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    private static int contentNum = 0;  //背包中物品的数量
    private static int selectedPos = 0;     //方框选中的位置
    private static int selectedItemId = -1; //选中物品的ID

    public static int[] BagContentId { get => bagContentId; set => bagContentId = value; }
    public static int ContentNum { get => contentNum; set => contentNum = value; }
    public static int SelectedPos {
        get => selectedPos;
        set {
            selectedPos = value;
            SelectedItemId = bagContentId[selectedPos];
        }
    }
    public static int SelectedItemId { get => selectedItemId; set => selectedItemId = value; }

    public static void UseItem() {
        if (bagContentId[selectedPos] != -1) {
            bagContentId[selectedPos] = -1;
        }
        else
            return;
        for (int i = selectedPos; i < 8; i++) {
            if (bagContentId[i] == -1 && bagContentId[i + 1] == -1) {
                break;
            }
            if (bagContentId[i] == -1) {
                bagContentId[i] = bagContentId[i + 1];
                bagContentId[i + 1] = -1;
            }
        }
        SelectedItemId = BagContentId[SelectedPos]; //修改框选物体的ID
        contentNum--;
    }

    public static void UseItem(int id) {
        for (int i = 0; i < 9; i++) {
            if (bagContentId[i] == id) {
                bagContentId[i] = -1;
            }
        }
        for (int i = 0; i < 8; i++) {
            if (bagContentId[i] == -1 && bagContentId[i + 1] == -1) {
                break;
            }
            if (bagContentId[i] == -1) {
                bagContentId[i] = bagContentId[i + 1];
                bagContentId[i + 1] = -1;
            }
        }
        SelectedItemId = BagContentId[SelectedPos]; //修改框选物体的ID
        contentNum--;
    }

    public static void AddItem(int id) {
        bagContentId[contentNum] = id;  //修改背包内容
        SelectedItemId = BagContentId[SelectedPos]; //修改框选物体的ID
        contentNum++;
    }
}