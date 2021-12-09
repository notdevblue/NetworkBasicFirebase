using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


[ExecuteInEditMode]
public class EditManager : MonoBehaviour
{
    static EditManager instance;

#if UNITY_EDITOR

    [MenuItem("Stage/Save/Save as txt")]

    static void SaveAs()
    {
        Debug.Log("Saving current edit...");
        if (instance)
        {
            string[] filters = { "Text", "txt", "Json", "json", "All Files", "*" };
            var path = EditorUtility.SaveFilePanel(
                "Save Stage as txt",
                "",
                "stage0000.txt",
                "txt"
            );

            if (path.Length != 0)
            {
                string str = instance.StringfyStage();
                File.WriteAllText(path, str);
            }
        }
    }

    [MenuItem("Stage/Load/Load txt")]
    static void Load()
    {
        Debug.Log("Load edit...");
        if (instance)
        {
            var path = EditorUtility.OpenFilePanel(
                "Load form txt",
                "",
                ""
            );

            if (path.Length != 0)
            {
                string str = File.ReadAllText(path);
                instance.Parse(str);
            }
        }
    }

    private string StringfyStage()
    {
        string result = "";
        GameObject stage = GameObject.Find("Stage");

        if (stage != null)
        {
            result += StringfyNode(stage, 0);
            result += System.Environment.NewLine;
        }

        return result;
    }

    private string SetIndent(int indent)
    {
        string result = "";
        for (int j = 0; j < indent; ++j)
        {
            result += "  ";
        }

        return result;
    }

    private string StringfyNode(GameObject node, int indent = 1)
    {
        string result = SetIndent(indent) + "{";
        result += "name:" + node.transform.name + ", data:[" + System.Environment.NewLine;

        for (int i = 0; i < node.transform.childCount; ++i)
        {
            Transform tr = node.transform.GetChild(i);
            SpriteRenderer sr = tr.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (i > 0)
                {
                    result += "," + System.Environment.NewLine;
                }
                result += SetIndent(indent + 1);
                result += "{name:" + tr.name + ",sprite:" + sr.sprite.name + ",x:" + tr.position.x + ",y:" + tr.position.y + "}";
            }
            else
            {
                result += StringfyNode(tr.gameObject, indent + 1);
            }
        }

        result += "]}";

        return result;
    }


    private void Parse(string str)
    {
        int idx = 0;
        ParseNode(str, idx);
    }

    public bool ParseNode(string str, int idx)
    {
        int st = str.IndexOf('{', idx);
        if (st >= idx)
        {
            int nextSt = str.IndexOf('{', idx + 1);
            if (nextSt >= idx + 1)
            {
                ParseNode(str, nextSt);
            }
            else
            {
                int endSt = str.IndexOf('}', idx + 1);
                if (endSt >= idx + 1)
                {
                    string nodeStr = str.Substring(idx + 1, endSt - st - 1);
                    string[] data = nodeStr.Split(',');

                    for (int i = 0; i < data.Length; ++i)
                    {
                        ParseNode(data[i], 0);
                    }
                }
                else
                {
                    Debug.LogError("Parse err");
                }
            }
        }
        else
        {
            Debug.Log(str);
        }

        return true;
    }

    private void Start() 
    {
        instance = this;
    }


#endif

}
