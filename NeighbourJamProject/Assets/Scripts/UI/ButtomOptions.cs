using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomOptions : MonoBehaviour
{
    public AudioSource AudioHold, audioPressed;
    public AudioClip AudioHoldHov, audioPressedClick;

    public void HoverSound()
    {
        AudioHold.PlayOneShot(AudioHoldHov);
    }
    public void ClickSound()
    {
        audioPressed.PlayOneShot(audioPressedClick);
    }
    //
    //
<<<<<<< Updated upstream
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void IntroScene()
=======
    public void NavMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NavIntroScene()
>>>>>>> Stashed changes
    {
        SceneManager.LoadScene("IntroScene");
    }

<<<<<<< Updated upstream
    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditsScene"); 
=======
    public void NavMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NavCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
>>>>>>> Stashed changes
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    //
    //
    public void PortfolioRaulski()
    {
        Application.OpenURL("https://arsullivan.artstation.com/");
    }
    public void PortfolioAdri()
    {
        Application.OpenURL("http://linkedin.com/in/adriansirvientepardo");
    }
    public void PortfolioJose()
    {
        Application.OpenURL("http://linkedin.com/in/djoecarioca");
    }
    public void PortfolioCelia()
    {
        Application.OpenURL("http://linkedin.com/in/celia-reyes-lamoneda");
    }
}
