using UnityEngine;
using TMPro;

public class PinManager : MonoBehaviour
{
    [Header("Pin Setup")]
    public GameObject[] pins; 
    public float fallThreshold = 45f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private bool[] pinFallen;
    private int fallenCount = 0;

    void Start()
    {
        Debug.Log("PinManager started, setting score to 0.");
        pinFallen = new bool[pins.Length];
        UpdateScoreText();
    }

    void Update()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (!pinFallen[i])
            {
                float angle = Vector3.Angle(pins[i].transform.forward, Vector3.up);
                if (angle > fallThreshold)
                {
                    pinFallen[i] = true;
                    fallenCount++;
                    UpdateScoreText();
                    Debug.Log(fallenCount.ToString());
                }
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = fallenCount.ToString();
        }
    }

    public void ResetPins()
    {
        fallenCount = 0;
        for (int i = 0; i < pinFallen.Length; i++)
        {
            pinFallen[i] = false;
        }
        UpdateScoreText();
    }
}
