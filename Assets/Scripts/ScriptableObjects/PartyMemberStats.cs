using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartyMember", menuName = "Party/Party Member")]
public class PartyMemberStats : ScriptableObject {
    public int vitality;
    public int wisdom;
    public int strength;
    public int defense;
    public int magic;
    public int resistance;
    public int speed;
    public int luck;
}
