public enum UMBRELLA_STATE
{
    CLOSE,
    OPEN,
    NOHAVING,
}

public class Umbrella
{
    //状態フラグ
    public UMBRELLA_STATE state = UMBRELLA_STATE.OPEN;


    //状態取得
    public UMBRELLA_STATE GetState()
    {
        return state;
    }

    //傘の開閉
    public void ChangeOpen()
    {
        if (state == UMBRELLA_STATE.NOHAVING) return;//持っていないなら変化しない

        //開閉切り替え
        if (state == UMBRELLA_STATE.OPEN)
            state = UMBRELLA_STATE.CLOSE;
        else
            state = UMBRELLA_STATE.OPEN;
    }
    //傘を失くす
    public void Lost()
    {
        if (state == UMBRELLA_STATE.NOHAVING) return;//持っていないなら変化しない

        state = UMBRELLA_STATE.NOHAVING;//未所持にする
    }
    //傘を手に入れる
    public void PickUp()
    {
        if (state != UMBRELLA_STATE.NOHAVING) return;//持っていないなら変化しない

        state = UMBRELLA_STATE.CLOSE;//所持にする
    }
}
