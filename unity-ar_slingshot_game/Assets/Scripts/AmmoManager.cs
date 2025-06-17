using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    private Vector2 dragStart;
    private bool isDragging = false;
    private Camera arCamera;
    private Rigidbody rb;
    private Vector3 initialPos;
    private float forceMultiplier = 0.02f;
    private LineRenderer lineRenderer;
    private int score = 0;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI ammoLeftText;
    public int ammoLeft = 7;
    private const int maxAmmo = 7;
    private PlaneSelector planeSelector;

    void Start()
    {
        arCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        ResetAmmo();
        planeSelector = FindFirstObjectByType<PlaneSelector>();
        
        scoreText = GameObject.Find("Score")?.GetComponent<TextMeshProUGUI>();
        ammoLeftText = GameObject.Find("AmmoLeft")?.GetComponent<TextMeshProUGUI>();

        SetScoreText();
        SetAmmoText();
    }

    void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame) TryStartDrag(touch.position.ReadValue());
            else if (touch.press.isPressed) ContinueDrag();
            else if (touch.press.wasReleasedThisFrame) ReleaseDrag(touch.position.ReadValue());
        }

        if (!isDragging && rb.isKinematic)
        {
            transform.position = arCamera.transform.position + arCamera.transform.forward * 0.3f;
            transform.rotation = arCamera.transform.rotation;
        }

        if (transform.position.y < -1f)
        {
            ResetAmmo();
        }

        bool outOfAmmo = ammoLeft <= 0;
        bool noTargetsLeft = GameObject.FindGameObjectsWithTag("Target").Length == 0;

        if (outOfAmmo || noTargetsLeft)
        {
            planeSelector.TogglePlayAgainBtn(true);
        }
    }

    private void TryStartDrag(Vector2 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            dragStart = screenPos;
        }
    }

    private void ContinueDrag()
    {
        lineRenderer.enabled = true;
        int numPoints = 30;
        lineRenderer.positionCount = numPoints;

        Vector2 dragDelta = Touchscreen.current.primaryTouch.position.ReadValue() - dragStart;
        Vector3 force = new Vector3(-dragDelta.x, -dragDelta.y, 300) * forceMultiplier;
        Vector3 launchDir = arCamera.transform.TransformDirection(force);

        Vector3 startPos = transform.position;
        Vector3 startVel = launchDir / rb.mass;

        float timeStep = 0.05f;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            Vector3 point = startPos + startVel * t + 0.5f * Physics.gravity * t * t;
            lineRenderer.SetPosition(i, point);
        }
    }

    private void ReleaseDrag(Vector2 screenPos)
    {
        if (!isDragging) return;
        isDragging = false;
        lineRenderer.enabled = false;
        rb.isKinematic = false;

        Vector2 dragDelta = screenPos - dragStart;
        Vector3 screenDirection = new Vector3(-dragDelta.x, -dragDelta.y, 0).normalized;
        Vector3 worldDirection = arCamera.transform.TransformDirection(screenDirection);
        float dragMagnitude = dragDelta.magnitude;

        Vector3 force = (arCamera.transform.forward + worldDirection) * dragMagnitude * forceMultiplier;
        rb.AddForce(force, ForceMode.Impulse);

        ammoLeft -= 1;
        SetAmmoText();
    }

    public void ResetAmmo()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        initialPos = arCamera.transform.position + arCamera.transform.forward * 0.3f;
        transform.position = initialPos;
        lineRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            score += 10;
            SetScoreText();
            Invoke(nameof(ResetAmmo), 0.5f);
        }
        else if (collision.gameObject.CompareTag("Plane"))
        {
            Invoke(nameof(ResetAmmo), 0.5f);
        }
    }

    private void SetScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"{score}";
    }

    public void SetAmmoText()
    {
        if (ammoLeftText != null)
            ammoLeftText.text = $"{ammoLeft}";
    }

    public void Reset()
    {
        score = 0;
        ammoLeft = maxAmmo;
        SetScoreText();
        SetAmmoText();
    }
}
