using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(int lvl)
    {
        SceneManager.LoadScene(("Level" + lvl), LoadSceneMode.Single);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectionLevel");
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }
}