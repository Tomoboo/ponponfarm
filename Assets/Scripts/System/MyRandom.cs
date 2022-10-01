
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
        var prev_state = Random.state; // 仕様前の状態
        Random.state = state; // 前回の状態にセット
        var result = Random.Range(min, max); state = Random.state; // 現在の状態を記録
        Random.state = prev_state; //　仕様前の状態に
        return result;
    }

}
