using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    #region fields
    [SerializeField]
    private int _ammoAdd = 50; // amount of giving ammo
    #endregion

    #region methods
    private void Update()
    {
        Destroy(gameObject, 10);        // destroy object after the amount of time
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))                 // check if contact with player
        {
            GameData.instance.ammoCount += _ammoAdd;    // give the amount of ammo to player
            Destroy(gameObject);                        // destroy object
        }
    }
    #endregion
}
