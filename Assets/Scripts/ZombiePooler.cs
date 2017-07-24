using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePooler : AbstractObjectPooler {

    public static ZombiePooler current;

    public void Awake()
    {
        ZombiePooler.current = this;
    }
}
