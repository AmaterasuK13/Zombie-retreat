using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region fields
    [SerializeField] 
    private float _speed; // player character speed

    [SerializeField]
    private Animator _swatAnim; // player character animator

    private Camera _camera; // main camera

    [SerializeField]
    private List<Projectile> _bullet; // variations of projectiles

    [SerializeField]
    private List<float> _bulletShotDelay; // variations of projectiles shooting delay

    [SerializeField] 
    private Transform _shootPoint; // the point where projectiles spawn

    [SerializeField] 
    private GameObject _bulletFire; // fire partical system

    [SerializeField] 
    private GameObject _gamePanel; // in game UI panel

    [SerializeField] 
    private GameObject _lossPanel; // loss UI panel

    [SerializeField] 
    private Text _finScoreText; // text that show the final score when player die

    private float _currentShootDelay = 0; // variable that realize shooting delay

    private PlayerCharacter _playerCharacter; 
    private PlayerInput _playerInput;
    #endregion

    #region properties
    /// <summary>
    /// Shows current using projectile
    /// </summary>
    public int CurrentProjectile { get; private set; }
    #endregion

    #region methods
    private void Awake()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();     //
        _playerInput = GetComponent<PlayerInput>();             // Getting components
        _camera = Camera.main;                                  //
    }

    private void Start()
    {
        Time.timeScale = 1;             // Set time to go normal
        GameData.instance.LoadData();   // Load all game datas
    }

    private void Update()
    {
        Move();         // Realizing player movement, rotation, shooting and death

        Rotate();

        Shoot();

        Death();
    }

    /// <summary>
    /// Method that realize player movement
    /// </summary>
    public void Move()
    {
        if (!_playerCharacter.IsDead)       // check that player is alive
        {
            Vector3 newPosition = _playerInput.MoveDirection.normalized;

            transform.Translate(translation: _speed * Time.deltaTime * newPosition, Space.World);

            if (_playerInput.MoveDirection != Vector3.zero)     // swithcing animations
                _swatAnim.SetBool("isRunning", true);
            else
                _swatAnim.SetBool("isRunning", false);
        }
    }

    /// <summary>
    /// Method that realize player shooting
    /// </summary>
    public void Shoot()
    {
        if (GameData.instance.ammoCount > 0 && Time.time >= _currentShootDelay && _playerInput.IsShootig > 0)   // check that player got ammo,
        {                                                                                                       // the delay of projectile comes to an end,
            _bullet[CurrentProjectile].CreateProjectile(_shootPoint);  // create projectile                     // player pressed shoot button
            _shootPoint.gameObject.GetComponent<AudioSource>().Play(); // play shoot audio clip
            StartCoroutine(ShootFire());                               // play shoot particle system
            _currentShootDelay = Time.time + _bulletShotDelay[CurrentProjectile];  // increase projectile delay 
            GameData.instance.ammoCount--;                                         // spend ammo amount
        }
    }

    /// <summary>
    /// Method that realize player death
    /// </summary>
    public void Death()
    {
        if (_playerCharacter.IsDead)                                                    // check is player dead
        {
            GameData.instance.SaveData();                                               // save progress
            _gamePanel.GetComponent<Image>().color = new Color(1, .3f, .3f, .1f);       // make blood screen effect
            GetComponentInChildren<Animator>().SetBool("isDead", true);                 // play player death animation
            GetComponent<PlayerInput>().enabled = false;                                // swithc of player input
            _lossPanel.SetActive(true);                                                 // activate loss game panel
            _finScoreText.text = GameData.instance.currentScore.ToString();             // show final score 
        }
    }

    /// <summary>
    /// Method that realize player rotation
    /// </summary>
    public void Rotate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition + new Vector3(25, -25, 0));  // creating ray
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);                            // creating invisible plane where ray points

        if (groundPlane.Raycast(ray, out float rayDistance))                                // check if ray on the plane 
        {
            Vector3 point = ray.GetPoint(rayDistance);                                      // get the position of ray point on plane
                Debug.DrawLine(ray.origin, point, Color.red);                               // show ray in scene mode
                transform.LookAt(point);                                                    // rotate player to ray
        }
    }

    IEnumerator ShootFire()                     // create fire partical system works
    {
        _bulletFire.SetActive(true);
        yield return new WaitForSeconds(.2f);
        _bulletFire.SetActive(false);
    }
    #endregion
}
