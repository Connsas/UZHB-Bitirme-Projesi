using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Deagle
{
    public static int TotalAmmo = 35;
    public static int MaxMagazineAmmo = 7;
    public static int ReservedAmmo = TotalAmmo - MaxMagazineAmmo;
    public static int Damage = 20;
    public static float FireCooldown = 0.5f;
    public static float ReloadTime = 2.2f;
    public static int Cost = 500;
}
