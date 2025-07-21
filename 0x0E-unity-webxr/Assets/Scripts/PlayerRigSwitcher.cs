using UnityEngine;
using UnityEngine.XR;

public class PlayerRigSwitcher : MonoBehaviour
{
    public GameObject vrRig;
    public GameObject nonVrRig;

    void Start()
    {
        bool isVR = XRSettings.isDeviceActive;

        nonVrRig.SetActive(!isVR);
        vrRig.SetActive(isVR);
    }
}
