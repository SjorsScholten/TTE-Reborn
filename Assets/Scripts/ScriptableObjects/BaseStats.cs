using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseStats", menuName = "Stats/Base Stats")]
public class BaseStats : ScriptableObject {
    public int vitality;
    public int wisdom;
    public int strength;
    public int defense;
    public int magic;
    public int resistance;
    public int speed;
    public int luck;
}
