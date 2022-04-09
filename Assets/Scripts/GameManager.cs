using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public int level { get; private set; } = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(bool reload){
        if (reload){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        level ++; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}