using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent _agent;

    public Transform PatrolRoute;
    private List<Transform> _location = new List<Transform>();
    private int _locationIndex = 0;

    private Transform _player;

    private GameBehaviour _gameManager;

    public int _enemyLives = 3;
    public int EnemyLives
    {
        get { return _enemyLives; }
        private set
        {
            _enemyLives = value;
            if (_enemyLives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Defeated!");
            }
        }
    }

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();

        _agent = GetComponent<NavMeshAgent>();
        _player= GameObject.Find("Player").transform;

        if (PatrolRoute != null)
        {
            foreach(Transform child in PatrolRoute)
            {
               
                    _location.Add(child);
               
            }
        }

        if (_location.Count > 0 ) {
            MoveToNextPatrolLocation();
        }

    }

    private void Update()
    {
        if(_location.Count > 0 && !_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (_location.Count == 0) return;
        _agent.destination = _location[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % _location.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player Detected - Attack");

            if (_gameManager != null)
            {
                _gameManager.HP -= 2;
                Debug.Log("Player Hit! HP: " + _gameManager.HP);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player Out Of Range, continue patrol");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Enemy Hit! Lives: " + EnemyLives);
            Destroy(collision.gameObject);
          
        }
    }

    
}
