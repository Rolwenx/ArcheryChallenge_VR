using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public class PullString : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    public Transform start, end;
    public GameObject notch;

    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor _pullingInteractor = null;

    // Variables to control the elastic behavior
    private float smoothPullVelocity = 0f; // Used by SmoothDamp to track velocity of pullAmount change
    public float elasticTime = 0.2f; // How fast the string moves towards the final pulled position
    public float elasticOvershoot = 0.1f; // Additional overshoot to give a bouncy effect

    public AudioClip pullingStringSound; // Pulling string sound clip
    private AudioSource audioSource; // Reference to the AudioSource



    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        _pullingInteractor = args.interactorObject;

    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        _pullingInteractor = null;
        pullAmount = 0f; // Reset the pull amount when released

        // Reset notch position for the next shot, keeping it aligned
        ResetNotchPosition();

        UpdateString();
    }

    private void ResetNotchPosition()
    {
        // Assuming that the initial notch position is at the start
        notch.transform.localPosition = start.localPosition; // Set notch to the start position
    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = _pullingInteractor.transform.position;
                float targetPullAmount = CalculatePull(pullPosition);

                // Play pulling sound while pulling
                if (pullAmount < 1f && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(pullingStringSound); // Play pulling sound
                }

                // Smooth the pull amount for an elastic effect
                pullAmount = Mathf.SmoothDamp(pullAmount, targetPullAmount, ref smoothPullVelocity, elasticTime);

                UpdateString();
                Haptic();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;

        return Mathf.Clamp(pullValue, 0, 1 + elasticOvershoot); // Allow for a slight overshoot for elasticity
    }

    private void UpdateString()
    {
        // Calculate the position of the line based on the pull amount
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);

        // Center the notch with the string
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z);

        // Update the line renderer
        _lineRenderer.SetPosition(1, linePosition);
    }

    private void Haptic()
    {
        if (_pullingInteractor != null)
        {
            ActionBasedController currentController = _pullingInteractor.transform.gameObject.GetComponent<ActionBasedController>();
            currentController.SendHapticImpulse(pullAmount, .1f);
        }
    }
}