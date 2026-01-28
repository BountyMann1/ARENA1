using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
            if(other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
