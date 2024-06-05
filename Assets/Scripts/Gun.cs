using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int totalAmmo;
    public int magazineAmmo;
    public int maxMagazineAmmo;
    public int reservedAmmo;
    public int damage;
    public bool isFired;
    public bool isReloading;
    public float fireCooldown;
    public float reloadTime;
    public int cost;
    [SerializeField] private GameObject gun;
    [SerializeField] private AudioClip DeagleReload;
    [SerializeField] private AudioClip DeagleFire;
    [SerializeField] private AudioClip StartingPistolReload;
    [SerializeField] private AudioClip StartingPistolFire;
    [SerializeField] private AudioClip UspsReload;
    [SerializeField] private AudioClip UspsFire;
    private AudioClip gunReload;
    private AudioClip gunFire;
    private string gunName;
    private AudioSource audioSource;
    [SerializeField]private Animator animator;

    private void Start()
    {
        gunName = gameObject.name;
        audioSource = gun.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (gunName.Equals("StartingPistol"))
        {
            
            totalAmmo = StartingPistol.TotalAmmo;
            maxMagazineAmmo = StartingPistol.MaxMagazineAmmo;
            reservedAmmo = StartingPistol.ReservedAmmo;
            damage = StartingPistol.Damage;
            fireCooldown = StartingPistol.FireCooldown;
            reloadTime = StartingPistol.ReloadTime;
            cost = StartingPistol.Cost;
            gunReload = StartingPistolReload;
            gunFire = StartingPistolFire;
        }
        else if(gunName.Equals("USP-S"))
        {
            totalAmmo = Usp.TotalAmmo;
            maxMagazineAmmo = Usp.MaxMagazineAmmo;
            reservedAmmo = Usp.ReservedAmmo;
            damage = Usp.Damage;
            fireCooldown = Usp.FireCooldown;
            reloadTime = Usp.ReloadTime;
            cost = Usp.Cost;
            gunReload = UspsReload;
            gunFire = UspsFire;
        }
        else if(gunName.Equals("Deagle"))
        {
            totalAmmo = Deagle.TotalAmmo;
            maxMagazineAmmo = Deagle.MaxMagazineAmmo;
            reservedAmmo = Deagle.ReservedAmmo;
            damage = Deagle.Damage;
            fireCooldown = Deagle.FireCooldown;
            reloadTime = Deagle.ReloadTime;
            cost = Deagle.Cost;
            gunReload = DeagleReload;
            gunFire = DeagleFire;
        }

        isFired = false;
        isReloading = false;
        magazineAmmo = maxMagazineAmmo;
    }

    private void Update()
    {
        Fire();
        Reload();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && magazineAmmo > 0 && !isFired && !isReloading)
        {
            audioSource.clip = gunFire;
            audioSource.Play();
            magazineAmmo--;
            RaycastHit Hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit))
            {
                Hit.transform.SendMessage("hit", damage);
            }

            StartCoroutine(FireCooldown());
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && magazineAmmo != maxMagazineAmmo && !isReloading)
        {
            audioSource.clip = gunReload;
            audioSource.Play();
            
            if (reservedAmmo >= maxMagazineAmmo)
            {
                reservedAmmo -= maxMagazineAmmo - magazineAmmo;
                magazineAmmo = maxMagazineAmmo;
            }
            else
            {
                if (magazineAmmo + reservedAmmo > maxMagazineAmmo)
                {
                    reservedAmmo = magazineAmmo + reservedAmmo - maxMagazineAmmo;
                    magazineAmmo = maxMagazineAmmo;
                }
                else
                {
                    magazineAmmo = magazineAmmo + reservedAmmo;
                    reservedAmmo = 0;
                }
            }
            StartCoroutine(ReloadCooldown());
        }
    }

    private IEnumerator ReloadCooldown()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("isReloading", false);
        isReloading = false;
    }

    private IEnumerator FireCooldown()
    {
        isFired = true;
        animator.SetBool("isFired", true);
        yield return new WaitForSeconds(fireCooldown);
        animator.SetBool("isFired", false);
        isFired = false;
    }
}
