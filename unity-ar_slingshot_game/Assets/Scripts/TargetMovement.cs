using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private ARPlane plane;

    public void Initialize(ARPlane selectedPlane, float movementSpeed)
    {
        this.plane = selectedPlane;
        this.speed = movementSpeed;

        Vector2 dir2D = Random.insideUnitCircle.normalized;
        direction = new Vector3(dir2D.x, 0, dir2D.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (plane == null) return;

        transform.position += direction * speed * Time.deltaTime;
        Vector3 localPos = plane.transform.InverseTransformPoint(transform.position);

        float halfX = plane.size.x / 2f;
        float halfY = plane.size.y / 2f;

        bool bounced = false;

        if (localPos.x < -halfX || localPos.x > halfX)
        {
            direction.x = -direction.x; 
            bounced = true;
        }
        if (localPos.z < -halfY || localPos.z > halfY)
        {
            direction.z = -direction.z; 
            bounced = true;
        }

        if (bounced)
        {
            localPos.x = Mathf.Clamp(localPos.x, -halfX, halfX);
            localPos.z = Mathf.Clamp(localPos.z, -halfY, halfY);
            transform.position = plane.transform.TransformPoint(localPos);
        }
    }
    
}
