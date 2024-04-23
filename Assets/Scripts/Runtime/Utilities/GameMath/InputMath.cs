namespace Akashic.Runtime.Utilities.GameMath
{
    internal static class InputMath
    {
        public static int GetAxisValueAsInt(float value)
        {
            if (value == 0f)
            {
                return 0;
            }
            
            return value < 0 ? -1 : 1;
        }
    }
}