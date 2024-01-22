using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class RNManager : MonoBehaviourPunCallbacks
{
    public static RNManager instance { get; private set; }

    public string[] messageSplit;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += LoadScene;
    }

    public void RNMessage(string message)
    {
        if (message.Contains(','))
        {
            messageSplit = message.Split(',');
            messageSplit[1] = messageSplit[1].ToLower();

            StartCoroutine(messageSplit[0]);
        }

        else
        {
            StartCoroutine(message);
        }
    }

    private IEnumerator LoadRoomScene()
    {
        SceneManager.LoadScene("RoomEditScene");

        yield break;
    }

    private IEnumerator LoadAvatarScene()
    {
        SceneManager.LoadScene("AvatarScene");

        yield break;
    }

    private void LoadScene(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "RoomEditScene":
                break;
            case "AvatarScene":
                AvatarDressUp.instance.avatar_gender = messageSplit[1];
                AvatarDressUp.instance.AvatarDress(messageSplit[1]);
                break;
            default:
                break;
        }
    }
}
