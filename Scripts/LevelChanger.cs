using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public EscapeWin win;

    // Update is called once per frame
    private void Start()
    {
        

    }
    void Update()
    {
        if (EscapeWin.win)
        {
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 <= 3)
        {
            Debug.Log("Cargando escena " + SceneManager.GetActiveScene().buildIndex);
            FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex + 1 == 4)
        {
            FadeToLevel(0);
        }
    }
    public void FadeToLevel(int nivelIndex)
    {
        levelToLoad = nivelIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
