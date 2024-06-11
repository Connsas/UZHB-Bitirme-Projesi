using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage;
    public static bool isFired;
    public static bool isReloading;
    public float fireCooldown;
    public float reloadTime;
    public int cost;
    public GameObject hit;
    public AudioSource audioSource;
    public Animator animator;

    private void Start()
    {
        hit = GameObject.Find("Hit");
    }

    private void Update()
    {
    }

    public virtual void Fire(ref int magazineAmmo, AudioClip gunFire)
    {
        if (Input.GetButtonDown("Fire1") && magazineAmmo > 0 && !isFired && !isReloading && Time.timeScale != 0)
        {
            audioSource.clip = gunFire;
            audioSource.Play();
            magazineAmmo--;
            RaycastHit Hit;

            if (Physics.Raycast(hit.transform.position, transform.TransformDirection(Vector3.forward), out Hit))
            {
                if (Hit.collider != null && GameObject.FindGameObjectWithTag("EnemyHeadFront") != null && GameObject.FindGameObjectWithTag("EnemyHeadBack") != null 
                    && GameObject.FindGameObjectWithTag("EnemyBodyFront") != null && GameObject.FindGameObjectWithTag("EnemyBodyBack") != null)
                {

                    Hit.collider.gameObject.SendMessage("Hit" ,damage);
                }

            }

            StartCoroutine(FireCooldown());
        }
    }

    public virtual void Reload(ref int magazineAmmo, ref int maxMagazineAmmo, ref int reservedAmmo, ref AudioClip gunReload)
    {
        if (Input.GetKeyDown(KeyCode.R) && magazineAmmo != maxMagazineAmmo && !isReloading && magazineAmmo + reservedAmmo > 0)
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
