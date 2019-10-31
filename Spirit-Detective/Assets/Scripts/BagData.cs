using UnityEngine;

public static class BagData {

    private static int[] bagContentId = new int[9] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    private static int contentNum = 0;
    private static int framePos = 0;
    public static int[] BagContentId { get => bagContentId; set => bagContentId = value; }
    public static int ContentNum { get => contentNum; set => contentNum = value; }
    public static int FramePos { get => framePos; set => framePos = value; }

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
        contentNum--;
    }

    public static void AddItem(int id) {
        bagContentId[contentNum] = id;
        contentNum++;
    }

}