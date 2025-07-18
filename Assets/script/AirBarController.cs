using UnityEngine;
using UnityEngine.UI;

public class AirBarController : MonoBehaviour
{
    private Slider waterBar;
    public float maxWater = 2500f;
    public float currentWater;
    public float decreaseRate = 5f; // air berkurang per detik saat semprot

    private bool isSpraying;

    public Image fillImage;

    public bool IsOutOfWater => currentWater <= 0f;

    void Start()
    {
        waterBar = GetComponent<Slider>();
        currentWater = maxWater;
        waterBar.maxValue = maxWater;
        waterBar.value = currentWater;

        UpdateFillImage(); // tampilkan saat awal
    }

    void Update()
    {
        isSpraying = Input.GetKey(KeyCode.F);

        if (isSpraying && currentWater > 0)
        {
            currentWater -= decreaseRate * Time.deltaTime;
            currentWater = Mathf.Clamp(currentWater, 0, maxWater);
            waterBar.value = currentWater;

            UpdateFillImage();
        }
    }

    public void RefillWater()
    {
        currentWater = maxWater;
        waterBar.value = maxWater;

        UpdateFillImage();
    }

    private void UpdateFillImage()
    {
        if (fillImage != null)
        {
            fillImage.enabled = currentWater > 0.01f;
        }
    }
}
