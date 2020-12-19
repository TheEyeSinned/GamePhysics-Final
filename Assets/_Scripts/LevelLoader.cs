using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void IntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    // Update is called once per frame
    public void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    
}
