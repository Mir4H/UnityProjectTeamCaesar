using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class Triggers : MonoBehaviour, IDataPersistence
{
    private string playedScene;

    public void LoadData(GameData data)
    {
        //nothing
    }
    public void SaveData(GameData data)
    {
        if (playedScene != null)
        {
            data.scenesCompleted.Add(playedScene, true);
            data.lastScene = playedScene;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "goal")
        {
            playedScene = SceneManager.GetActiveScene().name.ToString();
            OnTimerStop();
            OnFinishSuccess();

        }

        if (collision.gameObject.tag == "ChooseDoor")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
