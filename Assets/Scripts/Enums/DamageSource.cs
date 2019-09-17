using System;

[System.Flags]
public enum DamageSource {
    Sword = 1 << 0,
    Bow = 1 << 1,
    Fire = 1 << 2,
    Water = 1 << 3,
    Wood = 1 << 4,
    Metal = 1 << 5,
    Earth = 1 << 6,
    Flail = 1 << 7,
    DirectPhysical = 1 << 8,
    Potion = 1 << 9,
    Dagger = 1 << 10,
    Artifact = 1 << 11,
    AreaOfEffect = 1 << 12,
    Explosion = 1 << 13,
    Hazard = 1 << 14,
    Magic = Fire | Water | Wood | Metal | Earth
}