using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class mainmanu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public AudioMixer audiomixer;

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Menuvolume", volume);    
    }

    
    
    
        
}
