public static class GameMath
{
    public static float Normalize(this float value, float max, bool reverse = false)
    {
        return reverse ? 1 - value / max : value / max;
    }
}