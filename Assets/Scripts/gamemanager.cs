using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
   public static gamemanager instance;

    [Header("Game Data")]
    public int totalFruits;
    public int collectedFruits;
    public float time = 60f;

    private float startTime = 60f;
    private bool gameEnded = false;
    private bool gameStarted = false;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject settingsPanel;
    public Toggle soundToggle;
    public Slider volumeSlider;
    public GameObject noticeBoard;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 0f;

        gameStarted = false;
        gameEnded = false;

        collectedFruits = 0;
        time = startTime;

        totalFruits = GameObject.FindGameObjectsWithTag("Fruit").Length;

        startPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        if (noticeBoard != null)
            noticeBoard.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted || gameEnded)
            return;

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            LoseGame();
        }

        scoreText.text = "Fruits: " + collectedFruits;
        timerText.text = "Time: " + Mathf.Ceil(time);

        if (collectedFruits >= totalFruits)
        {
            WinGame();
        }
    }

    public void AddFruit()
    {
        if (!gameEnded)
            collectedFruits++;
    }

    public void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1f;

        startPanel.SetActive(false);

        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        if (noticeBoard != null)
            noticeBoard.SetActive(true);
    }

    void WinGame()
    {
        gameEnded = true;

        Time.timeScale = 0f;

        winPanel.SetActive(true);

        if (noticeBoard != null)
            noticeBoard.SetActive(false);
    }

    void LoseGame()
    {
        gameEnded = true;

        Time.timeScale = 0f;

        losePanel.SetActive(true);

        if (noticeBoard != null)
            noticeBoard.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetVolume(float volume)
    {
    AudioListener.volume = volume;
    }
    public void ToggleSound(bool isOn)
    {
    if (isOn)
    {
        AudioListener.volume = volumeSlider.value;
    }
    else
    {
        AudioListener.volume = 0f;
    }
    }
    public void OpenSettings()
    {
    settingsPanel.SetActive(true);

    Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
    settingsPanel.SetActive(false);

    Time.timeScale = 1f;
    }
}
