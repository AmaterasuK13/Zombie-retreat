using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region fields
    [SerializeField]
    protected float _projectileDamage;

    [SerializeField]
    protected float _projectileSpeed;
    #endregion

    #region methods
    protected void Start()
    {
        Destroy(gameObject, 5);
    }

    protected void Update()
    {
        ProjectileMovement();
    }

    /// <summary>
    /// Method that realize projectile movement
    /// </summary>
    public virtual void ProjectileMovement()
    {
        transform.position += _projectileSpeed * Time.deltaTime * transform.forward;
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
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyCharacter>().TakeDamage(_projectileDamage);
            other.GetComponent<EnemyMovement>()._zombieGetHit.Play();
            Destroy(gameObject);
        }
    }
    #endregion
}
