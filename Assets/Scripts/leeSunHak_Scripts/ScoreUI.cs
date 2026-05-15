using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text Text_CurrentScore;
    
    public void CurrentCoinScore(int currentScore)
    {
        Text_CurrentScore.text = $"현재 코인 : {currentScore} / 목표 코인 : 5";
    }
}
