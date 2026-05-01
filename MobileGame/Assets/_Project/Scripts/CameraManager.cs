using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float distanceY;
    [SerializeField] private float distanceZ;

    void Update()
    {
        Vector3 p = target.position;
        p.y += distanceY;
        p.z -= distanceZ;
        transform.position = Vector3.Lerp(transform.position, p, speed * Time.deltaTime);
    }
}
