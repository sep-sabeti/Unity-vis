using UnityEngine;

public class MouseEffect : MonoBehaviour
{


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    LineRenderer p = GameObject.Find(hit.transform.gameObject.name).GetComponent<LineRenderer>();
                    
                    if (p.startColor == Color.red | p.startColor == Color.green)
                    {
                        GameObject.Find(hit.transform.gameObject.name).GetComponent<LineRenderer>().startColor = Color.yellow;
                        GameObject.Find(hit.transform.gameObject.name).GetComponent<LineRenderer>().endColor = Color.yellow;
                        TextMesh text = new GameObject("Text for line").AddComponent<TextMesh>();
                        Vector3[] positions = new Vector3[p.positionCount];
                        p.GetPositions(positions);
                        float sum = 0;
                        for(int i = 0; i < p.positionCount; i++)
                        {
                            sum += positions[i].y;
                        }
                        float average = sum / p.positionCount;

                        text.text = average.ToString();
                        text.transform.position = hit.point;
                        text.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
                        text.color = Color.red;

                    }
                    else
                    {
                        Vector3[] positions = new Vector3[p.positionCount];
                        p.GetPositions(positions);
                        Destroy(GameObject.Find("Text for line"));

                        if (positions[0].y < 0.5 & positions[1].y < 0.5f & positions[2].y < 0.5f & positions[3].y < 0.5f & positions[4].y < 0.5f & positions[5].y < 0.5f)
                        {
                            p.startColor = Color.green;
                            p.endColor = Color.green;

                        }
                        else
                        {
                            p.startColor = Color.red;
                            p.endColor = Color.red;
                        }
                    }
                }





            }

        }

    }


}