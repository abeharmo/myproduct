using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //ボタン：ゲームを続けるボタン
    public GameObject buttonContinueGame;           //続けるボタン

    // Start is called before the first frame update
    void Start()
    {
        int stageClear = PlayerPrefs.GetInt("STAGECLEAR");
        if(stageClear != 0)
        {
            buttonContinueGame.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButtonNewGame()
    {
        if(PlayerPrefs.HasKey("STAGECLEAR") == true) {
            PlayerPrefs.DeleteAll();
        }
        SceneManager.LoadScene("OpeningScene");
    }

    public void PushButtonContinueGame()
    {
        int stageClear = PlayerPrefs.GetInt("STAGECLEAR");
        switch (stageClear)
        {
            case 1:
                SceneManager.LoadScene("GameScene_stage2");
                break;
            case 2:
                SceneManager.LoadScene("GameScene_stage3");
                break;
            case 3:
                SceneManager.LoadScene("EndingScene");
                break;
            default:
                Debug.Log("PushContinueGameButton Error");
                break;
        }
    }

}
