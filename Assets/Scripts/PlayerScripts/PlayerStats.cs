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
    public static int reward = 0;

    private void Update()
    {
        int staminaInt = ((int)stamina);
        _textStamina.text = staminaInt.ToString();
    }
}
