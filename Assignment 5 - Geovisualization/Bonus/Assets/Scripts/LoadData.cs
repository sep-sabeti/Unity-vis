using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    List<float> longitude = new List<float>();
    List<float> latitude = new List<float>();
    List<float> tempreture = new List<float>();

    // Start is called before the first frame update
    void Start()
    {

        LoadingData();
        makePlot();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadingData()
    {
        string fileDirectory = "Data";
        TextAsset file = Resources.Load<TextAsset>(fileDirectory);
        string loadedData = file.ToString();
        string[] rows = loadedData.Split('\n');

        if (rows != null)
        {
            for (int i = 0; i < rows.Length - 1; i++)
            {
                string[] row = rows[i].Split(',');

                float.TryParse(row[0], out float xAxis);
                float.TryParse(row[1], out float yAxis);
                float.TryParse(row[2], out float temp);

                longitude.Add(xAxis);
                latitude.Add(yAxis);
                tempreture.Add(temp);

            }
        }

    }

    public void makePlot()

    {

        float minX = longitude.Min();
        float maxX = longitude.Max();
        float minY = latitude.Min();
        float maxY = latitude.Max();

        for(int i = 0; i < longitude.Count; i++)
        {
            longitude[i] = 100f * (longitude[i] - minX) / (maxX - minX);
            latitude[i] = 100f * (latitude[i] - minY) / (maxY - minY);
        }

        for (int i = 0; i < longitude.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Sphere " + i.ToString();
            sphere.transform.position = new Vector3(longitude[i] , latitude[i], 0);
            sphere.GetComponent<Renderer>().material.color = colorChoice(tempreture[i]);


        }

        GameObject.Find("Main Camera").transform.position = new Vector3(longitude.Average(), latitude.Average()+10f, -150f);
        TextMesh text1 = new GameObject("I-77").AddComponent<TextMesh>();
        TextMesh text2 = new GameObject("I-81").AddComponent<TextMesh>();

        text1.transform.position = new Vector3(100f,96f, 0f);
        text1.text = "I-77";
        text1.color = Color.blue;
        text1.fontSize = 100;

        text2.transform.position = new Vector3(60f, 40f, 0f);
        text2.text = "I-81";
        text2.color = Color.blue;
        text2.fontSize = 100;

    }

    public Color colorChoice(float temp)
    {
        if (0 < temp && temp< 80f)
        {
            return Color.blue;
        }
        else if (80 <= temp && temp < 85f)
        {
            return Color.green;
        }
        else if (85 <= temp && temp < 90f)
        {
            return Color.yellow;
        }
        else
        {
            return Color.red;
        }
    }


   
}
