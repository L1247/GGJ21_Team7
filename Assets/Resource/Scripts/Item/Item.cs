using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
}
public enum ItemType
{
    LeftKey,
    Jump,
    Climb,
    Animation,
    Audio,
    BackgroundColor,
    Light,
    Restart,
    Teleport,
    Icon
}
