using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float distanceY;
    [SerializeField] private float distanceZ;

    [SerializeField] private Transform followPosition;
    [SerializeField] private Transform victoryPosition;
    [SerializeField] private float transitionDuration;
    private Transform targetPoint;

    public static bool inFollowPosition = false;
    public static bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            if (!inFollowPosition)
            {
                transform.position = followPosition.position;
                transform.rotation = followPosition.rotation;
                inFollowPosition = true;
            }

            if (inFollowPosition)
            {
                if (transform.rotation != followPosition.rotation)
                {
                    transform.position = followPosition.position;
                    transform.rotation = followPosition.rotation;
                }

                Vector3 p = target.position;
                p.y += distanceY;
                p.z -= distanceZ;
                transform.position = Vector3.Lerp(transform.position, p, speed * Time.deltaTime);
            }
        }
    }

    public void StartFollowing()
    {
        targetPoint = followPosition;
        StartCoroutine(Transition(true));
    }

    public void MoveToVictoryPosition()
    {
        isFollowing = false;
        targetPoint = victoryPosition;
        StartCoroutine(Transition(false));
    }

    IEnumerator Transition(bool startedFollowing)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float elapsed = 0;
        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            transform.SetPositionAndRotation(Vector3.Lerp(startPos, targetPoint.position, t), 
                                             Quaternion.Slerp(startRot, targetPoint.rotation, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.SetPositionAndRotation(targetPoint.position, targetPoint.rotation);

        if (startedFollowing)
        {
            isFollowing = true;
            GetComponent<Camera>().fieldOfView = 77;
            GameStateManager.inGame = true;
        }
    }
}
