using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextAnimation : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    private List<int> wordIndexes;
    private List<int> wordLengths;
    Mesh mesh;
    Vector3[] vertices;
    private Color[] colors;
    public Gradient grad;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();
        string s = textMesh.text;
        for(int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }

        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    
    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        colors = mesh.colors;

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex+i];

                int charindex = c.vertexIndex;

                colors[charindex] = grad.Evaluate(Mathf.Repeat(Time.time + vertices[charindex].x*0.0001f,1f));
                colors[charindex + 1] = grad.Evaluate(Mathf.Repeat(Time.time + vertices[charindex+1].x * 0.0001f, 0.5f));
                colors[charindex + 2] = grad.Evaluate(Mathf.Repeat(Time.time + vertices[charindex+2].x * 0.0001f, 0.5f));
                colors[charindex + 3] = grad.Evaluate(Mathf.Repeat(Time.time + vertices[charindex+3].x * 0.0001f, 0.5f));

                vertices[charindex] += offset;
                vertices[charindex + 1] += offset;
                vertices[charindex + 2] += offset;
                vertices[charindex + 3] += offset;
            }
        }
        mesh.colors = colors;
        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    void WobbleChars()
    {
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }
    }
    void WobbleVerts()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] = vertices[i] + offset;
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time*10f), Mathf.Cos(time*5f));
    }
}
