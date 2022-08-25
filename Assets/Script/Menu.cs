using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    //pause menu and game setting menu
    public GameObject pauseMenu,gameSetting;
    //setting the music 
    public AudioMixer audioMixer;
    public void StartGame()
    {
       //load scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //game setting

    //quit game
    public void QuitGame()
    {
        //quit game
        Application.Quit();

    }
    //pause game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    //back to the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //game setting
    public void GameSetting()
    {
        gameSetting.SetActive(true);
        Time.timeScale = 0f;

    }

    //return menu
    public void ReturnMenu()
    {
        gameSetting.SetActive(false);
        Time.timeScale = 1f;
    }

    //set music volume
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume",value);
    }
    
    









}
