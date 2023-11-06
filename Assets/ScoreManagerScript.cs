using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public int currentScore = 0;
    public Text scoreBoard;

    [ContextMenu("Update Score")]
    public void UpdateScore()
    {
        currentScore += 1; 
        scoreBoard.text = $"Score:{currentScore}";
    }
}
