using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 targetPos;
    private Camera camera;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        camera = Camera.main;
    }
    private void FixedUpdate()
    {
        targetPos = new Vector3(target.position.x, target.position.y, -10f);
        camera.transform.position = Vector3.Slerp(camera.transform.position, targetPos, 0.25f) ;
    }
}
