using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    public int currentHealth;
    public int currentExp;
    public int currentLevel;
    public int currentPhysicalAttack;
    public int currentMagicalAttack;
    public int currentEvade;
    public int currentPhysicalDefense;
    public int currentMagicalDefense;
    public int characterID;
    public static int characterCount = 0;

    public override string ToString()
    {
        return "ID: " + characterID + " Current Health: " + currentHealth;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        CharacterData objAsPart = obj as CharacterData;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public override int GetHashCode()
    {
        return characterID;
    }
    public bool Equals(CharacterData other)
    {
        if (other == null) return false;
        else return (this.characterID.Equals(other.characterID));
    }
}
