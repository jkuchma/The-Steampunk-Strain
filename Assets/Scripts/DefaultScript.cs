using UnityEngine;
using UnityEngine.SceneManagement;

/* since this class is attached the only object in the default scene, it is the first and only script ran on startup,
** and is a nice quick hack into Unity to place logic that decides default scene/action behavior when the game starts */
public class DefaultScript : MonoBehaviour
{
    GameType start = GameType.Debug;    // change this to one of the enum values below to decide the type of game that will be launched

    enum GameType
    {
        Debug,  // development mode; will load a test scene for developing & debugging code
        Demo    // prototype mode; will load the demo scene for level building and presentation
    }

    void Start()
    { 
        switch (start)
        { 
            case(GameType.Demo): SceneManager.LoadScene("DemoScene"); break;
            case(GameType.Debug): SceneManager.LoadScene("TestScene"); break;
            default: break;
        }

    }

}