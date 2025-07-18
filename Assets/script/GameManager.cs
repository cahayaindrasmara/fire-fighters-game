using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public float gameTime = 180f; // 3 menit
    private float timeRemaining;
    private bool gameEnded = false;

    public Fire[] fireZones; // drag semua api
    public GameObject winPanel;
    public GameObject losePanel;
    // public GameObject[] bintangUI; // array 3 gambar bintang
    public Sprite starFull;
    public Sprite starEmpty;
    public Image[] bintangUI;
    // public Text timerText;
    public TMP_Text timerText;
    public AirBarController airBar;
    public AudioSource winMusic;
    public AudioSource loseMusic;

    void Start()
    {
        timeRemaining = gameTime;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (AllFiresExtinguished())
        {
            WinGame();
        }
        else if (timeRemaining <= 0f || airBar.IsOutOfWater)
        {
            LoseGame();
        }
    }

    void UpdateTimerUI()
    {
        // int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        // int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        // timerText.text = $"{minutes:00}:{seconds:00}";

        float timeToShow = Mathf.Max(0f, timeRemaining); // ⬅️ cegah nilai negatif
        int minutes = Mathf.FloorToInt(timeToShow / 60f);
        int seconds = Mathf.FloorToInt(timeToShow % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    bool AllFiresExtinguished()
    {
        foreach (var fire in fireZones)
        {
            if (fire.IsBurning()) return false;
        }
        return true;
    }

    void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);

        int stars = 1;

        if (timeRemaining > 120f)
            stars = 3;
        else if (timeRemaining > 60f)
            stars = 2;

        ShowStars(stars);

        if (winMusic != null && !winMusic.isPlaying)
        {
            winMusic.Play();
        }
    }

    void LoseGame()
    {
        gameEnded = true;
        losePanel.SetActive(true);
        ShowStars(0);

        if (loseMusic != null && !loseMusic.isPlaying)
        {
            loseMusic.Play();
        }
    }

    void ShowStars(int count)
    {
        for (int i = 0; i < bintangUI.Length; i++)
        {
            // Ubah urutan dari kanan ke kiri
            int index = bintangUI.Length - 1 - i;
            bintangUI[index].sprite = i < count ? starFull : starEmpty;
        }
    }
    }
