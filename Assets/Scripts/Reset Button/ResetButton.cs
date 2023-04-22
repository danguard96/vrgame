using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    InitialPosition[] resetObjects;

    // Start is called before the first frame update

    private void Awake()
    {
        resetObjects = FindObjectsByType<InitialPosition>(FindObjectsSortMode.None);
    }
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        foreach (InitialPosition objectToReset in resetObjects)
        {
            onRelease.AddListener(objectToReset.resetPosition);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Reset();
        }
    }

    public void Reset()
    {
        onRelease.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
