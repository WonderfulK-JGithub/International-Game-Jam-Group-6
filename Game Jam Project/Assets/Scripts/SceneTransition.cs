using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition current;

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
