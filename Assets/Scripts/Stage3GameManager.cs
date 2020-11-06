using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Stage3GameManager : GameManagerBase
{
    //各パネルの定数定義
    //謎2,3と牢屋の部屋
    public const int WALL5  = 4;       
    public const int WALL6  = 5;       
    public const int WALL7  = 6;      
    //謎の拡大パネル
    public const int WALL8  = 7;      //箱の謎
    public const int WALL9  = 8;      //部屋の謎1
    public const int WALL10 = 9;      //部屋の謎2
    public const int WALL11 = 10;     //部屋の謎3
    public const int WALL12 = 11;     //牢屋の謎

    //アルファベットを管理
    private string[] alphabets = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                                   "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

    //各アイテムのアイコン
    public GameObject ButtonHintIcon;          //ヒント1
    public GameObject ButtonBookIcon;          //英語のヒントが書かれた本
    public GameObject ButtonHammerIcon;        //ハンマー
    public GameObject ButtonSwordIcon;         //剣

    public GameObject ButtonSelectedBookLarge;  //ボタン：ヒントが書かれた本の拡大画面
    public GameObject ImageSelectedBook;        //画像：ヒントが書かれた本の拡大画面

    //各アイテム（アイテムメニューに表示）
    public GameObject ButtonHint;               //ヒント1
    public GameObject ButtonBook;               //パネル上の本
    public GameObject ButtonHammer;             //ハンマー
    public GameObject ButtonSword;              //剣

    //イラスト（パネル内に表示）
    public GameObject ButtonHintOnDesk;          //ボタン：パネルに表示しているヒント
    public GameObject ButtonFirstDoorOpen;       //ボタン：一つ目の部屋の開いているドア
    public GameObject ButtonSecondDoorOpen;      //ボタン：一つ目の部屋の開いているドア
    public GameObject ButtonThirdDoorOpen;       //ボタン：一つ目の部屋の開いているドア
    public GameObject ButtonBraveManOutPrison;   //ボタン：牢屋の外に出た勇者
    public GameObject ButtonHammerOnPanel;       //ボタン：ルーム2のハンマー
    public GameObject ButtonMagicArray;          //ボタン：魔法陣
    public GameObject ImageMagicArray;           //画像：魔法陣

    //メッセージウィンドウ
    public GameObject buttonFlagMessage;           //フラグのテキストウィンドウ
    public GameObject buttonFlagMessageText;       //フラグのテキスト
    public GameObject panelMissingMessage;         //謎が解けなかった時にテキストを表示するパネル
    public GameObject buttonMissingMessage;        //謎が解けなかった時のテキストウィンドウ
    public GameObject buttonMissingMessageText;    //謎が解けなかった時のテキスト

    //イラスト（パネル内の背景等）
    public Sprite imageBoxOpen;             //画像：開いている箱
    public Sprite imagePrisonOpen;          //画像；開いている牢屋
    public Sprite imageMagicArrayBroken;    //画像：破壊された魔法陣

    public Sprite[] imageHint = new Sprite[4];  //画像：ヒント
    private int numberHint;                     //表示しているヒントを保存する数字

    //ボタン：謎に使われているオブジェクト
    public GameObject[] buttonNumbers    = new GameObject[4];     //白い箱の数字
    public GameObject[] buttonAlphabets1 = new GameObject[3];     //一つ目の部屋の謎のアルファベット
    public GameObject[] buttonAlphabets2 = new GameObject[4];     //二つ目の部屋の謎のアルファベット
    public GameObject[] buttonAlphabets3 = new GameObject[4];     //三つ目の部屋の謎のアルファベット
    public GameObject[] buttonAlphabets4 = new GameObject[3];     //牢屋の謎のアルファベット

    //文字列：謎で変化するアルファベット
    public GameObject[] textAlphabet1 = new GameObject[3];      //一つ目の扉の謎のアルファベット
    public GameObject[] textAlphabet2 = new GameObject[4];      //二つ目の扉の謎のアルファベット
    public GameObject[] textAlphabet3 = new GameObject[4];      //三つ目の扉の謎のアルファベット

    //ボタン：入力の拡大画面から元の画面に戻るボタン
    public GameObject buttonAnswer1;
    public GameObject buttonAnswer2;
    public GameObject buttonAnswer3;

    //謎のボタンの値を管理する数字
    private int[] numberAlphabets1 = new int[3];
    private int[] numberAlphabets2 = new int[4];
    private int[] numberAlphabets3 = new int[4];
    private int[] numberAlphabets4 = new int[3];

    //アイテムの所持
    private bool doesHaveHammer;      //ハンマー
    private int doesHaveSword;        //剣 （セーブの都合でint型）
    //オブジェクトの状態
    private bool doesOpenBox;          //白い箱の状態
    private bool doesOpenDoor1;        //扉1の状態
    private bool doesOpenDoor2;        //扉2の状態
    private bool doesOpenDoor3;        //扉3の状態
    private bool doesOpenPrison;       //牢屋の鍵
    //勇者の状態
    private bool doesSaveBraveMan;
    
//初期設定
void Start()
    {
        base.Start();

        doesHaveHammer  = true;
        doesHaveSword   = 0;

        doesOpenBox        = false;
        doesOpenDoor1      = false;
        doesOpenDoor2      = false;
        doesOpenDoor3      = false;
        doesOpenPrison     = false;
        doesSaveBraveMan   = false;

        DisabledButton2(ButtonMagicArray);

    }

    //ボタンを押されたらメッセージウィンドウを消す
    public void PushButtonFlagMessage()
    {
        buttonFlagMessage.SetActive(false);
        SelectStayOrMove();
    }

    //メッセージウィンドウの表示
    void DisplayFlagMessage(string message)
    {
        buttonFlagMessage.SetActive(true);
        buttonFlagMessageText.GetComponent<Text>().text = message;
    }

    //謎が解けなかったときのメッセージウィンドウを消す
    public void PushButtonMissingMessage()
    {
        panelMissingMessage.SetActive(false);
    }

    //謎が解けなかったときのメッセージウィンドウの表示
    void DisplayMissingMessage(string message)
    {
        panelMissingMessage.SetActive(true);
        buttonMissingMessageText.GetComponent<Text>().text = message;
    }

    //机の上のヒントの紙を押したら
    public void PushButtonHintOnDesk()
    {
        DisplayMessage("何か書かれている紙を拾った。");
        ButtonHintOnDesk.SetActive(false);
        DisabledButton1(buttonItem);
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);

        ButtonHintIcon.SetActive(true);
        ButtonHint.SetActive(true);
    }

    //扉1の謎の入力画面
    public void PushButtonMystery1()
    {
        buttonAnswer1.SetActive(true);
        DisabledButton1(buttonItem);
        DisabledButton1(GameObject.Find("ButtonBackPanelWall1FM1"));
    }

    //扉2の謎の入力画面
    public void PushButtonMystery2()
    {
        buttonAnswer2.SetActive(true);
        DisabledButton1(buttonItem);
        DisabledButton1(GameObject.Find("ButtonBackPanelSecondFM2"));
    }

    //扉3の謎の入力画面
    public void PushButtonMystery3()
    {
        buttonAnswer3.SetActive(true);
        DisabledButton1(buttonItem);
        DisabledButton1(GameObject.Find("ButtonBackPanelThirdFM3"));
    }

    //扉1の謎の入力画面から戻る
    public void PushButtonAnswer1()
    {
        buttonAnswer1.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelWall1FM1"));
    }

    //扉2の謎の入力画面から戻る
    public void PushButtonAnswer2()
    {
        buttonAnswer2.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelSecondFM2"));
    }

    //扉3の謎の入力画面から戻る
    public void PushButtonAnswer3()
    {
        buttonAnswer3.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelThirdFM3"));
    }

    //勇者を押したら
    public void PushButtonBraveMan()
    {
        if (doesHaveSword == 1)
        {
            DisplayFlagMessage("おー！信じてたぞ、剣を見つけてくれて本当に助かった！！");
        }
        else if (doesSaveBraveMan == true)
        {
            DisplayFlagMessage("恩にきる！もしよかったら一緒に剣も探してくれると助かる。");
        }
        else
        {
            DisplayMessage("助けてくれ！下手こいて捕まっちまった。" +
                           "ここから出してくれれば魔王を倒すの手伝うぞ！");
        }

    }

    //魔法陣を押したら
    public void PushButtonMagicArray()
    {
        if (doesHaveHammer)
        {
            DisplayMessage("壁をハンマーで叩くと中から剣が出てきた。");
            ImageMagicArray.GetComponent<Image>().sprite = imageMagicArrayBroken;
            DisabledButton2(ButtonMagicArray);
            ChangeArrowActive(0, ButtonLeft);
            ChangeArrowActive(0, ButtonRight);
            DisabledButton1(ButtonHammer);

            ButtonSwordIcon.SetActive(true);
            ButtonSword.SetActive(true);

            doesHaveSword = 1;
        } 
    }

    //ハンマーを押したら
    public void PushButtonHammerOnPanel()
    {
        DisplayMessage("ハンマーを入手した。");
        DisabledButton1(buttonItem);
        DisabledButton1(GameObject.Find("ButtonBackPanelWall1"));
        ButtonHammerOnPanel.SetActive(false);

        EnabledButton2(ButtonMagicArray);
        ButtonHammerIcon.SetActive(true);
        ButtonHammer.SetActive(true);
        doesHaveHammer = true;
    }

    //ヒントのアイコンを消す処理
    public void PushButtonHintIcon()
    {
        ButtonHintIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }

    //本のアイコンを消す処理
    public void PushButtonBookIcon()
    {
        ButtonBookIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelWall2"));
    }

    //ハンマーのアイコンを消す処理
    public void PushButtonHammerIcon()
    {
        ButtonHammerIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelWall1"));
    }

    //剣のアイコンを消す処理
    public void PushButtonSwordIcon()
    {
        ButtonSwordIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }

    //他のパネルへ移動するための関数
    public void PushButtonMoveToSomePanel(int number)
    {
        wallNow = number;
        if((wallNow >= 0)&&(wallNow < 4))
        {
            ChangeArrowActive(1, ButtonLeft);
            ChangeArrowActive(1, ButtonRight);
        }
        else
        {
            ChangeArrowActive(0, ButtonLeft);
            ChangeArrowActive(0, ButtonRight);
        }
        DisplayWallExtended();
    }

    //本の拡大画面を押したとき
    public void PushBookLarge()
    {
        if(numberHint < 4)
        {
            ImageSelectedBook.GetComponent<Image>().sprite = imageHint[numberHint];
        }
        else
        {
            ButtonSelectedBookLarge.SetActive(false);
        }
        numberHint = (numberHint + 1) % 5;
    }

    //アイテム欄の本の選択
    public void PushBookIcon(Sprite imageBookIcon)
    {
        ButtonSelectedBookLarge.SetActive(true);
        ImageSelectedBook.GetComponent<Image>().sprite = imageBookIcon;
    }

    //アイテムの選択
    public void PushSomeIcon(Sprite someItem)
    {
        ImageSelectedItem.GetComponent<Image>().sprite = someItem;
        ImageSelectedItemLarge.GetComponent<Image>().sprite = someItem;
        ImageSelectedItem.SetActive(true);
        ButtonSelectedItemLarge.SetActive(true);
    }

    //次のステージへのボタン
    public void PushButtonNextStage()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 3);
        PlayerPrefs.SetInt("SWORD3", doesHaveSword);
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndingScene");
    }

    //クリアしてタイトルに戻るボタン
    public void PushButtonReturnTitleFromStage3()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 3);
        PlayerPrefs.SetInt("SWORD3", doesHaveSword);
        PlayerPrefs.Save();
        SceneManager.LoadScene("TitleScene");
    }

    //WALL1～12への画面遷移の処理
    void DisplayWallExtended()
    {
        if ((wallNow >= WALL1) && (wallNow < WALL5))
        {
            DisplayWallBase();
        }
        else if ((wallNow >= WALL5) && (wallNow <= WALL12))
        {
            if (buttonMessage.activeSelf == true)
            {
                DeleteMessage();
            }

            switch (wallNow)
            {
                case WALL5:
                    Vector3 tmp1 = GameObject.Find("PanelSecondRoom").transform.localPosition;
                    panelWalls.transform.localPosition = tmp1 * -1;
                    break;
                case WALL6:
                    Vector3 tmp2 = GameObject.Find("PanelThirdRoom").transform.localPosition;
                    panelWalls.transform.localPosition = tmp2 * -1;
                    break;
                case WALL7:
                    Vector3 tmp3 = GameObject.Find("PanelPrison").transform.localPosition;
                    panelWalls.transform.localPosition = tmp3 * -1;
                    break;
                case WALL8:
                    Vector3 tmp4 = GameObject.Find("PanelBoxMystery").transform.localPosition;
                    panelWalls.transform.localPosition = tmp4 * -1;
                    break;
                case WALL9:
                    Vector3 tmp5 = GameObject.Find("PanelMystery1").transform.localPosition;
                    panelWalls.transform.localPosition = tmp5 * -1;
                    break;
                case WALL10:
                    Vector3 tmp6 = GameObject.Find("PanelSecondMystery").transform.localPosition;
                    panelWalls.transform.localPosition = tmp6 * -1;
                    break;
                case WALL11:
                    Vector3 tmp7 = GameObject.Find("PanelThirdMystery").transform.localPosition;
                    panelWalls.transform.localPosition = tmp7 * -1;
                    break;
                case WALL12:
                    Vector3 tmp8 = GameObject.Find("PanelPrisonRock").transform.localPosition;
                    panelWalls.transform.localPosition = tmp8 * -1;
                    break;
                default:
                    UnityEngine.Debug.Log("DisplayWallExtended Error");
                    break;
            }
        }
        else
        {
            UnityEngine.Debug.Log(wallNow);
            UnityEngine.Debug.Log("wallNow Error");
        }

    }

    //ボタンを押せなくする
    public void ButtonAllDisabledPush(GameObject[] button)
    {
       for(int i= 0; i < button.Length; i++)
        {
            DisabledButton2(button[i]);
        }
    }

    //箱の謎の入力の処理
    public void PushButtonBoxM(int i)
    {
        ChangeNumber(i);
        CheckBoxMysteryAnswer();
    }

    //扉1の謎の入力の処理
    public void PushButtonDoor1M(int i)
    {
        ChangeAlphabet1(i);
        CheckDoor1MysteryAnswer();
    }

    //扉2の謎の入力の処理
    public void PushButtonDoor2M(int i)
    {
        ChangeAlphabet2(i);
        CheckDoor2MysteryAnswer();
    }

    //扉3の謎の入力の処理
    public void PushButtonDoor3M(int i)
    {
        ChangeAlphabet3(i);
        CheckDoor3MysteryAnswer();
    }

    //牢屋の鍵の謎の入力の処理
    public void PushButtonPrisonRockM(int i)
    {
        ChangeAlphabet4(i);
        CheckPrisonRockMysteryAnswer();
    }

    //箱の謎の数字の変更の処理
    void ChangeNumber(int buttonNo)
    {
        int numberKind = int.Parse(buttonNumbers[buttonNo].GetComponentInChildren<Text>().text);
        string numberString = ((numberKind + 1) % 5).ToString("0");
        buttonNumbers[buttonNo].GetComponentInChildren<Text>().text = numberString;
    }

    //扉謎1のアルファベットの変更の処理
    void ChangeAlphabet1(int buttonNo)
    {
        numberAlphabets1[buttonNo] = (numberAlphabets1[buttonNo] + 1) % 26;
        buttonAlphabets1[buttonNo].GetComponentInChildren<Text>().text = alphabets[numberAlphabets1[buttonNo]];
        textAlphabet1[buttonNo].GetComponent<Text>().text = alphabets[numberAlphabets1[buttonNo]];
    }

    //扉謎2のアルファベットの変更の処理
    void ChangeAlphabet2(int buttonNo)
    {
        numberAlphabets2[buttonNo] = (numberAlphabets2[buttonNo] + 1) % 26;
        buttonAlphabets2[buttonNo].GetComponentInChildren<Text>().text = alphabets[numberAlphabets2[buttonNo]];
        textAlphabet2[buttonNo].GetComponent<Text>().text = alphabets[numberAlphabets2[buttonNo]];
    }
    //扉謎3のアルファベットの変更の処理
    void ChangeAlphabet3(int buttonNo)
    {
        numberAlphabets3[buttonNo] = (numberAlphabets3[buttonNo] + 1) % 26;
        buttonAlphabets3[buttonNo].GetComponentInChildren<Text>().text = alphabets[numberAlphabets3[buttonNo]];
        textAlphabet3[buttonNo].GetComponent<Text>().text = alphabets[numberAlphabets3[buttonNo]];
    }
    //牢屋の鍵の謎のアルファベットの変更の処理
    void ChangeAlphabet4(int buttonNo)
    {
        numberAlphabets4[buttonNo] = (numberAlphabets4[buttonNo] + 1) % 26;
        buttonAlphabets4[buttonNo].GetComponentInChildren<Text>().text = alphabets[numberAlphabets4[buttonNo]];
    }

    //白い箱の謎の答えのチェック
    void CheckBoxMysteryAnswer()
    {
        int[] number = new int[4];
        for (int i = 0; i < 4; i++)
        {
            number[i] = int.Parse(buttonNumbers[i].GetComponentInChildren<Text>().text);
        }

        if ((number[0] == 4) && (number[1] == 3) && (number[2] == 2) && (number[3] == 1))
        {
            if (doesOpenBox == false)
            {
                DisplayMessage("「カシャ」と音がして箱が開いた。中には本が入っていた。");

                GameObject.Find("ButtonBox").GetComponent<Image>().sprite = imageBoxOpen;
                DisabledButton1(buttonItem);
                DisabledButton2(GameObject.Find("ButtonBox"));
                DisabledButton1(GameObject.Find("ButtonBackPanelWall2"));
                ButtonAllDisabledPush(buttonNumbers);

                ButtonBookIcon.SetActive(true);
                ButtonBook.SetActive(true);

                doesOpenBox = true;
            }
        }
    }

    //扉1の謎の答えのチェック
    void CheckDoor1MysteryAnswer()
    {
        if ((numberAlphabets1[0] == 18) && (numberAlphabets1[1] == 4) && (numberAlphabets1[2] == 15))
        {
            if (doesOpenDoor1 == false)
            {
                DisplayMessage("扉が横にずれて次の部屋への道が現れた。");
                GameObject ButtonFirstDoor = GameObject.Find("ButtonFirstDoor");

                DisabledButton2(GameObject.Find("ButtonMystery1"));
                ButtonAllDisabledPush(buttonAlphabets1);
                Vector3 posDoor1 = ButtonFirstDoor.transform.localPosition;
                ButtonFirstDoor.transform.localPosition = new Vector3(posDoor1.x + 200.0f, posDoor1.y, posDoor1.z);

                ButtonFirstDoorOpen.SetActive(true);
                doesOpenDoor1 = true;
            }
        }
    }

    //扉2の謎の答えのチェック
    void CheckDoor2MysteryAnswer()
    {
        if ((numberAlphabets2[0] == 14) && (numberAlphabets2[1] == 15) && (numberAlphabets2[2] == 4) 
            && (numberAlphabets2[3] == 13))
        {
            if (doesOpenDoor2 == false)
            {
                DisplayMessage("扉が横にずれて次の部屋への道が現れた。");
                GameObject ButtonSecondDoor = GameObject.Find("ButtonSecondDoor");

                DisabledButton2(GameObject.Find("ButtonMystery2"));
                ButtonAllDisabledPush(buttonAlphabets2);
                Vector3 posDoor2 = ButtonSecondDoor.transform.localPosition;
                ButtonSecondDoor.transform.localPosition = new Vector3(posDoor2.x + 200.0f, posDoor2.y, posDoor2.z);

                ButtonSecondDoorOpen.SetActive(true);
                doesOpenDoor2 = true;
            }
        }
    }

    //扉3の謎の答えのチェック
    void CheckDoor3MysteryAnswer()
    {
        if ((numberAlphabets3[0] == 12) && (numberAlphabets3[1] == 13) && (numberAlphabets3[2] == 10)
            && (numberAlphabets3[3] == 11))
        {
            if (doesOpenDoor3 == false)
            {
                DisplayMessage("扉が横にずれて次の部屋への道が現れた。");
                GameObject ButtonThirdDoor = GameObject.Find("ButtonThirdDoor");

                DisabledButton2(GameObject.Find("ButtonMystery3"));
                ButtonAllDisabledPush(buttonAlphabets3);
                Vector3 posDoor3 = ButtonThirdDoor.transform.localPosition;
                ButtonThirdDoor.transform.localPosition = new Vector3(posDoor3.x + 200.0f, posDoor3.y, posDoor3.z);

                ButtonThirdDoorOpen.SetActive(true);
                doesOpenDoor3 = true;
            }
        }
    }

    //牢屋の鍵の謎の答えのチェック
    void CheckPrisonRockMysteryAnswer()
    {
        if ((numberAlphabets4[0] == 4) && (numberAlphabets4[1] == 2) && (numberAlphabets4[2] == 20))
        {
            if (doesOpenPrison == false)
            {
                DisplayMessage("「ガシャ」と音がして牢屋の鍵が開いた。");

                GameObject.Find("ImagePrison").GetComponent<Image>().sprite = imagePrisonOpen;
                GameObject.Find("ButtonPrisonRock").SetActive(false);
                GameObject.Find("ImageBraveManInPrison").SetActive(false);
                GameObject.Find("ButtonBraveManInPrison").SetActive(false);
                ButtonAllDisabledPush(buttonAlphabets4);

                ButtonBraveManOutPrison.SetActive(true);
                ButtonHammerOnPanel.SetActive(true);

                doesOpenPrison = true;
                doesSaveBraveMan = true;
            }
        }
    }
}
