using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BLOCKTYPE
{
    GROUND,
    OBJECT,
    CHARACTER,
    WALL
};
public enum DIRECTION
{
    UP,
    LEFT,
    DOWN,
    RIGHT,
    STAY
};

public enum COLOR
{
    RED,
    BLUE,
    PURPLE
};

public enum MONSTER
{
    MOUSE,
    HYENA,
    BEAR,
    RACOON
}

public enum SIGHTTYPE
{
    NEVERSEEN,
    ONCESAW,
    NOWSEEING
}

public enum ITEM
{
    POTION
}