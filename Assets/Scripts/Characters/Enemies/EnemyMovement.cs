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
    private float _damage;

    [SerializeField]
    private ParticleSystem _blood;

    [SerializeField]
    private AudioSource _bite;

    public AudioSource _zombieGetHit;
    #endregion

    #region methods
    private void Awake()
    {
        _enemyCharacter = GetComponent<Character>();
        _anim = GetComponentInChildren<Animator>();     
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveCharacter();

        StopMoveAndDie();
    }

    /// <summary>
    /// Method that realize enemy movement
    /// </summary>
    public void MoveCharacter()
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
        if (other.CompareTag("Player"))
        {                                                           
            _anim.SetTrigger("Attack");
            _bite.Play();
            other.GetComponent<Character>().TakeDamage(_damage);
            if (other.GetComponent<Character>().CurrentHealth <= 0)
            {                                                       
                _anim.SetBool("IfPlayerDeath", true);
                _agent.isStopped = true;
            }
        }
        else if (other.CompareTag("Bullet"))
        {
            _blood.Play();
        }
    }
    #endregion
}
