using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int pointsValue;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
