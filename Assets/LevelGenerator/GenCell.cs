
using UnityEngine;

public class GenCell : MonoBehaviour
{
    public bool isPath;
    public bool isPart;
    public bool isHole;
    public bool isDev;
    int x, y;
 
    public GameObject pathPrefab;
    public GameObject partPrefab;
    public GameObject holePrefab;


    public void Setup( int a, int b)
    {
      

        x = a;
        y = b;


    }

    void OnMouseDown()
    {
        if (isDev)
        {
            if (isPath) { levelGenerator.LevelMakerInt = 0; }
            if (isPart) { levelGenerator.LevelMakerInt = 1; }
            if (isHole) { levelGenerator.LevelMakerInt = 2; }
        }
        else  
        {
            if (levelGenerator.LevelMakerInt == 0)
            {
                GameObject cellObj;
                cellObj = Instantiate(pathPrefab, gameObject.transform.position, Quaternion.identity);
                cellObj.GetComponent<GenCell>().Setup(x, y);
                levelGenerator.Instance.cell[x,y] = cellObj.GetComponent<GenCell>();
                Destroy(gameObject);
            }
            if (levelGenerator.LevelMakerInt == 1)
            {
                GameObject cellObj;
                cellObj = Instantiate(partPrefab, gameObject.transform.position, Quaternion.identity);
                cellObj.GetComponent<GenCell>().Setup(x,y);
                levelGenerator.Instance.cell[x, y] = cellObj.GetComponent<GenCell>();
                Destroy(gameObject);
            }
            if (levelGenerator.LevelMakerInt == 2)
            {
                GameObject cellObj;
                cellObj = Instantiate(holePrefab, gameObject.transform.position, Quaternion.identity);
                cellObj.GetComponent<GenCell>().Setup(x, y);
                levelGenerator.Instance.cell[x, y] = cellObj.GetComponent<GenCell>();
                Destroy(gameObject);
            }
          //  Debug.Log("MakerSeç");
        }

        
        Debug.Log($"Týklanan hücre x : {x} ile y : {y}koordinatlarýnda yer alýyor.");
        
    }
  

  
}
