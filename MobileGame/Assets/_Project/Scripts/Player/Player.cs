using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private float runSpeed;
    [SerializeField] private float stepSpeed;
    [SerializeField] private float currentLane = 0;
    [SerializeField] private float laneLimit = 1;

    [Header("Jump Settings")]
    [SerializeField] private Transform sensorGround;
    [SerializeField] private float sensorRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;

    [Header("Other Settings")]
    [SerializeField] private float distanceToWin;

    private Vector3 currentPosition;

    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        anim = playerMesh.GetComponent<Animator>();
        currentPosition = transform.position;
    }

    private void Update()
    {
        if (GameManager.inGame)
        {
            if (anim.GetBool("pInGame") != GameManager.inGame) anim.SetBool("pInGame", GameManager.inGame);
            Move();
        }
    }

    private void Move()
    {
        //Move entre as Pistas
        currentPosition = new Vector3(currentLane, currentPosition.y, currentPosition.z);
        currentPosition.z += runSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPosition, stepSpeed * Time.deltaTime);

        if (Mathf.Abs((currentPosition.x - transform.position.x)) < 0.01f)
        {
            transform.position = new Vector3(currentPosition.x, transform.position.y, transform.position.z);
        }
    }

    private bool OnGround()
    {
        return Physics.CheckSphere(sensorGround.position, sensorRadius, groundLayer);
    }

    public void ChangeLane(int direction)
    {
        if (direction < 0)
        {
            if (currentLane > -laneLimit)
            {
                currentLane -= laneLimit;
            }
        }
        else if (direction > 0)
        {
            if (currentLane < laneLimit)
            {
                currentLane += laneLimit;
            }
        }
    }

    public void Jump()
    {
        if (OnGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    public float GetDistanceToWin()
    {
        return distanceToWin;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(sensorGround.position, sensorRadius);
    }
}
