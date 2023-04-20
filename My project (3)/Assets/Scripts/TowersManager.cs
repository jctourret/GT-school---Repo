using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Cinemachine;

public class TowersManager : MonoBehaviour
{
    Block[] jengaBlocks;
    public static Action onTowerBuilding;
    [SerializeField]
    JengaTower[] towers;
    [SerializeField]
    Transform[] lookAtTargets;
    [SerializeField]
    Transform lookAt;
    [SerializeField]
    CinemachineFreeLook machineBrain;
    void Start()
    {
        StartCoroutine(GetData());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            lookAt.position = lookAtTargets[0].position;
            Debug.Log("A");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lookAt.position = lookAtTargets[1].position;
            Debug.Log("S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lookAt.position = lookAtTargets[2].position;
            Debug.Log("D");
        }
    }
    public void DistributeBlocksToTowers()
    {
        for(int i = 0; i < jengaBlocks.Length; i++)
        {
            if(jengaBlocks[i].mastery == 0)
            {
            }
            switch (jengaBlocks[i].grade)
            {
                case "6th Grade":
                    towers[0].blocks.Add(jengaBlocks[i]);
                    break;
                case "7th Grade":
                    towers[1].blocks.Add(jengaBlocks[i]);
                    break;
                case "8th Grade":
                    towers[2].blocks.Add(jengaBlocks[i]);
                    break;
            }
        }
    }
    
    IEnumerator GetData()
    {
        string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log("Request done");
            try
            {
                // Deserialize the JSON response into an array of strings
                jengaBlocks = JsonConvert.DeserializeObject<Block[]>(json);
                Debug.Log("Stack: " + string.Join(", ", jengaBlocks[0].mastery));
            }
            catch (JsonException e)
            {
                Debug.Log("Error parsing JSON: " + e.Message);
            }
        }
        DistributeBlocksToTowers();
        onTowerBuilding?.Invoke();
    }
}