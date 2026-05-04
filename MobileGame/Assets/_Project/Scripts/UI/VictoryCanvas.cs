using UnityEngine;
using UnityEngine.UI;

public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreRecordText;

    private void OnEnable()
    {
        scoreText.text = PlayerData.score.ToString().PadLeft(4, '0');
        scoreRecordText.text = PlayerData.scoreRecord.ToString().PadLeft(4, '0');
    }
}
