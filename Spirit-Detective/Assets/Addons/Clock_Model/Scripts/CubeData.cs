using UnityEngine;

public class CubeData {

    private static int ClickNum = 0;
    private static int FramePosition = 4;
    private static int Stick1Pos = 1;
    private static int Stick2Pos = 1;
    private static bool[] Solved = new bool[9] {
        true,
        false,
        false,
        true,
        true,
        false,
        false,
        false,
        true
    };
    private readonly static Vector2[] PosData = new Vector2[9] {
        new Vector2(-1, -1),
        new Vector2(1, 1),
        new Vector2(2, 2),
        new Vector2(-1, -1),
        new Vector2(-1, -1),
        new Vector2(3, 3),
        new Vector2(4, 4),
        new Vector2(5, 5),
        new Vector2(-1, -1)
    };

    public void SetFramePos(int x) {
        FramePosition = x;
    }

    public int GetFramePos() {
        return FramePosition;
    }

    public void AddStick1Pos() {
        Stick1Pos++;
        if (Stick1Pos == 6) Stick1Pos = 1;
    }

    public void AddStick2Pos() {
        Stick2Pos++;
        if (Stick2Pos == 6) Stick2Pos = 1;
    }

    public int GetStick1Pos() {
        return Stick1Pos;
    }

    public int GetStick2Pos() {
        return Stick2Pos;
    }

    public bool CheckTrue() {
        if (!Solved[FramePosition]) {
            if (PosData[FramePosition] == new Vector2(Stick1Pos, Stick2Pos)) {
                Solved[FramePosition] = true;
                return true;
            }
        }
        return false;
    }

    public void AddClickNum() {
        ClickNum++;
    }

    public int GetClickNum() {
        return ClickNum;
    }

}