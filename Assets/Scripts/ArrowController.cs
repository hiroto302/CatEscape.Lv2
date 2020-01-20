using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float arrowSpeed = -0.1f;  //矢の落下速度
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, this.arrowSpeed, 0);  //フレームごとに落下するスピード
        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            //見えない所でも、処理が続いてしまうのでDestroyメソッドで破壊すること、引数に渡すobjectを破棄する
            //gameObject変数は自分自身のオブジェクトを指す、ここではアタッチ先のarrowオブジェクトである
        }

        //あたり判定の実装
        Vector2 p1 = transform.position;
        Vector2 p2 = this.player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;

        float r1 = 0.5f;
        float r2 = 1.0f;

        if(d < r1+r2)
        {
            Destroy(gameObject);

            //当たったことをGameDirectorに伝え、実行したいメソッドを呼び出す
            //他のscriptでメソッドを利用出来るようにするために、空のオブジェクトにdirectorスクリプトをアタッチして、呼び出せるようにしているのかな
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
        }

    }
}
