using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene("GameScene_stage1");
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
                SceneManager.LoadScene("GameScene_Ending");
                break;
            default:
                Debug.Log("PushContinueGameButton Error");
                break;
        }
    }

}
