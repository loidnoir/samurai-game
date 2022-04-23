using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomType
{
    Constant,
    RandomBetweenTwoConstants
}

[Serializable]
public class RandomFloat
{
    public RandomType type;
    public float constantValue;
}
