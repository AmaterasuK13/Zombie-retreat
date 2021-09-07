using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    #region methods
    /// <summary>
    /// Method that realize player heal
    /// </summary>
    /// <param name="heal">Heal amount</param>
    public void Heal(float heal)
    {
        if (CurrentHealth < 3)
        {
            CurrentHealth += heal;
        }
    }

    /// <summary>
    /// Method that flag player death
    /// </summary>
    public override void Die()
    {
        IsDead = true;
    }
    #endregion
}
