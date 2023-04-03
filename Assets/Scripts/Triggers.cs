using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class Triggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (collision.gameObject.tag == "goal")
        {
            OnTimerStop();
            OnFinishSuccess();
        }

        if (collision.gameObject.tag == "ChooseDoor")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
