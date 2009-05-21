public class Bowling
{
    public int Score { get; set; }
    public void Hit(int pins)
    {
        Score += pins;
    }
}

