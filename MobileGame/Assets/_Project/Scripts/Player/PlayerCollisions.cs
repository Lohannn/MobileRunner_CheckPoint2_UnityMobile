using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            print("Fish");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Sardine"))
        {
            print("Sardine");
            Destroy(other.gameObject);
        }
    }
}
