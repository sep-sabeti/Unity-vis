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
    List<float> tempreture_2014 = new List<float>();
    List<float> tempreture_2015 = new List<float>();
    List<float> tempreture_2016 = new List<float>();
    List<float> tempreture_2017 = new List<float>();
    List<float> tempreture_2018 = new List<float>();

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
                float.TryParse(row[2], out float temp_1);
                float.TryParse(row[2], out float temp_2);
                float.TryParse(row[2], out float temp_3);
                float.TryParse(row[2], out float temp_4);



                longitude.Add(xAxis);
                latitude.Add(yAxis);
                tempreture_2014.Add(temp);
                tempreture_2015.Add(temp_1);
                tempreture_2016.Add(temp_2);
                tempreture_2017.Add(temp_3);
                tempreture_2018.Add(temp_4);


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
            sphere.transform.position = new Vector3(longitude[i] , 0, latitude[i]);
            sphere.GetComponent<Renderer>().material.color = Color.grey;

            GameObject tempreture_2014_new = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempreture_2014_new.name = "tempreture_2014 " + i.ToString();
            tempreture_2014_new.transform.position = new Vector3(longitude[i], 10, latitude[i]);
            tempreture_2014_new.GetComponent<Renderer>().material.color = colorChoice(tempreture_2014[i]);

            GameObject tempreture_2015_new = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempreture_2015_new.name = "tempreture_2015 " + i.ToString();
            tempreture_2015_new.transform.position = new Vector3(longitude[i], 20, latitude[i]);
            tempreture_2015_new.GetComponent<Renderer>().material.color = colorChoice(tempreture_2015[i]);

            GameObject tempreture_2016_new = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempreture_2016_new.name = "tempreture_2016 " + i.ToString();
            tempreture_2016_new.transform.position = new Vector3(longitude[i], 30, latitude[i]);
            tempreture_2016_new.GetComponent<Renderer>().material.color = colorChoice(tempreture_2016[i]);

            GameObject tempreture_2017_new = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempreture_2017_new.name = "tempreture_2017 " + i.ToString();
            tempreture_2017_new.transform.position = new Vector3(longitude[i], 40, latitude[i]);
            tempreture_2017_new.GetComponent<Renderer>().material.color = colorChoice(tempreture_2017[i]);

            GameObject tempreture_2018_new = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempreture_2018_new.name = "tempreture_2018 " + i.ToString();
            tempreture_2018_new.transform.position = new Vector3(longitude[i], 50, latitude[i]);
            tempreture_2018_new.GetComponent<Renderer>().material.color = colorChoice(tempreture_2018[i]);

        }

        GameObject.Find("Main Camera").transform.position = new Vector3(longitude.Average(), latitude.Average()+30f, -193f);
        TextMesh text1 = new GameObject("I-77").AddComponent<TextMesh>();
        TextMesh text2 = new GameObject("I-81").AddComponent<TextMesh>();

        text1.transform.position = new Vector3(110f, 0f, 96f);
        text1.text = "I-77";
        text1.color = Color.blue;
        text1.fontSize = 100;

        text2.transform.position = new Vector3(60f, 0f, 40f);
        text2.text = "I-81";
        text2.color = Color.blue;
        text2.fontSize = 100;


        TextMesh text2014 = new GameObject("temp-2014").AddComponent<TextMesh>();
        text2014.transform.position = new Vector3(-100f, 0f, 40f);
        text2014.text = "Max tempreture - 2014";
        text2014.color = Color.blue;
        text2014.fontSize = 80;


        TextMesh text2015 = new GameObject("temp-2015").AddComponent<TextMesh>();
        text2015.transform.position = new Vector3(-100f, 15f, 40f);
        text2015.text = "Max tempreture - 2015";
        text2015.color = Color.blue;
        text2015.fontSize = 80;

        TextMesh text2016 = new GameObject("temp-2016").AddComponent<TextMesh>();
        text2016.transform.position = new Vector3(-100f, 30f, 40f);
        text2016.text = "Max tempreture - 2016";
        text2016.color = Color.blue;
        text2016.fontSize = 80;

        TextMesh text2017 = new GameObject("temp-2017").AddComponent<TextMesh>();
        text2017.transform.position = new Vector3(-100f, 45f, 40f);
        text2017.text = "Max tempreture - 2017";
        text2017.color = Color.blue;
        text2017.fontSize = 80;

        TextMesh text2018 = new GameObject("temp-2018").AddComponent<TextMesh>();
        text2018.transform.position = new Vector3(-100f, 60f, 40f);
        text2018.text = "Max tempreture - 2018";
        text2018.color = Color.blue;
        text2018.fontSize = 80;


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
