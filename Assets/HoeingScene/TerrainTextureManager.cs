using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTextureManager : MonoBehaviour
{

    Terrain targetTerrain; //terrain object to edit
    TerrainData targetTerrainData; //data for editable terrain
    float[,] terrainHeightMap; //floats representing the height map
    int heightMapWidth;
    int heightMapHeight;

    public Texture2D brushImage; //image you want to paint onto texture
    float[,] brush; //stores pixel data from brush image
    public int areaOfEffectSize = 100; //size area to paint
    public float brushStrength;

    public TerrainLayer[] paints; //list containing your paints
    public int paint; //which paint you are using
    float[,,] splat; //used to overlay paints onto terrain
    
    
    void Awake()
    {
        brush = GenerateBrush(brushImage, areaOfEffectSize); //creates brush with image of indicated size
        targetTerrain = this.GetComponent<Terrain>(); //get this terrain for editing
        targetTerrainData = targetTerrain.terrainData; //get this terrain's data

        //get height map, height and width from terrain data
        terrainHeightMap = targetTerrain.terrainData.GetHeights(0, 0, 
            targetTerrain.terrainData.heightmapResolution, targetTerrain.terrainData.heightmapResolution);
        
        heightMapWidth = targetTerrain.terrainData.heightmapResolution;
        heightMapHeight = targetTerrain.terrainData.heightmapResolution;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetTerrainCoordinates(RaycastHit hit, out int x, out int z)
    {
        int offset = areaOfEffectSize / 2; //accounts for brush size (size starts at top left corner)
        Vector3 tempTerrainCoordinates = hit.point - hit.transform.position; //coordinates relative to terrain

        //coordinates relative to height map (sometimes different dimensions)
        Vector3 terrainCoordinates = new Vector3(
            tempTerrainCoordinates.x / targetTerrain.terrainData.size.x,
            tempTerrainCoordinates.y / targetTerrain.terrainData.size.y,
            tempTerrainCoordinates.z / targetTerrain.terrainData.size.z);

        Vector3 locationInTerrain = new Vector3 (
            terrainCoordinates.x * heightMapWidth,
            0,
            terrainCoordinates.z * heightMapHeight);
        
        //final x and z values 
        x = (int)locationInTerrain.x - offset;
        z = (int)locationInTerrain.z - offset;
    }   

    public float[,] GenerateBrush(Texture2D texture, int size)
    {
        float[,] heightMap = new float[size,size]; //2d array to store brush
        Texture2D scaledBrush = ResizeBrush(texture, size, size); //resizes brush image

        //iterate over rescaled image and convert pixel color to value between 0 and 1
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Color pixelValue = scaledBrush.GetPixel(i, j); //get pixel location
                heightMap[i, j] = pixelValue.grayscale / 255;
            }
        }

        return heightMap;
    }

    public static Texture2D ResizeBrush(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(src, width, height, mode);

        //get rendered data back to new texture
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
        result.Resize(width, height);
        result.ReadPixels(texR, 0, 0, true);

        return result;
    }

    static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
    {
        //get source texture in vram to render with it
        src.filterMode = fmode;
        src.Apply(true);

        //make render texture
        RenderTexture rtt = new RenderTexture(width, height, 32);
        Graphics.SetRenderTarget(rtt); //set rtt so you can render to it
        GL.LoadPixelMatrix(0, 1, 1, 0); //set 2d matrix range 0-1
        
        GL.Clear(true, true, new Color(0, 0, 0, 0)); //clear old texture
        Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
    }

    void ModifyTexture(int x, int z)
    {

    }

#region BasicBrushSettings
    public void SetPaint(int num)
    {
        paint = num;
    }

    public void SetLayers(TerrainData t)
    {
        t.terrainLayers = paints;
    }

    public void SetBrushSize(int value)
    {
        areaOfEffectSize += value;
        if(areaOfEffectSize > 50)
            areaOfEffectSize = 50; 
        else if(areaOfEffectSize < 1)
            areaOfEffectSize = 1;

        brush = GenerateBrush(brushImage, areaOfEffectSize); //regenerate brush with new size
    }

    public void SetBrushStrength(float value)
    {
        brushStrength += value;
        if(brushStrength > 1)
            brushStrength = 1;
        else if(brushStrength < 0.01f)
            brushStrength = 0.01f;
    }

    #endregion
}
