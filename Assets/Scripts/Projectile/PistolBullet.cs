using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Projectile
{
    #region methods
    public override void ProjectileMovement()
    {
        base.ProjectileMovement();
    }

    public override void CreateProjectile(Transform shootPoint)
    {
        Instantiate(gameObject, shootPoint.position, shootPoint.rotation);
    }
    #endregion
}
