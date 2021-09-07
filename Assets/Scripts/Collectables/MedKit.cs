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
        Destroy(gameObject, 10);                                            // destroy object after the amount of time
    }

    private void OnTriggerEnter(Collider other)                             
    {
        if (other.CompareTag("Player"))                                     // check if contact with player
        {
            if (other.GetComponent<PlayerCharacter>().CurrentHealth < 3)    // check if players amount of hp is not full
            {
                other.GetComponent<PlayerCharacter>().Heal(_healCount);     // healing player
                Destroy(gameObject);                                        // destroy object
            }
        }
    }
    #endregion
}
