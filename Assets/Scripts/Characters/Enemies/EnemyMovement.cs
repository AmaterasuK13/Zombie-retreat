using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IMoveable
{
    #region fields
    private Character _enemyCharacter; 
    private NavMeshAgent _agent;
    private Animator _anim;
    private GameObject _player;

    [SerializeField]
    private float _damage; // the amount of damage enemy dealing to player

    [SerializeField]
    private ParticleSystem _blood;

    [SerializeField]
    private AudioSource _bite;

    public AudioSource _zombieGetHit;
    #endregion

    #region methods
    private void Awake()
    {
        _enemyCharacter = GetComponent<Character>();                // Getting components for fields
        _anim = GetComponentInChildren<Animator>();                 //       
        _agent = GetComponent<NavMeshAgent>();                      //
        _player = GameObject.FindGameObjectWithTag("Player");       // Finding player in scene
    }

    void Update()
    {
        Move();                 // Realizing enemy movement and death
                                //
        StopMoveAndDie();       //
    }

    /// <summary>
    /// Method that realize enemy movement
    /// </summary>
    public void Move()
    {
        _agent.SetDestination(_player.transform.position);
    }

    /// <summary>
    /// Method that make enemy stay in position and destroy when die
    /// </summary>
    public void StopMoveAndDie()
    {
        if (_enemyCharacter.CurrentHealth <= 0)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(GetComponent<Collider>());
            Destroy(GetComponentInChildren<Collider>());
            Destroy(gameObject, 2);
            GetComponentInChildren<Animator>().SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))                             // If enemy contact with player
        {                                                           
            _anim.SetTrigger("Attack");                             // Play attack animation
            _bite.Play();                                           // Play attack sound clip
            other.GetComponent<Character>().TakeDamage(_damage);    // Realize player taking damage
            if (other.GetComponent<Character>().CurrentHealth <= 0) // If player die
            {                                                       
                _anim.SetBool("IfPlayerDeath", true);               // Play enemy taunt animation when player die
                _agent.isStopped = true;                            // Stop enemy movement
            }
        }
        else if (other.CompareTag("Bullet"))                        // If enemy contact with projectile
        {
            _blood.Play();                                          // Play blood partical system
        }
    }
    #endregion
}
