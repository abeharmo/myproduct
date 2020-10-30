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
    //PanelCaveFire1～4
    public const int WALL5  = 4;       
    public const int WALL6  = 5;       
    public const int WALL7  = 6;       
    public const int WALL8  = 7;
    //PanelCaveWater1～4
    public const int WALL9  = 8;       
    public const int WALL10 = 9;      
    public const int WALL11 = 10;     
    public const int WALL12 = 11;
    //PanelCaveWiind1～4
    public const int WALL13 = 12;     
    public const int WALL14 = 13;     
    public const int WALL15 = 14;     
    public const int WALL16 = 15;
    //PanelCavePrison
    public const int WALL17 = 16;
    //拡大パネル
    public const int WALL18 = 17;   //赤い箱
    public const int WALL19 = 18;   //青い台
    public const int WALL20 = 19;   //青い箱
    public const int WALL21 = 20;   //金庫
    public const int WALL22 = 21;   //石碑
    public const int WALL23 = 22;   //牢屋の鍵


    //色を数字で管理
    public const int RED       = 0;  //赤
    public const int GREEN     = 1;  //緑
    public const int BLUE      = 2;  //青
    public const int YELLOW    = 3;  //黄色
    public const int PURPLE    = 4;  //紫
    public const int LIGHTBLUE = 5;  //水色

    //石碑の線及びろうそくが光っているか数字で管理
    public const int DARK      = 0;  //光っていない
    public const int LIGHT     = 1;  //光っている


    //各アイテムのアイコン
    public GameObject ButtonBookIcon;          //本
    public GameObject ButtonBallsIcon;         //6個の球
    public GameObject ButtonHint1Icon;         //赤い箱の謎のヒント
    public GameObject ButtonAshIcon;           //灰
    public GameObject ButtonBallsColorIcon;    //6個の色つきボール
    public GameObject ButtonKeyIcon;           //鍵
    public GameObject ButtonWateringCanIcon;   //じょうろ
    public GameObject ButtonMatchIcon;         //マッチ
    public GameObject ButtonOrbWindIcon;       //風のオーブ
    public GameObject ButtonOrbWaterIcon;      //水のオーブ
    public GameObject ButtonOrbFireIcon;       //火のオーブ
    public GameObject ButtonPickelIcon;        //ピッケル
    public GameObject ButtonSwordIcon;         //剣


    //各アイテム（アイテムメニューに表示）
    public GameObject ButtonBook;             //パネル上の本
    public GameObject ButtonBalls;            //6個 の球
    public GameObject ButtonHint1;            //赤い箱の謎のヒント
    public GameObject ButtonAsh;              //灰
    public GameObject ButtonBallsColor;       //6個の色つきボール
    public GameObject ButtonKey;              //鍵
    public GameObject ButtonWateringCan;      //じょうろ
    public GameObject ButtonMatch;            //マッチ
    public GameObject ButtonOrbWind;          //風のオーブ
    public GameObject ButtonOrbWater;         //水のオーブ
    public GameObject ButtonOrbFire;          //火のオーブ
    public GameObject ButtonPickel;           //ピッケル
    public GameObject ButtonSword;            //剣

    //イラスト（パネル内に表示）
    public GameObject ButtonBookOnPanel;         //ボタン：パネル上の本
    public GameObject ButtonBallsOnPanel;        //ボタン：パネル上のボール
    public GameObject ButtonBurnFire;            //ボタン：焚火
    public GameObject ButtonLake;                //ボタン：湖
    public GameObject ButtonSwitch;              //ボタン：炎、水の間の謎を開放するスイッチ
    public GameObject ButtonCaveLoad4;           //ボタン：牢屋へ通じる通路
    public GameObject ImageCaveLoad4;            //画像：牢屋へ通じる通路
    public GameObject ButtonBraveManOutPrison;   //ボタン：勇者（牢屋の外）
    public GameObject ButtonPickelOnLake;        //ボタン：パネル上のピッケル

    public GameObject ImageBoxOpenRed;          //画像：赤い開いてる箱
    public GameObject ImageBoxOpenBlue;         //画像：青い開いてる箱
    public GameObject imageAshFly;              //画像：空中に浮かぶ灰
    public GameObject imageBallsColorFly;       //画像；空中に浮かぶ色つきボール


    //メッセージウィンドウ
    public GameObject buttonFlagMessage;           //フラグのテキストウィンドウ
    public GameObject buttonFlagMessageText;       //フラグのテキスト
    public GameObject panelMissingMessage;         //謎が解けなかった時にテキストを表示するパネル
    public GameObject buttonMissingMessage;        //謎が解けなかった時のテキストウィンドウ
    public GameObject buttonMissingMessageText;    //謎が解けなかった時のテキスト

    //イラスト（パネル内の背景等）
    public Sprite imageValutOpen;           //画像：開いている金庫
    public Sprite imagePrisonOpen;          //画像；開いている牢屋
    
    //イラスト：オーブ
    public GameObject imageOrbWind;             //画像：風のオーブ
    public GameObject imageOrbWater;            //画像：水のオーブ
    public GameObject imageOrbFire;             //画像：火のオーブ


    //謎に使われているオブジェクトのアクティブ状態を管理
    public GameObject Beakers;             //ビーカー
    public GameObject Bumboos;             //竹
    public GameObject Candles;             //ろうそく

    //ボタン：謎に使われているオブジェクト
    public GameObject[] buttonNumbers = new GameObject[6];    //赤い箱の数字
    public GameObject[] buttonColors  = new GameObject[6];    //青い箱の色つきボタン
    public GameObject[] buttonLines   = new GameObject[12];   //石碑の線
    public GameObject[] buttonCandles = new GameObject[6];    //ろうそく
    public GameObject[] buttonBumboos = new GameObject[6];    //竹
    public GameObject[] buttonBeakers = new GameObject[3];    //ビーカー
    public GameObject[] buttonPrison  = new GameObject[18];   //牢屋の鍵

    //イラストの種類
    public Sprite[] imageLine     = new Sprite[2];
    public Sprite[] imageCandle   = new Sprite[2];
    public Sprite[] imageBumboos  = new Sprite[6];
    public Sprite[] imageBeaker10 = new Sprite[11];
    public Sprite[] imageBeaker7  = new Sprite[8];
    public Sprite[] imageBeaker3  = new Sprite[4];

    //謎のイラスト管理用ナンバー
    private int[] numberColors = new int[6]; 
    private int[] numberLine   = new int[12];
    private int[] numberCandle = new int[6];
    private int[] numberBumboo = new int[6];
    private int[] numberBeaker = new int[3];

    //水の間の謎1のボタンを押した回数の管理
    private int numberManage;
    //水の間の謎2
    private int[] beakerSelected = new int[2];      //選択したビーカーの管理
    private int beakerManage;                       //ビーカーの選んだ順番
    //炎の間の謎2
    private int candleManage;                       //ろうそくの火を灯した回数の管理
    private int[] orderManage = new int [6];        //ろうそくの火を灯した順番の管理

    //アイテムの所持
    private bool doesHaveBalls;        //6個のボール     
    private bool doesHaveBook;         //本
    private bool doesHaveAsh;          //灰
    private bool doesHaveBallsColor;   //6個の色付きボール
    private bool doesHaveKey;          //鍵
    private bool doesHaveWateringCan;  //じょうろ
    private bool doesHaveMatch;        //マッチ     
    private bool doesHaveOrbFire;      //火のオーブ
    private bool doesHaveOrbWater;     //水のオーブ
    private bool doesHaveOrbWind;      //風のオーブ
    private bool doesHavePickel;       //ピッケル
    private int  doesHaveSword;        //剣 （セーブの都合でint型）
    //オブジェクトの状態
    private bool doesOpenBoxRed;       //赤い箱の状態
    private bool doesOpenBoxBlue;      //青い箱の状態 
    private bool doesOpenPrison;       //牢屋の鍵
    //勇者の状態
    private bool doesSaveBraveMan;
    //風の間の条件の状態
    private bool doesFlyAsh;
    private bool doesFlyBallsColor;

