// FadeManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float delayBeforeFade = 5f;
    public float fadeDuration = 2f;
    public string nextSceneName = "MainMenu";

    private float timer = 0f;
    private bool isFading = false;

    void Start()
    {
        if (fadeImage != null)
        {
            // Set initial color transparan
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
        }

        Invoke("StartFade", delayBeforeFade);
    }

    void StartFade()
    {
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0f, 0f, 0f, Mathf.Clamp01(alpha));
            }

            if (alpha >= 1f)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
