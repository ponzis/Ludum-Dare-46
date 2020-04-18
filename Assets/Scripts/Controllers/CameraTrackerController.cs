using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackerController : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float CameraOffset = -10.0f;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = Target.position;
        newPosition.z = CameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}