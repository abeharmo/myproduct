using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    //ボタン：メッセージ
    public GameObject buttonMessage;             //ボタン：メッセージ
    public GameObject buttonMessageText;         //ボタン：テキスト

    public GameObject buttonEnd;                 //ボタン：エンドの種類
    public GameObject buttonEndText;             //ボタン；エンドの内容

    private int Sword1;
    private int Sword2;
    private int Sword3;

    public GameObject imageBraveManRed;          //炎の勇者
    public GameObject imageBraveManBlue;         //水の勇者
    public GameObject imageBraveManGreen;        //風の勇者

    public Sprite imageBraveManRedS;             //画像：炎の勇者
    public Sprite imageBraveManBlueS;            //画像：水の勇者
    public Sprite imageBraveManGreenS;           //画像：風の勇者

    public AudioClip messageSE;                  //効果音：メッセージ表示
    public AudioClip evilLaughSE;                //効果音：魔王の笑い声
    public AudioClip badEndSE;                   //効果音：バッドエンド
    public AudioClip normalEndSE;                //効果音：ノーマルエンド
    public AudioClip trueEndSE;                  //効果音：真エンド


    private int countText;                       //テキストを表示した回数のカウント
    private AudioSource audioSource;             //SE音源
    private AudioSource bgmAudioSource;          //BGM音源

    // Start is called before the first frame update
    void Start()
    {
        //audioSourceの取得
        audioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource = gameObject.GetComponent<AudioSource>();

        //各勇者の剣の取得状態
        Sword1 = PlayerPrefs.GetInt("SWORD1");
        Sword2 = PlayerPrefs.GetInt("SWORD2");
        Sword3 = PlayerPrefs.GetInt("SWORD3");
        BraveMans( Sword1, Sword2, Sword3);
        countText = 0;
        buttonMessage.SetActive(true);
        Ending();
    }


    //メッセージウィンドウの表示
    public void DisplayMessage(string message)
    {
        buttonMessageText.GetComponent<Text>().text = message;
        audioSource.PlayOneShot(messageSE);
    }

    //メッセージウィンドウを押すと
    public void PushButtonMessage()
    {
        Ending();
        countText++;
    }

    //エンドウィンドウの表示
    public void DisplayEnd(string message)
    {
        buttonEndText.GetComponent<Text>().text = message;
    }

    //エンディングからタイトルに戻る
    public void PushButtonEnd()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //勇者が剣を回収しているか
    void BraveMans(int Sword1, int Sword2, int Sword3)
    {
        if (Sword1 == 1)
        {
            imageBraveManBlue.GetComponent<Image>().sprite = imageBraveManBlueS;
        }
        if (Sword2 == 1)
        {
            imageBraveManRed.GetComponent<Image>().sprite = imageBraveManRedS;
        }
        if (Sword3 == 1)
        {
            imageBraveManGreen.GetComponent<Image>().sprite = imageBraveManGreenS;
        }
    }

    //勇者が剣を持っているかによるエンディングの変化
    public void Ending()
    {
        if (Sword1 == 1 && Sword2 == 1 && Sword3 == 1)
        {
            EndingMessage3();
        }
        else if (Sword1 == 0 && Sword2 == 0 && Sword3 == 0)
        {
            EndingMessage1();
        }
        else
        {
            EndingMessage2();
        }
        countText++;
    }

    //エンディング1の会話文
    void EndingMessage1()
    {
        switch(countText)
        {
            case 0:
                DisplayMessage("「やってきたか勇者ども。」");
                break;
            case 1:
                DisplayMessage("「ん、貴様ら一人も武器を持ってはいないではないか！」");
                break;
            case 2:
                DisplayMessage("「舐めおって！！" +
                    "武器のない勇者に負けるわけがないだろう！」");
                break;
            case 3:
                DisplayMessage("「消えるがよい！！」");
                break;
            case 4:
                DisplayMessage("魔王に敗北した。");
                break;
            case 5:
                buttonMessage.SetActive(false);
                buttonEnd.SetActive(true);
                DisplayEnd("Bad End");
                bgmAudioSource.Pause();
                audioSource.PlayOneShot(badEndSE);
                break;
            default:
                UnityEngine.Debug.Log("End1 Error");
                break;
        }

    }

    //エンディング2の会話文
    void EndingMessage2()
    {
        switch (countText)
        {
            case 0:
                DisplayMessage("「やってきたか勇者ども。」");
                break;
            case 1:
                DisplayMessage("「では、勝負だ！！」");
                break;
            case 2:
                DisplayMessage("ー数時間後ー");
                break;
            case 3:
                DisplayMessage("「く、今回は見逃してやる。さらばだ！」");
                GameObject.Find("ImageEvil").SetActive(false);
                break;
            case 4:
                DisplayMessage("魔王を撃退した！");
                break;
            case 5:
                buttonMessage.SetActive(false);
                buttonEnd.SetActive(true);
                DisplayEnd("End");
                bgmAudioSource.Pause();
                audioSource.PlayOneShot(normalEndSE);
                break;
            default:
                UnityEngine.Debug.Log("End2 Error");
                break;
        }

    }

    //エンディング3の会話文
    void EndingMessage3()
    {
        switch (countText)
        {
            case 0:
                DisplayMessage("「やってきたか勇者ども。」");
                break;
            case 1:
                DisplayMessage("「では、勝負だ！！」");
                break;
            case 2:
                DisplayMessage("ー数時間後ー");
                break;
            case 3:
                DisplayMessage("「く、ここまでのようだな。ぐわーーーーー。」");
                GameObject.Find("ImageEvil").SetActive(false);
                break;
            case 4:
                DisplayMessage("魔王を討伐した！！");
                break;
            case 5:
                buttonMessage.SetActive(false);
                buttonEnd.SetActive(true);
                DisplayEnd("True End");
                bgmAudioSource.Pause();
                audioSource.PlayOneShot(trueEndSE);
                break;
            default:
                UnityEngine.Debug.Log("End3 Error");
                break;
        }

    }
}
