using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition current;

    [SerializeField] Image imagen;
    [SerializeField] Color loveColor;

    Animator anim;

    int target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        current = this;
    }

    public void ReloadScene()
    {
        target = SceneManager.GetActiveScene().buildIndex;

        anim.Play("Scene_Exit");
    }

    public void ReloadSceneWithLove()
    {
        target = SceneManager.GetActiveScene().buildIndex;
        imagen.color = loveColor;

        anim.Play("Scene_Exit");
    }

    public void EnterScene(int _sceneIndex)
    {
        target = _sceneIndex;

        anim.Play("Scene_Exit");
    }

    void Exit()
    {
        SceneManager.LoadScene(target);
    }
}
