using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField] int startLevel = 1;
    public int level { get; private set; } = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        level = startLevel;
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