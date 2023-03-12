using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject obstacle;
    public Transform spawnPoint;
    public float xMaxSpawnPoint;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highestScoreText;
    public GameObject menuPanel;

    int score = 0;
    bool gameStarted = false;
    int highestScore = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("highestScore"))
        {
            highestScore = PlayerPrefs.GetInt("highestScore");
            highestScoreText.text = "The Highest Score: " + highestScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !gameStarted)
        {
            menuPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);

            StartCoroutine("SpawnObstacles");
            gameStarted = true;
        }
    }

    IEnumerator SpawnObstacles()
    {
        while(true)
        {
            Spawn();

            yield return new WaitForSeconds(1f);
        }
    }

    public void Spawn()
    {
        float randomSpawnX = Random.Range(-xMaxSpawnPoint, xMaxSpawnPoint);

        Vector3 obstacleSpawnPosition = spawnPoint.position;
        obstacleSpawnPosition.x = randomSpawnX;

        Instantiate(obstacle, obstacleSpawnPosition, Quaternion.identity);
    }

    public void Restart()
    {
        if(score > highestScore)
        {
            PlayerPrefs.SetInt("highestScore", score);
        }

        SceneManager.LoadScene("Game");
    }

    public void ScoreUp()
    {
        score++;

        scoreText.text = score.ToString();
    }
}
