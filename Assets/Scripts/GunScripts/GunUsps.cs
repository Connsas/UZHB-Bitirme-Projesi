using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunUsps : Gun
{
    public static int totalAmmoUsps;
    public static int magazineAmmoUsps;
    public static int maxMagazineAmmoUsps;
    public static int reservedAmmoUsps;
    [SerializeField] private AudioClip UspsReload;
    [SerializeField] private AudioClip UspsFire;

    // Start is called before the first frame update
    void Start()
    {
        totalAmmoUsps = Usp.TotalAmmo;
        maxMagazineAmmoUsps = Usp.MaxMagazineAmmo;
        magazineAmmoUsps = maxMagazineAmmoUsps;
        reservedAmmoUsps = Usp.ReservedAmmo;
        damage = Usp.Damage;
        fireCooldown = Usp.FireCooldown;
        reloadTime = Usp.ReloadTime;
        cost = Usp.Cost;
        isFired = false;
        isReloading = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowAmmo.magazineAmmo = magazineAmmoUsps;
        ShowAmmo.reservedAmmo = reservedAmmoUsps;
        Fire(ref magazineAmmoUsps, UspsFire);
        Reload(ref magazineAmmoUsps, ref maxMagazineAmmoUsps, ref reservedAmmoUsps, ref UspsReload);
    }
}
