namespace Fitomad.OpenAI.Models.Audio;

public enum Speed
{
    Slow,
    Normal,
    X2,
    X3,
    X4
}

public static class SpeedExtension
{
    public static double GetValue(this Speed speed)
    {
        var speedName = speed switch
        {
            Speed.Slow => 0.50,
            Speed.Normal => 1.0,
            Speed.X2 => 2.0,
            Speed.X3 => 3.0,
            Speed.X4 => 4.0,
            _ => 1.0
        };

        return speedName;
    }
}