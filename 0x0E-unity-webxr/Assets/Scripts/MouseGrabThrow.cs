using UnityEngine;

public class MouseGrabThrow : MonoBehaviour
{
    public LayerMask grabbableLayer;
    public float grabDistance;
    public float throwForceMultiplier = 10f;

    private Rigidbody grabbedRb;
    private GameObject ball;
    private Vector3 lastMouseWorldPos;
    private Vector3 currentVelocity;
    public Camera NonVRCam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }
        else if (Input.GetMouseButtonUp(0) && grabbedRb != null)
        {
            Release();
        }
        else if (grabbedRb != null)
        {
            Hold();
        }
    }

    void TryGrab()
    {
        Ray ray = NonVRCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                grabbedRb = hit.collider.GetComponent<Rigidbody>();
                if (grabbedRb != null)
                {
                    grabbedRb.useGravity = false;
                }
                grabDistance = Vector3.Distance(NonVRCam.transform.position, hit.point);
                lastMouseWorldPos = GetMouseWorldPosition();
            }
        }
    }

    void Hold()
    {
        Vector3 targetPos = GetMouseWorldPosition();
        currentVelocity = (targetPos - lastMouseWorldPos) / Time.deltaTime;
        grabbedRb.MovePosition(targetPos);
        lastMouseWorldPos = targetPos;
    }

    void Release()
    {
        grabbedRb.useGravity = true;
        grabbedRb.velocity = currentVelocity * throwForceMultiplier;
        BallSteering steering = grabbedRb.GetComponent<BallSteering>();
        if (steering != null)
        {
            steering.OnRelease();
        }

        grabbedRb = null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = grabDistance; 
        return NonVRCam.ScreenToWorldPoint(mousePos);
    }
}
