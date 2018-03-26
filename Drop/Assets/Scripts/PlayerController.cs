using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SwipeControls swipeControls;
    public float speed;
    public Text scoreText;
    public GameObject gameOverPanel, nextLevelPanel;//,startGamePanel;
    public Text highScoreText;
    public int score = 0;
    private int startScore;
    public float maxX, maxZ, minX, minZ;
    public TileGenerator tileGenerator;
    private Vector3 startPos;
    public GameManager gameManager;
    private float difficultyFactor;
    public AudioSource levelClearedSound;

    private float startSpeed;
    public float accelerationStep;
    public float acceletarionDensity;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gameManager.difficulty == 0)
        {
            difficultyFactor = 2f;
        }
        else if (gameManager.difficulty == 1)
        {
            difficultyFactor = 1.5f;
        }
        else
        {
            difficultyFactor = 1f;
        }
        speed = speed / difficultyFactor;
        accelerationStep = accelerationStep / difficultyFactor;
        startScore = gameManager.totalScore;
        startSpeed = speed;
        startPos = transform.position;
        Time.timeScale = 1;
        scoreText.text = "SCORE: " + startScore;
        gameOverPanel.SetActive(false);
        nextLevelPanel.SetActive(false);
        highScoreText.enabled = false;
        //startGamePanel.SetActive(true);
        minX = 0f;
        maxX = (float)tileGenerator.colums - 1f;
        minZ = 0f;
        maxZ = (float)tileGenerator.rows - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        MovePlayer();
        RefreshScore();
        transform.position -= Vector3.up * speed * Time.deltaTime;
        Accelerate();
    }

    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.swipeUp)
        {
            if (transform.position.z > maxZ - 0.1f)
                return;
            transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.swipeDown)
        {
            if (transform.position.z < minZ + 0.1f)
                return;
            transform.position += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || swipeControls.swipeRight)
        {
            if (transform.position.x > maxX - 0.1f)
                return;
            transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeControls.swipeLeft)
        {
            if (transform.position.x < minX + 0.1f)
                return;
            transform.position += Vector3.left;
        }

        if (transform.position.y < -tileGenerator.levels * (tileGenerator.levelsDistance) + tileGenerator.levelsDistance / 2)
        {
            LevelEnd();
        }
    }

    private void Accelerate()
    {
        float playerY = transform.position.y;

        if (playerY < -acceletarionDensity && playerY > -acceletarionDensity * 2)
            speed = startSpeed + accelerationStep * 1;
        else if (playerY < -acceletarionDensity * 2 && playerY > -acceletarionDensity * 3)
            speed = startSpeed + accelerationStep * 2;
        else if (playerY < -acceletarionDensity * 3 && playerY > -acceletarionDensity * 4)
            speed = startSpeed + accelerationStep * 3;
        else if (playerY < -acceletarionDensity * 4 && playerY > -acceletarionDensity * 5)
            speed = startSpeed + accelerationStep * 4;
        else if (playerY < -acceletarionDensity * 5 && playerY > -acceletarionDensity * 6)
            speed = startSpeed + accelerationStep * 5;
    }

    private void RefreshScore()
    {
        if (transform.position.y > 0) 
        {
            return;
        }
        if (transform.position.y < -(tileGenerator.levels * tileGenerator.levelsDistance - tileGenerator.levelsDistance + 0.1f))
        {
            return;
        }

        score = -(int)(transform.position.y - transform.localScale.y - 0.3f);
        scoreText.text = "SCORE: " + (score + startScore);

    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameManager.totalScore = score + startScore;
        gameManager.SetHighScore();
        gameManager.timeScaleInt = 0;
        gameManager.level = 1;
    }

    public void PlayAgain()
    {
        highScoreText.enabled = false;
        gameManager.PlayAgain();
    }

    public void LevelEnd()
    {
        Time.timeScale = 0;
        nextLevelPanel.SetActive(true);
        gameManager.totalScore = score + startScore;
        levelClearedSound.Play();
    }

    public void NextLevel()
    {
        gameManager.level += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
