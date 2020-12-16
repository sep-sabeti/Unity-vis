using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class scatterplot : MonoBehaviour
{
    // Variables to store data, xy locations and cluster IDs
    private List<float> longitude = new List<float>();
    private List<float> latitude = new List<float>();
    private List<string> magnitude = new List<string>();

    // Use this for initialization
    void Start () {
        readData("Assets/Data/Earthquake-Dataset.csv");
        makePlot();
    }
	// reading and parsing CSV file and adding data to appropriate data structures
    public void readData(string filename)
    {
        string[] reader = System.IO.File.ReadAllLines(filename);
        for (int i = 0; i < reader.Length; i++)
        {
            string[] line = reader[i].Split(',');
            latitude.Add(float.Parse(line[0]));
            longitude.Add(float.Parse(line[1]));
            magnitude.Add(line[2]);
        }

    }


    // creating Unity built-in primitive(sphere) and using it as a dataPoint in scatter-plot
    public void makePlot()
    {
        float scale = 0.02f;
        for (int i = 0; i < latitude.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.localPosition = new Vector3(longitude[i], latitude[i], 1f);
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.transform.position = Quaternion.AngleAxis(longitude[i], -Vector3.up) * Quaternion.AngleAxis(latitude[i], -Vector3.right) * new Vector3(0, 0, 1);
            dataPt.transform.localScale = new Vector3(scale, scale, scale);
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = RandomColorGenerator();
            dataPt.GetComponent<Renderer>().material = newMaterial;
            dataPt.gameObject.SetActive(true);
        }
    }

    public static Color RandomColorGenerator()
    {
    Color randomColor = new Color(
    Random.Range(0f, 1f), //Red
    Random.Range(0f, 1f), //Green
    Random.Range(0f, 1f), //Blue
    0.5f //Alpha (transparency)
    );
     return randomColor;
    }

    // Update is called once per frame
    void Update() { }
}
