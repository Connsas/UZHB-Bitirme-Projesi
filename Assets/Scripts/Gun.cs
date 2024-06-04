using System.Collections;
using System.Collections.Generic;
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
    private string gunName;

    private void Start()
    {
        gunName = gameObject.name;
        if (gunName.Equals("StartingPistol"))
        {
            totalAmmo = StartingPistol.TotalAmmo;
            maxMagazineAmmo = StartingPistol.MaxMagazineAmmo;
            reservedAmmo = StartingPistol.ReservedAmmo;
            damage = StartingPistol.Damage;
            fireCooldown = StartingPistol.FireCooldown;
            reloadTime = StartingPistol.ReloadTime;
            cost = StartingPistol.Cost;
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
            magazineAmmo--;
            RaycastHit Atis;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Atis))
            {
                Atis.transform.SendMessage("hit", damage);
            }

            StartCoroutine(FireCooldown());
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && magazineAmmo != maxMagazineAmmo && !isReloading)
        {
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
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private IEnumerator FireCooldown()
    {
        isFired = true;
        yield return new WaitForSeconds(fireCooldown);
        isFired = false;
    }
}
