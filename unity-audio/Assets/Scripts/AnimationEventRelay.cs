using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>(); // Get reference
    }

    public void PlayFootstepSounds()
    {
        Debug.Log("PlayFootstepSounds triggered!");

        if (playerController != null)
        {
            playerController.PlayFootstepSounds(); // Call the original function
        }
        else
        {
            Debug.LogError("PlayerController not found!");
        }
    }

    public void PlayLandingSound() {
        if (playerController != null) {
            playerController.PlayLandingSound();
        }
        else {
            Debug.LogError("PlayerController not found!");
        }
    }
}
