using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Text message;
    public GameObject ok;
    public GameObject exit;

    public void Message(int score)
    {
        message.text = "Oops, it's end of the game\nYour score: " + score;
    }
    public void OK()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
