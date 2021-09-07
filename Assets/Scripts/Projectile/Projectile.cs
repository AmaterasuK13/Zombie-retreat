using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region fields
    [SerializeField]
    protected float _damage;                // amount of damage projectile deal

    [SerializeField]
    protected float _projectileSpeed;       // speed of projectile 
    #endregion

    #region methods
    protected void Start()
    {
        Destroy(gameObject, 5);     // destroy object after amount of time
    }

    protected void Update()
    {
        ProjectileMovement();       // realizing projectile movement
    }

    /// <summary>
    /// Method that realize projectile movement
    /// </summary>
    public virtual void ProjectileMovement()
    {
        transform.position += transform.forward * _projectileSpeed * Time.deltaTime;
    }

    /// <summary>
    /// method that realize cerating of projectile
    /// </summary>
    /// <param name="shootPoint">Position where projectile creating</param>
    public virtual void CreateProjectile(Transform shootPoint)
    {
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))                                  // check if projectile contact with enemy
        {
            other.GetComponent<EnemyCharacter>().TakeDamage(_damage);   // realize enemy taking damage
            other.GetComponent<EnemyMovement>()._zombieGetHit.Play();   // play enemy getting damage audio clip
            Destroy(gameObject);                                        // destroy object
        }
    }
    #endregion
}
