using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Text message;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _acceptButton.onClick.AddListener(ReloadScene);
        _exitButton.onClick.AddListener(StopApp);
    }
    public void Message(int score)
    {
        message.text = "Oops, it's end of the game\nYour score: " + score;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void StopApp()
    {
        Application.Quit();
    }
}
