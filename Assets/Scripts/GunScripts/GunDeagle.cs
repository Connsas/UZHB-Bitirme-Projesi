using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDeagle : Gun
{

    public static int totalAmmoDeagle;
    public static int magazineAmmoDeagle;
    public static int maxMagazineAmmoDeagle;
    public static int reservedAmmoDeagle;
    [SerializeField] private AudioClip DeagleReload;
    [SerializeField] private AudioClip DeagleFire;

    // Start is called before the first frame update
    void Start()
    {
        totalAmmoDeagle = Deagle.TotalAmmo;
        maxMagazineAmmoDeagle = Deagle.MaxMagazineAmmo;
        magazineAmmoDeagle = maxMagazineAmmoDeagle;
        reservedAmmoDeagle = Deagle.ReservedAmmo;
        damage = Deagle.Damage;
        fireCooldown = Deagle.FireCooldown;
        reloadTime = Deagle.ReloadTime;
        cost = Deagle.Cost;
        isFired = false;
        isReloading = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowAmmo.magazineAmmo = magazineAmmoDeagle;
        ShowAmmo.reservedAmmo = reservedAmmoDeagle;
        Fire(ref magazineAmmoDeagle, DeagleFire);
        Reload(ref magazineAmmoDeagle, ref maxMagazineAmmoDeagle, ref reservedAmmoDeagle, ref DeagleReload);
    }
}
