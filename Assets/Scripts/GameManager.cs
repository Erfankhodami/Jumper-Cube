
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Index;
    
    [SerializeField]private GameObject gameOverUI;

    [SerializeField] private GameObject gameFinishedUI;

    [SerializeField] private TextMeshProUGUI gameFinishedScore;

    public bool IsGameActive=false;

    [SerializeField] private TextMeshProUGUI scoreText;

    public int score;

    [SerializeField] private GameObject player;

    [SerializeField] public static Vector2 StartLocation=new Vector2(0,0);

    [SerializeField] private GameObject pauseButton;
    

    private void Awake()
    {
        if (Index == 1)
        {
            ContinueGame();
        }
        else
        {
            NewGame();
        }
    }

    private void Update()
    {
        UpdateScore();
    }

    

    public void ContinueGame()
    {
        score = PlayerPrefs.GetInt("Score");
        Vector2 Pos = new Vector2(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"));
        player.transform.position = Pos;
        IsGameActive = true;
        StartGame();
    }

    public void NewGame()
    {
        player.transform.position = StartLocation;
        SetPlayerPrefsData(StartLocation.x,StartLocation.y);
        StartGame();
    }

    public void StartGame()
    {
        pauseButton.SetActive(true);
        IsGameActive = true;
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
        
    }

    public void UpdateScore()
    {
        if (IsGameActive)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        IsGameActive = false;
        SetPlayerPrefsData(StartLocation.x,StartLocation.y);
        pauseButton.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerPrefsData(float x,float y)
    {
        PlayerPrefs.SetFloat("X",x);
        PlayerPrefs.SetFloat("Y",y);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameFinished()
    {
        gameFinishedUI.SetActive(true);
        IsGameActive = false;
        SetPlayerPrefsData(StartLocation.x,StartLocation.y);
        pauseButton.SetActive(false);
        gameFinishedScore.text = "Your Score:" + PlayerPrefs.GetInt("Score");
    }
    
    
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    
}
