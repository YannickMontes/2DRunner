using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleTilePlateformPooler : AbstractObjectPooler {

    public static MiddleTilePlateformPooler current;

    public void Awake()
    {
        MiddleTilePlateformPooler.current = this;
    }
}
