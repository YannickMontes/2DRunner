using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePooler : AbstractObjectPooler
{
    public static CratePooler current;

    public void Awake()
    {
        CratePooler.current = this;
    }
}
