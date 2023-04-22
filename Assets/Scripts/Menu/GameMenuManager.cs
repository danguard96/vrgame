using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty pauseButton;
    public bool gameIsPaused = false;

    void Update()
    {
        if (pauseButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1;
            if (!gameIsPaused)
            {
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
            else if (gameIsPaused)
            {
                Time.timeScale = 1f;
                gameIsPaused = false;
            }
        }
    }
}
