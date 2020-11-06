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

    public GameObject buttonEnd;
    public GameObject buttonEndText;

    private int Sword1;
    private int Sword2;
    private int Sword3;

    public GameObject imageBraveManRed;          //炎の勇者
    public GameObject imageBraveManBlue;         //水の勇者
    public GameObject imageBraveManGreen;        //風の勇者

    public Sprite imageBraveManRedS;          //炎の勇者
    public Sprite imageBraveManBlueS;         //水の勇者
    public Sprite imageBraveManGreenS;        //風の勇者


    private int countText;

    // Start is called before the first frame update
    void Start()
    {
        Sword1 = PlayerPrefs.GetInt("SWORD1");
        Sword2 = PlayerPrefs.GetInt("SWORD2");
        Sword3 = PlayerPrefs.GetInt("SWORD3");
        BraveMans( Sword1, Sword2, Sword3);
        countText = 0;
        buttonMessage.SetActive(true);
        Ending();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //メッセージウィンドウの表示
    public void DisplayMessage(string message)
    {
        buttonMessageText.GetComponent<Text>().text = message;
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
                DisplayEnd("Bad End");
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
                break;
            case 4:
                DisplayMessage("魔王を撃退した！");
                break;
            case 5:
                buttonMessage.SetActive(false);
                DisplayEnd("End");
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
                break;
            case 4:
                DisplayMessage("魔王を討伐した！！");
                break;
            case 5:
                buttonMessage.SetActive(false);
                buttonEnd.SetActive(true);
                DisplayEnd("True End");
                break;
            default:
                UnityEngine.Debug.Log("End3 Error");
                break;
        }

    }
}
