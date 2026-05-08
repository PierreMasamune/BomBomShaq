using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player Detected - Attack");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player Out Of Range, continue patrol");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
