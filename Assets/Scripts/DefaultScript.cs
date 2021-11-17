using UnityEngine;
using UnityEngine.SceneManagement;

/* since this class is attached the only object in the default scene, it is the first and only script ran on startup,
** and is a nice quick hack into Unity to place logic that decides default scene/action behavior when the game starts */
public class DefaultScript : MonoBehaviour
{
    [SerializeField] GameType startingScene = GameType.Debug;

    enum GameType
    {
        Debug,  /* development mode; will load a test scene for developing & debugging of code */
        Demo    /* prototype mode; will load the demo scene for level building and presentation */
    }

    void Start()
    { 
        switch (startingScene)
        { 
            case(GameType.Demo): SceneManager.LoadScene("DemoScene"); break;
            case(GameType.Debug): SceneManager.LoadScene("DebugScene"); break;
            // default: break;
        }

    }

}