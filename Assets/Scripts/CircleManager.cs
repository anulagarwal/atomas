using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int maxCircles;

    [Header("Component References")]
    [SerializeField] List<Circle> circles;
    [SerializeField] List<GameObject> circlesg;

    [SerializeField] GameObject circle;
    [SerializeField] Transform center;
    [SerializeField] float radius;

    public static CircleManager Instance = null;


    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnCircles(14);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(circlesg[0]);
            circlesg.RemoveAt(0);
            UpdatePosition();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Spawn();
        }

        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }

    public void Shoot(GameObject circ)
    {
        RaycastHit hit;
        Ray origin = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(origin, out hit, Mathf.Infinity))
        {

            Vector3 p = (center.position + (new Vector3(hit.point.x, hit.point.y, 0) - new Vector3(center.position.x, center.position.y, 0)).normalized) * radius;

            //  print (new Vector3(hit.point.x, hit.point.y, 0) - new Vector3(center.position.x, center.position.y, 0).normalized) ;
            float angle = 0;
            angle = Mathf.Atan2(hit.point.y - center.transform.position.y, hit.point.x - center.transform.position.x) * 180 / Mathf.PI;
            List<float> i = new List<float>();
            foreach (GameObject g in circlesg)
            {
                float f = 0;



                if (f < -180)
                {
                    f += 180f;
                }
                else if (f > 180)
                {
                    f -= 180f;
                }
                f = Mathf.Abs(GetActualAngle(angle) - GetActualAngle(g.GetComponent<Circle>().angle));
                if (f > 180)
                {
                    f = 360 - f;
                }
                i.Add(f);

            }


            //  j.Sort();
            //print(angle);
            //360/3
            int winIndex = i.IndexOf(i.Min());
            int secondWinIndex = i.IndexOf(i.Find(x => x < 360 / circlesg.Count() && x != i[winIndex]));
            SpawnAt(winIndex > secondWinIndex && winIndex != 0 ? winIndex : winIndex < secondWinIndex && winIndex == 0 ? winIndex : winIndex < secondWinIndex && winIndex != 0 ? secondWinIndex : winIndex, circ);
        }
    }

    public float GetActualAngle(float f)
    {
        if (f < 0)
        {
            //f = 360 + f;
        }
        return f;
    }
    public void SpawnAt(int index, GameObject circ)
    {
        var radians = 2 * Mathf.PI / (circlesg.Count + 1) * circlesg.Count + 1;

        /* Get the vector direction */
        var vertical = Mathf.Sin(radians);
        var horizontal = Mathf.Cos(radians);

        var spawnDir = new Vector3(horizontal, vertical, 0);

        /* Get the spawn position */
        var spawnPos = center.position + spawnDir * radius; // Radius is just the distance away from the point

        /* Now spawn */
        var c = circ;

        circlesg.Insert(index,c);
        /* Adjust height */
        c.transform.Translate(new Vector3(0, 0, c.transform.localScale.y / 2));
        UpdatePosition();
    }
    public void Spawn()
    {
        var radians = 2 * Mathf.PI / (circlesg.Count+1) * circlesg.Count + 1;

        /* Get the vector direction */
        var vertical = Mathf.Sin(radians);
        var horizontal = Mathf.Cos(radians);

        var spawnDir = new Vector3(horizontal, vertical, 0);

        /* Get the spawn position */
        var spawnPos = center.position + spawnDir * radius; // Radius is just the distance away from the point

        /* Now spawn */
        var c = Instantiate(circle, spawnPos, Quaternion.identity) as GameObject;

        circlesg.Add(c);
        /* Adjust height */
        c.transform.Translate(new Vector3(0, 0, c.transform.localScale.y / 2));
        UpdatePosition();
    }

    public void GetIndex(Vector3 dir)
    {

        var radians = 2 * Mathf.PI / (circlesg.Count + 1) * circlesg.Count + 1;
        /* Get the vector direction */
        var vertical = Mathf.Asin(dir.y);
        var horizontal = Mathf.Acos(dir.x);
        var spawnDir = new Vector3(horizontal, vertical, 0);


    }

    public void SpawnCircles(int num )
    {
      
     for (int i = 0; i < num; i++){
         
         /* Distance around the circle */  
         var radians = 2 * Mathf.PI / num * i;
         
         /* Get the vector direction */ 
         var vertical = Mathf.Sin(radians);
         var horizontal = Mathf.Cos(radians); 
         
         var spawnDir = new Vector3 (horizontal, vertical,0);
         
         /* Get the spawn position */ 
         var spawnPos = center.position + spawnDir * radius; // Radius is just the distance away from the point
         /* Now spawn */
         var c = Instantiate (circle, spawnPos, Quaternion.identity) as GameObject;
            circlesg.Add(c);
            c.name = "" + circlesg.Count;
            c.GetComponent<Circle>().angle = Mathf.Atan2(c.transform.position.y - center.transform.position.y, c.transform.position.x - center.transform.position.x) * 180 / Mathf.PI;
            /* Adjust height */
            c.transform.Translate (new Vector3 (0, 0, c.transform.localScale.y / 2));
     
        }   
    }
    public void UpdatePosition()
    {

        for (int i = 0; i < circlesg.Count; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / circlesg.Count * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, vertical, 0);

            /* Get the spawn position */
            var spawnPos = center.position + spawnDir * radius; // Radius is just the distance away from the point

            circlesg[i].transform.position = spawnPos;
            circlesg[i].GetComponent<Circle>().angle = Mathf.Atan2(circlesg[i].transform.position.y - center.transform.position.y, circlesg[i].transform.position.x - center.transform.position.x) * 180 / Mathf.PI;
        }
    }

    #region Get Set Add

    public void AddCircleAt(Circle c, int i)
    {
        circles.Insert(i, c);
        if(c.GetCircleType() == CircleType.Powerup)
        {

        }
        //Here also add circle
        //Here also update other circle positions
        //Here also check if the circle is a powerup and needs to merge other circles
    }
    public void AddCircle(Circle c)
    {
        circles.Add(c);
    }

    public void RemoveCircle(Circle c)
    {
        circles.Remove(c);
    }

    public List<Circle> GetCircles()
    {
        return circles;
    }

    public Circle GetCircleAt(int index)
    {
        return circles[index];
    }
    #endregion


    public void PushCircle(Circle c)
    {

    }
}
