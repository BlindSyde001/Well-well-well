using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance; // Singleton instance
    public AudioClip startGameAudio;
    private bool loadingScene = false;

    private void Awake()
    {
        // Create or destroy duplicate instances
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && GameManager.state == GameManager.GameStates.MainMenu)
        {
            if (Instance.loadingScene)
                return;

            Instance.loadingScene = true;
            if (startGameAudio != null)
            {
                AudioSource.PlayClipAtPoint(startGameAudio, Vector3.zero);
            }
            LoadScene("Home_Interior_Day1");
            UIQuests.Instance.questsPanel.SetActive(true);
            GameManager.state = GameManager.GameStates.Playing;
            AudioManager.Instance.PlaySound(AudioManager.Instance.backgroundSound);
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Instance.loadingScene = false;
    }

    public void endGame() {
        Instance.loadingScene = true;
        LoadScene("theEnd");
    }

}
