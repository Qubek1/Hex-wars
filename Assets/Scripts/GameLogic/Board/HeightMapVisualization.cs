using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class HeightMapVisualization : MonoBehaviour
{
    public HeightMapParameters parameters;
    public GameObject go;

    private HeightMapGenerator generator;

    public Gradient gradient;
    public bool button = true;

    private List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        generator = new HeightMapGenerator(parameters);
        generator.Generate();
        for (int x = -50; x < 50; x++)
        {
            for (int y = -50; y < 50; y++)
            {
                gameObjects.Add(Instantiate(go, new Vector3(0.1f * x, 0.1f * y, generator.GetHeightInPosition(new Vector2(x, y))), new Quaternion(0, 0, 0, 0)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (button)
        {
            button = false;
            foreach (GameObject a in gameObjects)
            {
                a.GetComponent<MeshRenderer>().material.color = gradient.Evaluate((a.transform.position.z - parameters.minHeight)/(parameters.maxHeight - parameters.minHeight));
            }
        }
    }
}
