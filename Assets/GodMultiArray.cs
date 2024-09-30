using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMultiArray : MonoBehaviour
{
    [SerializeField]
    private int myGridLength;

    [SerializeField]
    private int myGridHight;

    Vector2 myGridSize;

    [SerializeField]
    private GameObject myCell;

    [SerializeField]
    private GameObject[,] myCells;
    
    [SerializeField]
    private GameObject[][] myCellsJagged  = { new GameObject[56], new GameObject[57] };

    private void Start()
    {
        myGridSize = new Vector2(myGridLength, myGridHight);
        myCells = new GameObject[myGridLength, myGridHight];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < myCells.GetLength(0); i++)
            {
                for(int j = 0; j < myCells.Length; j++)
                {
                    GameObject newCell = Instantiate(myCell, new Vector3(j, i, 0), transform.rotation);
                    myCells[i, j] = newCell;
                    myCellsJagged[i][j] = newCell;
                }
            }
        }
    }

    private void GenerateGrid()
    {

    }
}
