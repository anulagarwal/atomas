using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int index;
    [SerializeField] CircleType type;
    [SerializeField] public float angle;
    [SerializeField] public int value;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    public int GetIndex()
    {
        return index;
    }

    public CircleType GetCircleType()
    {
        return type;
    }



    public void UpdateState(CircleType ct)
    {
        switch (ct)
        {
            case CircleType.Normal:

                break;

            case CircleType.Pizza:

                break;
        }
    }
}
