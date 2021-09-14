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
        GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.SetActive(true);
        }
    }
    #endregion
}
