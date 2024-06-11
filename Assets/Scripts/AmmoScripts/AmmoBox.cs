using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private AudioClip _ammoPickup;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (StartingPistol.isAvailable)
            {
                if (GunStartingPistol.magazineAmmoStartingPistol + GunStartingPistol.reservedAmmoStartingPistol <= GunStartingPistol.totalAmmoStartingPistol - 10)
                {
                    GunStartingPistol.reservedAmmoStartingPistol += 10;
                }
                else
                {
                    GunStartingPistol.reservedAmmoStartingPistol = GunStartingPistol.totalAmmoStartingPistol - GunStartingPistol.magazineAmmoStartingPistol;
                }
            }
            if (Usp.isAvailable)
            {
                if (GunUsps.magazineAmmoUsps + GunUsps.reservedAmmoUsps <= GunUsps.totalAmmoUsps - 8)
                {
                    GunUsps.reservedAmmoUsps += 8;
                }
                else
                {
                    GunUsps.reservedAmmoUsps = GunUsps.totalAmmoUsps - GunUsps.magazineAmmoUsps;
                }
            }
            if (Deagle.isAvailable)
            {
                if (GunDeagle.magazineAmmoDeagle + GunDeagle.reservedAmmoDeagle <= GunDeagle.totalAmmoDeagle - 5)
                {
                    GunDeagle.reservedAmmoDeagle += 5;
                }
                else
                {
                    GunDeagle.reservedAmmoDeagle = GunDeagle.totalAmmoDeagle - GunDeagle.magazineAmmoDeagle;
                }
            }
            StartCoroutine(playSound());
        }
        
    }

    private IEnumerator playSound()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
        GetComponent<AudioSource>().clip = _ammoPickup;
        GetComponent<AudioSource>()?.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
