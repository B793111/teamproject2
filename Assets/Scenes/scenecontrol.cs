using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Main()
    {
        SceneManager.LoadScene(0);
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Help()
    {
        SceneManager.LoadScene(2);
    }
}
