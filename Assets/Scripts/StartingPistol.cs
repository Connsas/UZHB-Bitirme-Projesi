using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StartingPistol
{
    public static int TotalAmmo = 60;
    public static int MaxMagazineAmmo = 12;
    public static int ReservedAmmo = TotalAmmo - MaxMagazineAmmo;
    public static int Damage = 8;
    public static float FireCooldown = 0.3f;
    public static float ReloadTime = 1.6f;
    public static int Cost = 0;
}
