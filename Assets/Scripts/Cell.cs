using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isPath;
    public bool isPart;
    public bool isHole;
    
    public int x, y;
    Vector2Int pathData ;

    private bool isSelected = false;

    public void Setup(int cellData, int a, int b)
    {
        isPath = (cellData == 0);
        isPart = (cellData == 1);
        isHole = (cellData == 2);
        
        x = a;
        y = b;
        pathData = new Vector2Int(x, y);
    }


    void OnMouseDown()
    {
        Debug.Log($"T�klanan h�cre x : {x} ile y : {y}koordinatlar�nda yer al�yor.");
        if (isPart&& !GridManager.Instance.isSelected)
        {
            GridManager.Instance.isSelected = true;
            isSelected = true;
            GridManager.Instance.selectedCell = GetComponent<Cell>();  
            Debug.Log("�");
        }
        else if (isPart && GridManager.Instance.isSelected)
        {
            GridManager.Instance.targetCell = GetComponent<Cell>();
           GridManager.Instance.FindAndShowPath(GridManager.Instance.selectedCell.pathData , pathData);
        }
        else if(!isPart && GridManager.Instance.isSelected)
        {
            GridManager.Instance.isSelected = false;
        }
    }

   
}
