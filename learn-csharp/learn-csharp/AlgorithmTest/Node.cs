using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace learn_csharp;

public class Node
{
    public int Start { get; set; }
    public int End { get; set; }
    public Dictionary<char, Node> Children { get; set; } = new Dictionary<char, Node>();
    public bool IsLeaf { get; set; } = false;
    public Node SuffixLink { get; set; } = null;

    public int Length => End - Start + 1;

    public Node(int start, int end)
    {
        Start = start;
        End = end;
    }
    
    // ----------------------------------------------------------------------
    // 아래의 코드가 원래 문제에서 제시되는 코드
    
    // // 아래 두 필드는 이 노드의 Label 이 S.Substring(startIndex: Start, length: End - Start + 1) 임을 의미합니다.
    // // End == S.Length - 1 인 경우 Leaf 노드입니다.
    // // 단, (Label 이 존재하지 않는) Root 노드의 경우 둘 다 -1 이라고 가정합니다.
    // public int Start { get; set; }
    // public int End { get; set; } 
    //
    // // 아래 Children 은 각 자식 노드의 Label 의 첫번째 문자를 key로, 해당 자식 노드를 Value 로 갖습니다. 
    // // 단, Leaf 노드의 경우 빈 Map 이라고 가정합니다.
    // public Dictionary<char, Node> Children { get; set; }
    //
    // public bool IsLeaf { get; set; } = false;
    //
    // public int Length => End - Start + 1;
}

public class SuffixTreeHelper
{
    // S 에 대한 SuffixTree 를 구축해서, 그 Root Node를 리턴해줍니다.
    public static Node ConstructSuffixTree(string S)
    {
        return new Node(-1, -1);
    }
    
    // ConstructSuffixTree() 함수를 활용해 패턴을 검색하는 예시코드
    int FindPattern(string S, string pattern)
    {
        S = S + "$"; // '$(=0)' 는 악보의 끝을 의미합니다.
        Node rootNode = ConstructSuffixTree(S);
    
        Node curNode = rootNode;
        for (int i = 0; i < pattern.Length;)
        {
            if (!curNode.Children.ContainsKey(pattern[i]))
                return -1;
            
            curNode = curNode.Children[pattern[i]];

            int compareLen = Math.Min(curNode.End - curNode.Start + 1, pattern.Length - i);

            if (S.Substring(curNode.Start, compareLen) != pattern.Substring(i, compareLen))
                return -1;

            if (pattern.Length == i + compareLen)
                return curNode.Start - i;
            else
                i += compareLen;

        } 
        return -1;
    }
}


