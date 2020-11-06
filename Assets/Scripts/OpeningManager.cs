using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    //ボタン：メッセージ
    public GameObject buttonMessage;           //ボタン：メッセージ
    public GameObject buttonMessageText;       //ボタン：テキスト

    private int countText;

    // Start is called before the first frame update
    void Start()
    {
        countText = 0;
        buttonMessage.SetActive(true);
        PushButtonMessage();
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
        OpeningMessage();
        countText++;
    }

    //オープニングの会話文
    void OpeningMessage()
    {
        switch(countText)
        {
            case 0:
                DisplayMessage("初めまして私は女神です。");
                break;
            case 1:
                DisplayMessage("あなたには異世界で勇者を救ってもらうために、" +
                    "こちらに呼ばせていただきました。");
                break;
            case 2:
                DisplayMessage("このあと異世界に転移させますが、" +
                    "そこで勇者は魔王によってどこかに閉じ込められてしまっています。");
                break;
            case 3:
                DisplayMessage("謎を解いて閉じ込められている勇者を救ってください。");
                break;
            case 4:
                DisplayMessage("大変かと思いますが、どうかよろしくお願いします。");
                break;
            case 5:
                SceneManager.LoadScene("GameScene_stage1");
                break;
            default:
                UnityEngine.Debug.Log("OpeningMessage Error");
                break;
        }

    }
}
