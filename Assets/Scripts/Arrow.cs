using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    public AudioClip collisionSound; // Collision sound clip
    public AudioClip releaseSound; // Release sound clip
    private AudioSource audioSource; // Reference to the AudioSource


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullString.PullActionReleased += Release;
        audioSource = gameObject.AddComponent<AudioSource>();

        Stop();
    }

    private void OnDestroy()
    {
        PullString.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullString.PullActionReleased -= Release;
        _inAir = true;
        gameObject.transform.parent = null;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);
        audioSource.PlayOneShot(releaseSound); // Play release sound

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.layer != 8)
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }
                Stop();
                audioSource.PlayOneShot(collisionSound); // Play collision sound
            }
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // Call the scoring method when hitting the target
            GameManager.Instance.AddScore(GetScoreBasedOnZone(collision));
            Destroy(gameObject);
        }
    }

    private int GetScoreBasedOnZone(Collision collision)
    {
        // Assuming the target has colliders for different zones
        if (collision.gameObject.CompareTag("Score4"))
        {
            Debug.Log("zone 4");
            return 4; 
        }
        else if (collision.gameObject.CompareTag("Score3"))
        {
            return 3; 
        }
        else if (collision.gameObject.CompareTag("Score2"))
        {
            return 2; 
        }
        else if (collision.gameObject.CompareTag("Score1"))
        {
            return 1; 
        }

        return 0; // Default case
    }
}