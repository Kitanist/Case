using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GridManager : MonoSingeleton<GridManager>
{
    public int width, height;
    public GameObject pathPrefab;
    public GameObject partPrefab;
    public GameObject holePrefab;
    public GridLevelData[] gridLevelDatas;
    public GridLevelData currentLevelData;
    public bool isSelected;
    public Cell selectedCell,targetCell;
    public Cell[,] grid;
    public GameEvent FinishGame;
    private int _currentLevel;
    public int clickamount = 0,winclickamount = 3;

    public int CurrentLevel 
    {  
        get {  return _currentLevel; }
        set { _currentLevel = value; PlayerPrefs.SetInt("CurrentLevel", _currentLevel); }
    }
    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        grid = new Cell[width+10, height+10];
        currentLevelData = gridLevelDatas[CurrentLevel];
        SetupGrid();
    }

    public void SetupGrid()
    {
        
            
            int extendedWidth = width + 10; 
            int extendedHeight = height + 10; 

            for (int x = -5; x < extendedWidth - 5; x++)
            {
                for (int y = -5; y < extendedHeight - 5; y++)
                {
                    Vector3 position = new Vector3(x, 0, y);

                    GameObject cellObj;

                    
                    if (x < 0 || x >= width || y < 0 || y >= height)
                    {
                        cellObj = Instantiate(holePrefab, position, Quaternion.identity);
                        grid[x+5, y+5] = cellObj.GetComponent<Cell>();
                        grid[x + 5, y + 5].Setup(2,x,y);

                }
                    else
                    {
                       
                        int cellData = currentLevelData.GetCellData(x, y, width);

                        if (cellData == 0)
                        {
                            cellObj = Instantiate(pathPrefab, position, Quaternion.identity);
                        grid[x + 10, y + 10] = cellObj.GetComponent<Cell>();
                        grid[x + 10, y + 10].Setup(cellData, x, y);

                    }
                        else if (cellData == 1)
                        {
                            cellObj = Instantiate(partPrefab, position, Quaternion.identity);
                        grid[x + 10, y + 10] = cellObj.GetComponent<Cell>();
                        grid[x + 10, y + 10].Setup(cellData, x, y);

                    }
                        else if (cellData == 2)
                        {
                            cellObj = Instantiate(holePrefab, position, Quaternion.identity);
                        grid[x + 10, y + 10] = cellObj.GetComponent<Cell>();
                        grid[x + 10, y + 10].Setup(cellData, x, y);

                    }
                        else
                        {
                            continue;
                        }
                    }
               
               



            }
            }
        

    }
    public void FindAndShowPath(Vector2Int start, Vector2Int end)
    {
        Pathfinding pathfinding = GetComponent<Pathfinding>();
        List<Vector2Int> path = pathfinding.FindPath(start, end);
        GameObject cellObj;
        if (path != null)
        {
            cellObj = Instantiate(pathPrefab, selectedCell.transform.position, Quaternion.identity);

            grid[selectedCell.GetComponent<Cell>().x+10, selectedCell.GetComponent<Cell>().y+10] = cellObj.GetComponent<Cell>();

            cellObj = Instantiate(pathPrefab, targetCell.transform.position, Quaternion.identity);

            grid[targetCell.GetComponent<Cell>().x+10, targetCell.GetComponent<Cell>().y + 10] = cellObj.GetComponent<Cell>();

            selectedCell.gameObject.SetActive(false);
            targetCell.gameObject.SetActive(false);
            isSelected = false;
            selectedCell = null;
            targetCell = null;
            
            foreach (Vector2Int position in path)
            {
                 
              
                // Bu hücrelerin üzerine renk deðiþecek unutma
                Debug.Log("Yol: " + position);
            }
            CheckFinish();

        }
        else
        {
            isSelected = false;
            selectedCell = null;
            targetCell = null;
            // görselleri güncelle
            Debug.Log("Yol bulunamadý.");
        }
          
    }
    public void CheckFinish()
    {
        bool hasParts = true;
        clickamount++;
        if (clickamount >= winclickamount)
        {
            hasParts = false;
        }
        
      //  for (int i = -5; i < width+5; i++)
      //  {
      //      for (int j = -5; j < height+5; j++)
      //      {
      //          // Hücrenin null olup olmadýðýný kontrol et
      //          if (grid[i+10, j+10] == null)
      //          {
      //            //  Debug.LogError($"Grid hücresi ({i}, {j}) null referans!");
      //              continue;
      //          }
      //
      //          // Hücrede bir part olup olmadýðýný kontrol et
      //          if (grid[i+10, j+10].isPart)
      //          {
      //              hasParts = true;
      //              Debug.Log($"Part hücresi bulundu: ({i}, {j})");
      //              break; // Bir part hücresi bulduðumuzda döngüyü bitir
      //          }
      //      }
      //
      //      // Eðer part hücresi bulunduysa, dýþ döngüyü bitir
      //      if (hasParts) break;
      //  }
      //
      //  // Eðer hiç part hücresi bulunmadýysa, oyunu bitir
        if (!hasParts)
        {
            Debug.Log("Hiç part kalmadý. FinishGame.Raise() çaðrýlýyor.");
            FinishGame.Raise();
        }
    }


    public void Nextlevel()
    {
        CurrentLevel++;
     
        SceneManager.LoadScene(0);

    }
   
}
