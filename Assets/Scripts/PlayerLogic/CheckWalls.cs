
using UnityEngine;

public class CheckWalls : MonoBehaviour
{

    //маска по которой мы будем искать объект
    public LayerMask NeedLayer;
    public GameObject target;
    void Update()
    {

        RaycastHit2D[] hit = Physics2D.RaycastAll(this.transform.position, (Vector2)target.transform.position,100f,  NeedLayer);
        foreach (var collision in hit) { Debug.Log(collision.transform); }

     
    }

}