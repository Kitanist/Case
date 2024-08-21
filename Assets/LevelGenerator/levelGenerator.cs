using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class levelGenerator : MonoSingeleton<levelGenerator>
{
    public int width, height;
    public GameObject pathPrefab;
    public GameObject partPrefab;
    public GameObject holePrefab;
    public GridLevelData SettedLevelData;
    [SerializeField] public GenCell[,] cell;
    int ResetData = 0 ;
    public static int  LevelMakerInt;
    
    private void Start()
    {
        Array.Resize(ref SettedLevelData.cells, width * height);
        cell = new GenCell[width, height];

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GameObject cellObj;
                Vector3 position = new Vector3(i, 0, j);
                cellObj = Instantiate(pathPrefab, position, Quaternion.identity);
                cell[i,j] = cellObj.GetComponent<GenCell>();
                cellObj.GetComponent<GenCell>().Setup(i,j);
                SettedLevelData.SetCellData(i, j, width, ResetData);
            }
        }

    }
   
    public void saveLevel()
    {
        
           for (int i = 0; i < width; i++)
           {
               for (int j = 0; j < height; j++)
               {
                   GenCell currentCell = cell[i, j];
              
          
                   int cellType = 0; // Varsayýlan 

                if (currentCell.isPath)
                {
                    cellType = 0; // Path 
                    Debug.Log("göt");
                }
                else if (currentCell.isPart)
                {
                    cellType = 1; // Part 
                }
                else if (currentCell.isHole)
                {
                    cellType = 2; // Hole 
                }
                else {  }
          
                  
                   SettedLevelData.SetCellData(i, j, width, cellType);
               }
           }

            
            EditorUtility.SetDirty(SettedLevelData);
            
       }
    void OnApplicationQuit()
    {
        saveLevel(); 
        Debug.Log("Oyun kapanmadan önce level kaydedildi.");
    }


}
