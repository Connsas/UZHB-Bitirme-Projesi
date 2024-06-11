using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausedMenu : MonoBehaviour
{

    [Header("Sound Settings")]
    [SerializeField] private TMP_Text _textVolumeValue;
    [SerializeField] private Slider _slider = null;
    [SerializeField] private float defaultVolume = 1.0f;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _ui;

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _ui.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
        GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
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
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameState.GameCondition == GameState.GameConditions.GAME_PLAY)
        {
            GameState.GameCondition = GameState.GameConditions.GAME_PAUSED;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            AudioListener.pause = true;
            _ui.SetActive(false);
            _menu.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.Escape) && GameState.GameCondition == GameState.GameConditions.GAME_PAUSED)
        {
            GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            AudioListener.pause = false;
            _ui.SetActive(true);
            _menu.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     PauseGame();
    }
}
