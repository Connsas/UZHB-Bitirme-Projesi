using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowAmmo : MonoBehaviour
{

    [SerializeField] private TMP_Text textMagazineAmmo;
    [SerializeField] private TMP_Text textReservedAmmo;
    public static int magazineAmmo;
    public static int reservedAmmo;
    
    void Awake()
    {
        textMagazineAmmo.text = "0";
        textReservedAmmo.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        textMagazineAmmo.text = magazineAmmo.ToString();
        textReservedAmmo.text = reservedAmmo.ToString();
    }
}
