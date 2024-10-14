using System;

namespace learn_csharp;

public class SuffixTree
{
    private readonly string _text;
    private readonly Node _root;

    public SuffixTree(string text)
    {
        _text = text + "$"; // '$'를 추가하여 문자열의 끝을 표시합니다.
        _root = new Node(-1, -1); // 루트 노드는 명시적인 경계가 없습니다.
        BuildSuffixTree();
    }

    private void BuildSuffixTree()
    {
        for (int i = 0; i < _text.Length; i++)
        {
            InsertSuffix(i);
        }
    }

    private void InsertSuffix(int startIndex)
    {
        Node currentNode = _root;
        int suffixIndex = startIndex;

        while (suffixIndex < _text.Length)
        {
            char currentChar = _text[suffixIndex];

            if (currentNode.Children.ContainsKey(currentChar))
            {
                Node nextNode = currentNode.Children[currentChar];
                int edgeLength = nextNode.Length;

                int j = 0;
                while (j < edgeLength && _text[nextNode.Start + j] == _text[suffixIndex])
                {
                    j++;
                    suffixIndex++;
                }

                if (j == edgeLength)
                {
                    currentNode = nextNode;
                }
                else
                {
                    Node splitNode = new Node(nextNode.Start, nextNode.Start + j - 1);
                    currentNode.Children[currentChar] = splitNode;

                    splitNode.Children[_text[suffixIndex]] = new Node(suffixIndex, _text.Length - 1);
                    nextNode.Start += j;
                    splitNode.Children[_text[nextNode.Start]] = nextNode;

                    return;
                }
            }
            else
            {
                currentNode.Children[currentChar] = new Node(suffixIndex, _text.Length - 1);
                return;
            }
        }
    }

    public void PrintTree()
    {
        PrintNode(_root, 0);
    }

    private void PrintNode(Node node, int level)
    {
        foreach (var child in node.Children)
        {
            Node childNode = child.Value;
            string edgeLabel = _text.Substring(childNode.Start, childNode.Length);
            Console.WriteLine(new string(' ', level * 2) + edgeLabel);
            PrintNode(childNode, level + 1);
        }
    }
}