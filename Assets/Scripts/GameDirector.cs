using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject hpGauge;
    GameObject hpCount;
    GameObject time;
    GameObject generator;  //レベルデザイン
    GameObject player;
    GameObject threeCount;
    GameObject readyStart;

    public AudioClip damageSE;
    AudioSource audio;

    int hp = 9;  //初期hpはここ
    float timeLimit = 30.0f;  //初期time
    float span = 0.1f;
    float delta = 0;
    string finishTime = "0.0";
    float startTime = 3.0f;

    enum GAMEMODE  //ゲームモードの実装
    {
        READYTIME,
        PLAY,
    };
    GAMEMODE nowMode;
    void Start()
    {
        this.hpGauge = GameObject.Find("hpGauge");
        this.hpCount = GameObject.Find("hpCount");
        this.time = GameObject.Find("time");
        this.audio = GetComponent<AudioSource>();
        this.generator = GameObject.Find("ArrowGenerator");
        this.player = GameObject.Find("player");
        this.threeCount = GameObject.Find("ReadyTime");
        this.readyStart = GameObject.Find("ReadyStart");
        this.readyStart.SetActive(false);


        nowMode = GAMEMODE.READYTIME;
    }

    public void DecreaseHp()  //ArrowContorollerでこのメッドを呼び出せるようにpublic
    {
        // 誤作動を起こしていた時の間違った構文の書き方では、hpが1の時点でページが遷移してしまっていた。
        // 最初に当たったとき、ゲーム画面のUI上表示されているのは9だが、すでに処理としてその数値は8となっている。
        // なのでUI上の１の時点でhpは0なので遷移していた

        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
        this.hpCount.GetComponent<Text>().text = this.hp.ToString("D1");
        this.hp -=  1;
        //hpの初期値はスコープ外で指定すること、実行される度に同じ値がまた入る、同じ変数が２つあるとかだめだよ

        this.audio.PlayOneShot(this.damageSE);  //ダメージを受けた時の効果音
        this.player.GetComponent<ParticleSystem>().Play();  //ダメージを受けた時のエフェクト
        Debug.Log("ダメージ受けたよ");


        if(this.hp < 0)  //hpが0になるとgameOver
        // hp == 0では駄目だよ
        //処理は上から下に実行されていくので上記のコードとの順番に注意.UIで表示させたいことと、hpに実際に代入されている数値はなんなのか常に気にかけること
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    void Update()
    {
        // タイマーを設定し、
        this.time.GetComponent<Text>().text = this.timeLimit.ToString("F1");
        this.delta += Time.deltaTime;  //フレーム毎の時間差を代入
        this.startTime -= Time.deltaTime;

        switch (nowMode)
        {
            case GAMEMODE.READYTIME:
                if(2.0f < this.startTime )
                {
                    this.threeCount.GetComponent<Text>().text = "3";
                    this.generator.GetComponent<ArrowGenerator>().readyTime = true;  //矢の生成停止状態
                }
                else if(1.0f < this.startTime)
                {
                    this.threeCount.GetComponent<Text>().text = "2";
                    this.generator.GetComponent<ArrowGenerator>().readyTime = true;
                }
                else if(0 < this.startTime)
                {
                    this.threeCount.GetComponent<Text>().text = "1";
                    this.generator.GetComponent<ArrowGenerator>().readyTime = true;
                }
                else
                {
                    this.generator.GetComponent<ArrowGenerator>().readyTime = false;  //矢の生成開始
                    this.threeCount.SetActive(false);
                    this.readyStart.SetActive(true);
                    nowMode = GAMEMODE.PLAY;
                }
            break;
        //３秒後にgameStart
            case GAMEMODE.PLAY:
            {
                //一定時間経過後、GameClear画面に遷移
                if(this.time.GetComponent<Text>().text.Equals(this.finishTime))  //文字列の比較
                {
                    SceneManager.LoadScene("GameClearScene");
                }
                else if(this.delta > this.span)
                {
                    this.delta = 0;  //ここでdeltaを0に初期化し忘れないこと。deltaの数値が大きくなりif文が常にフレームごとにtrueを返し、すごい勢いで処理を重ねてしまい正しく時間経過を測ることができないため
                    this.timeLimit -= this.span;

                    if(26 <= this.timeLimit && this.timeLimit <31)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.8f, -0.1f);
                    }
                    else if(20 <= this.timeLimit && this.timeLimit < 26)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.55f, -0.15f);
                    }
                    else if(13 <= this.timeLimit && this.timeLimit < 20)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.35f, -0.2f);
                    }
                    else if(4 <= this.timeLimit && this.timeLimit < 13)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.25f, -0.15f);
                    }
                    else if(2 <= this.timeLimit && this.timeLimit < 4)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.33f, -0.125f);
                    }
                    else if(0 <= this.timeLimit && this.timeLimit < 2)
                    {
                        this.generator.GetComponent<ArrowGenerator>().SetParameter(0.4f, -0.1f);
                    }
                }
            break;
            }
        }

        // 下記の記述ではClearSceneに遷移しない、なぜかわからない。
        // this.time.GetComponent<Text>().text = this.timeLimit.ToString("F1");
        // this.delta += Time.deltaTime;  //フレーム毎の時間差を代入
        // if(this.timeLimit == 28.0f)
        // {
        //     SceneManager.LoadScene("GameClearScene");
        // }
        // else if(this.delta> this.span)
        // {
        //     this.delta = 0;  //ここでdeltaを0に初期化し忘れないこと。deltaの数値が大きくなりif文が常にフレームごとにtrueを返し、すごい勢いで処理を重ねてしまい正しく時間経過を測ることができないため
        //     this.timeLimit -= this.span;
        // }
    }
}