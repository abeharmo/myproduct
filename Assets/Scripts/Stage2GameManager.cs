using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Stage2GameManager : GameManagerBase
{
    //各パネルの定数定義
    public const int WALL5 = 4;     //PanelGround
    public const int WALL6 = 5;     //PanelBox
    public const int WALL7 = 6;     //PanelStornMonument
    public const int WALL8 = 7;     //PanelInTreeHouse
    public const int WALL9 = 8;     //PanelPrisonRock

    //各記号の定数定義
    public const int CIRCLE   = 0;      //〇
    public const int CROSS    = 1;      //×
    public const int TRIANGLE = 2;      //△
    public const int SQUARE   = 3;      //□

    //各アイテムのアイコン
    public GameObject ButtonKeyUpIcon;      //鍵上部
    public GameObject ButtonKeyDownIcon;    //鍵下部
    public GameObject ButtonKeyIcon;        //鍵下部
    public GameObject ButtonShovelIcon;     //スコップ
    public GameObject ButtonSwordIcon;      //剣

    //各アイテム（アイテムメニューに表示）
    public GameObject ButtonKeyUp;     //ボタン：鍵上部
    public GameObject ButtonKeyDown;   //ボタン：鍵下部
    public GameObject ButtonKey;       //ボタン：鍵
    public GameObject ButtonShovel;    //ボタン：スコップ
    public GameObject ButtonSword;     //ボタン：剣

    //イラスト（パネル内に表示）
    public GameObject ButtonShovelPrison;        //ボタン：スコップ（牢屋内）
    public GameObject ButtonMagicArrayZoom;      //ボタン：魔法陣
    public GameObject ButtonBraveManOutPrison;   //ボタン：勇者（牢屋の外）

    //メッセージウィンドウ
    public GameObject buttonFlagMessage;        //フラグのテキストウィンドウ
    public GameObject buttonFlagMessageText;    //フラグのテキスト

    //ボタン：一つ目の謎
    public GameObject[] buttonMystery1 = new GameObject[4];
    //ボタン：二つ目の謎
    public GameObject[] buttonMystery2 = new GameObject[4];
    //ボタン：三つ目の謎
    public GameObject[] buttonMystery3 = new GameObject[3];

    //イラスト（パネル内の背景等）
    public Sprite imageBoxOpen;
    public Sprite imagePrisonOpen;
    public Sprite imageMagiArrayDigged;

    //絵：記号
    public Sprite[] symbolPicture = new Sprite[4];

    // 効果音
    public AudioClip completeSE;                    //効果音：フラグメッセージの出現
    public AudioClip openBoxSE;                     //効果音：箱が開く
    public AudioClip openKeySE;                     //効果音：鍵が開く
    public AudioClip openWoodDoorSE;                //効果音：木のドア開ける
    public AudioClip closeWoodDoorSE;               //効果音：木のドアを閉める
    public AudioClip prisonSE;                      //効果音：牢屋ガシャガシャ
    public AudioClip diggedShovelSE;                //効果音：スコップで魔法陣を掘る
    public AudioClip mergedKeySE;                   //効果音：鍵を組み合わせる
    public AudioClip fallItemSE;                    //効果音：鍵が落ちてくる

    //記号の種類
    private int[] symbolKind1 = new int[4];
    private int[] symbolKind2 = new int[4];

    //アイテムの所持
    private bool doesHaveKeyUp;       //鍵上部
    private bool doesHaveKeyDown;     //鍵下部
    private bool doesHaveKey;         //鍵
    private bool doesHaveShovel;      //スコップ
    private int  doesHaveSword;       //剣 （セーブの都合でint型）
    //ドアの状態
    private bool doesOpenHouse;       //家のドア
    private bool doesOpenPrison;      //牢屋の鍵
    //勇者の状態
    private bool doesSaveBraveMan;

    //初期設定
    void Start()
    {
        base.Start();
        doesHaveKeyUp    = false;     
        doesHaveKeyDown  = false; 
        doesHaveKey      = false;    
        doesHaveShovel   = false;
        doesHaveSword    = 0;
        doesOpenHouse    = false;
        doesOpenPrison   = false;
        doesSaveBraveMan = false;

        DisabledButton2(ButtonShovelPrison);
        DisabledButton2(ButtonMagicArrayZoom);

    }


    //魔法陣を押したらPanelGroundへ
    public void PushMagicArray()
    {
        wallNow = WALL5;
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        audioSource.PlayOneShot(changePanelSE);
        DisplayWallExtended();
    }
    //机の上の箱を押したらPanelBoxへ
    public void PushBox()
    {
        wallNow = WALL6;
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        audioSource.PlayOneShot(changePanelSE);
        DisplayWallExtended();
    }
    //石碑を押したらPanelStornMonumentへ
    public void PushStornMonument()
    {
        wallNow = WALL7;
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        audioSource.PlayOneShot(changePanelSE);
        DisplayWallExtended();
    }
    //木の家のドアを押したらPanelInTreeHouseへ
    public void PushTreeHouseDoor()
    {
        if (doesHaveKey == true)
        {
            wallNow = WALL8;
            ChangeArrowActive(0, ButtonLeft);
            ChangeArrowActive(0, ButtonRight);
            audioSource.PlayOneShot(changePanelSE);
            DisplayWallExtended();
            audioSource.PlayOneShot(openWoodDoorSE);

            if (doesOpenHouse == false)
            {
                DisplayMessage("鍵を使ってドアを開けた。");
                doesOpenHouse = true;
            }
        }
        else
        {
            DisplayMessage("ドアには鍵が掛かっている。");
            audioSource.PlayOneShot(closeWoodDoorSE);
        }
    }
    //牢屋のドアを押したらPanelPrisonRockへ
    public void PushPrison()
    {
        wallNow = WALL9;
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        audioSource.PlayOneShot(changePanelSE);
        DisplayWallExtended();
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

    //勇者を押したら
    public void PushButtonBraveMan()
    {
        if (doesHaveSword == 1)
        {
            DisplayFlagMessage("それは僕の愛剣！見つけてくれてありがとう！");
            audioSource.PlayOneShot(completeSE);
        }
        else if (doesSaveBraveMan == true)
        {
            DisplayFlagMessage("助けてくれてありがとう！あとは愛剣が見つかれば…。");
            audioSource.PlayOneShot(completeSE);
        }
        else
        {
            DisplayMessage("変わった服装をしているそこの君！僕をここから出してくれないか？！" +
                           "魔王によって閉じ込められたんだ！");
            audioSource.PlayOneShot(prisonSE);
        }

    }


    //木のスコップを押したら
    public void PushButtonShovelPrison()
    {
        DisplayMessage("木のスコップを入手した。");
        DisabledButton1(buttonItem);
        ButtonShovelPrison.SetActive(false);
        GameObject.Find("ImageShovel").SetActive(false);

        EnabledButton2(ButtonMagicArrayZoom);
        ButtonShovelIcon.SetActive(true);
        ButtonShovel.SetActive(true);
        doesHaveShovel = true;
    }

    //魔法陣を押したら
    public void PushButtonMagicArrayZoom()
    {
        DisplayMessage("スコップで魔法陣を掘ると………\n" +
                       "剣が埋まっていた！");
        audioSource.PlayOneShot(diggedShovelSE);
        DisabledButton1(buttonItem);
        DisabledButton2(ButtonMagicArrayZoom);
        ButtonMagicArrayZoom.GetComponent<Image>().sprite = imageMagiArrayDigged;

        ButtonSwordIcon.SetActive(true);
        ButtonSword.SetActive(true);
        doesHaveSword = 1;
    }


    //パネル1へ戻る
    public void BackPanelWall1()
    {
        audioSource.PlayOneShot(changePanelSE);
        BackPanelWall(WALL1);
    }
    //パネル2へ戻る
    public void BackPanelWall2()
    {
        audioSource.PlayOneShot(changePanelSE);
        BackPanelWall(WALL2);
    }
    //パネル3へ戻る
    public void BackPanelWall3()
    {
        audioSource.PlayOneShot(changePanelSE);
        BackPanelWall(WALL3);
    }
    //パネル4へ戻る
    public void BackPanelWall4()
    {
        audioSource.PlayOneShot(changePanelSE);
        BackPanelWall(WALL4);
    }
    //パネルWallInTreeHousに戻る
    public void BackPanelWallInTreeHouse()
    {
        wallNow = WALL8;
        DisplayWallExtended();
    }


    //謎1の解答の入力の処理
    public void PushButtonM1(int i)
    {
        ChangeButtonSymbol1(i);
        CheckMystery1Answer(symbolKind1);
    }
    //謎2の解答の入力の処理
    public void PushButtonM2(int i)
    {
        ChangeButtonSymbol2(i);
        CheckMystery2Answer(symbolKind2);
    }
    //謎3の解答の入力の処理
    public void PushButtonM3(int i)
    {
        ChangeButtonNumber(i);
        CheckMystery3Answer();
    }


    //鍵上部のアイコン消す処理
    public void PushButtonKeyUpIcon()
    {
        ButtonKeyUpIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonBackPanelWall2"));
        EnabledButton1(buttonItem);
    }
    //鍵下部のアイコンを消す処理
    public void PushButtonKeyDownIcon()
    {
        ButtonKeyDownIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonBackPanelWall3"));
        EnabledButton1(buttonItem);
    }

    //鍵のアイコンを消す処理
    public void PushButtonKeyIcon()
    {
        ButtonKeyIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonReturnGame"));
        EnabledButton1(GameObject.Find("ButtonReturnTitle"));
    }

    //スコップのアイコンを消す処理
    public void PushButtonShovelIcon()
    {
        ButtonShovelIcon.SetActive(false);
        EnabledButton1(buttonItem);
    }

    //スコップのアイコンを消す処理
    public void PushButtonSwordIcon()
    {
        ButtonSwordIcon.SetActive(false);
        EnabledButton1(buttonItem);
    }

    //アイテムの選択
    public void PushSomeIcon(Sprite someItem)
    {
        Sprite alt1 = ButtonKeyUpIcon.GetComponent<Image>().sprite;
        Sprite alt2 = ButtonKeyDownIcon.GetComponent<Image>().sprite;
        Sprite alt3 = ImageSelectedItem.GetComponent<Image>().sprite;

        if ( ((someItem == alt1)&&(alt3 == alt2)) || ((someItem == alt2)&&(alt3 == alt1)) ){
            MergeKey();
            ImageSelectedItem.SetActive(false);
        }
        else {
            ImageSelectedItem.GetComponent<Image>().sprite = someItem;
            ImageSelectedItemLarge.GetComponent<Image>().sprite = someItem;
            ImageSelectedItem.SetActive(true);
            ButtonSelectedItemLarge.SetActive(true);
        }
    }

    //次のステージへのボタン
    public void PushButtonNextStage()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 2);
        PlayerPrefs.SetInt("SWORD2", doesHaveSword);
        PlayerPrefs.Save();
        audioSource.PlayOneShot(saveSE);
        SceneManager.LoadScene("GameScene_stage3");
    }

    //タイトルに戻るボタン
    public void PushButtonReturnTitleFromStage2()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 2);
        PlayerPrefs.SetInt("SWORD2", doesHaveSword);
        PlayerPrefs.Save();
        audioSource.PlayOneShot(saveSE);
        SceneManager.LoadScene("TitleScene");
    }

    //WALL5～9への画面遷移の処理
    void DisplayWallExtended()
    {
        if ((wallNow < WALL5) && (wallNow > -1))
        {
            DisplayWallBase();
        }
        else if ((wallNow >= WALL5) && (wallNow <= WALL9))
        {
            if (buttonMessage.activeSelf == true)
            {
                DeleteMessage();
            }

            switch (wallNow)
            {
                case WALL5:
                    Vector3 tmp1 = GameObject.Find("PanelGround").transform.localPosition;
                    panelWalls.transform.localPosition = tmp1 * -1;
                    break;
                case WALL6:
                    Vector3 tmp2 = GameObject.Find("PanelBox").transform.localPosition;
                    panelWalls.transform.localPosition = tmp2 * -1;
                    break;
                case WALL7:
                    Vector3 tmp3 = GameObject.Find("PanelStornMonument").transform.localPosition;
                    panelWalls.transform.localPosition = tmp3 * -1;
                    break;
                case WALL8:
                    Vector3 tmp4 = GameObject.Find("PanelInTreeHouse").transform.localPosition;
                    panelWalls.transform.localPosition = tmp4 * -1;
                    break;
                case WALL9:
                    Vector3 tmp5 = GameObject.Find("PanelPrisonRock").transform.localPosition;
                    panelWalls.transform.localPosition = tmp5 * -1;
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


    //謎1の解答の記号の変更の処理
    void ChangeButtonSymbol1(int buttonNo)
    {
        symbolKind1[buttonNo] = (symbolKind1[buttonNo] + 1) % symbolKind1.Length;
        buttonMystery1[buttonNo].GetComponent<Image>().sprite = symbolPicture[symbolKind1[buttonNo]];
        audioSource.PlayOneShot(changeMysteryObjectSE);
    }

    //謎2の解答の記号の変更の処理
    void ChangeButtonSymbol2(int buttonNo)
    {
        symbolKind2[buttonNo] = (symbolKind2[buttonNo] + 1) % symbolKind2.Length;
        buttonMystery2[buttonNo].GetComponent<Image>().sprite = symbolPicture[symbolKind2[buttonNo]];
        audioSource.PlayOneShot(changeMysteryObjectSE);
    }

    //謎3の解答の数字の変更の処理
    void ChangeButtonNumber(int buttonNo)
    {
        int numberKind = int.Parse(buttonMystery3[buttonNo].GetComponentInChildren<Text>().text);
        string numberString = ((numberKind + 1) % 10).ToString("0");
        buttonMystery3[buttonNo].GetComponentInChildren<Text>().text = numberString;
        audioSource.PlayOneShot(changeMysteryObjectSE);
    }

    //謎1の答えのチェック
    void CheckMystery1Answer(int[] symbolKind)
    {
        if (doesHaveKeyUp == false)
        {
            if ((symbolKind[0] == CIRCLE) && (symbolKind[1] == CROSS) && (symbolKind[2] == TRIANGLE) && (symbolKind[3] == SQUARE))
            {
                DisplayMessage("「カシャ」という音が聞こえて箱が空いた。中には金属のかけらのようなものが入っていた。");
                audioSource.PlayOneShot(openBoxSE);

                DisabledButton1(buttonItem);
                DisabledButton1(GameObject.Find("ButtonBackPanelWall2"));
                GameObject.Find("ButtonBoxClose").GetComponent<Image>().sprite = imageBoxOpen;
                DisabledButton2(GameObject.Find("ButtonBoxClose"));
                DisabledButton2(GameObject.Find("ButtonBoxClose"));

                ButtonKeyUpIcon.SetActive(true);
                ButtonKeyUp.SetActive(true);
                doesHaveKeyUp = true;
            }
        }
    }

    //謎2の答えのチェック
    void CheckMystery2Answer(int[] symbolKind)
    {
        if ((symbolKind[0] == SQUARE) && (symbolKind[1] == CROSS) && (symbolKind[2] == TRIANGLE) && (symbolKind[3] == CIRCLE))
        {
            if (doesHaveKeyDown == false)
            {
                DisplayMessage("空から何か落ちてきた。どうやら金属のかけらのようなものだ。");
                audioSource.PlayOneShot(fallItemSE);

                DisabledButton1(buttonItem);
                DisabledButton1(GameObject.Find("ButtonBackPanelWall3"));

                ButtonKeyDownIcon.SetActive(true);
                ButtonKeyDown.SetActive(true);
                doesHaveKeyDown = true;
            }
        }
    }

    //謎3の答えのチェック
    void CheckMystery3Answer()
    {
        int number1 = int.Parse(buttonMystery3[0].GetComponentInChildren<Text>().text);
        int number2 = int.Parse(buttonMystery3[1].GetComponentInChildren<Text>().text);
        int number3 = int.Parse(buttonMystery3[2].GetComponentInChildren<Text>().text);

        if ((number1 == 5) && (number2 == 2) && (number3 == 9))
        {
            if (doesOpenPrison == false)
            {
                DisplayMessage("「ガシャ」と音がして牢屋の鍵が開いた。");
                audioSource.PlayOneShot(openKeySE);
                GameObject.Find("ImagePrison").GetComponent<Image>().sprite = imagePrisonOpen;
                GameObject.Find("ButtonPrisonRock").SetActive(false);
                GameObject.Find("ImageBraveManInPrison").SetActive(false);
                GameObject.Find("ButtonBraveManInPrison").SetActive(false);
                ButtonBraveManOutPrison.SetActive(true);

                EnabledButton2(ButtonShovelPrison);

                doesOpenPrison   = true;
                doesSaveBraveMan = true;
            }
        }
    }

    //鍵上部と鍵下部の合成
    void MergeKey()
    {
        DisplayMessage("二つの金属片が組み合わさって鍵になった。");
        audioSource.PlayOneShot(mergedKeySE);

        DisabledButton1(ButtonKeyDown);
        DisabledButton1(ButtonKeyUp);
        DisabledButton1(GameObject.Find("ButtonReturnGame"));
        DisabledButton1(GameObject.Find("ButtonReturnTitle"));

        ButtonKeyIcon.SetActive(true);
        ButtonKey.SetActive(true);
        doesHaveKey = true;

    }


}
