
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
        for (int i = 0; poolSize > 0; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }



}
