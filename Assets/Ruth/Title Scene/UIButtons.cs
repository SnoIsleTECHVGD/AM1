using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private AudioSource buttonPress;

    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private AudioSource exitButton;

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();
    }

    // Global

    IEnumerator GoToScene(string Scene)
    {
        AudioSource audio;

        if (Scene == "Quit")
            audio = exitButton;
        else
            audio = buttonPress;

        audio.Play();
        music.Stop();

        while (audio.isPlaying)
        {
            yield return null;
        }

        if (Scene == "Quit")
            Application.Quit();
        else
            SceneManager.LoadScene(Scene);
    }

    // Buttons

    public void Play()
    {
        StartCoroutine(GoToScene("Level1"));
    }
    public void Credits()
    {
        StartCoroutine(GoToScene("Credits"));
    }
    public void Quit()
    {
        StartCoroutine(GoToScene("Quit"));
    }
}
