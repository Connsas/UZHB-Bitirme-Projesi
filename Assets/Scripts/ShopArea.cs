using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArea : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            Shop.isShopAvaliable = true;
        }
    }

    void OnTriggerExit()
    {
        Shop.isShopAvaliable = false;
    }
}
