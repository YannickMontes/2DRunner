using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSidePlateformPooler : AbstractObjectPooler {

    public static RightSidePlateformPooler current;

    public void Awake()
    {
        RightSidePlateformPooler.current = this;
    }
}
