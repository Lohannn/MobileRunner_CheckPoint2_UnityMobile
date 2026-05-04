using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private float runSpeed;
    [SerializeField] private float stepSpeed;
    [SerializeField] private float currentLane = 0;
    [SerializeField] private float laneLimit = 1;
    [SerializeField] private float knockbackForce;

    [Header("Jump Settings")]
    [SerializeField] private Transform sensorGround;
    [SerializeField] private float sensorRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;

    [Header("Other Settings")]
    [SerializeField] private float distanceToWin;

    [Header("Stage End Canvas")]
    [SerializeField] private Canvas loseCanva;
    [SerializeField] private Canvas winCanva;
    [SerializeField] private float fadeSpeed;

    [SerializeField] private CameraManager mainCamera;

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
        if (GameStateManager.hasGameStarted)
        {
            if (anim.GetBool("pInGame") != GameStateManager.hasGameStarted) anim.SetBool("pInGame", GameStateManager.hasGameStarted);
            Move();
        }

        anim.SetBool("pOnGround", OnGround());
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
        if (!GameStateManager.hasGameStarted) return;

        if (OnGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    public float GetDistanceToWin()
    {
        return distanceToWin;
    }

    public void Lose()
    {
        GameStateManager.hasGameStarted = false;
        runSpeed = 0;

        string RandomDeathAnim = "pDeath" + Random.Range(1, 4);
        anim.SetTrigger(RandomDeathAnim);

        rb.AddForce(Vector3.back * knockbackForce, ForceMode.Impulse);

        loseCanva.gameObject.SetActive(true);
        loseCanva.GetComponent<StageEndCanvas>().StartFadeIn(fadeSpeed);
    }

    public void Win()
    {
        GameStateManager.hasGameStarted = false;
        runSpeed = 0;
        PlayerData.CheckScoreRecord();

        mainCamera.MoveToVictoryPosition();

        if (Random.Range(1, 11) == 10)
        {
            anim.SetTrigger("pRareDance");
        }
        else
        {
            anim.SetTrigger("pVictoryDance");
        }

        winCanva.gameObject.SetActive(true);
        winCanva.GetComponent<StageEndCanvas>().StartFadeIn(fadeSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(sensorGround.position, sensorRadius);
    }
}
