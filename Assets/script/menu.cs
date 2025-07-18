using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject infopanel;
    public GameObject settingspanel;
    public VolumeControl volumeControl;

    // Start is called before the first frame update
    void Start()
    {
       menupanel.SetActive(true);
       infopanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton (string scenename) {
        SceneManager.LoadScene(scenename);
    }
    
    public void Level1 (string scenename) {
        SceneManager.LoadScene(scenename);
    }

    public void InfoButton () {
        menupanel.SetActive(false);
        infopanel.SetActive(true);
    }

    public void SettingsButton () {
        menupanel.SetActive(false);
        settingspanel.SetActive(true);
    }

    public void BackButton () {
        infopanel.SetActive(false);
        settingspanel.SetActive(false);
        menupanel.SetActive(true);
        Debug.Log("tombol ini sudah ditekan...");
    }

    public void QuitButton () {
        Application.Quit();
        Debug.Log("Tomboll keluar telah ditekan...");
    }
}
