using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
         grid = new Grid(4, 2, 10f, new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            grid.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition),10);
        }
        if (Input.GetMouseButtonDown(1)) {

            
           Debug.Log(grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }
}
