using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class LoadData : MonoBehaviour

{
    List<Data> datapoints = new List<Data>();
    // Start is called before the first frame update

    void Start()
    {
        TextAsset data = Resources.Load<TextAsset>("Data");

        GameObject.Find("Main Camera").transform.position = new Vector3(8, 1, -20);


        string[] newData = data.text.Split(new char[] { '\n' });
        string[] index = newData[0].Split(new char[] { ',' });


        for (int i = 1; i < newData.Length; i++)
        {
            string[] row = newData[i].Split(new char[] { ',' });

            if (!row.Contains("") & !row.Contains("NA"))
            {
                Data d = new Data();
                float.TryParse(row[0], out d.column1);
                float.TryParse(row[1], out d.column2);
                float.TryParse(row[2], out d.column3);
                float.TryParse(row[3], out d.column4);
                float.TryParse(row[4], out d.column5);
                float.TryParse(row[5], out d.column6);
                float.TryParse(row[6], out d.column7);
                float.TryParse(row[7], out d.column8);
                float.TryParse(row[8], out d.column9);
                float.TryParse(row[9], out d.column10);
                float.TryParse(row[10], out d.column11);
                float.TryParse(row[11], out d.column12);
              
                datapoints.Add(d);
            }

        }

        float column1min = +100000000;
        float column2min = +100000000;
        float column3min = +100000000;
        float column4min = +100000000;
        float column5min = +100000000;
        float column6min = +100000000;
        float column7min = +100000000;
        float column8min = +100000000;
        float column9min = +100000000;
        float column10min = +100000000;
        float column11min = +100000000;
        float column12min = +100000000;



        float column1max = -100000000;
        float column2max = -100000000;
        float column3max = -100000000;
        float column4max = -100000000;
        float column5max = -100000000;
        float column6max = -100000000;
        float column7max = -100000000;
        float column8max = -100000000;
        float column9max = -100000000;
        float column10max = -100000000;
        float column11max = -100000000;
        float column12max = -100000000;




        float GetMin(float d , float min)
        {
            if (d < min)
            {
                min = d;
            }
            return min;
        }

        float GetMax(float d, float max)
        {
            if (d > max)
            {
                max = d;
            }
            return max;
        }

        foreach (Data d in datapoints)
        {
            column1min = GetMin(d.column1, column1min);
            column2min = GetMin(d.column2, column2min);
            column3min= GetMin(d.column3, column3min);
            column4min= GetMin(d.column4, column4min);
            column5min = GetMin(d.column5, column5min);
            column6min = GetMin(d.column6, column6min);
            column7min = GetMin(d.column7, column7min);
            column8min = GetMin(d.column8, column8min);
            column9min = GetMin(d.column9, column9min);
            column10min = GetMin(d.column10, column10min);
            column11min = GetMin(d.column11, column11min);
            column12min = GetMin(d.column12, column12min);


            column1max = GetMax(d.column1, column1max);
            column2max = GetMax(d.column2, column2max);
            column3max = GetMax(d.column3, column3max);
            column4max = GetMax(d.column4, column4max);
            column5max = GetMax(d.column5, column5max);
            column6max = GetMax(d.column6, column6max);
            column7max = GetMax(d.column7, column7max);
            column8max = GetMax(d.column8, column8max);
            column9max = GetMax(d.column9, column9max);
            column10max = GetMax(d.column10, column10max);
            column11max = GetMax(d.column11, column11max);
            column12max = GetMax(d.column12, column12max);


        }

        float Normalize(float d, float min, float max)
        {
            return 2 * (d - min) / (max - min) - 1;
        }



        //Normalizing Data with a function

        List<Data> normalizeddatapoints = new List<Data>();

        foreach (Data d in datapoints)
        {
            Data normalizedD = new Data();
            normalizedD.column1 = Normalize(d.column1, column1min, column1max);
            normalizedD.column2 = Normalize(d.column2, column2min, column2max);
            normalizedD.column3 = Normalize(d.column3, column3min, column3max);
            normalizedD.column4 = Normalize(d.column4, column4min, column4max);
            normalizedD.column5 = Normalize(d.column5, column5min, column5max);
            normalizedD.column6 = Normalize(d.column6, column6min, column6max);
            normalizedD.column7 = Normalize(d.column7, column7min, column7max);
            normalizedD.column8 = Normalize(d.column8, column8min, column8max);
            normalizedD.column9 = Normalize(d.column9, column9min, column9max);
            normalizedD.column10 = Normalize(d.column10, column10min, column10max);
            normalizedD.column11 = Normalize(d.column11, column11min, column11max);
            normalizedD.column12 = Normalize(d.column12, column12min, column12max);
            normalizeddatapoints.Add(normalizedD);
        }

        //Finding a number of positions for the parallel coordinates

        Data datacounter = new Data();

        foreach(Data d in normalizeddatapoints)
            {
                LineRenderer p = new GameObject("Line").AddComponent<LineRenderer>();
                p.positionCount = datacounter.numberOfColumns;
                Vector3 point1 = new Vector3(0, d.column1, 0);
                Vector3 point2 = new Vector3(2 , d.column2, 0);
                Vector3 point3 = new Vector3(4, d.column3, 0);
                Vector3 point4 = new Vector3(6, d.column4, 0);
                Vector3 point5 = new Vector3(8, d.column5, 0);
                Vector3 point6 = new Vector3(10, d.column6, 0);
                Vector3 point7 = new Vector3(12, d.column7, 0);
                Vector3 point8 = new Vector3(14, d.column8, 0);
                Vector3 point9 = new Vector3(16, d.column9, 0);
                Vector3 point10 = new Vector3(18, d.column10, 0);
                Vector3 point11 = new Vector3(20, d.column11, 0);
                Vector3 point12 = new Vector3(22, d.column12, 0);

                p.SetPosition(0, point1);
                p.SetPosition(1, point2);
                p.SetPosition(2, point3);
                p.SetPosition(3, point4);
                p.SetPosition(4, point5);
                p.SetPosition(5, point6);
                p.SetPosition(6, point7);
                p.SetPosition(7, point8);
                p.SetPosition(8, point9);
                p.SetPosition(9, point10);
                p.SetPosition(10, point11);
                p.SetPosition(11, point12);

                p.material = new Material(Shader.Find("Sprites/Default"));


            if (d.column1 < 0.5 & d.column2 < 0.5f & d.column3 < 0.5f & d.column4 < 0.5f & d.column5 < 0.5f & d.column6 < 0.5f)
            {
                p.startColor = Color.green;
                p.endColor = Color.green;
            }
            else
            {
                p.startColor = Color.red;
                p.endColor = Color.red;
            }


             
                p.startWidth = 0.01f;
                p.endWidth = 0.01f;

        }


        //Drawing parallel axes here :


        for (int i = 0; i < datacounter.numberOfColumns ; i++)
        {

            Vector3 start = new Vector3(2 * i, -1, 0);
            Vector3 end = new Vector3(2 * i, +1, 0);
            LineRenderer line = new GameObject("Line").AddComponent<LineRenderer>();
            line.name = "Axis " + i.ToString();
            line.SetPosition(0, start);
            line.SetPosition(1, end);
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.gray;
            line.endColor = Color.gray;
            line.startWidth = 0.08f;
            line.endWidth = 0.08f;

            TextMesh ind = new GameObject("Text").AddComponent<TextMesh>();
            ind.text = index[i+1].ToString();
            ind.transform.position = new Vector3(2 * i-2, -2, 0);
            ind.transform.Rotate(0.0f, 0.0f, +30.0f, Space.Self);
            ind.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);



        }

        //Rendering a title with rotation, transform and scale
        TextMesh text = new GameObject("Text").AddComponent<TextMesh>();
        text.text = "Parallel Coordinates";
        text.transform.position = new Vector3(0, 7, 0);
        text.transform.Translate(new Vector3(0, -3, 0));

        Quaternion rotation = Quaternion.Euler(0, 0, 20);
        text.transform.rotation = rotation;

        text.transform.Rotate(0.0f, 0.0f, -20.0f, Space.Self);


        text.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        text.transform.localScale = new Vector3(0.9f, 1.2f, 1.3f);


        TextMesh lowerLimit = new GameObject("Text").AddComponent<TextMesh>();
        lowerLimit.text = "-1";
        lowerLimit.transform.position = new Vector3(23, -1, 0);
        lowerLimit.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
        lowerLimit.color = Color.red;

        TextMesh upperLimit = new GameObject("Text").AddComponent<TextMesh>();
        upperLimit.text = "+1";
        upperLimit.transform.position = new Vector3(23, +1, 0);
        upperLimit.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
        upperLimit.color = Color.green;

    }



}















