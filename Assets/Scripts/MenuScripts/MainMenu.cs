using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Sound Settings")] 
    [SerializeField] private TMP_Text _textVolumeValue;
    [SerializeField] private Slider _slider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    public void Start()
    {
        _slider.onValueChanged.AddListener(SetVolume);
        SetVolume(defaultVolume);
    }

    public void StartGame()
    {
        SetDefaultValues();
        GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
        SceneManager.LoadScene("GameScene");
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("mainVolume", AudioListener.volume);
        _textVolumeValue.text = volume.ToString("0.0");
    }

    public void ResetVolume()
    {
        AudioListener.volume = defaultVolume;
        _slider.value = defaultVolume;
        _textVolumeValue.text = defaultVolume.ToString("0.0");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
        SceneManager.LoadScene("MainMenuScene");
    }

    public static void SetDefaultValues()
    {
        //Player Stats
        PlayerStats.playerHealth = 100;
        PlayerStats.reward = 0;

        //Gun States
        Deagle.isAvailable = false;
        Usp.isAvailable = false;

        //Enemy Counter
        EnemyCounter.EnemyCount = 23;

        //Ammo Counter
        AmmoGenerate.ammoDropCount = 0;
    }
}
