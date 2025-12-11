using System.Collections;

public abstract class GameplayScript
{
    public Main Game { get; set; }
    public abstract IEnumerator Script();
}