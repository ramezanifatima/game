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
    public float time = 120f;

    private float startTime = 120f;
    private bool gameEnded = false;
    private bool gameStarted = false;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject settingsPanel;
    public GameObject noticeBoard;

    [Header("UI Controls")]
    public Toggle soundToggle;
    public Slider volumeSlider;

    [Header("Audio")]
    public AudioSource farmAudio;
    public AudioSource sfxAudio;
    public AudioClip collectClip;
    public AudioClip loseClip;

    private IEnumerator FreezeGame()
    {
    yield return new WaitForSecondsRealtime(0.5f);
    Time.timeScale = 0f;
    }
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
        settingsPanel.SetActive(false);

        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        if (noticeBoard != null)
            noticeBoard.SetActive(false);

        // ===== AUDIO INIT =====
        volumeSlider.value = 1f;
        soundToggle.isOn = true;

        farmAudio.mute = false;
        sfxAudio.mute = false;

        farmAudio.volume = 1f;
        sfxAudio.volume = 1f;
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
        if (gameEnded) return;

        collectedFruits++;

        if (collectClip != null)
            sfxAudio.PlayOneShot(collectClip);
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
        if (gameEnded) return;

        gameEnded = true;
        Time.timeScale = 0f;

        winPanel.SetActive(true);

        if (noticeBoard != null)
            noticeBoard.SetActive(false);
    }

    public void LoseGame()
{
    if (gameEnded) return;

    gameEnded = true;

    if (loseClip != null)
        sfxAudio.PlayOneShot(loseClip);

    losePanel.SetActive(true);

    if (noticeBoard != null)
        noticeBoard.SetActive(false);

    StartCoroutine(FreezeGame());
}

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ===== AUDIO SYSTEM (FIXED) =====

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);

        farmAudio.volume = volume;
        sfxAudio.volume = volume;

        // sync toggle state
        if (soundToggle.isOn)
        {
            farmAudio.mute = false;
            sfxAudio.mute = false;
        }
    }

    public void ToggleSound(bool isOn)
    {
        farmAudio.mute = !isOn;
        sfxAudio.mute = !isOn;
    }

    // ===== SETTINGS =====

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
