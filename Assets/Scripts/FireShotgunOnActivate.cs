using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireShotgunOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;
    public float radius;
    public float depth;
    public float angle;
    [Range(0.01f, 10)]
    public float timeToWait = 0.5f;
    private bool isPressed;
    private bool isWaiting;
    private bool hasSlide = true;
    AudioSource sound;
    public AudioClip cocking;
    private Physics physics;

    public float BulletForce = 100;

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
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (isWaiting || !hasSlide)
        {
            return;
        }
        sound.Play();
        ShotPellets();
        isWaiting = true;
        StartCoroutine(WaitingTime());
        isPressed = true;
        hasSlide = false;
    }

    public void Slide()
    {
        hasSlide = true;
        sound.PlayOneShot(cocking);
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

    public void ShotPellets()
    {
        RaycastHit[] raycastHits = physics.ConeCastAll(transform.position, radius, transform.forward, depth, angle);
        foreach (RaycastHit hit in raycastHits)
        {
            Rigidbody rigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 hitDirection = hit.transform.TransformDirection(spawnPoint.position);
                rigidbody.AddForceAtPosition(hitDirection * BulletForce, hit.point);
            }
        }
    }
}
