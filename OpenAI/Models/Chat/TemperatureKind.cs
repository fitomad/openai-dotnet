using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Fitomad.OpenAI.Models.Chat;

public enum TemperatureKind
{
    Precise,
    SomePrecise,
    Regular,
    SomeRandom,
    Random
}

public static class TemperatureKindExtension
{
    public static double GetValue(this TemperatureKind temperature)
    {
        var value = temperature switch
        {
            TemperatureKind.Precise => 0.0,
            TemperatureKind.SomePrecise => 0.5,
            TemperatureKind.Regular => 1.0,
            TemperatureKind.SomeRandom => 1.5,
            TemperatureKind.Random => 2.0,
            _ => 1.0
        };

        return value;
    }
}