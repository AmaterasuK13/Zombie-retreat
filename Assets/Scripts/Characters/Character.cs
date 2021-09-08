using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    #region fields
    [SerializeField]
    protected float _maxHealth; 
    #endregion

    #region properties
    /// <summary>
    /// Shows is this character is dead
    /// </summary>
    public bool IsDead { get; protected set; }
    /// <summary>
    /// Shows current amont of health point of character
    /// </summary>
    public float CurrentHealth { get; protected set; }
    #endregion

    #region methods
    protected void Awake()
    {
        CurrentHealth = _maxHealth;
        IsDead = false;
    }

    /// <summary>
    /// Method that realize character getting damaged
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// Method that realize death of character
    /// </summary>
    public virtual void Die()
    {

    }
    #endregion
}
