using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{

    [SerializeField] private GameObject _wonPopup;
    [SerializeField] private GameObject _lostPopup;

    public void BackToMenu()
    {
        GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PlayAgain()
    {
        MainMenu.SetDefaultValues();
        GameState.GameCondition = GameState.GameConditions.GAME_PLAY;
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        _wonPopup.SetActive(false);
        _lostPopup.SetActive(false);
        if(GameState.GameCondition == GameState.GameConditions.GAME_WON) _wonPopup.SetActive(true);
        if (GameState.GameCondition == GameState.GameConditions.GAME_LOST) _lostPopup.SetActive(true);
    }
}
