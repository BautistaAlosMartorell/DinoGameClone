using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private Player player;
    private Spawner spawner;

    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public float score;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else{
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();


       NewGame();
    }

    public  void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D4");
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHighScore();

    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highscore", highScore);


        }

        highScoreText.text = Mathf.FloorToInt(highScore).ToString("D4");

    }
}
