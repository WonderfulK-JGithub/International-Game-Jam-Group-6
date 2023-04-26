using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vin2 : MonoBehaviour
{
    [SerializeField] GameObject milkEndingMoment;
    void CheckForMilk()
    {
        if(S_GameManager.score >= 4)
        {
            GetComponent<Animator>().Play("Win_milk");
            milkEndingMoment.SetActive(true);
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GetComponent<Animator>().Play("aa");

        SceneTransition.current.ReloadSceneWithLove();
    }
}
