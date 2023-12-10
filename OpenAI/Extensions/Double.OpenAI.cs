using System;

namespace OpenAI.Extensions;

public static class DoubleOpenAIExtension
{
    private const double MinValue = -2.0;
    private const double MaxValue = 2.0;

    public static bool IsOutOfOpenAIRange(this double value) 
    {
        if((value < MinValue) || (value > MaxValue))
        {
            return true;
        }

        return false;
    }
}