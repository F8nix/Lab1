using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 100.0f;
    public bool isAutopilot;
    public Transform target;


    void Update()
    {
        if(!isAutopilot){
            ManualMove();
        } else {
            Autopilot();
        }
    }

    private float DistanceToTarget() {
        return (transform.position - target.position).magnitude;
    }

    private void ManualMove() {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation); 
    }

    private void Autopilot() {
        Vector2 followerPos = transform.position;
        Vector2 targetPos = target.position;

        var direction = DirectionToTarget().normalized;
        var forward = transform.up;

        float angle = ((Vector2)forward).angle(direction);
        float cross = forward.crossProduct(direction).z;
        if(cross<0) angle = -angle;
        // float moveTowardsAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, transform.eulerAngles.z+angle, rotationSpeed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(0, 0, moveTowardsAngle);
        float reducedAngle = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);
        if(Mathf.Abs(reducedAngle) > 0){
          transform.Rotate(new Vector3(0, 0,reducedAngle));
        }
        
        if(DistanceToTarget() > 0.1f){
            transform.Translate(speed * Time.deltaTime * transform.up, Space.World);
        }
    }
    private Vector3 DirectionToTarget() {
        return target.position - transform.position;
    }
}