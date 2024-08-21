using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Level", menuName = "Grid Level Data")]
public class GridLevelData : ScriptableObject
{
    
    public int[] cells;

    public int GetCellData(int x, int y, int a)
    {
       
        return cells[x + y * a];
    }
    public void SetCellData(int x, int y, int width, int cellType)
    {
        int index = x + y * width; 
        if (index < cells.Length) 
        {
            cells[index] = cellType; 
            Debug.Log($"H�cre kaydedildi: Index {index}, T�r {cellType}");
        }
        else
        {
            Debug.LogError("H�cre indeksi s�n�rlar�n d���nda: " + index);
        }
    }
}
