using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int crystals = 0;
    public bool gameOver = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text crystalText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void AddCrystal()
    {
        crystals++;
        crystalText.text = crystals.ToString();
    }

    public void LoseCrystals()
    {
        if(crystals == 1)
        {
            ShowLosePanel();
        } else
        {
            crystals = crystals / 2;
        }

        crystalText.text = crystals.ToString();
    }

    public void StartGame()
    {
        //change to instructions screen 
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        //change to instructions screen 
        SceneManager.LoadScene("Credits");
    }

    public void EndGame()
    {
        //Exit Game
        Application.Quit();
    }
}
