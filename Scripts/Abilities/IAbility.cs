﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    float uses { get; }
    IEnumerator Use();
}
