// C
using UnityEngine;
using UnityEngine.SceneManagement;

/* since this class is attached the only object in the default scene, it is the first and only script ran on startup,
** and is a nice quick hack into Unity to place logic that decides default scene/action behavior when the game starts */
public class DefaultScript : MonoBehaviour
{

    StartType start = StartType.Debug; // change this with the enum below to decide what happens when the play is pressed
    

    enum StartType
    {
        Debug, Demo
    }
    

    void Start()
    { 
        switch (start)
        { 
            case(StartType.Demo): SceneManager.LoadScene("DemoScene"); break;
            case(StartType.Debug): SceneManager.LoadScene("TestScene"); break;
            default: break;
        }
        
    }


}