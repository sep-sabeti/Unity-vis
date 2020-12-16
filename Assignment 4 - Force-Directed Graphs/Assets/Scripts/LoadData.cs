using JetBrains.Annotations;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class LoadData : MonoBehaviour
{
    List<Node> Nodes;
    List<int> NodeIndexes;
    int iter = 0;
    // Start is called before the first frame update
    void Start()
    {
         Nodes = DataLoading();
        NodeIndexes = getCreatedNodesIndexes(Nodes);

        makePlot(Nodes);
    }

    // Update is called once per frame
    void Update()
    {

        if (iter < 10)
        {
            iter++;
            Debug.Log(iter);
            foreach (Node node in Nodes)
            {
                appliedForce(node, NodeIndexes, Nodes);

                GameObject.Find("Node " + node.getNodeName().ToString()).transform.localPosition = node.getPosition();

                for (int i = 0; i <= node.getChildren().Count - 1; i++)
                {
                    List<Node> childrenToBeRendered = node.getChildren();
                    string lineName = "Line " + node.getNodeName();
                    LineRenderer p = GameObject.Find(lineName).GetComponent<LineRenderer>();
                    p.SetPosition(0, node.getPosition());
                    p.SetPosition(1, childrenToBeRendered[i].getPosition());
                }
            }
        }
     


    }

    //Getting the indexes of the created nodes
    public List<int> getCreatedNodesIndexes(List<Node> nodes)
    {
        List<int> nodeIndexes = new List<int>();
        foreach(Node node in nodes)
        {
            nodeIndexes.Add(node.getNodeName());
        }
        return nodeIndexes;
    }


    //creating a nodeline graph based on a given adjancey matrix

    public void makePlot(List<Node> Nodes)
    {
        int lineNumber = 0;

        foreach (Node node in Nodes)
        {
            //rendering spheres and connections 
            var gameobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameobject.name = "Node " + node.getNodeName().ToString();
            gameobject.transform.localPosition = node.getPosition();
            gameobject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            for (int i = 0; i <= node.getChildren().Count - 1; i++)
            {
                List<Node> childrenToBeRendered = node.getChildren();
                LineRenderer p = new GameObject("Line").AddComponent<LineRenderer>();
                p.name = "Line " + lineNumber.ToString();
                p.SetPosition(0, node.getPosition());
                p.SetPosition(1, childrenToBeRendered[i].getPosition());
                p.startWidth = 0.2f;
                p.endWidth = 0.2f;
                p.startColor = Color.red;
                p.endColor = Color.blue;
                lineNumber++;
            }

        }
    }

    // Creating class node that contains main features of a node including its adjancy list
    public class Node
    {
        float Energy;
        Vector3 EnergyDirection ;

        public Vector3 position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f),1);
        public List<Node> children = new List<Node>();
        public List<Node> disconnectedNodes = new List<Node>();
        public List<Vector3> fAttraction = new List<Vector3>();
        public List<Vector3> fRepel = new List<Vector3>();
         public int nodeName;

        public void addNodeToChildren(Node node)
        {
            children.Add(node);
        }
        public List<Node> getChildren()
        {
            return children;
        }
        public void setNodeName(int name)
        {
            nodeName = name;
        }
        public int getNodeName()
        {
            return nodeName;
        }

        public Vector3 getPosition()
        {
            return position;
        }

        public void setPosition(Vector3 Position)
        {
            this.position = Position;
        }

        public void setPositionAfterForce(Vector3 Position)
        {
            this.position = this.position + Position;
        }

        public List<Node> getDisconnectedNodes(List<int> Nodes , List<Node> CreatedNodes)
        {
            List<int> childrenNodeIndices = new List<int>();
            List<int> DisconnectedNodeIndices = new List<int>();
            List<Node> DisconnectedNode = new List<Node>();

            foreach (Node node in children)
            {
                childrenNodeIndices.Add(node.getNodeName());
            }

            foreach (int i in Nodes)
            {
                if(!childrenNodeIndices.Contains(i))
                {
                    DisconnectedNodeIndices.Add(i);
                }
            }

            foreach(Node node in CreatedNodes)
            {
                foreach(int i in DisconnectedNodeIndices)
                {
                    if(i == node.getNodeName())
                    {
                        DisconnectedNode.Add(node);
                    }
                }
            }

                       return DisconnectedNode;

        }

        public void setEnergy(float energy)
        {
            Energy = energy;
        }

        public float getEnergy()
        {
            return Energy ;
        }

        public void setEnergyDirection(Vector3 energyDirection)
        {
            EnergyDirection = energyDirection;
        }

        public Vector3 getEnergyDirection()
        {
            return EnergyDirection;
        }
    }


    public void appliedForce(Node node, List<int> Nodes, List<Node> CreatedNoeds)

    {
        Vector3 currentNodePosition = node.getPosition();
        if (currentNodePosition.magnitude < 10)
        {
            foreach (Node connectedNode in node.getChildren())
            {
               
                var distance = currentNodePosition - connectedNode.getPosition();
                if(distance.magnitude < 10)
                {
                    var attractionForce = Mathf.Log10(distance.magnitude / 1);
                    currentNodePosition = currentNodePosition + attractionForce * distance.normalized;
                }
               
            }

            
        }

        List<Node> DisconnectedNode = node.getDisconnectedNodes(Nodes, CreatedNoeds);
        foreach (Node disconnectedNode in DisconnectedNode)
        {
            if (currentNodePosition.magnitude < 10)
            {
                var distance = currentNodePosition - disconnectedNode.getPosition();
                if(distance.magnitude <= 10)
                {
                    var repelForce = 1 / Mathf.Pow(distance.magnitude , 2) ;
                    currentNodePosition = currentNodePosition - repelForce * distance.normalized;
                }
                
            }
        }
        node.setPositionAfterForce(currentNodePosition);
        Debug.Log(currentNodePosition);
    }


    //
    // A function to find a node within a list of nodes
    public Node findNode(int index, List<Node> createdNodes)
    {
        bool nodeFound = false;
        int j = 0;
        Node nodeToBeAdded = null;
        while (!nodeFound && j <= createdNodes.Count)
        {
            if (createdNodes[j].getNodeName() == index)
            {
                nodeToBeAdded = createdNodes[j];
                nodeFound = true;
            }
            j++;
        }

        return nodeToBeAdded;

    }

    //Loading data and creating Node object and adjancy lits
    public List<Node> DataLoading()
    {
        string fileDirectory = "Data";
        List<int> createdNodes = new List<int>();
        List<Node> createdNodesInNode = new List<Node>();


 
        TextAsset file = Resources.Load<TextAsset>(fileDirectory);
        string loadedData = file.ToString();
        string[] rows = loadedData.Split('\n');

        if(rows != null)
        {
            for (int i = 0; i < rows.Length-1; i++)
            {
                string[] row = rows[i].Split(',');

                int.TryParse( row[0],out int nodeIndex);
                int.TryParse(row[1], out int nodeIndex2);

                if (!createdNodes.Contains(nodeIndex))
                {
                    createdNodes.Add(nodeIndex);
                    Node Node1 = new Node();
                    Node1.setNodeName(nodeIndex);
                    createdNodesInNode.Add(Node1);

                    if (!createdNodes.Contains(nodeIndex2))
                    {
                        createdNodes.Add(nodeIndex2);
                        Node Node2 = new Node();
                        Node2.setNodeName(nodeIndex2);
                        createdNodesInNode.Add(Node2);
                        Node1.addNodeToChildren(Node2);
                    } else
                    {
                        Node1.addNodeToChildren(findNode(nodeIndex2, createdNodesInNode));
                    }
                }
                else
                {
                    if (!createdNodes.Contains(nodeIndex2))
                    {
                        createdNodes.Add(nodeIndex2);
                        Node Node2 = new Node();
                        Node2.setNodeName(nodeIndex2);
                        createdNodesInNode.Add(Node2);

                        Node nodeToBeFound = findNode(nodeIndex2, createdNodesInNode);
                        nodeToBeFound.addNodeToChildren(Node2);
                    }
                    else
                    {
                        Node nodeToBeFound = findNode(nodeIndex, createdNodesInNode);
                        Node nodeToBeAdded = findNode(nodeIndex2, createdNodesInNode);
                        nodeToBeFound.addNodeToChildren(nodeToBeAdded);
                    }

                }
            }

        }

 

        return createdNodesInNode;

    }
}
