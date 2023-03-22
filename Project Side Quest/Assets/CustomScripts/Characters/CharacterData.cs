using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Character/New Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    string CharacterName;

    [SerializeField]
    EClassType CharacterClass;

    [SerializeField]
    float BaseHealth;

    [SerializeField]
    float BaseDamage;
    
    [SerializeField] 
    float BaseSpeed;
}

enum EClassType
{
    None,
    Fighter,
    Ranger,
    Rogue,
    Cleric,
    Wizard
};