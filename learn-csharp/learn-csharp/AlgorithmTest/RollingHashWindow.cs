using System;

namespace learn_csharp;

public class RollingHashWindow
{
    public RollingHashWindow(char[] S, int windowSize)
    {
        this.S = S;
        this.windowSize = windowSize;
        pow = 1;

        currentPosition = 0;
		
        // 초기 window 영역의 해시값 계산
        hash = S[windowSize - 1];
        for (int i = 1; i < windowSize; ++i)
        {
            pow = (pow * p) % m;
            hash = (hash + S[windowSize - i - 1] * pow) % m;
        }
    }

    public bool RollForward()
    {
        if (currentPosition >= S.Length - windowSize) return false;

        currentPosition += 1;
		
        hash = (hash + S[currentPosition - 1] * (m - pow)) % m;
        hash = (hash * p + S[currentPosition + windowSize - 1]) % m;
		
        return true;
    }

    public int GetHashState() => hash;
    public int GetCurrentPosition() => currentPosition;

    public ReadOnlySpan<char> GetString() => new (S, currentPosition, windowSize);

    const int p = 50;
    const int m = 19260817;

	
    private int hash;
    private int currentPosition;
    private readonly int pow;
    private readonly int windowSize;
    private readonly char[] S;
}
