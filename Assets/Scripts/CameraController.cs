using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothTime;
    [SerializeField] private float xOffset;

    private Vector3 refVel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!GameController.instance.GameOver)
        //{
        //    FollowTarget();
        //}

        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x + xOffset, target.transform.position.y / 2.5f, transform.position.z);
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPos, ref refVel, smoothTime);
        transform.position = newPosition;
    }
}
