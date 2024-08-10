public enum UMBRELLA_STATE
{
    CLOSE,
    OPEN,
    NOHAVING,
}

public class Umbrella
{
    //��ԃt���O
    public UMBRELLA_STATE state = UMBRELLA_STATE.OPEN;


    //��Ԏ擾
    public UMBRELLA_STATE GetState()
    {
        return state;
    }

    //�P�̊J��
    public void ChangeOpen()
    {
        if (state == UMBRELLA_STATE.NOHAVING) return;//�����Ă��Ȃ��Ȃ�ω����Ȃ�

        //�J�؂�ւ�
        if (state == UMBRELLA_STATE.OPEN)
            state = UMBRELLA_STATE.CLOSE;
        else
            state = UMBRELLA_STATE.OPEN;
    }
    //�P��������
    public void Lost()
    {
        if (state == UMBRELLA_STATE.NOHAVING) return;//�����Ă��Ȃ��Ȃ�ω����Ȃ�

        state = UMBRELLA_STATE.NOHAVING;//�������ɂ���
    }
    //�P����ɓ����
    public void PickUp()
    {
        if (state != UMBRELLA_STATE.NOHAVING) return;//�����Ă��Ȃ��Ȃ�ω����Ȃ�

        state = UMBRELLA_STATE.CLOSE;//�����ɂ���
    }
}
