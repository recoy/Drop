using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSettings : MonoBehaviour
{

    public Slider difficultySlider;
    //public static int difficultyLevel;
    public GameManager gameManager;
    public GameObject[] disableMenus;
    public GameObject homeMenu, highScoreMenu, settingsMenu, startMenu;
    public Text highScoreText;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        difficultySlider.value = PlayerPrefs.GetInt("Difficulty");
        //gameManager.highScoreText = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
       
        //foreach (GameObject menu in disableMenus)
        //{
        //    menu.SetActive(false);
        //}
        //homeMenu.SetActive(true);
        //gameManager.highScoreText = highScoreText;
        //gameManager.SetHighScore(highScoreText);
        
        if (!gameManager.gameStarted)
        {
            PlayAgainPressed();
        }

        gameManager.GetHighScore();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    gameManager.difficulty = (int)difficultySlider.value;
    //    PlayerPrefs.SetInt("Difficulty", gameManager.difficulty);
    //}

    public void SetGMHighScoreText()
    {
        gameManager.highScoreText = highScoreText;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAgainPressed()
    {
        highScoreText.text = null;
        homeMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void SetDifficulty()
    {
        int difValue;
        difValue = (int)difficultySlider.value;
        PlayerPrefs.SetInt("Difficulty", difValue);
    }


    public void ResetPlayerPrefs()
    {
        gameManager.ResetPlayerPrefs();
    }
}