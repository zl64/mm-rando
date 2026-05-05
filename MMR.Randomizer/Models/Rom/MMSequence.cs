namespace MMR.Randomizer.Models.Rom;

public class MMSequence
{
    public int Addr;
    public int Size => Data?.Length ?? 0;
    public byte[] Data;
}
