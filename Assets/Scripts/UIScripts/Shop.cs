using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _ui;
    [SerializeField] private Button _buttonUsps;
    [SerializeField] private Text _textUsps;
    [SerializeField] private TMP_Text _textCostUsps;
    [SerializeField] private Button _buttonDeagle;
    [SerializeField] private Text _textDeagle;
    [SerializeField] private TMP_Text _textCostDeagle;
    private bool isShopOpened = false;
    public static bool isShopAvaliable = false;

    public void Purchase(string gunName)
    {
        switch (gunName)
        {
            case "deagle":
                if (PlayerStats.reward >= Deagle.Cost)
                {
                    _textDeagle.text = "Purchased";
                    _buttonDeagle.interactable = false;
                    Deagle.isAvailable = true;
                    PlayerStats.reward -= Deagle.Cost;
                }

                break;
            case "usps":
                if (PlayerStats.reward >= Usp.Cost)
                {
                    _textUsps.text = "Purchased";
                    _buttonUsps.interactable = false;
                    Usp.isAvailable = true;
                    PlayerStats.reward -= Usp.Cost;
                }

                break;
        }
    }

    public void OpenShop()
    {
        if (isShopAvaliable)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isShopOpened && GameState.GameCondition == GameState.GameConditions.GAME_PLAY)
            {
                isShopOpened = true;
                _shop.SetActive(true);
                _ui.SetActive(false);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;

            }
            else if (Input.GetKeyDown(KeyCode.E) && isShopOpened && GameState.GameCondition == GameState.GameConditions.GAME_PLAY)
            {
                isShopOpened = false;
                _shop.SetActive(false);
                _ui.SetActive(true);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }

    private void CheckButtonState()
    {
        if (_shop.activeInHierarchy)
        {
            if (PlayerStats.reward >= Usp.Cost && !Usp.isAvailable)
            {
                _buttonUsps.interactable = true;
            }

            if (PlayerStats.reward >= Deagle.Cost && !Deagle.isAvailable)
            {
                _buttonDeagle.interactable = true;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _shop.SetActive(false);
        _textCostDeagle.text = Deagle.Cost.ToString();
        _textCostUsps.text = Usp.Cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        OpenShop();
        CheckButtonState();
    }
}
