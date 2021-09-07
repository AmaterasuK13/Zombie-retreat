using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    #region fields
    [SerializeField ]
    private GameplayStateMachine _state; // getting state machine component
    #endregion

    #region properties
    /// <summary>
    /// Player move direction vector
    /// </summary>
    public Vector3 MoveDirection { get; private set; }

    /// <summary>
    /// Flag that shows if player is shooting
    /// </summary>
    public float IsShootig { get; private set; }
    #endregion

    #region methods
    void Update()
    {
        MoveDirection = new Vector3(Input.GetAxis(GameData.HORIZONTAL_AXIS), 0, Input.GetAxis(GameData.VERTICAL_AXIS));     // Move direction vector that depends of player input

        IsShootig = Input.GetAxis("Fire1");                                                                                 // Changing the value when player pressing fire button

        if (Input.GetKeyDown(KeyCode.Escape))                                                                               // Swithing on the pause panel when escape button pressed
        {
            _state.PauseOrUnpause();
        }
    }
    #endregion
}
