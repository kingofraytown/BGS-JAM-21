using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public int crystals = 0;
    public bool gameOver = false;
    public bool levelComplete = false;
    public bool winAnimation = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text crystalText;
    public float acceleration;
    public GameFloat speed;
    public float startSpeed;
    public CinemachineCamera goalCam;
    public float winDelay = 7f;
    public GameObject slideWitch;
    public float goalTime;
    public float goalTimer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed.SetValue(startSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (winAnimation)
        {
            goalTimer -= Time.deltaTime;
            if(goalTimer <= 0f)
            {
                winAnimation = false;
                slideWitch.transform.position = new Vector3(slideWitch.transform.position.x, slideWitch.transform.position.y, goalCam.transform.position.z);
                slideWitch.SetActive(true);
            }
        }

        if (levelComplete)
        {
            winDelay -= Time.deltaTime;
            if(winDelay <= 0f)
            {
                levelComplete = false;
                winPanel.SetActive(true);
            }
        }
    }

    public void ShowWinPanel()
    {
        //winPanel.SetActive(true);
        speed.SetValue(0);
        goalCam.Priority = 40;
        levelComplete = true;
        winAnimation = true;
        goalTimer = goalTime;
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        speed.SetValue(0);
    }

    public void AddCrystal()
    {
        crystals++;
        crystalText.text = crystals.ToString();
        speed.SetValue(speed.value() + acceleration);
    }

    public int LoseCrystals()
    {
        int rtn = 0;
        if(crystals <= 1)
        {
            ShowLosePanel();
        } else
        {
            rtn = crystals / 2;
            speed.SetValue(speed.value() - (rtn * acceleration));
            crystals = crystals / 2;
        }

        crystalText.text = crystals.ToString();
        return rtn;
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
