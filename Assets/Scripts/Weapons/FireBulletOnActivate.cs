using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;
    public bool isAutomatic;
    [Range(0.01f, 10)]
    public float timeToWait = 0.5f;
    private bool isPressed;
    private bool isWaiting;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        isWaiting = false;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.deactivated.AddListener(StopFiring);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            FireBullet(null);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FireBullet(null);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            StopFiring(null);
        }
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (isWaiting)
        {
            return;
        }
        if (isPressed && !isAutomatic)
        {
            return;
        }
        sound.Play();
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.tag = "Bullet";
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 1);
        if (isAutomatic)
        {
            isWaiting = true;
            StartCoroutine(WaitingTime());
        }
        isPressed = true;
    }

    public void StopFiring(DeactivateEventArgs arg)
    {
        isPressed = false;
    }

    public IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(timeToWait);
        isWaiting = false;
        yield return null;
    }
}
