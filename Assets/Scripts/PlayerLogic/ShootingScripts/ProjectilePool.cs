
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
            pool[i].SetActive(false);
        }
    }



    public GameObject Get()
    {
        for (int i = 0; poolSize > i; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public GameObject Get(Transform transform)
    {
        for(int i = 0;poolSize > i; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = transform.position;
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }


}
