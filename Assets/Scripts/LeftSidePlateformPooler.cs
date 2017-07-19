using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSidePlateformPooler : AbstractObjectPooler
{

    public static LeftSidePlateformPooler current;

    public void Awake()
    {
        LeftSidePlateformPooler.current = this;
    }
}
