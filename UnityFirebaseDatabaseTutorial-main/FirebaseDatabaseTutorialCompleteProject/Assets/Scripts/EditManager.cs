using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class NodeClass
{
    public string name;
    public string sprite;
    public float x;
    public float y;

    public List<NodeClass> data;
}

[ExecuteInEditMode]
public class EditManager : MonoBehaviour
{
    private static EditManager instance;

    private Dictionary<string, GameObject> dictPrefabs;
    private Dictionary<string, Sprite> dictSprites;
    public List<GameObject> listPrefabs;
    public GameObject tilePrefab;
    private Sprite[] tileSprites;

#if UNITY_EDITOR
    [MenuItem("Stage/Save/Save as Txt")]
    static void SaveAs()
    {
        print("Saving current edit..");
        if (instance)
        {
            string[] filters = { "Text", "txt", "json", "All Files", "*" };
            var path = EditorUtility.SaveFilePanel("Save Stage as txt", "", "stage0000.txt", "txt");
            if (path.Length != 0)
            {
                string str = instance.StringfyStage();
                File.WriteAllText(path, str);
            }
        }
    }

    [MenuItem("Stage/Load/Load Txt")]
    static void Load()
    {
        print("Load edit..");
        if (instance)
        {
            var path = EditorUtility.OpenFilePanel("Load from txt", "", "txt");
            if (path.Length != 0)
            {
                string str = File.ReadAllText(path);
                NodeClass stage = new NodeClass();
                stage = JsonUtility.FromJson<NodeClass>(str);
                instance.ParseNode(stage);
            }
        }
    }
    public string StringfyStage()
    {
        string result = "";
        GameObject obj = GameObject.Find("Stage");
        if (obj != null)
        {
            result += StringfyNode(obj, 0);
        }
        return result;
    }


    private string SetIndent(int indent)
    {
        string result = "";
        for (int i = 0; i < indent; i++)
        {
            result += " ";
        }
        return result;
    }

    public string StringfyNode(GameObject node, int indent = 1)
    {
        string result = SetIndent(indent) + "{";
        result += "\"name\" :\"" + node.transform.name + "\", \"data\":[\n";

        for (int i = 0; i < node.transform.childCount; i++)
        {
            Transform tr = node.transform.GetChild(i);
            SpriteRenderer sr = tr.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (i > 0)
                {
                    result += ",\n";
                }
                result += SetIndent(indent + 1);
                result += $"{{\"name\":\"{tr.name}\", \"sprite\":\"{sr.sprite.name}\", \"x\":\"{tr.position.x}\", \"y\":\"{tr.position.y}\"}}";
            }
            else
            {
                result += StringfyNode(tr.gameObject, indent + 1);
            }
        }
        result += "\n" + SetIndent(indent) + "]}";

        return result;
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
            obj = Instantiate(tilePrefab);

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

    private void Start()
    {
        instance = this;

        dictPrefabs = new Dictionary<string, GameObject>();
        foreach (var obj in listPrefabs)
        {
            dictPrefabs[obj.name] = obj;
        }

        tileSprites = Resources.LoadAll<Sprite>("Platforms");
        dictSprites = new Dictionary<string, Sprite>();
        foreach (var spr in tileSprites)
        {
            dictSprites.Add(spr.name, spr);
        }
    }
#endif
}