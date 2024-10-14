using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace learn_csharp;

public class FindLongestPartialStringTest
{
    public static bool IsComboExist(char[] s, int windowSize)
    {
        Dictionary<int, List<int>> hashToStartPosMap = new();
	
        RollingHashWindow rollingHash = new(s, windowSize);
        do
        {
            int hash = rollingHash.GetHashState();
            int curWindowStartPos = rollingHash.GetCurrentPosition();
            if (!hashToStartPosMap.TryGetValue(hash, out var posList))
            {
                hashToStartPosMap.Add(hash, new() { curWindowStartPos });
            }
            else
            {
                ReadOnlySpan<char> thisSpan = new(s, curWindowStartPos, windowSize);
 
                foreach (int pos in posList)
                {
                    ReadOnlySpan<char> otherSpan = new(s, pos, windowSize);
                    if (thisSpan.SequenceEqual(otherSpan))
                    {
                        return true;
                    }
                }
 
                posList.Add(curWindowStartPos);
            }			   
        } while (rollingHash.RollForward());

        return false;
    }

    public static (string maxComboStr, int D) Solve(string text, Node Root)
    {
        int maxLength = Int32.MinValue;
        Node maxLengthNode = null;
        int resultStart = -1;
        foreach (var childNode in Root.Children.Values)
        {
            FindMaxLengthNode(childNode, 0);
        }

        void FindMaxLengthNode(Node node, int currLength)
        {
            if (node.IsLeaf) return;

            currLength += node.Length;
		
            if (node.Children.Count >= 1 && maxLength < currLength)
            {
                maxLength = currLength;
                maxLengthNode = node;
                resultStart = node.End - currLength;
            }
		
            foreach (var child in node.Children.Values)
            {
                FindMaxLengthNode(child, currLength);
            }
        }

        int FindDelay(Node node)
        {
            Debug.Assert(!node.IsLeaf);
            Debug.Assert(node.Children != null);
            Debug.Assert(node.Children.Count >= 2);

            var childArr = node.Children.Values.ToArray();
            return Math.Abs(childArr[0].Start - childArr[1].Start);
        }
	
	
        return (text.Substring(resultStart, maxLength), FindDelay(maxLengthNode));
    }

}