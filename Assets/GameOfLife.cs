using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameOfLife : MonoBehaviour
{
    [Header("Play mode")]
    [SerializeField]
    public bool SpawnFillProcent;

    [SerializeField]
    public bool DrawYourStart;

    [Space]
    [Header("Simluation Settings")]
    [SerializeField]
    private int myTargetFramrate;

    [SerializeField]
    private int myGridLength;

    [SerializeField]
    private int myFillProcent;

    [SerializeField]
    private Cell[] myCells;

    [Space]
    [Header("Set before start")]
    [SerializeField]
    private GameObject myCameraObject;
    [SerializeField]
    private Material myDeadCellMaterial;
    [SerializeField]
    private Material myLivingCellMaterial;
    [SerializeField]
    private Material myJustDiedCell;
    [SerializeField]
    private GameObject myCell;

    private bool myGridIsGenerated;

    [SerializeField]
    private bool isPlaying;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateGrid();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressPlay();
        }

        if (isPlaying)
        {
            UpdateLife();
        }
            
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePs = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (myGridIsGenerated)
            {
                if(Mathf.RoundToInt(mousePs.x) + Mathf.RoundToInt(mousePs.y) * myGridLength < myGridLength * myGridLength && Mathf.RoundToInt(mousePs.x) + Mathf.RoundToInt(mousePs.y) * myGridLength > 0)
                { 
                    Cell cell = myCells[Mathf.RoundToInt(mousePs.x) + Mathf.RoundToInt(mousePs.y) * myGridLength];
                    cell.myIsAlive = true;
                    cell.GetComponent<Renderer>().material = myLivingCellMaterial;
                }
            }
        }
        if(isPlaying)
        {
            Application.targetFrameRate = myTargetFramrate;
        }
        else
        {
            Application.targetFrameRate = 120;
        }
    }

    public void GenerateGrid()
    {

        if (!myGridIsGenerated)
        {
            myCells = new Cell[myGridLength * myGridLength];
            myCameraObject.transform.position = new Vector3(myGridLength / 2, myGridLength / 2, -myGridLength);
            Camera.main.orthographicSize = myGridLength;

            for (int i = 0; i < myGridLength; i++)
            {
                for (int j = 0; j < myGridLength; j++)
                {
                    GameObject newCell = Instantiate(myCell, new Vector3(i, j, 0), Quaternion.identity);
                    myCells[i + j * myGridLength] = newCell.GetComponent<Cell>();
                    myGridIsGenerated = true;
                    if (SpawnFillProcent)
                    {
                        if (Random.Range(0, 100) < myFillProcent)
                        {
                            myCells[i + j * myGridLength].myIsAlive = true;
                            myCells[i + j * myGridLength].GetComponent<Renderer>().material = myLivingCellMaterial;
                        }
                        else
                        {
                            myCells[i + j * myGridLength].myIsAlive = false;
                            myCells[i + j * myGridLength].GetComponent<Renderer>().material = myDeadCellMaterial;
                        }
                    }
                    else
                    {
                        myCells[i + j * myGridLength].myIsAlive = false;
                        myCells[i + j * myGridLength].GetComponent<Renderer>().material = myDeadCellMaterial;
                    }
                }
            }
        }
        else
        {
            foreach (Cell cell in myCells)
            {
                Destroy(cell.gameObject);
            }
            myGridIsGenerated = false;
        }


    }

    private void UpdateLife()
    {
        if (myGridIsGenerated)
        {
            for (int i = 0; i < myGridLength; i++)
            {
                for (int j = 0; j < myGridLength; j++)
                {
                    int count = 0;
                    //Edge case bottom left and left edge
                    if (i + j * myGridLength - 1 >= 0 && (i + j * myGridLength - 1) - (j * myGridLength) >= 0)
                    {
                        if (myCells[i + j * myGridLength - 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    //Edge case top right and right edge
                    if (i + j * myGridLength + 1 < myGridLength * myGridLength && (i + j * myGridLength + 1) - ((1 + j) * myGridLength) < 0)
                    {
                        if (myCells[i + j * myGridLength + 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    //under
                    if (i + j * myGridLength - myGridLength > 0)
                    {
                        if (myCells[i + j * myGridLength - myGridLength].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    //Above
                    if (i + j * myGridLength + myGridLength < myGridLength * myGridLength - 1)
                    {

                        if (myCells[i + j * myGridLength + myGridLength].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    //EdgeCase Left
                    
                    //EdgeCase Right
                    //Check Diagonal
                    if (i + j * myGridLength - myGridLength - 1 > 0)
                    {
                        if (myCells[i + j * myGridLength - myGridLength - 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    if (i + j * myGridLength - myGridLength + 1 > 0)
                    {
                        if (myCells[i + j * myGridLength - myGridLength + 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    if (i + j * myGridLength + myGridLength - 1 < myGridLength * myGridLength - 1)
                    {

                        if (myCells[i + j * myGridLength + myGridLength - 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }
                    if (i + j * myGridLength + myGridLength + 1 < myGridLength * myGridLength - 1)
                    {

                        if (myCells[i + j * myGridLength + myGridLength + 1].myIsAlive)
                        {
                            count += 1;
                        }
                    }

                    myCells[i + j * myGridLength].myAliveNeighbors = count;
                }
            }
            for (int i = 0; i < myGridLength; i++)
            {
                for (int j = 0; j < myGridLength; j++)
                {
                    Cell cell = myCells[i + j * myGridLength];
                    int count = cell.myAliveNeighbors;
                    if (myCells[i + j * myGridLength].myIsAlive)
                    {
                        if (count < 2 || count > 3)
                        {
                            cell.myIsAlive = false;
                            cell.GetComponent<Renderer>().material = myJustDiedCell;
                        }
                        else if (count == 2 || count == 3)
                        {
                            cell.gameObject.SetActive(true);
                            cell.myIsAlive = true;
                            cell.GetComponent<Renderer>().material = myLivingCellMaterial;
                        }
                    }
                    else if (count == 3)
                    {
                        cell.myIsAlive = true;
                        cell.GetComponent<Renderer>().material = myLivingCellMaterial;
                    }
                    else
                    {
                        cell.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    
    public void PlayMode(GameObject aCheckBox)
    {
        if(SpawnFillProcent)
        {
            aCheckBox.SetActive(true);
            SpawnFillProcent = false;
            DrawYourStart = true;
        }
        else
        {
            aCheckBox.SetActive(false);
            SpawnFillProcent = true;
            DrawYourStart = false;
            
        }
    }

    public void PressPlay()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            myCameraObject.GetComponent<Camera>().backgroundColor = new Color(0.3843137f, 0f, 0.1389233f, 0f); 
        }
        else
        {
            myCameraObject.GetComponent<Camera>().backgroundColor = new Color(0f, 0.0627451f, 0.3843137f, 0f);
        }
    }

   
}
