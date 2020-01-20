using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip CatEscapeSE;
    AudioSource audio;
    int key = 1; //keyの初期値、playerの最初のscaleのxの値

    void Start()
    {
        this.audio = GetComponent<AudioSource>();
    }

    // キー操作に対応
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         transform.Translate(-3, 0, 0);
    //     }
    //     if(Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         transform.Translate(3, 0, 0);
    //     }
    // }

    //ボタン操作に対応
    public void RButtonDown()
    {
        Vector3 localPos = transform.localPosition;
        float x = localPos.x;
        if(x == 6)
        {
            transform.Translate(0, 0, 0);
        }
        else
        {
            transform.Translate(3, 0, 0);
        }

        this.audio.PlayOneShot(this.CatEscapeSE);  //移動操作の効果音

        if( this.key == 1)
        {
            this.key = -1;
            transform.localScale = new Vector3(key, 1, 1);  //(1, 0, 0)などscaleの値に0を代入するとオブジェクトが消えるよ
        }
        else if( this.key != 1)
        {
            this.key = 1;
            transform.localScale = new Vector3(key, 1, 1);
        }

    }
    public void LButtonDown()
    {
        //行動範囲の制限の実装
        Vector3 localPos = transform.localPosition;
        float x = localPos.x;
        if(x == -6)
        {
            transform.Translate(0, 0, 0);
        }
        else
        {
            transform.Translate(-3, 0, 0);
        }

        this.audio.PlayOneShot(this.CatEscapeSE);  //移動操作の効果音

        if( this.key == 1)
        {
            this.key = -1;
            transform.localScale = new Vector3(key, 1, 1);  //(1, 0, 0)などscaleの値に0を代入するとオブジェクトが消えるよ
        }
        else if( this.key != 1)
        {
            this.key = 1;
            transform.localScale = new Vector3(key, 1, 1);
        }
    }
}
