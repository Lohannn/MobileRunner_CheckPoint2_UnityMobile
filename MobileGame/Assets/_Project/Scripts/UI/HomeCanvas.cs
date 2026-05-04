using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : MonoBehaviour
{
    [SerializeField] private CameraManager mainCamera;

    [SerializeField] private Text recordText; 

    private void Start()
    {
        recordText.text = PlayerData.scoreRecord.ToString().PadLeft(4, '0');

        if (GameStateManager.inGame)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        mainCamera.StartFollowing();
        gameObject.SetActive(false);
    }
}
