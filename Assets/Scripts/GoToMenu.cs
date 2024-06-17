using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public void Update()
    {
        SceneManager.LoadScene("Menu");
    }
}
