using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region fields
    [SerializeField] 
    private float _speed;

    [SerializeField]
    private Animator _swatAnim;

    private Vector3 _camForward;
    private Vector3 _moveAnim;
    private Vector3 _moveAnimInput;

    private float _forwardAnimAmount;
    private float _turnAnimAmount;

    private Camera _camera;

    private PlayerCharacter _playerCharacter; 
    private PlayerInput _playerInput;
    #endregion

    #region methods
    private void Awake()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerInput = GetComponent<PlayerInput>();           
        _camera = Camera.main;                                  
    }

    private void Start()
    {
        Time.timeScale = 1;            
        GameData.Instance.LoadData();  
    }

    private void Update()
    {
        MoveCharacter();        

        RotateAndLookOnCoursor();

        ShowThatYouDie();
    }

    private void FixedUpdate()
    {
        if (_camera != null)
        {
            _camForward = Vector3.Scale(_camera.transform.up, new Vector3(1, 0, 1)).normalized;
            _moveAnim = _playerInput.MoveDirection.z * _camForward + _playerInput.MoveDirection.x * _camera.transform.right;
        }
        else
            _moveAnim = _playerInput.MoveDirection.z * Vector3.forward + _playerInput.MoveDirection.x * Vector3.right;

        AnimationController(_moveAnim);
    }

    private void AnimationController(Vector3 moveAnim)
    {
        if (moveAnim.magnitude > 1)
        {
            moveAnim.Normalize();
        }

        _moveAnimInput = moveAnim;

        ConvertMoveInput();
        UpdateAnimator();
    }

    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(_moveAnimInput);
        _turnAnimAmount = localMove.x;

        _forwardAnimAmount = localMove.z;
    }

    private void UpdateAnimator()
    {
        _swatAnim.SetFloat("Forward", _forwardAnimAmount, 0.1f, Time.deltaTime);
        _swatAnim.SetFloat("Turn", _turnAnimAmount, 0.1f, Time.deltaTime);
    }

    /// <summary>
    /// Method that realize player movement
    /// </summary>
    public void MoveCharacter()
    {
        if (!_playerCharacter.IsDead)       
        {
            Vector3 newPosition = _playerInput.MoveDirection.normalized;

            transform.Translate(translation: _speed * Time.deltaTime * newPosition, Space.World);
        }
    }

    /// <summary>
    /// Method that realize player visual death
    /// </summary>
    public void ShowThatYouDie()
    {
        if (_playerCharacter.IsDead)                                                    
        {
            GameData.Instance.SaveData();
            GetComponentInChildren<Animator>().SetBool("isDead", true);                 
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    /// <summary>
    /// Method that realize player looking on mouse coursor
    /// </summary>
    public void RotateAndLookOnCoursor()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition + new Vector3(25, -25, 0));  
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);                            

        if (groundPlane.Raycast(ray, out float rayDistance))                                
        {
            Vector3 point = ray.GetPoint(rayDistance);                              
                Debug.DrawLine(ray.origin, point, Color.red);                       
                transform.LookAt(point);   
        }
    }
    #endregion
}
