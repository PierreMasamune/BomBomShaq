using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

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
