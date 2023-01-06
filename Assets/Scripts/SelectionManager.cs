using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int currentIndex = 0;

    [Header("Component References")]
    [SerializeField] Transform spawnPos;
    [SerializeField] List<GameObject> circles;
    [SerializeField] GameObject circle;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CircleManager.Instance.Shoot(circle);
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject g = Instantiate(circles[currentIndex], spawnPos.position, Quaternion.identity);
        g.transform.SetParent(spawnPos);
        circle = g;
        currentIndex++;       
    }

    public void ActiveSelection(bool active)
    {
        spawnPos.gameObject.SetActive(active);
    }

    public void AddToGrid(Vector3 pos)
    {

    }
}
