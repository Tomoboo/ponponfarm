
using UnityEngine;

public class MyRandom
{
    private Random.State state;

    public MyRandom() : this((int)System.DateTime.Now.Ticks) { }

    public MyRandom(int seed){
        setSeed(seed);
    }

    public void  setSeed(int seed){
        var prev_state = Random.state; Random.InitState(seed);
        state = Random.state; Random.state = prev_state;
    }

    public int Range(int min, int max){
        var prev_state = Random.state; // �d�l�O�̏��
        Random.state = state; // �O��̏�ԂɃZ�b�g
        var result = Random.Range(min, max); state = Random.state; // ���݂̏�Ԃ��L�^
        Random.state = prev_state; //�@�d�l�O�̏�Ԃ�
        return result;
    }

}
