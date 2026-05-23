using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    //private const int MANAGER_SCENE_INDEX = 0; //index found in the 'build settings,' allegedly. As it stands right now, scene 0 is "Game"
    // This line above is commented as it was loading a second "game" scene while on runtime. Also see line 12 regarding the LoadSceneAsync line which is related to this comment.

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] public static void Initialize()
    {
        EventManager.Initialize(); //Starts the event manager
        //SceneManager.LoadSceneAsync(MANAGER_SCENE_INDEX, LoadSceneMode.Additive); //Loads initial scene.
    }
}
