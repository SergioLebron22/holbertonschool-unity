using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TMPro;

public class PlaneSelector : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    public Camera arCamera;
    public Image BackGround;
    public Text text;
    public GameObject startButtonUI;
    public static ARPlane SelectedPlane { get; private set; } = null;
    public GameObject targetPreab;
    public int numOfTargets = 5;
    public GameObject ammoPrefab;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool planeSelected = false;
    public Button restartBtn;
    public Button quitBtn;
    public Image ammoChamber;
    public TextMeshProUGUI scoreText;
    public Button playAgainButton;
    public int maxAmmo = 7;
    private int score = 0;

    void Update()
    {
        if (planeManager.trackables.count > 0)
        {
            BackGround.color = new Color(0, 255, 0, 255);
            text.text = "Select a plane";
        }

        if (!planeSelected && Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARPlane plane = planeManager.GetPlane(hits[0].trackableId);
                    if (plane != null)
                    {
                        SelectPlane(plane);
                    }
                }
            }
        }
    }

    private void SelectPlane(ARPlane plane)
    {
        SelectedPlane = plane;
        planeSelected = true;

        foreach (var p in planeManager.trackables)
        {
            if (p.trackableId != plane.trackableId)
            {
                p.gameObject.SetActive(false);
            }
        }

        planeManager.enabled = false;

        if (startButtonUI != null)
            startButtonUI.SetActive(true);
    }

    public void StartGame()
    {
        ARPlane plane = SelectedPlane;
        if (plane == null)
        {
            Debug.LogError("No plane selected. Please select a plane first.");
            return;
        }

        if (startButtonUI != null)
            startButtonUI.SetActive(false);

        BackGround.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

        restartBtn.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(true);
        ammoChamber.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        for (int i = 0; i < numOfTargets; i++)
        {
            Vector3 localPos = new Vector3(
                Random.Range(-plane.size.x / 2, plane.size.x / 2),
                0.3f,
                Random.Range(-plane.size.y / 2, plane.size.y / 2)
            );

            Vector3 worldPos = plane.transform.TransformPoint(localPos);

            Vector3 dir = Camera.main.transform.position - worldPos;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);

            GameObject target = Instantiate(targetPreab, worldPos, rotation);


            float movementSpeed = 0.2f;
            var movement = target.GetComponent<TargetMovement>();
            if (movement != null)
                movement.Initialize(plane, movementSpeed);
        }

        float distanceFromCamera = 0.2f;
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * distanceFromCamera;
        Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
    }

    public void Restart()
    {
        SelectedPlane = null;
        planeSelected = false;

        planeManager.enabled = true;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }

        foreach (var target in GameObject.FindGameObjectsWithTag("Target"))
        {
            Destroy(target);
        }
        foreach (var ammo in GameObject.FindGameObjectsWithTag("Ammo"))
        {
            Destroy(ammo);
        }
        if (startButtonUI != null) startButtonUI.SetActive(false);
        if (BackGround != null)
        {
            BackGround.gameObject.SetActive(true);
            BackGround.color = Color.gray;
        }
        if (text != null)
        {
            text.gameObject.SetActive(true);
            text.text = "Searching for surfaces...";
        }
        if (restartBtn != null)
            restartBtn.gameObject.SetActive(false);
        if (quitBtn != null)
            quitBtn.gameObject.SetActive(false);
        if (ammoChamber != null)
            ammoChamber.gameObject.SetActive(false);

        AmmoManager ammoManager = FindAnyObjectByType<AmmoManager>();
        if (ammoManager != null)
            ammoManager.Reset();

    }

    public void SpawnTargetsOnPlane(ARPlane plane)
    {
        for (int i = 0; i < numOfTargets; i++)
        {
            Vector3 localPos = new Vector3(
                Random.Range(-plane.size.x / 2, plane.size.x / 2),
                0.3f,
                Random.Range(-plane.size.y / 2, plane.size.y / 2)
            );

            Vector3 worldPos = plane.transform.TransformPoint(localPos);

            Vector3 dir = Camera.main.transform.position - worldPos;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);

            GameObject target = Instantiate(targetPreab, worldPos, rotation);


            float movementSpeed = 0.2f;
            var movement = target.GetComponent<TargetMovement>();
            if (movement != null)
                movement.Initialize(plane, movementSpeed);
        }

        float distanceFromCamera = 0.2f;
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * distanceFromCamera;
        Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        score = 0;
        if (scoreText != null)
            scoreText.text = $"{score}";

        AmmoManager ammoManager = FindAnyObjectByType<AmmoManager>();
        if (ammoManager != null)
        {
            ammoManager.ammoLeft = maxAmmo;
            ammoManager.SetAmmoText(); 
            ammoManager.ResetAmmo();   
        }

        foreach (var target in GameObject.FindGameObjectsWithTag("Target"))
        {
            Destroy(target);
        }
        foreach (var ammo in GameObject.FindGameObjectsWithTag("Ammo"))
        {
            Destroy(ammo);
        }

        if (SelectedPlane != null)
        {
            SpawnTargetsOnPlane(SelectedPlane);
        }

        if (playAgainButton != null)
            playAgainButton.gameObject.SetActive(false);
    }

    public void TogglePlayAgainBtn(bool value)
    {
        playAgainButton.gameObject.SetActive(value);
    }
}
