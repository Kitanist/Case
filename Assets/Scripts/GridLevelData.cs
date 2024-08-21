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
            Debug.Log($"Hücre kaydedildi: Index {index}, Tür {cellType}");
        }
        else
        {
            Debug.LogError("Hücre indeksi sýnýrlarýn dýþýnda: " + index);
        }
    }
}
