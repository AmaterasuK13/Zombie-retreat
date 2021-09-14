using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    #region fields
    [SerializeField] 
    private int _enemyDeathPoints; // amount of points, that enemy will give when dies

    [SerializeField]
    private GameObject _ammo; // ammo prefab, that enemy can drop

    [SerializeField]
    private GameObject _medKit; // med kit prefab, that enemy can drop
    #endregion

    #region methods
    /// <summary>
    /// Method that realize enemies death
    /// </summary>
    public override void Die()
    {
        RandomDrop(_ammo, _medKit);
        GameData.Instance.enemyCount--;
        GameData.Instance.currentScore += _enemyDeathPoints;
    }

    /// <summary>
    /// Method that realize random drop when enemies die
    /// </summary>
    /// <param name="ammo">Ammo prefab</param>
    /// <param name="medKit">Med kit prefab</param>
    private void RandomDrop(GameObject ammo, GameObject medKit)
    {
        int rnd = Random.Range(0, 100);
        if (rnd >= 5 && rnd <= 20)
            Instantiate(ammo, transform.position, Quaternion.identity);
        else if (rnd >= 46 && rnd <= 50)
            Instantiate(medKit, transform.position, Quaternion.identity);
    }
    #endregion
}
