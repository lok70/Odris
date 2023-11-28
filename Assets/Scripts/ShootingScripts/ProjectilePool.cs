
using UnityEngine;


public class ProjectilePool : MonoBehaviour
{

    [SerializeField] private GameObject pref;
    [SerializeField] private int poolSize;
    private GameObject[] pool;
    private void Start()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(pref);
            pool[i].transform.SetParent(this.transform);
<<<<<<< HEAD
=======

>>>>>>> kitttooo`sbranch
            pool[i].SetActive(false);
        }
    }



    public GameObject Get()
    {
<<<<<<< HEAD
        for (int i = 0; poolSize > i; i++)
=======
        for (int i = 0; poolSize > 0; i++)
>>>>>>> kitttooo`sbranch
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }



}
