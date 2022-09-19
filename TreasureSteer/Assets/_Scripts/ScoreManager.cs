using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    public void setScore(int score)
    {
        textScore.text = score.ToString();
    }
}
