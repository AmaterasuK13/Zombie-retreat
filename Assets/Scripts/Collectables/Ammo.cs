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
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameData.Instance.ammoCount += _ammoAdd;
            Destroy(gameObject);
        }
    }
    #endregion
}
