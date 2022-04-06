using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(bool reload){
        if (reload){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
