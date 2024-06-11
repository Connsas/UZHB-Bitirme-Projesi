using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStartingPistol : Gun
{

    public static int totalAmmoStartingPistol;
    public static int magazineAmmoStartingPistol;
    public static int maxMagazineAmmoStartingPistol;
    public static int reservedAmmoStartingPistol;
    [SerializeField] private AudioClip StartingPistolReload;
    [SerializeField] private AudioClip StartingPistolFire;

    // Start is called before the first frame update
    void Start()
    {
        totalAmmoStartingPistol = StartingPistol.TotalAmmo;
        maxMagazineAmmoStartingPistol = StartingPistol.MaxMagazineAmmo;
        reservedAmmoStartingPistol = StartingPistol.ReservedAmmo;
        magazineAmmoStartingPistol = maxMagazineAmmoStartingPistol;
        damage = StartingPistol.Damage;
        fireCooldown = StartingPistol.FireCooldown;
        reloadTime = StartingPistol.ReloadTime;
        cost = StartingPistol.Cost;
        isFired = false;
        isReloading = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowAmmo.magazineAmmo = magazineAmmoStartingPistol;
        ShowAmmo.reservedAmmo = reservedAmmoStartingPistol;
        Fire(ref magazineAmmoStartingPistol, StartingPistolFire);
        Reload(ref magazineAmmoStartingPistol, ref maxMagazineAmmoStartingPistol, ref reservedAmmoStartingPistol, ref StartingPistolReload);
    }
}
