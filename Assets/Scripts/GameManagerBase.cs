﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBase : MonoBehaviour
{
    //各パネルの定数定義
    public const int WALL1 = 0;
    public const int WALL2 = 1;
    public const int WALL3 = 2;
    public const int WALL4 = 3;

    //各画面の切り替えボタンの方向
    public const int LEFT  = 0;
    public const int RIGHT = 1;

    public int[] activeArrow = new int[4];

    //全てのパネル
    public GameObject panelWalls;
    //画面の切り替えボタン
    public GameObject ButtonLeft;
    public GameObject ButtonRight;

    //はいといいえを表示するパネル
    public GameObject PanelDispYesOrNo;
    //次のステージ移動するか選択させるパネル
    public GameObject PanelSelectStayOrMove;
    //選択したアイテムを表示するアイコン
    public GameObject ImageSelectedItem;

    //自分が今いるパネル
    protected int wallNow;

    //メッセージウィンドウ
    public GameObject buttonMessage;        //テキストウィンドウ
    public GameObject buttonMessageText;    //テキスト

    //メニューウィンドウ
    public GameObject panelItem;            //メニューパネル
    public GameObject buttonItem;           //ボタン：アイテム

    //アイテムが何もないアイテムアイコン
    public Sprite imageIcon;

    // Start is called before the first frame update
    public void Start()
    {
        wallNow = WALL1;
        activeArrow[LEFT]  = 1;
        activeArrow[RIGHT] = 1;

        panelItem.SetActive(false);
        BackPanelWall(wallNow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //左矢印を押したときの処理
    public void PushLeftArrowButton()
    {
        wallNow--;
        wallNow = (wallNow + 4) % 4;
        DisplayWallBase();
    }

    //右矢印を押したときの処理
    public void PushRightArrowButton()
    {
        wallNow++;
        wallNow = wallNow % 4;
        DisplayWallBase();
    }

    //PanelWall1～4のいずれかへ戻る関数
    public void BackPanelWall(int WALL)
    {
        wallNow = WALL;
        ChangeArrowActive(1, ButtonLeft);
        ChangeArrowActive(1, ButtonRight);
        DisplayWallBase();
    }

    //画面の切り替えを行う関数
    public void DisplayWallBase()
    {
        if (buttonMessage.activeSelf == true){
            DeleteMessage();
        }

        switch (wallNow)
        {
            case WALL1:
                panelWalls.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case WALL2:
                panelWalls.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
                break;
            case WALL3:
                panelWalls.transform.localPosition = new Vector3(-2000.0f, 0.0f, 0.0f);
                break;
            case WALL4:
                panelWalls.transform.localPosition = new Vector3(-3000.0f, 0.0f, 0.0f);
                break;
            default:
                Debug.Log("DisplayWallBase Error");
                break;
        }
        
    }

    //画面切り替えの矢印を表示を操作するための関数
    public void ChangeArrowActive(int active, GameObject Button)
    {
        switch (active)
        {
            case 0:
                Button.SetActive(false);
                break;
            case 1:
                Button.SetActive(true);
                break;
            default:
                Debug.Log("ChangeArrowActive Error");
                break;
        }
    }

    //メッセージウィンドウを消す
    public void DeleteMessage()
    {
        buttonMessage.SetActive(false);
    }

    //ボタンを押されたらメッセージウィンドウを消す
    public void PushButtonMessage()
    {
        DeleteMessage();
    }

    //メッセージウィンドウの表示
    public void DisplayMessage(string message)
    {
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text>().text = message;
    }

    //アイテムボタンを押したときの処理
    public void PushButtonItem()
    {
        DeleteMessage();
        panelItem.SetActive(true);
    }

    //アイテムウィンドウを消す処理
    public void ExitItem()
    {
        panelItem.SetActive(false);
    }


    //ゲームに戻るボタンを押した処理
    public void PushButtonReturnGame()
    {
        ImageSelectedItem.SetActive(false);
        ImageSelectedItem.GetComponent<Image>().sprite = imageIcon;
        ExitItem();
    }

    //タイトルに戻るボタンを押した処理
    public void PushButtonReturnTitle()
    {
        PanelDispYesOrNo.SetActive(true);
    }

    //タイトルに戻るボタンを押したあとに「はい」のボタンを押したら
    public void PushButtonReturnTitleYes()
    {
        ImageSelectedItem.GetComponent<Image>().sprite = imageIcon;
        ExitItem();;
        ImageSelectedItem.SetActive(false);
        PanelDispYesOrNo.SetActive(false);
        SceneManager.LoadScene("TitleScene");
    }

    //タイトルに戻るボタンを押したあとに「いいえ」のボタンを押したら
    public void PushButtonReturnTitleNo()
    {
        PanelDispYesOrNo.SetActive(false);
    }

    //ボタンの無効化(半透明化)
    public void DisabledButton1(GameObject pushButton)
    {
        Button btn = pushButton.GetComponent<Button>();
        btn.interactable = false;
    }

    //ボタンの有効化(半透明化)
    public void EnabledButton1(GameObject pushButton)
    {
        Button btn = pushButton.GetComponent<Button>();
        btn.interactable = true;
    }


    //ボタンの無効化(透過なし)
    public void DisabledButton2(GameObject pushButton)
    {
        Button btn = pushButton.GetComponent<Button>();
        btn.enabled = false;
    }

    //ボタンの有効化(透過なし)
    public void EnabledButton2(GameObject pushButton)
    {
        Button btn = pushButton.GetComponent<Button>();
        btn.enabled = true;
    }

    //ステージクリア画面へ移動するか否か
    public void SelectStayOrMove()
    {
        PanelSelectStayOrMove.SetActive(true);
    }

    //ステージクリア画面に移動する「はい」のボタンを押したら
    public void PushButtonSelectMove()
    {
        PanelSelectStayOrMove.SetActive(false);
        buttonItem.SetActive(false);
        GameObject.Find("CanvasChangePanelWalls").SetActive(false);
        panelWalls.transform.localPosition = new Vector3(-4000.0f, 0.0f, 0.0f);
    }

    //ステージクリア画面に移動しない「いいえ」のボタンを押したら
    public void PushButtonSelectStay()
    {
        PanelSelectStayOrMove.SetActive(false);
    }

}
