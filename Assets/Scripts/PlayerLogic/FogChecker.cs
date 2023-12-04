
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class FogChecker : MonoBehaviour
{
    public VisualEffect vfxRenderer;
    [SerializeField] private float fogOffset = 15f;
    private Vector3 FogVec;  
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        FogVec = new Vector3(rb.position.x, rb.position.y + fogOffset, 0);
        vfxRenderer.SetVector3("ColliderPos", FogVec);
    }

}
