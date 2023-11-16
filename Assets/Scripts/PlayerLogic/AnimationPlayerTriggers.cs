
using UnityEngine;


public class AnimationPlayerTriggers : MonoBehaviour
{
    public Animator anim;
    private float hAxis;
    private float vAxis;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            transform.localScale = new Vector3(-5, 5, 1);
        }

        if (hAxis > 0.01 || hAxis < -0.01)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

   
}
