using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PickUps;

public class Score : MonoBehaviour
{
    public int ScoreValue;

    public Image[] ScoreImages;

    private void OnEnable()
    {
        PickUps.OnPickUp += AddScore;

    }

    private void Update()
    {
        int Score = PlayerPrefs.GetInt("Score",0);
        int CurrentNumber;
        for (int i = 0; i < ScoreImages.Length; i++)
        {
            CurrentNumber = Score % 10;
            Score /= 10;
            ScoreImages[i].sprite = Resources.Load<Sprite>("Sprites/Numbers/Number_" + CurrentNumber);
        }
    }

    private void AddScore(PickUpType Type, int value)
    {
        ScoreValue += value;
        PlayerPrefs.SetInt("Score", ScoreValue);
    }

    private void OnDisable()
    {
        PickUps.OnPickUp -= AddScore;
    
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
}
