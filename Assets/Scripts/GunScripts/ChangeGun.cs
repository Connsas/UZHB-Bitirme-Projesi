using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{

    [SerializeField] private GameObject _StartingPistol;
    [SerializeField] private GameObject _Usps;
    [SerializeField] private GameObject _Deagle;

    private void Change()
    {
        if (!Gun.isReloading && !Gun.isFired)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _StartingPistol.SetActive(true);
                _Usps.SetActive(false);
                _Deagle.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Usp.isAvailable)
                {
                    _StartingPistol.SetActive(false);
                    _Usps.SetActive(true);
                    _Deagle.SetActive(false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Deagle.isAvailable)
                {
                    _StartingPistol.SetActive(false);
                    _Usps.SetActive(false);
                    _Deagle.SetActive(true);
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }
}
