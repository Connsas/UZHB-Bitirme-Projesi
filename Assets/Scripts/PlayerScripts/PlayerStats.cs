using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private TMP_Text _textStamina;
    public static int playerHealth = 100;
    public static float stamina = 100;
    public static float staminaLimit = 100;
    public static int reward = 700;
    private int staminaInt;

    public void Start()
    {
        staminaInt = 100;
    }

    private void Update()
    { 
        staminaInt = ((int)stamina);
        if(staminaInt > 100) staminaInt = 100;
        _textStamina.text = staminaInt.ToString();
    }
}
