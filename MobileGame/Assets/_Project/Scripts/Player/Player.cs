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
        //Gerado com I.A ao bster de frente com um problema que causava o player mudar de lane muito lentamente

        // 1. Calcula a nova posição Z (frente) independentemente
        float nextZ = transform.position.z + (runSpeed * Time.deltaTime);

        // 2. Calcula a nova posição X (lateral) usando MoveTowards
        float nextX = Mathf.MoveTowards(transform.position.x, currentLane, stepSpeed * Time.deltaTime);

        // 3. Aplica ao Transform (Mantendo o Y atual do Rigidbody/Física)
        transform.position = new Vector3(nextX, transform.position.y, nextZ);

        // Atualiza o currentPosition para o próximo frame
        currentPosition = transform.position;
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
