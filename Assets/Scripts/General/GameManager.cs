using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Menu UI
    private GameObject menu;
    private Button continueButton;
    private Button restartButton;
    private Button exitButton;
    private TextMeshProUGUI menuText;

    //Components
    private PlayerController playerController;

    //Params
    private string playerDeathText = "You Died";
    private string playerWinText = "Success";
    private static int sceneIndex = 1;

    // Start is called before the first frame update
    private void Awake()
    { 
        menu = GameObject.Find("Menu UI");

        continueButton = menu.transform.GetChild(1).GetChild(1).GetComponent<Button>();
        continueButton.onClick.AddListener(NextLevel);

        restartButton = menu.transform.GetChild(1).GetChild(0).GetComponent<Button>();
        restartButton.onClick.AddListener(RestartLevel);

        exitButton = menu.transform.GetChild(1).GetChild(2).GetComponent<Button>();
        exitButton.onClick.AddListener(QuitGame);

        menuText = menu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        Destroy(Camera.main.GetComponent<FollowPlayer>());
        menuText.text = playerDeathText;
        menu.GetComponent<Canvas>().enabled = true;
        continueButton.interactable = false;
        playerController.paused = true;
    }

    public void OnPlayerWin()
    {
        Destroy(Camera.main.GetComponent<FollowPlayer>());
        menuText.text = playerWinText;
        menu.GetComponent<Canvas>().enabled = true;
        continueButton.interactable = true;
        playerController.paused  = true;
    }

    public void RestartLevel()
    {
        menu.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerController.paused = false;
    }

    public void NextLevel()
    {   
        Debug.Log("called next level");
        SceneManager.LoadScene(sceneIndex);
        menu.GetComponent<Canvas>().enabled = false;
        sceneIndex++;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
