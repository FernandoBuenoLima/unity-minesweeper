using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public void GoToEasy()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMedium()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToHard()
    {
        SceneManager.LoadScene(2);
    }
}
