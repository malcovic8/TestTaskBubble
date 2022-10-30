using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using System.Linq;

public class BubbleMatrixController : MonoBehaviour
{
    private const int SIZE_MIN_CLUSTRER = 2;
    private const int SIZE_MAX_CLUSTRER = 4;

    [SerializeField] 
    private Transform[] line = default;
    
    [SerializeField] 
    private Transform matrixView = default;
    
    [SerializeField] 
    private BubbleView prefabBubble = default;

    [SerializeField]
    private BubbleBox prefabBubbleBox = default;
    
    [SerializeField] 
    private int countRow = default;

    [SerializeField] 
    private int countColumn = default;
    
    private List<List<BubbleBox>> matrix = new List<List<BubbleBox>>();

    public List<List<BubbleBox>> Matrix
    {
        get => matrix;
        private set
        {
            matrix = value;
        }
    }
    
    public void AddBubble(BubbleView bubbleView)
    {
        int minRow = 0;
        int minColumn = 0;

        float minDistation =
            Vector2.Distance(bubbleView.transform.position, matrix[minRow][minColumn].transform.position);

        float distation = 0f;

        for (int row = 0; row < matrix.Count; row++)
        {
            for (int column = 0; column < matrix[row].Count; column++)
            {
                distation = Vector2.Distance(bubbleView.transform.position, matrix[row][column].transform.position);
                if (distation < minDistation)
                {
                    minDistation = distation;
                    minRow = row;
                    minColumn = column;
                }
            }
        }

        matrix[minRow][minColumn].InitBubble(bubbleView);
        matrix[minRow][minColumn].Bubble.transform.SetParent(matrix[minRow][minColumn].transform);
        matrix[minRow][minColumn].Bubble.transform.localPosition = Vector3.zero;
        CheckNeighbors(minRow, minColumn);
    }

    public Color[] RemainingСolors()
    {
        List<Color> remainingColor = new List<Color>();
        for (int index = 0; index < matrix.Count; index++)
        {
            remainingColor.AddRange(matrix[index].Where(bubbleBox => bubbleBox.Bubble != null).Select(bubbleBox => bubbleBox.Bubble.ColorBubble).ToList());
        }
        return remainingColor.Distinct().ToArray();
    }

    private void Awake()
    {
        GenerateMatrix();
    }
    
    private void GenerateMatrix()
    {
        GenerateMatrixBubble();
        AddEmptyLine();
    }

    private void GenerateMatrixBubble()
    {
        int clusterDivisionSize = countColumn % SIZE_MAX_CLUSTRER;
        Color generateColor = default;
        for (int row = 0; row < countRow; row++)
        {
            matrix.Add(new List<BubbleBox>(countColumn));

            int clusterSize = Random.Range(SIZE_MIN_CLUSTRER, SIZE_MAX_CLUSTRER);

            int column = 0;
            while (column < countColumn - clusterDivisionSize)
            {
                generateColor = ColorHelper.ColorEnum[Random.Range(0, ColorHelper.ColorEnum.Count)];
                for (int clusterIndex = 0; clusterIndex < clusterSize; clusterIndex++)
                {
                    CreateBubble(generateColor, row);
                }
                column += clusterSize;
            }
        }
    }

    
    private void CreateBubble(Color generateColor, int row)
    {
        BubbleBox bubbleBox = Instantiate<BubbleBox>(prefabBubbleBox, Vector3.zero, Quaternion.identity, line[row]);
        BubbleView bubble = Instantiate<BubbleView>(prefabBubble, Vector3.zero, Quaternion.identity, bubbleBox.transform);
        bubbleBox.InitBubble(bubble);
        bubble.Init(generateColor);
        matrix[row].Add(bubbleBox);
    }

    private void AddEmptyLine()
    {
        for (int row = countRow; row < matrixView.childCount; row++)
        {
            matrix.Add(new List<BubbleBox>(countColumn));
            for (int column = 0; column < countColumn; column++)
            {
                BubbleBox bubbleBox = Instantiate<BubbleBox>(prefabBubbleBox, Vector3.zero, Quaternion.identity, line[row]);
                matrix[row].Add(bubbleBox);
            }
        }
    }

    private void CheckNeighbors(int rowIndex, int columnIndex)
    {
        List<(int, int)> checkedNeighbors = new List<(int, int)>();

        int rowMatrixMin = default;
        int rowMatrixMax = default;
        int columnMatrixMin = default;
        int columnMatrixMax = default;

        CheckRowNeighbors(rowIndex, out rowMatrixMin, out rowMatrixMax);
        CheckColumnNeighbors(columnIndex, rowIndex, out columnMatrixMin, out columnMatrixMax);
        

        for (int row = rowMatrixMin; row < rowMatrixMax; row++)
        for (int column = columnMatrixMin; column < columnMatrixMax; column++)
        {
            if (!(row == rowIndex && column == columnIndex) 
                && matrix[row][column].Bubble != null 
                && matrix[row][column].Bubble.ColorBubble == matrix[rowIndex][columnIndex].Bubble.ColorBubble)
            {
                checkedNeighbors.Add((row, column));
            }
        }

        if (checkedNeighbors.Count != 0)
        {
            checkedNeighbors.Add((rowIndex, columnIndex));
        }
        
        foreach (var rowColumnBubbleBox in checkedNeighbors)
        {
            Destroy(matrix[rowColumnBubbleBox.Item1][rowColumnBubbleBox.Item2].Bubble.gameObject);
            matrix[rowColumnBubbleBox.Item1][rowColumnBubbleBox.Item2].InitBubble(null);
        }
    }

    private void CheckRowNeighbors(int rowIndex, out int rowMatrixMin, out int rowMatrixMax)
    {
        if (rowIndex == 0)
        {
            rowMatrixMin = 0;
            rowMatrixMax = 1;
        }
        else if(rowIndex == matrix.Count - 1)
        {
            rowMatrixMin = matrix.Count - 1;
            rowMatrixMax = matrix.Count;
        }

        else
        {
            rowMatrixMin = rowIndex - 1;
            rowMatrixMax = rowIndex + 1;
        }
    }

    private void CheckColumnNeighbors(int columnIndex, int rowIndex, out int columnMatrixMin, out int columnMatrixMax)
    {
        if (columnIndex == 0)
        {
            columnMatrixMin = 0;
            columnMatrixMax = 1;
        }
        else if (columnIndex == matrix[rowIndex].Count - 1)
        {
            columnMatrixMin = matrix[rowIndex].Count - 1;
            columnMatrixMax = matrix[rowIndex].Count;
        }
        else
        {
            columnMatrixMin = columnIndex - 1;
            columnMatrixMax = columnIndex + 1;
        }
    }
}
