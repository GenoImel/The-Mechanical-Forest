namespace Akashic.Runtime.Character
{
    [System.Serializable]
    internal sealed class CharacterData 
    {
        public int CurrentHealth;
        public int CurrentExp;
        public int CurrentLevel;
        public int CurrentPhysicalAttack;
        public int CurrentMagicalAttack;
        public int CurrentEvade;
        public int CurrentPhysicalDefense;
        public int CurrentMagicalDefense;
        public int CharacterID;
        public static int CharacterCount = 0;

        public override string ToString()
        {
            return "ID: " + CharacterID + " Current Health: " + CurrentHealth;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            var objAsPart = obj as CharacterData;
            
            if (objAsPart == null)
            {
                return false;
            }
            
            return Equals(objAsPart);
        }
        
        public override int GetHashCode()
        {
            return CharacterID;
        }
        
        public bool Equals(CharacterData other)
        {
            if (other == null)
            {
                return false;
            }
            
            return (CharacterID.Equals(other.CharacterID));
        }
    }
}
