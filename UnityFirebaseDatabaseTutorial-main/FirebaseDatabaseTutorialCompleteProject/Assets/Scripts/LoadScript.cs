using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    FirebaseManager fm;

    private Dictionary<string, GameObject> dictPrefabs;
    private Dictionary<string, Sprite> dictSprites;
    public List<GameObject> listPrefabs;
    public GameObject titlePrefab;
    private Sprite[] titleSprites;

    private void Start()
    {
        fm = GameObject.Find("FirebaseManager").GetComponent<FirebaseManager>();

        dictPrefabs = new Dictionary<string, GameObject>();
        foreach (var obj in listPrefabs)
        {
            dictPrefabs[obj.name] = obj;
        }

        titleSprites = Resources.LoadAll<Sprite>("Platforms");
        dictSprites = new Dictionary<string, Sprite>();
        foreach (var spr in titleSprites)
        {
            dictSprites.Add(spr.name, spr);
        }

        NodeClass stage = new NodeClass();
        stage = JsonUtility.FromJson<NodeClass>(fm.stageData);
        ParseNode(stage);
    }

    public void ParseNode(NodeClass node, GameObject parent = null)
    {
        if (parent == null)
        {
            parent = new GameObject("NewStage");
        }
        if (node != null)
        {
            if (node.data != null && node.data.Count > 0)
            {
                GameObject curObj;

                curObj = MakeNode(node, parent);

                foreach (var child in node.data)
                {
                    ParseNode(child as NodeClass, curObj);
                }
            }
            else
            {
                MakeNode(node, parent);
            }
        }
    }

    private GameObject MakeNode(NodeClass node, GameObject parent)
    {
        GameObject obj = null;

        if (parent != null)
        {
            if (dictPrefabs.ContainsKey(node.name))
            {
                obj = Instantiate(dictPrefabs[node.name]);
                obj.transform.position = new Vector3(node.x, node.y);
                obj.transform.SetParent(parent.transform);
            }
            else
            {
                if (node.sprite != null)
                {
                    obj = MakeSpriteNode(node, parent);
                }
                else
                {
                    obj = new GameObject(node.name);
                    obj.transform.SetParent(parent.transform);
                }
            }
        }
        return obj;
    }

    private GameObject MakeSpriteNode(NodeClass node, GameObject parent)
    {
        GameObject obj = null;

        if (parent != null)
        {
            obj = Instantiate(titlePrefab);

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            obj.name = node.name;
            if (dictSprites.ContainsKey(node.sprite))
            {
                sr.sprite = dictSprites[node.sprite];
            }
            else
            {
                print($"Sprite not found: {node.sprite}");
            }
            obj.transform.SetParent(parent.transform);
            obj.transform.position = new Vector2(node.x, node.y);
        }

        return obj;
    }
}
