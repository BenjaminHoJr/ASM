using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
        ASM_MN = ASM_MN.Instance;
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();

        int id = 1; // Replace with actual ID
        string name = "PlayerName"; // Replace with actual name
        int score = scoreKeeper.GetScore(); // Replace with actual score
        string region = "RegionName";

        ASM_MN.YC1(id, name, score, region);
        ASM_MN.YC2();
        ASM_MN.YC3();
        ASM_MN.YC4();
        ASM_MN.YC5();
        ASM_MN.YC6();
        ASM_MN.YC7();
    }

    


}
