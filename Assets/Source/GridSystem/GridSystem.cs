using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public string levelName;
    private const float ScreenWidth = 2.7f;
    private const float ScreenHeight = 2.7f;

    private const int Rows = 8;
    private const int Columns = 14;

    private float _cellSizeW;
    private float _cellSizeH;

    private float _minCellSize;

    private float _halfMinCellSize;

    private float _widthFixing;
    private float _heightFixing;


    public int[,] playerArray; //int array

    public GameObject[] objectPrefabs;

    // Start is called before the first frame update
    private void Start()
    {
        playerArray = new int[Rows,Columns];
        var i = 0;
        using(StreamReader reader = new StreamReader(GenerateStreamFromString(Resources.Load<TextAsset>(levelName).text)))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                for (var index = 0; index < values.Length; index++)
                {
                    var value = values[index];
                    playerArray[i,index] = Convert.ToInt32(value);
                }
                i++;
            }
        }
        
        
        _cellSizeW = ScreenWidth / Columns;
        _cellSizeH = ScreenHeight / Rows;

        _minCellSize = Mathf.Min(_cellSizeW, _cellSizeH);

        _widthFixing = (ScreenWidth - _minCellSize * Columns) / 2.0f;
        _heightFixing = (ScreenHeight - _minCellSize * Rows) / 2.0f;

        _halfMinCellSize = _minCellSize / 2.0f;

        CallEach();
    }

    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    
    public void AddPlayer(int player, int row, int column) //currently represented as int
    {
        playerArray[row, column] = player;
    }

    private void CallEach()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                if (playerArray[i, j] == 0) continue;
                Vector2 viewportPosition = new Vector2(
                    (_widthFixing + _halfMinCellSize) + (j * _minCellSize) - ScreenWidth / 2,
                    (_heightFixing + _halfMinCellSize) + (i * _minCellSize) - ScreenHeight / 2);
                GameObject gridCell =
                    Instantiate(objectPrefabs[playerArray[i, j]], viewportPosition, Quaternion.identity);
                gridCell.transform.parent = gameObject.transform;
            }
        }
    }

    public void
        InitializeObject(Vector2 position,
            int type) 
        //1 = horizontal, 2 = slopeDown, 3 = vertical, 4 = slopeUp, 5 = slowAnimal, 6 = normalAnimal, 7 = fastAnimal
    {
        GameObject gridCellSprite =
            Instantiate(objectPrefabs[type], position, Quaternion.identity); //THIS IS OF TYPE Gameoooobjjeeccctttt
        gridCellSprite.transform.parent = gameObject.transform;
    }
}