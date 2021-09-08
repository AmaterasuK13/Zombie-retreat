using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    #region fields
    [SerializeField]
    private float _healCount = 1;   // amount of player heal
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
            if (other.GetComponent<PlayerCharacter>().CurrentHealth < 3)
            {
                other.GetComponent<PlayerCharacter>().Heal(_healCount);
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
