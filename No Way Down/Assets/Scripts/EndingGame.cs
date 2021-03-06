using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingGame : MonoBehaviour
{
    public GameObject player;
    bool EndGame = false;
    public GameObject winTextObject;
    public static float timeScale { get; set; }
    bool Ending = false;
    float EndDelay = 3.5f;
    public TextMeshProUGUI TimeCostText;
    public TextMeshProUGUI ScoreText;
    public Score score;
    public Timer timer;
    public GameObject TimeCost;
    public GameObject GBScore;

    public GameObject StopTimeText;
    public GameObject StopScoreText;

    public GameObject dataSave;

    void Start()
    {
        winTextObject.SetActive(false);
        TimeCost.SetActive(false);
        GBScore.SetActive(false);
        dataSave = GameObject.FindGameObjectWithTag("Data");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            EndGame = true;
        }
    }

    void Update()
    {
        if (EndGame)
        {
            EndLevel(true);
        }
        if (Ending == true)
        {
            EndDelay -= Time.fixedDeltaTime;
        }
        if (EndDelay <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
            
    }

    void EndLevel(bool next)
    {
        if (next)
        {
            dataSave.GetComponent<DataSave>().L3NA = false;
            int temp = score.checkScore() + (100 - timer.getTime()) * 10;

            TimeCostText.text = "Total Time: " + timer.getTime().ToString() + "s";
            TimeCost.SetActive(true);
            if (timer.getTime() >= 100)
            {
                ScoreText.text = "Score: " + score.checkScore();
            }
            else
            {
                
                ScoreText.text = "Score: " + temp;
            }
            GBScore.SetActive(true);
            if (dataSave.GetComponent<DataSave>().L3Time > timer.getTime())
            {
                dataSave.GetComponent<DataSave>().L3Time = timer.getTime();
            }
            if (dataSave.GetComponent<DataSave>().L3Score < temp)
            {
                dataSave.GetComponent<DataSave>().L3Score = temp;
            }
            StopTimeText.SetActive(false);
            StopScoreText.SetActive(false);
            winTextObject.SetActive(true);
            Time.timeScale = 0;
            Ending = true;
        }
    }
}
