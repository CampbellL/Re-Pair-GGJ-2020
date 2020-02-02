using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private float screenWidth = 2.7f;
    private float screenHeight = 2.7f;

    private static int rows = 8;
    private static int columns = 14;

    private float cellSizeW;
    private float cellSizeH;

    private float minCellSize;

    private float halfMinCellSize;

    private float widthFixing;
    private float heightFixing;

    private Camera camera;

    public int[,] playerArray; //int array

    public Dictionary<Vector2, int> openWith = //not string but BasicObject
        new Dictionary<Vector2, int>();

    public GameObject[] objectPrefabs;

    public GameObject spritePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        cellSizeW = screenWidth / columns;
        cellSizeH = screenHeight / rows;

        minCellSize = Mathf.Min(cellSizeW, cellSizeH);

        widthFixing = (screenWidth - minCellSize * columns) / 2.0f;
        heightFixing = (screenHeight - minCellSize * rows) / 2.0f;

        halfMinCellSize = minCellSize / 2.0f;

        camera = FindObjectOfType<Camera>();

        playerArray = new int[rows, columns];

        callEach();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPlayer(int player, int row, int column) //currently represented as int
    {
        playerArray[row, column] = player;
    }

    public void callEach()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (playerArray[i, j] != null)
                {
                    Vector2 viewportPosition = new Vector2(
                        (widthFixing + halfMinCellSize) + (j * minCellSize) - screenWidth / 2,
                        (heightFixing + halfMinCellSize) + (i * minCellSize) - screenHeight / 2);


                    openWith.Add(viewportPosition, 1);
                    /*
                    Vector3 p = camera.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, camera.nearClipPlane));
                    var gridCell = Instantiate(playerSprite, p, Quaternion.identity);
                    */
                    /*
                    Vector2 viewportPosition = new Vector2((widthFixing+halfMinCellSize) + (j * minCellSize) - screenWidth/2,
                        (heightFixing+halfMinCellSize) + (i * minCellSize) - screenHeight/2);
                    
                    
                    Vector3 p = camera.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, camera.nearClipPlane));
    */
                    
                    
                    var gridCell = Instantiate(spritePrefab, viewportPosition, Quaternion.identity);
                    gridCell.transform.parent = gameObject.transform;
                    
                }
            }
        }
    }
/*
    public void InitializeObject(Dictionary<Vector2, int> helpWith){}
    {
        
    }
    */

    public void InitializeObject(Vector2 position, int type) //0 = horizontal, 1 = slopeDown, 2 = vertical, 3 = slopeUp, 4 = slowAnimal, 5 = normalAnimal, 6 = fastAnimal
    {
        var gridCellSprite = Instantiate(objectPrefabs[type], position, Quaternion.identity); //THIS IS OF TYPE VAAAARRRRRR
        gridCellSprite.transform.parent = gameObject.transform;
    }


}
