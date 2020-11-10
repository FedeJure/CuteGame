using UnityEngine;

public class UnityDonutDestructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Enter triugger!");

        Destroy(other.gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.LogWarning("Enter collision!");
    }
}
