public static class GameMath
{
    public static float normalize(this float value, float max, bool reverse = false)
    {
        return reverse ? 1 - value / max : value / max;
    }
}