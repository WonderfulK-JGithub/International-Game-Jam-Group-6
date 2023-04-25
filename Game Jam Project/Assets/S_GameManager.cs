using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_GameManager : MonoBehaviour
{
    public static int score;

    int maxScore;

    GameObject[] bottles;

    [SerializeField]
    Text text;

    private void Start()
    {
        bottles = GameObject.FindGameObjectsWithTag("Milk");
        maxScore = bottles.Length;
    }

    private void Update()
    {
        text.text = score.ToString() + " / " + maxScore.ToString();

        if(score == maxScore)
        {
            text.color = Color.green;
        }
    }
}
