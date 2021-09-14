using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IShootable
{
    [SerializeField]
    private List<Projectile> _bullet;

    [SerializeField]
    private List<float> _bulletShotDelay; 

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private GameObject _bulletFire;

    private PlayerInput _playerInput;

    private float _currentShootDelay = 0;

    #region properties
    public int CurrentProjectile { get; private set; }
    #endregion

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (GameData.Instance.ammoCount > 0 && Time.time >= _currentShootDelay && _playerInput.IsShootig > 0)
        {
            _bullet[CurrentProjectile].CreateProjectile(_shootPoint);
            _shootPoint.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(ShootFire());
            _currentShootDelay = Time.time + _bulletShotDelay[CurrentProjectile];
            GameData.Instance.ammoCount--;
        }
    }

    IEnumerator ShootFire()
    {
        _bulletFire.SetActive(true);
        yield return new WaitForSeconds(.2f);
        _bulletFire.SetActive(false);
    }
}
