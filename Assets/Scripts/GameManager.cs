using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameObject _instance;
    public int totalScore;
    public PlayerController pC;
    private static bool created = false;
    public int timeScaleInt;
    public int level;
    public int difficulty;
    public int[] highScore = new int[10];
    public Text highScoreText = null;
    public bool gameStarted = true;
    //public GameObject mainMenuCanvas;
    public MainMenuSettings mainMenuSettings;
    private PlayerPrefsX playerPrefsX;

    void Awake()
    {
        if (!created)
        {
            //PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(this.gameObject);
            created = true;
            //pC = FindObjectOfType<PlayerController>();
            pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            totalScore = 0;
            Time.timeScale = 0;
            level = 1;
            difficulty = 2;

            //highScoreText.text = highScore.ToString();
            //timeScaleInt = 0;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetScore()
    {
        totalScore += pC.score;
    }

    public void GetHighScore()
    {
        mainMenuSettings = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenuSettings>();
        highScoreText = mainMenuSettings.highScoreText;

        //if (!gameStarted)
        //{
        //    SetHighScore();
        //}

        highScore = PlayerPrefsX.GetIntArray("HighScore");
        if(highScore.Length < 10)
        {
            Debug.Log("Lisää nollia highscoreen");
            highScore = new int[10];
            for(int i = 0; i < 10; ++i)
            {
                if (highScore[i] != 0)
                    return;
                else
                    highScore[i] = 0;
            }
        }

        for (int i = 0; i < highScore.Length; ++i)
        {
            //highScore[i] = PlayerPrefs.GetInt("HighScore" + i);
            highScoreText.text = highScoreText.text + ((i + 1) + ". \t" + highScore[i] + "\n");
        }

        Debug.Log(highScoreText.text);
        totalScore = 0;
    }

    public void SetHighScore()
    {
        //mainMenuCanvas = GameObject.FindGameObjectWithTag("MainMenuCanvas");
        //mainMenuSettings = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenuSettings>();
        int i;
        for (i = 0; i < highScore.Length; ++i)
        {
            Debug.Log("Meneekö highscoreen?");
            if (totalScore > highScore[i])
            {
                Debug.Log("menee highscoreen paikalle " + i);
                ShowHighScorePanel(totalScore, i);
                highScore[highScore.Length - 1] = 0;
                Debug.Log("poista " + (highScore.Length - 1) + ". luku, jonka arvo on " + highScore[highScore.Length - 1]);
                break;
            }
            if (totalScore <= highScore[highScore.Length - 1])
            {
                return;
            }
        }
        int j;
        Debug.Log(" i = " + i);
        Debug.Log(" highscore length " + highScore.Length);
        Debug.Log(" j = " + (highScore.Length - 2));
        j = highScore.Length - 2;
        for (j = highScore.Length - 2; j >= i; j--)
        {
            Debug.Log("siirrä highscoret alaspäin");
            highScore[j + 1] = highScore[j];

            //int tmp = highScore[j+1];
            //highScore[j+1] = highScore[j];

            //PlayerPrefs.SetInt("HighScore" + highScore[j + 1], highScore[j]);
        }
        Debug.Log("aseta highscoreen paikalle " + i);
        highScore[i] = totalScore;
        PlayerPrefsX.SetIntArray("HighScore", highScore);

        //PlayerPrefs.SetInt("HighScore" + i, totalScore);
        //highScoreText.text = null;

        //for (int k = 0; k < highScore.Length; ++k)
        //{
        //    if (k == 0)
        //        highScoreText.text = ((k + 1) + ". " + highScore[k] + "\n");
        //    else
        //    {
        //        //hsText.text = ((k + 1) + ". " + highScore[k].ToString() + "\n");
        //        highScoreText.text = highScoreText.text + ((k + 1) + ". " + highScore[k] + "\n");
        //    }
        //}
        //Debug.Log("HighScore" + highScoreText.text);
    }


    public void PlayAgain()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameStarted = false;
        SceneManager.LoadScene(0);
        //mainMenuSettings = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenuSettings>();
        //mainMenuSettings.PlayAgainPressed();

        //SetHighScore();
    }

    private void Update()
    {
        if(pC == null)
            pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ResetPlayerPrefs()
    {
        //PlayerPrefs.DeleteAll();
        for(int i = 0; i < highScore.Length; ++i)
        {
            highScore[i] = 0;
        }
        highScoreText.text = null;
        PlayerPrefsX.SetIntArray("HighScore", highScore);
        Debug.Log("Playerprefs reset");
        GetHighScore();
    }

    public void ShowHighScorePanel(int score, int i)
    {
        //Text highScoreText = pC.highScoreText;
        pC.highScoreText.text = ("NEW HIGHSCORE \n" + (i+1) + ". " + score);
        pC.highScoreText.enabled = true;

    }
}





//public void GetHighScoreText()
//{
//    mainMenuSettings = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenuSettings>();
//    highScoreText = mainMenuSettings.highScoreText;

//    if (!gameStarted)
//    {
//        SetHighScore(highScoreText);
//    }


//    for (int i = 0; i < highScore.Length; ++i)
//    {
//        highScore[i] = PlayerPrefs.GetInt("HighScore" + i);
//        highScoreText.text = highScoreText.text + ((i + 1) + ". " + highScore[i] + "\n");
//    }
//    Debug.Log(highScoreText.text);
//    totalScore = 0;
//}

//public void SetHighScore(Text hsText)
//{

//    //mainMenuCanvas = GameObject.FindGameObjectWithTag("MainMenuCanvas");
//    mainMenuSettings = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenuSettings>();
//    int i;
//    for (i = 0; i < highScore.Length; ++i)
//    {
//        Debug.Log("Meneekö highscoreen?");
//        if (totalScore > highScore[i])
//        {
//            Debug.Log("menee highscoreen paikalle " + i);
//            highScore[highScore.Length - 1] = 0;
//            Debug.Log("poista " + (highScore.Length - 1) + ". luku, jonka arvo on " + highScore[highScore.Length - 1]);
//            break;
//        }
//        if (totalScore <= highScore[highScore.Length - 1])
//        {
//            return;
//        }

//    }
//    int j;
//    Debug.Log(" i = " + i);
//    Debug.Log(" highscore length " + highScore.Length);
//    Debug.Log(" j = " + (highScore.Length - 2));
//    j = highScore.Length - 2;
//    for (j = highScore.Length - 2; j >= i; j--)
//    {
//        Debug.Log("siirrä highscoret alaspäin");
//        highScore[j + 1] = highScore[j];

//        //int tmp = highScore[j+1];
//        //highScore[j+1] = highScore[j];
//        PlayerPrefs.SetInt("HighScore" + highScore[j + 1], highScore[j]);

//    }

//    Debug.Log("aseta highscoreen paikalle " + i);
//    highScore[i] = totalScore;
//    PlayerPrefs.SetInt("HighScore" + i, totalScore);
//    //highScoreText.text = null;

//    for (int k = 0; k < highScore.Length; ++k)
//    {
//        if (k == 0)
//            highScoreText.text = ((k + 1) + ". " + highScore[k] + "\n");
//        else
//        {
//            //hsText.text = ((k + 1) + ". " + highScore[k].ToString() + "\n");
//            highScoreText.text = highScoreText.text + ((k + 1) + ". " + highScore[k] + "\n");
//        }
//    }
//    Debug.Log("HighScore" + highScoreText.text);
//}