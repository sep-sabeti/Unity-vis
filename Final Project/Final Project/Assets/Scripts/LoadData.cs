using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    List<float> longitude = new List<float>();
    List<float> latitude = new List<float>();
    List<string> stationNames = new List<string>();
    List<string> stationIndeices = new List<string>();
    List<float> PRC_2020 = new List<float>();
    List<float> SNOW_2020 = new List<float>();
    List<float> TMAX_2020 = new List<float>();
    List<float> TMIN_2020 = new List<float>();

    List<float> PRC_2019 = new List<float>();
    List<float> SNOW_2019 = new List<float>();
    List<float> TMAX_2019 = new List<float>();
    List<float> TMIN_2019 = new List<float>();


    List<float> PRC_2018 = new List<float>();
    List<float> SNOW_2018 = new List<float>();
    List<float> TMAX_2018 = new List<float>();
    List<float> TMIN_2018 = new List<float>();

    List<float> PRC_2017 = new List<float>();
    List<float> SNOW_2017 = new List<float>();
    List<float> TMAX_2017 = new List<float>();
    List<float> TMIN_2017 = new List<float>();

    List<float> PRC_2016 = new List<float>();
    List<float> SNOW_2016 = new List<float>();
    List<float> TMAX_2016 = new List<float>();
    List<float> TMIN_2016 = new List<float>();

    List<float> PRC_2015 = new List<float>();
    List<float> SNOW_2015 = new List<float>();
    List<float> TMAX_2015 = new List<float>();
    List<float> TMIN_2015 = new List<float>();



    public String selectedStation;
    public String selectedYear = "2020";


    // Start is called before the first frame update
    void Start()
    {
        new GameObject("Station Name").AddComponent<TextMesh>();
        new GameObject("Year").AddComponent<TextMesh>();

        LoadingData();
        Debug.Log(SNOW_2015.Average());

        makePlot();

    }
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        // Adding keyboard control to the scene with arrows
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed , transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed , transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y , transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y , transform.position.z);
        }

        //Adding mouse control for changing z

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.z < -15f) // forward
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.z > -25f ) // backwards
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);

        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.tag != "year")
                {
                    Debug.Log(hit.transform.gameObject.name);
                    selectedStation = hit.transform.gameObject.name;

                    GameObject p = hit.transform.gameObject;
                    foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("selected"))
                    {
                        sphere.GetComponent<Renderer>().material.color = Color.gray;
                    }


                    if (p.GetComponent<Renderer>().material.color == Color.gray)
                    {
                        TextMesh text = GameObject.Find("Station Name").GetComponent<TextMesh>(); ;
                        p.GetComponent<Renderer>().material.color = Color.red;
                        text.transform.position = p.transform.position - new Vector3(0f, 0f, -14f);
                        text.text = p.name;
                        text.color = Color.blue;
                        text.fontStyle = FontStyle.Bold;
                        text.fontSize = 10;
                        p.tag = "selected";
                    }


                    else if (p.GetComponent<Renderer>().material.color == Color.red)
                    {
                        p.GetComponent<Renderer>().material.color = Color.gray;
                        TextMesh text = GameObject.Find("Station Name").GetComponent<TextMesh>(); ;
                        text.text = "";
                        selectedStation = null;

                    }



                    makeBoxPlot(selectedYear, selectedStation);


                }
                else if (hit.transform != null && hit.transform.gameObject.tag.Contains("year"))
                {

                    Debug.Log(hit.transform.gameObject.name);
                    Debug.Log(hit.transform.gameObject.tag.Contains("year"));

                    GameObject p = hit.transform.gameObject;
                    foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("selected - year"))
                    {
                        sphere.GetComponent<Renderer>().material.color = Color.green;
                    }

                    foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("year"))
                    {
                        sphere.GetComponent<Renderer>().material.color = Color.green;
                    }

                    if (p.GetComponent<Renderer>().material.color == Color.green)
                    {
                        TextMesh text = GameObject.Find("Year").GetComponent<TextMesh>(); ;
                        p.GetComponent<Renderer>().material.color = Color.yellow;
                        text.transform.position = p.transform.position - new Vector3(0f, +5f, -20f);
                        text.text = p.name;
                        text.color = Color.white;
                        text.fontStyle = FontStyle.Bold;
                        text.fontSize = 10;
                        selectedYear = p.name.Substring(5);                       
                    }
                    makeBoxPlot(selectedYear, selectedStation);

                }


            }

        }
        if (Input.GetMouseButtonDown(1))
        {

            TextMesh text = GameObject.Find("Station Name").GetComponent<TextMesh>();
            TextMesh text2 = GameObject.Find("Year").GetComponent<TextMesh>();
            GameObject [] box = GameObject.FindGameObjectsWithTag("box");


            foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("selected"))
            {
                sphere.GetComponent<Renderer>().material.color = Color.gray;
            }

            foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("year"))
            {
                sphere.GetComponent<Renderer>().material.color = Color.green;
            }

            text.text = "";
            text2.text = "";
            foreach(GameObject obj in box)
            {
                Destroy(obj);

            }

        }

        if (Input.GetMouseButtonDown(2))
        {
            TextMesh text = GameObject.Find("Year").GetComponent<TextMesh>();

            foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("year"))
            {
                sphere.GetComponent<Renderer>().material.color = Color.green;
            }

            text.text = "";
        }
    }


    //Here we will start loading data. It should be noted that the string needs to be distincted. 

    public void LoadingData()
    {
        Debug.Log("Started loading data");
        string fileDirectory = "Data";
        TextAsset file = Resources.Load<TextAsset>(fileDirectory);
        string loadedData = file.ToString();
        string[] rows = loadedData.Split('\n');

        if (rows != null)
        {
            for (int i = 1; i < rows.Length-1 ; i++)
            {
                string[] row = rows[i].Split(',');
                if (!stationNames.Contains(row[0]))
                {
                    stationNames.Add(row[0]);
                }


                    float.TryParse(row[1], out float xAxis);
                    float.TryParse(row[2], out float yAxis);
                    float.TryParse(row[3], out float prc20);
                    float.TryParse(row[4], out float snw20);
                    float.TryParse(row[5], out float tmax20);
                    float.TryParse(row[6], out float tmin20);

                float.TryParse(row[7], out float prc19);
                float.TryParse(row[8], out float snw19);
                float.TryParse(row[9], out float tmax19);
                float.TryParse(row[10], out float tmin19);

                float.TryParse(row[11], out float prc18);
                float.TryParse(row[12], out float snw18);
                float.TryParse(row[13], out float tmax18);
                float.TryParse(row[14], out float tmin18);

                float.TryParse(row[15], out float prc17);
                float.TryParse(row[16], out float snw17);
                float.TryParse(row[17], out float tmax17);
                float.TryParse(row[18], out float tmin17);

                float.TryParse(row[19], out float prc16);
                float.TryParse(row[20], out float snw16);
                float.TryParse(row[21], out float tmax16);
                float.TryParse(row[22], out float tmin16);

                float.TryParse(row[23], out float prc15);
                float.TryParse(row[24], out float snw15);
                float.TryParse(row[25], out float tmax15);
                float.TryParse(row[26], out float tmin15);

                stationIndeices.Add(row[0]);
                    longitude.Add(xAxis);
                    latitude.Add(yAxis);
                    TMAX_2020.Add(tmax20);
                    PRC_2020.Add(prc20);
                    TMIN_2020.Add(tmin20);
                    SNOW_2020.Add(snw20);

        
                TMAX_2020.Add(tmax20);
                PRC_2020.Add(prc20);
                TMIN_2020.Add(tmin20);
                SNOW_2020.Add(snw20);

            
                TMAX_2019.Add(tmax19);
                PRC_2019.Add(prc19);
                TMIN_2019.Add(tmin19);
                SNOW_2019.Add(snw19);

         
                TMAX_2018.Add(tmax18);
                PRC_2018.Add(prc18);
                TMIN_2018.Add(tmin18);
                SNOW_2018.Add(snw18);

                TMAX_2017.Add(tmax17);
                PRC_2017.Add(prc17);
                TMIN_2017.Add(tmin17);
                SNOW_2017.Add(snw17);

                TMAX_2016.Add(tmax16);
                PRC_2016.Add(prc16);
                TMIN_2016.Add(tmin16);
                SNOW_2016.Add(snw16);

                TMAX_2015.Add(tmax15);
                PRC_2015.Add(prc15);
                TMIN_2015.Add(tmin15);
                SNOW_2015.Add(snw15);
            }
            }
        
        Debug.Log("Finished loading data");
        Debug.Log(longitude.Count);
        Debug.Log(stationNames.Count);
        Debug.Log(TMIN_2020.Average());

    }


    public void makePlot()

    {
        

        //Adding a collider to each sphere
         void addSphereCollider(GameObject sphere)
        {
            var pos = sphere.transform.position;
            SphereCollider collider = sphere.AddComponent<SphereCollider>();
            collider.transform.parent = sphere.transform;
            collider.center = pos;
        }

        for (int i = 0; i < stationNames.Count-1; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Sphere " + i.ToString();
            int index = stationIndeices.IndexOf(stationNames[i]);
            float lat = (latitude[index] - 53.178469f) / 0.00001f * 0.12179047095976932582726898256213f;
            float lon = (longitude[index] - 6.503091f) / 0.000001f * 0.00728553580298947812081345114627f;
            sphere.transform.position = Quaternion.AngleAxis(lon, -Vector3.up) * Quaternion.AngleAxis(lat, -Vector3.right) * new Vector3(0, 0, 1);
            sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            addSphereCollider(sphere);
            sphere.GetComponent<Renderer>().material.color = Color.grey;
            sphere.name = stationNames[i];

        }
        GameObject.Find("Main Camera").transform.position = new Vector3(-1f, 0f, -15f);

        for (int i = 0; i < 6; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Year " + (i+2015).ToString();
            sphere.transform.position = new Vector3(-1f + i * 2 / 10f, -2f, -6f);
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            addSphereCollider(sphere);
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            sphere.GetComponent<Renderer>().material.color = Color.green;
            sphere.tag = "year";
        }
        GameObject sphere1 = GameObject.Find("Year 2020");
        sphere1.GetComponent<Renderer>().material.color = Color.yellow;
        TextMesh text2 = GameObject.Find("Year").GetComponent<TextMesh>(); ;
        text2.transform.position = sphere1.transform.position - new Vector3(0f, +5f, -20f);
        text2.text = sphere1.name;
        text2.color = Color.white;
        text2.fontStyle = FontStyle.Bold;
        text2.fontSize = 10;


    }


   
    public void makeBoxPlot(String selectedYear, String selectedStation)

    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("box"))
        {
            Destroy(obj);

        }
        List<int> indices = new List<int>();
        if(selectedStation != null)
        {
            Debug.Log(selectedStation);
            Debug.Log(selectedYear);

        }


        foreach (String i in stationIndeices)
        {
            if (i == selectedStation)
            {
                indices.Add(stationIndeices.IndexOf(i));
            }
        }


        if (int.Parse(selectedYear) == 2020)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2020[i]);
                tmin.Add(TMIN_2020[i]);
                prc.Add(PRC_2020[i]);
                snow.Add(SNOW_2020[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();

        
            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE/100 , 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f , -0.5f , -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE/100 , 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE , 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE , 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
        if (int.Parse(selectedYear) == 2019)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2019[i]);
                tmin.Add(TMIN_2019[i]);
                prc.Add(PRC_2019[i]);
                snow.Add(SNOW_2019[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();


            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE / 100, 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f, -0.5f, -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE / 100, 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE, 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE, 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
         if (int.Parse(selectedYear) == 2018)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2018[i]);
                tmin.Add(TMIN_2018[i]);
                prc.Add(PRC_2018[i]);
                snow.Add(SNOW_2018[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();


            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE / 100, 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f, -0.5f, -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE / 100, 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE, 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE, 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
         if (int.Parse(selectedYear) == 2017)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2017[i]);
                tmin.Add(TMIN_2017[i]);
                prc.Add(PRC_2017[i]);
                snow.Add(SNOW_2017[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();


            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE / 100, 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f, -0.5f, -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE / 100, 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE, 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE, 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
         if (int.Parse(selectedYear) == 2016)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2016[i]);
                tmin.Add(TMIN_2016[i]);
                prc.Add(PRC_2016[i]);
                snow.Add(SNOW_2016[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();


            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE / 100, 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f, -0.5f, -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE / 100, 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE, 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE, 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
         if (int.Parse(selectedYear) == 2015)
        {
            List<float> tmax = new List<float>();
            List<float> tmin = new List<float>();
            List<float> prc = new List<float>();
            List<float> snow = new List<float>();

            foreach (int i in indices)
            {
                tmax.Add(TMAX_2015[i]);
                tmin.Add(TMIN_2015[i]);
                prc.Add(PRC_2015[i]);
                snow.Add(SNOW_2015[i]);
            }

            float tmaxAVE = tmax.Average();
            float tminAVE = tmin.Average();
            float prcAVE = prc.Average();
            float snowAVE = snow.Average();


            Debug.Log(tmaxAVE);
            Debug.Log(tminAVE);
            Debug.Log(prcAVE);
            Debug.Log(snowAVE);

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box.tag = "box";
            box.name = "Box " + selectedYear;
            box.GetComponent<Renderer>().material.color = Color.red;
            box.transform.position = new Vector3(-5, -2, -6);
            box.transform.localScale = new Vector3(0.3f, tmaxAVE / 100, 1f);

            TextMesh textbox = new GameObject("TextBox").AddComponent<TextMesh>();
            textbox.tag = "box";
            textbox.text = "TMAX (F): " + tmaxAVE.ToString();
            textbox.transform.position = new Vector3(-4.13f, -0.5f, -8);
            textbox.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox.GetComponent<TextMesh>().color = Color.blue;
            textbox.fontStyle = FontStyle.Bold;
            textbox.fontSize = 20;
            textbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box2.tag = "box";
            box2.name = "Box1 " + selectedYear;
            box2.GetComponent<Renderer>().material.color = Color.red;
            box2.transform.position = new Vector3(-4, -2, -6);
            box2.transform.localScale = new Vector3(0.3f, tminAVE / 100, 1f);
            TextMesh textbox2 = new GameObject("TextBox2").AddComponent<TextMesh>();
            textbox2.tag = "box";
            textbox2.text = "TMIN (F) : " + tminAVE.ToString();
            textbox2.transform.position = new Vector3(-3.43f, -0.5f, -8);
            textbox2.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox2.GetComponent<TextMesh>().color = Color.blue;
            textbox2.fontStyle = FontStyle.Bold;
            textbox2.fontSize = 20;
            textbox2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box3.tag = "box";
            box3.name = "Box2 " + selectedYear;
            box3.GetComponent<Renderer>().material.color = Color.red;
            box3.transform.position = new Vector3(-3, -2, -6);
            box3.transform.localScale = new Vector3(0.3f, prcAVE, 1f);
            TextMesh textbox3 = new GameObject("TextBox3").AddComponent<TextMesh>();
            textbox3.tag = "box";
            textbox3.text = "Precipitation (Inch): " + prcAVE.ToString();
            textbox3.transform.position = new Vector3(-2.53f, -0.5f, -8);
            textbox3.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox3.GetComponent<TextMesh>().color = Color.blue;
            textbox3.fontStyle = FontStyle.Bold;
            textbox3.fontSize = 20;
            textbox3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


            GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            box4.tag = "box";
            box4.name = "Box3 " + selectedYear;
            box4.GetComponent<Renderer>().material.color = Color.red;
            box4.transform.position = new Vector3(-2, -2, -6);
            box4.transform.localScale = new Vector3(0.3f, snowAVE, 1f);
            TextMesh textbox4 = new GameObject("TextBox4").AddComponent<TextMesh>();
            textbox4.tag = "box";
            textbox4.text = "Snow (Inch): " + snowAVE.ToString();
            textbox4.transform.position = new Vector3(-1.83f, -0.5f, -8);
            textbox4.transform.Rotate(00.0f, 00.0f, 90.0f, Space.Self);
            textbox4.GetComponent<TextMesh>().color = Color.blue;
            textbox4.fontStyle = FontStyle.Bold;
            textbox4.fontSize = 20;
            textbox4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
    }
    

}
