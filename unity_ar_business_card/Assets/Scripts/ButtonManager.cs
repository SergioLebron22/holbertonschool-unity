using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OpenInstagram() {
        Application.OpenURL("https://www.instagram.com/_slebron_22?igsh=MTV4eGE4eW56bjc5Zg%3D%3D&utm_source=qr");
    }

    public void OpenLinkedIn() {
        Application.OpenURL("https://www.linkedin.com/in/sergio-lebron?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app");
    }

    public void OpenGithub() {
        Application.OpenURL("https://github.com/SergioLebron22");
    }

    public void OpenX() {
        Application.OpenURL("https://x.com/lebronsergio22?s=21");
    }
}
