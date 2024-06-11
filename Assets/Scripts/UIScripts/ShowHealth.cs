using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthValue;

    private void Show()
    {
        _healthValue.text = PlayerStats.playerHealth.ToString();
    }


    void Update()
    {
        Show();
    }
}