//初期設定
void Start()
    {
        base.Start();
        doesHaveBook        = false;
        doesHaveBalls       = false;
        doesHaveAsh         = false;
        doesHaveBallsColor  = false;
        doesHaveKey         = false;
        doesHaveWateringCan = false;
        doesHaveMatch       = false;
        doesHaveOrbFire     = false;
        doesHaveOrbWater    = false;
        doesHaveOrbWind     = false;

        doesHaveSword   = 0;

        doesFlyAsh         = false;
        doesFlyBallsColor  = false;
        doesOpenBoxRed     = false;
        doesOpenBoxBlue    = false;
        doesOpenPrison     = false;
        doesSaveBraveMan   = false;

        numberBeaker[0]    = 10;
        numberBeaker[1]    = 0;
        numberBeaker[2]    = 0;

        DisabledButton2(ButtonBurnFire);
        DisabledButton2(ButtonLake);

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

    //勇者を押したら
    public void PushButtonBraveMan()
    {
        if (doesHaveSword == 1)
        {
            DisplayFlagMessage("剣まで見つけてくれたのか！君はとてもいい人だな。本当に感謝するよ！");
        }
        else if (doesSaveBraveMan == true)
        {
            DisplayFlagMessage("助かったよ！どこかに持っていかれた剣も探さねば。");
        }
        else
        {
            DisplayMessage("すまないが、ここから出るのを手伝ってくれないか？" +
                           "魔王の狡猾な罠にはまってしまった…。");
        }

    }

    //赤い時計を押したら
    public void PushButtonClockRed()
    {
        DisplayMessage("22時10分30秒を指している。");
    }
    //本を押したら
    public void PushButtonBook()
    {
        DisplayMessage("本を入手した。");
        DisabledButton1(buttonItem);
        ButtonBookOnPanel.SetActive(false);
        DisabledButton1(GameObject.Find("ButtonCaveLoad2"));
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);

        EnabledButton2(ButtonBurnFire);
        ButtonBookIcon.SetActive(true);
        ButtonBook.SetActive(true);
        doesHaveBook = true;
    }
    //ボールを押したら
    public void PushButtonBalls()
    {
        DisplayMessage("数字の書かれた球を入手した。");
        DisabledButton1(buttonItem);
        ButtonBallsOnPanel.SetActive(false);
        DisabledButton1(GameObject.Find("ButtonCaveLoad3"));
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);

        EnabledButton2(ButtonLake);
        ButtonBallsIcon.SetActive(true);
        ButtonBalls.SetActive(true);
        doesHaveBalls = true;
    }

    //焚火を押したら
    public void PushButtonBurnFire()
    {
        DisplayMessage("「火の魔術書」と書かれた本を焚火で燃やすと、数字が書かれた紙と灰を入手した。");
        DisabledButton1(buttonItem);
        DisabledButton2(ButtonBurnFire);
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        ButtonBook.SetActive(false);

        ButtonAshIcon.SetActive(true);
        ButtonHint1Icon.SetActive(true);
        ButtonAsh.SetActive(true);
        ButtonHint1.SetActive(true);
        doesHaveAsh = true;
    }

    //湖を押したら
    public void PushButtonLake()
    {
        DisplayMessage("ボールを水につけるとボールに色が浮かび上がった。");
        DisabledButton1(buttonItem);
        DisabledButton2(ButtonLake);
        DisabledButton2(GameObject.Find("ButtonClockBlue"));
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        ButtonBalls.SetActive(false);

        ButtonBallsColorIcon.SetActive(true);
        ButtonBallsColor.SetActive(true);
        doesHaveBallsColor = true;
    }

    //緑の台の魔法陣を押したら
    public void PushButtonAirBrow()
    {
        if (doesHavePickel)
        {
            DisplayMessage("魔法陣の書かれた台をピッケルでたたき割ると剣が出てきた。台の破片は砂のように崩れて消えていった。");
            imageAshFly.SetActive(false);
            imageBallsColorFly.SetActive(false);
            DisabledButton1(buttonItem);
            ChangeArrowActive(0, ButtonLeft);
            ChangeArrowActive(0, ButtonRight);
            GameObject.Find("ImageMagicArray").SetActive(false);
            GameObject.Find("ImageAirBrow").SetActive(false);
            GameObject.Find("ButtonAirBrow").SetActive(false);

            ButtonSwordIcon.SetActive(true);
            ButtonSword.SetActive(true);
            doesHaveSword = 1;
        }
        else if (doesHaveAsh && !(doesFlyAsh)) 
        {
            DisplayMessage("灰を魔法陣の上に置くと灰が浮かび上がった。");
            ButtonAshIcon.SetActive(false);
            ButtonAsh.SetActive(false);
            imageAshFly.SetActive(true);
            doesFlyAsh = true;
        }
        else if (doesHaveBallsColor && !(doesFlyBallsColor))
        {
            DisplayMessage("色つきのボールを魔法陣の上に置くとボールが浮かび上がった。");
            ButtonBallsColorIcon.SetActive(false);
            ButtonBallsColor.SetActive(false);
            imageBallsColorFly.SetActive(true);
            doesFlyBallsColor = true;
        }
        else
        { 
            DisplayMessage("魔法陣の上に手をかざすと風を感じる。どうやら風を起こすための魔法陣のようだ。");
        }

    }

    //金庫の鍵を押したら
    public void PushButtonValutKeyHole()
    {
        if (doesHaveKey) 
        {
            DisplayMessage("金庫の鍵が開いた。");
            GameObject.Find("PanelValut").GetComponent<Image>().sprite = imageValutOpen;
            GameObject.Find("ButtonKeyHole").SetActive(false);
            ButtonKey.SetActive(false);

            ButtonSwitch.SetActive(true);
        }
        else
        {
            DisplayMessage("金庫は鍵が掛かっていて開かない。");
        }
    }

    //金庫の中のボタンを押したら
    public void PushButtonSwitch()
    {
        DisplayMessage("どこかの台から何か出たようだ。");
        DisabledButton2(ButtonSwitch);

        Beakers.SetActive(true);
        Candles.SetActive(true);
    }

    //風の間の地面のボタンを押したら
    public void PushButtonGround()
    {
        if (!(doesHaveWateringCan))
        {
            DisplayMessage("柔らかい地面だ。");
        }
        else
        {
            DisplayMessage("じょうろで水をかけると竹が生えてきた。");
            DisabledButton2(GameObject.Find("ButtonGround"));

            Bumboos.SetActive(true);
        }
    }

    //台座のボタンを押したら
    public void PushButtonStageOrb()
    {
        if (doesHaveOrbWind && !(imageOrbWind.activeSelf))
        {
            DisplayMessage("緑のオーブを台座の丸いくぼみにはめた。");
            DisabledButton1(ButtonOrbWind);
            imageOrbWind.SetActive(true);
        }
        else if (doesHaveOrbWater && !(imageOrbWater.activeSelf))
        {
            DisplayMessage("青のオーブを台座の丸いくぼみにはめた。");
            DisabledButton1(ButtonOrbWater);
            imageOrbWater.SetActive(true);
        }
        else if (doesHaveOrbFire && !(imageOrbFire.activeSelf))
        {
            DisplayMessage("赤のオーブを台座の丸いくぼみにはめた。");
            DisabledButton1(ButtonOrbFire);
            imageOrbFire.SetActive(true);
        }
        else
        {
            DisplayMessage("三つの丸いくぼみがある台座だ。");
        }

        if(imageOrbWind.activeSelf && imageOrbWater.activeSelf && imageOrbFire.activeSelf)
        {
            DisplayMessage("「ゴゴゴゴゴ……」と音を立てて、台座の後ろの壁が開いたようだ。");
            DisabledButton2(GameObject.Find("ButtonStageOrb"));
            ImageCaveLoad4.SetActive(true);
            ButtonCaveLoad4.SetActive(true);
        }
    }

    //湖の近くのピッケルを押したら
    public void PushButtonPickelOnLake()
    {
        DisplayMessage("ピッケルを拾った。");
        DisabledButton1(buttonItem);
        DisabledButton2(GameObject.Find("ButtonClockBlue"));
        ChangeArrowActive(0, ButtonLeft);
        ChangeArrowActive(0, ButtonRight);
        ButtonOrbWind.SetActive(false);
        ButtonPickelOnLake.SetActive(false);

        ButtonPickelIcon.SetActive(true);
        ButtonPickel.SetActive(true);
        doesHavePickel = true;
    }

    //左矢印を押したときの処理
    public void PushLeftArrowButton1()
    {   
        if ((wallNow >= 0) && (wallNow < 4))
        {
            wallNow--;
            wallNow = (wallNow + 4) % 4;
        }
        else if((wallNow >= 4) && (wallNow < 8))
        {
            wallNow--;
            wallNow = (wallNow + 4) % 4 + 4;
        }
        else if ((wallNow >= 8) && (wallNow < 12))
        {
            wallNow--;
            wallNow = (wallNow + 4) % 4 + 8;
        }
        else if((wallNow >= 12) && (wallNow < 16))
        {
            wallNow--;
            wallNow = (wallNow + 4) % 4 + 12;
        }
        else
        {
            UnityEngine.Debug.Log("PushLeftArrow Error");
        }

        DisplayWallExtended();
    }

    //右矢印を押したときの処理
    public void PushRightArrowButton1()
    {
        if ((wallNow >= 0) && (wallNow < 4))
        {
            wallNow++;
            wallNow = wallNow % 4;
        }
        else if ((wallNow >= 4) && (wallNow < 8))
        {
            wallNow++;
            wallNow = wallNow % 4 + 4;
        }
        else if ((wallNow >= 8) && (wallNow < 12))
        {
            wallNow++;
            wallNow = wallNow % 4 + 8;
        }
        else if ((wallNow >= 12) && (wallNow < 16))
        {
            wallNow++;
            wallNow = wallNow % 4 + 12;
        }
        else
        {
            UnityEngine.Debug.Log("PushRightArrow Error");
        }

        DisplayWallExtended();
    }

    //他のパネルへ移動するための関数
    public void PushButtonMoveToSomePanel(int number)
    {
        wallNow = number;
        if((wallNow >= 0)&&(wallNow < 16))
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
    

    //本のアイコン消す処理
    public void PushButtonBookIcon()
    {
        ButtonBookIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonCaveLoad2"));
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }
    //6個の球のアイコンを消す処理
    public void PushButtonBallsIcon()
    {
        ButtonBallsIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonCaveLoad3"));
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }
    //灰のアイコンを消す処理
    public void PushButtonAshIcon()
    {
        ButtonAshIcon.SetActive(false);
        EnabledButton1(GameObject.Find("ButtonCaveLoad3"));
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }
    //ヒント1のアイコンを消す処理
    public void PushButtonHint1Icon()
    {
        ButtonHint1Icon.SetActive(false);
    }
    //6個の色つきの球のアイコンを消す処理
    public void PushButtonBallsColorIcon()
    {
        ButtonBallsColorIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton2(GameObject.Find("ButtonClockBlue"));
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }
    //鍵のアイコンを消す処理
    public void PushButtonKeyIcon()
    {
        ButtonKeyIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelCaveFire2"));
    }
    //じょうろのアイコンを消す処理
    public void PushButtonWateringCanIcon()
    {
        ButtonWateringCanIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelCaveWater2"));
    }
    //マッチのアイコンを消す処理
    public void PushButtonMatchIcon()
    {
        ButtonMatchIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelCaveWind4"));
    }
    //風のオーブのアイコンを消す処理
    public void PushButtonOrbWindIcon()
    {
        ButtonOrbWindIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }

    //火のオーブのアイコンを消す処理
    public void PushButtonOrbFireIcon()
    {
        ButtonOrbFireIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }

    //水のオーブのアイコンを消す処理
    public void PushButtonOrbWaterIcon()
    {
        ButtonOrbWaterIcon.SetActive(false);
        EnabledButton1(buttonItem);
        EnabledButton1(GameObject.Find("ButtonBackPanelCaveWater1"));
    }

    //ピッケルのアイコンを消す処理
    public void PushButtonPickelIcon()
    {
        ButtonPickelIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }

    // 剣のアイコンを消す処理
    public void PushButtonSwordIcon()
    {
        ButtonSwordIcon.SetActive(false);
        EnabledButton1(buttonItem);
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
    }


    //アイテムの選択
    public void PushSomeIcon(Sprite someItem)
    {
        ImageSelectedItem.GetComponent<Image>().sprite = someItem;
        ImageSelectedItem.SetActive(true);
    }

    //次のステージへのボタン
    public void PushButtonNextStage()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 2);
        PlayerPrefs.SetInt("SWORD2", doesHaveSword);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene_stage3");
    }

    //タイトルに戻るボタン
    public void PushButtonReturnTitleFromStage1()
    {
        PlayerPrefs.SetInt("STAGECLEAR", 2);
        PlayerPrefs.SetInt("SWORD2", doesHaveSword);
        PlayerPrefs.Save();
        SceneManager.LoadScene("TitleScene");
    }

    //WALL1～23への画面遷移の処理
    void DisplayWallExtended()
    {
        if ((wallNow >= WALL1) && (wallNow < WALL5))
        {
            DisplayWallBase();
        }
        else if ((wallNow >= WALL5) && (wallNow <= WALL23))
        {
            if (buttonMessage.activeSelf == true)
            {
                DeleteMessage();
            }

            switch (wallNow)
            {
                case WALL5:
                    Vector3 tmp1 = GameObject.Find("PanelCaveFire1").transform.localPosition;
                    panelWalls.transform.localPosition = tmp1 * -1;
                    break;
                case WALL6:
                    Vector3 tmp2 = GameObject.Find("PanelCaveFire2").transform.localPosition;
                    panelWalls.transform.localPosition = tmp2 * -1;
                    break;
                case WALL7:
                    Vector3 tmp3 = GameObject.Find("PanelCaveFire3").transform.localPosition;
                    panelWalls.transform.localPosition = tmp3 * -1;
                    break;
                case WALL8:
                    Vector3 tmp4 = GameObject.Find("PanelCaveFire4").transform.localPosition;
                    panelWalls.transform.localPosition = tmp4 * -1;
                    break;
                case WALL9:
                    Vector3 tmp5 = GameObject.Find("PanelCaveWater1").transform.localPosition;
                    panelWalls.transform.localPosition = tmp5 * -1;
                    break;
                case WALL10:
                    Vector3 tmp6 = GameObject.Find("PanelCaveWater2").transform.localPosition;
                    panelWalls.transform.localPosition = tmp6 * -1;
                    break;
                case WALL11:
                    Vector3 tmp7 = GameObject.Find("PanelCaveWater3").transform.localPosition;
                    panelWalls.transform.localPosition = tmp7 * -1;
                    break;
                case WALL12:
                    Vector3 tmp8 = GameObject.Find("PanelCaveWater4").transform.localPosition;
                    panelWalls.transform.localPosition = tmp8 * -1;
                    break;
                case WALL13:
                    Vector3 tmp9 = GameObject.Find("PanelCaveWind1").transform.localPosition;
                    panelWalls.transform.localPosition = tmp9 * -1;
                    break;
                case WALL14:
                    Vector3 tmp10 = GameObject.Find("PanelCaveWind2").transform.localPosition;
                    panelWalls.transform.localPosition = tmp10 * -1;
                    break;
                case WALL15:
                    Vector3 tmp11 = GameObject.Find("PanelCaveWind3").transform.localPosition;
                    panelWalls.transform.localPosition = tmp11 * -1;
                    break;
                case WALL16:
                    Vector3 tmp12 = GameObject.Find("PanelCaveWind4").transform.localPosition;
                    panelWalls.transform.localPosition = tmp12 * -1;
                    break;
                case WALL17:
                    Vector3 tmp13 = GameObject.Find("PanelCavePrison").transform.localPosition;
                    panelWalls.transform.localPosition = tmp13 * -1;
                    break;
                case WALL18:
                    Vector3 tmp14 = GameObject.Find("PanelBoxRed").transform.localPosition;
                    panelWalls.transform.localPosition = tmp14 * -1;
                    break;
                case WALL19:
                    Vector3 tmp15 = GameObject.Find("PanelStandOneStage").transform.localPosition;
                    panelWalls.transform.localPosition = tmp15 * -1;
                    break;
                case WALL20:
                    Vector3 tmp16 = GameObject.Find("PanelBoxBlue").transform.localPosition;
                    panelWalls.transform.localPosition = tmp16 * -1;
                    break;
                case WALL21:
                    Vector3 tmp17 = GameObject.Find("PanelValut").transform.localPosition;
                    panelWalls.transform.localPosition = tmp17 * -1;
                    break;
                case WALL22:
                    Vector3 tmp18 = GameObject.Find("PanelStornMonument").transform.localPosition;
                    panelWalls.transform.localPosition = tmp18 * -1;
                    break;
                case WALL23:
                    Vector3 tmp19 = GameObject.Find("PanelPrisonRock").transform.localPosition;
                    panelWalls.transform.localPosition = tmp19 * -1;
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


    //炎の間の謎1の入力の処理
    public void PushButtonFM1(int i)
    {
        ChangeButtonNumber(i);
        CheckFireMystery1Answer();
    }

    //炎の間の謎2の入力の処理
    public void PushButtonFM2(int i)
    {
        if (doesHaveMatch) 
        {
            if (numberCandle[i] == 0)
            {
                ChangeCandleOn(i);
                if (candleManage == 6)
                {
                    CheckFireMystery2Answer();
                }
            }
        }
    }

    //水の間の謎1の入力の処理
    public void PushButtonWM1(int i)
    {
        SaveButtonColor(i);
        numberManage++;
        if (numberManage == 6)
        {
            CheckWaterMystery1Answer();
        }
    }

    //水の間の謎2の入力の処理
    public void PushButtonWM2(int i)
    {
        MoveBeakerWater(i);
        CheckWaterMystery2Answer();
    }

    //風の間の謎1の入力の処理
    public void PushButtonWiM1(int i)
    {
        ChangeLineBright(i);
        CheckWindMystery1Answer();
    }

    //風の間の謎2の入力の処理
    public void PushButtonWiM2(int i)
    {
        ChangeBumbooLength(i);
        CheckWindMystery2Answer();
    }

    //牢屋の鍵の謎の入力の処理
    public void PushButtonPrisonM(int i)
    {
        ChangeButtonPriosnRock(i);
        CheckPrisonRockMysteryAnswer();
    }

    //炎の間の謎1の解答の数字の変更の処理
    void ChangeButtonNumber(int buttonNo)
    {
        int numberKind = int.Parse(buttonNumbers[buttonNo].GetComponentInChildren<Text>().text);
        string numberString = ((numberKind + 1) % 10).ToString("0");
        buttonNumbers[buttonNo].GetComponentInChildren<Text>().text = numberString;
    }

    //炎の間の謎2の解答のろうそくの火を灯す処理
    void ChangeCandleOn(int buttonNo)
    {
        numberCandle[buttonNo] = 1;
        orderManage[candleManage] = buttonNo;
        buttonCandles[buttonNo].GetComponent<Image>().sprite = imageCandle[1];
        candleManage++;
    }
    //炎の間の謎2の解答のろうそくの火を全て消す処理
    void ChangeCandleAllOff()
    {
        for (int i=0; i < 6; i++) 
        {
            numberCandle[i] = 0;
            buttonCandles[i].GetComponent<Image>().sprite = imageCandle[0];
        }
    }
    //炎の間のろうそくのボタンを非アクティブにする
    void DisabledButtonCandles()
    {
        for (int i = 0; i < 6; i++)
        {
            DisabledButton2(buttonCandles[i]);
        }
    }

    //水の間の謎1の解答の保存の処理
    void SaveButtonColor(int color)
    {
        numberColors[numberManage] = color;
    }

    //水の間の謎2のビーカーの水の移動の処理
    void MoveBeakerWater(int beakerNo)
    {
        beakerSelected[beakerManage] = beakerNo;
        if (beakerManage == 1)
        {
            if(beakerSelected[0] == beakerSelected[1]) 
            {
                //何もしない
            }
            else if(beakerSelected[1] == 2) //二つ目に選ばれたビーカーがメモリ3のビーカーの時
            {
                if(numberBeaker[beakerSelected[0]] +  numberBeaker[beakerSelected[1]] < 4)
                {
                    numberBeaker[beakerSelected[1]] = numberBeaker[beakerSelected[1]] + numberBeaker[beakerSelected[0]];
                    numberBeaker[beakerSelected[0]] = 0;
                }
                else
                {
                    numberBeaker[beakerSelected[0]] = numberBeaker[beakerSelected[0]] - 3 + numberBeaker[beakerSelected[1]];
                    numberBeaker[beakerSelected[1]] = 3;
                }
            }
            else if (beakerSelected[1] == 1) //二つ目に選ばれたビーカーがメモリ7のビーカーの時
            {
                if (numberBeaker[beakerSelected[0]] + numberBeaker[beakerSelected[1]] < 8)
                {
                    numberBeaker[beakerSelected[1]] = numberBeaker[beakerSelected[1]] + numberBeaker[beakerSelected[0]];
                    numberBeaker[beakerSelected[0]] = 0;
                }
                else
                {
                    numberBeaker[beakerSelected[0]] = numberBeaker[beakerSelected[0]] - 7 + numberBeaker[beakerSelected[1]];
                    numberBeaker[beakerSelected[1]] = 7;
                }
            }
            else//二つ目に選ばれたビーカーがメモリ10のビーカーの時
            {
                numberBeaker[beakerSelected[1]] = numberBeaker[beakerSelected[1]] + numberBeaker[beakerSelected[0]];
                numberBeaker[beakerSelected[0]] = 0;
            }

            //  ビーカーの絵の差し替え
            buttonBeakers[0].GetComponent<Image>().sprite = imageBeaker10[numberBeaker[0]];
            buttonBeakers[1].GetComponent<Image>().sprite = imageBeaker7[numberBeaker[1]];
            buttonBeakers[2].GetComponent<Image>().sprite = imageBeaker3[numberBeaker[2]];
        }
        beakerManage = (beakerManage + 1) % 2;
    }

    //水の間のビーカーのボタンを非アクティブにする
    void DisabledButtonBeakers()
    {
        for (int i = 0; i < 3; i++)
        {
            DisabledButton2(buttonBeakers[i]);
        }
    }

    //風の間の謎1の解答のラインの変更の処理
    void ChangeLineBright(int buttonNo)
    {
        numberLine[buttonNo] = (numberLine[buttonNo] + 1) % 2;
        buttonLines[buttonNo].GetComponent<Image>().sprite = imageLine[numberLine[buttonNo]];
    }

    //風の間の謎2の解答の竹の長さの変更の処理
    void ChangeBumbooLength(int buttonNo)
    {
        numberBumboo[buttonNo] = (numberBumboo[buttonNo] + 1) % numberBumboo.Length;
        buttonBumboos[buttonNo].GetComponent<Image>().sprite = imageBumboos[numberBumboo[buttonNo]];
    }
    //風の間の竹のボタンを非アクティブにする
    void DisabledButtonBumboos()
    {
        for(int i = 0; i < 6; i++)
        {
            DisabledButton2(buttonBumboos[i]);
        }
    }

    //牢屋の鍵の謎の解答の数字の変更の処理
    void ChangeButtonPriosnRock(int buttonNo)
    {
        int numberKind = int.Parse(buttonPrison[buttonNo].GetComponentInChildren<Text>().text);
        string numberString = ((numberKind + 1) % 10).ToString("0");
        buttonPrison[buttonNo].GetComponentInChildren<Text>().text = numberString;
    }

    //炎の間の謎1の答えのチェック
    void CheckFireMystery1Answer()
    {
        int[] number = new int[6];
        for(int i = 0; i < 6; i++)
        {
            number[i] = int.Parse(buttonNumbers[i].GetComponentInChildren<Text>().text);
        }

        if ((number[0] == 4) && (number[1] == 2) && (number[2] == 6) && (number[3] == 1) && (number[4] == 9) && (number[5] == 1))
        {
            if (doesHaveKey == false)
            {
                DisplayMessage("「カシャ」と音が聞こえて箱が開いた。箱の中には鍵が入っていた。");

                DisabledButton1(buttonItem);
                GameObject.Find("ButtonBoxCloseRed").SetActive(false);
                DisabledButton1(GameObject.Find("ButtonBackPanelCaveFire2"));

                ImageBoxOpenRed.SetActive(true);
                ButtonKeyIcon.SetActive(true);
                ButtonKey.SetActive(true);
                doesHaveKey = true;
            }
        }
    }

    //火の間の謎2の答えのチェック
    void CheckFireMystery2Answer()
    {
        if (doesHaveOrbFire == false)
        {
            candleManage = 0;
            if ((orderManage[0] == 4) && (orderManage[1] == 3) && (orderManage[2] == 1) &&
                (orderManage[3] == 2) && (orderManage[4] == 0) && (orderManage[5] == 5))
            {
                DisplayMessage("突然空中で火がついた後、赤色のオーブが現れた。");

                DisabledButton1(buttonItem);
                DisabledButtonCandles();
                ChangeArrowActive(0, ButtonLeft);
                ChangeArrowActive(0, ButtonRight);

                ButtonOrbFireIcon.SetActive(true);
                ButtonOrbFire.SetActive(true);
                doesHaveOrbFire = true;
            }
            else
            {
                DisplayMissingMessage("ろうそくの火がすべて消えてしまった。");
                ChangeCandleAllOff();
            }
        }
    }

    //水の間の謎1の答えのチェック
    void CheckWaterMystery1Answer()
    {
        numberManage = 0;
        if (doesHaveWateringCan == false)
        {
            if ((numberColors[0] == LIGHTBLUE) && (numberColors[1] == RED) && (numberColors[2] == PURPLE) &&
                (numberColors[3] == BLUE) && (numberColors[4] == YELLOW) && (numberColors[5] == GREEN))
            {
                DisplayMessage("「カシャ」という音が聞こえて箱が空いた。箱の中にはじょうろが入っていた。");

                DisabledButton1(buttonItem);
                GameObject.Find("ButtonBoxCloseBlue").SetActive(false);
                DisabledButton1(GameObject.Find("ButtonBackPanelCaveWater2"));


                ImageBoxOpenBlue.SetActive(true);
                ButtonWateringCanIcon.SetActive(true);
                ButtonWateringCan.SetActive(true);
                doesHaveWateringCan = true;
            }
            else
            {
                DisplayMissingMessage("「ブブー」という音が聞こえた。何かを間違えたようだ。");
            }
        }
    }

    //水の間の謎2の答えのチェック
    void CheckWaterMystery2Answer()
    {
        if (doesHaveOrbWater == false)
        {
            if ((numberBeaker[0] == 5) || (numberBeaker[1] == 5))
            {
                DisplayMessage("目の前が一瞬霧で覆われ、霧が晴れると青色のオーブが現れた。");

                DisabledButton1(buttonItem);
                DisabledButtonBeakers();
                DisabledButton1(GameObject.Find("ButtonBackPanelCaveWater1"));

                ButtonOrbWaterIcon.SetActive(true);
                ButtonOrbWater.SetActive(true);
                doesHaveOrbWater = true;
            }
        }
 
    }

    //風の間の謎1の答えのチェック
    void CheckWindMystery1Answer()
    {
        if (doesHaveMatch == false)
        {
            if ((numberLine[0] == LIGHT) && (numberLine[1] == DARK) && (numberLine[2] == LIGHT) &&
                (numberLine[3] == DARK) && (numberLine[4] == LIGHT) && (numberLine[5] == DARK) &&
                (numberLine[6] == DARK) && (numberLine[7] == LIGHT) && (numberLine[8] == LIGHT) &&
                (numberLine[9] == DARK) && (numberLine[10] == LIGHT) && (numberLine[11] == DARK))
            {
                DisplayMessage("一瞬光った後、気づいたらマッチ箱が落ちていた。");

                DisabledButton1(buttonItem);
                DisabledButton1(GameObject.Find("ButtonBackPanelCaveWind4"));

                ButtonMatchIcon.SetActive(true);
                ButtonMatch.SetActive(true);
                doesHaveMatch = true;
            }
        }
    }
    //風の間の謎2の答えのチェック
    void CheckWindMystery2Answer()
    {
        if ((numberBumboo[0] == 5) && (numberBumboo[1] == 2) && (numberBumboo[2] == 4) && (numberBumboo[3] == 0) &&
             (numberBumboo[4] == 3) && (numberBumboo[5] == 1))
        {
            if (doesHaveOrbWind == false)
            {
                DisplayMessage("突風と共に緑色のオーブが目の前に現れた。");

                DisabledButton1(buttonItem);
                DisabledButtonBumboos();
                ChangeArrowActive(0, ButtonLeft);
                ChangeArrowActive(0, ButtonRight);

                ButtonOrbWindIcon.SetActive(true);
                ButtonOrbWind.SetActive(true);
                doesHaveOrbWind = true;
            }
        }
    }

    //牢屋の鍵の謎の答えのチェック
    void CheckPrisonRockMysteryAnswer()
    {
        int[] number = new int[18];
        for (int i = 0; i < 18; i++)
        {
            number[i] = int.Parse(buttonPrison[i].GetComponentInChildren<Text>().text);
        }

        if ((number[0] == 2) && (number[1] == 2) && (number[2] == 1) && (number[3] == 0) && (number[4] == 3) && (number[5] == 0) &&
            (number[6] == 1) && (number[7] == 8) && (number[8] == 4) && (number[9] == 9) && (number[10] == 2) && (number[11] == 7) &&
            (number[12] == 1) && (number[13] == 6) && (number[14] == 4) && (number[15] == 2) && (number[16] == 5) && (number[17] == 3))
        {
            if (doesSaveBraveMan == false)
            {
                DisplayMessage("「ガシャ」と音がして牢屋の鍵が開いた。またどこかで「ポチャン」と水がはねる音がした。");

                GameObject.Find("ImagePrison").GetComponent<Image>().sprite = imagePrisonOpen;
                GameObject.Find("ImagePrisonRock").SetActive(false);
                GameObject.Find("ButtonPrisonRock").SetActive(false);
                GameObject.Find("ImageBraveManInPrison").SetActive(false);
                GameObject.Find("ButtonBraveManInPrison").SetActive(false);
                ButtonBraveManOutPrison.SetActive(true);

                ButtonPickelOnLake.SetActive(true);

                doesOpenPrison = true;
                doesSaveBraveMan = true;
            }
        }
    }
}
