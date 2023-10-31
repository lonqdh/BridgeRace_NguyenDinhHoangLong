using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject bricksGroundPrefab;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f; // spacing between bricks
    public Vector3 gridOrigin = Vector3.zero;
    public Transform spawnPosition; // the spawn position of the grid
    public Transform brickGroundParent; // dung de organize bricks spawned in the hierarchy
    public List<ColorType> availableColors;

    public List<BrickGround> bricks = new List<BrickGround>(); // dung de luu 1 list cac vien gach cua stage


    private void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        int colorIndex = 0; // Initialize the color index

        //spawn bricks in a grid from left to right, the first column is at the left
        for (int i = 0; i < gridX; i++) //gridX is length
        {
            for (int j = 0; j < gridZ; j++) //gridZ is width
            {
                Vector3 positionToSpawn = spawnPosition.position + new Vector3(i * gridSpacingOffset, 0, j * gridSpacingOffset);

                ColorType selectedColor = availableColors[colorIndex]; // Get the color from the list

                PickAndSpawn(positionToSpawn, Quaternion.identity, selectedColor);

                colorIndex = (colorIndex + 1) % availableColors.Count; // Rotate through available colors
            }
        }
    }

    private void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn, ColorType color)
    {
        GameObject clone = Instantiate(bricksGroundPrefab, positionToSpawn, rotationToSpawn);

        ColorGameObject colorGameObject = clone.GetComponent<ColorGameObject>(); //set colorGameObject thanh colorgameobject cua thg clone sau do doi mau thg clone thong qua doi mau thang colorGameObject
        if (colorGameObject != null)
        {
            colorGameObject.ChangeColor(color); //tao brick xong thi phai doi mau nen phai co doan nay, tai mau ban dau cua brick la kcg/blue
        }

        BrickGround brickGround = clone.GetComponent<BrickGround>();
        if (brickGround != null)
        {
            bricks.Add(brickGround); //Add gach vao cua stage vao 1 list chua cac gach cua stage
        }

        if (brickGroundParent != null)
        {
            clone.transform.parent = brickGroundParent;
        }

    }

}
