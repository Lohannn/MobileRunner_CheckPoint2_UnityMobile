using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if ((transform.position.z - player.position.z) <= -5)
        {
            Destroy(gameObject);
        }
    }
}
