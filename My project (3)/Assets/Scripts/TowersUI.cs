using System;
using UnityEngine;
using TMPro;

public class TowersUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gradeDomain;
    [SerializeField]
    TextMeshProUGUI cluster;
    [SerializeField]
    TextMeshProUGUI description;

    public static Action OnTestStack;
    private void OnEnable()
    {
        Raycast.OnBlockHit += GetBlockData;
    }

    private void OnDisable()
    {
        Raycast.OnBlockHit -= GetBlockData;
    }

    void GetBlockData(Block block)
    {
        gradeDomain.text = block.grade + ": " + block.domain;
        cluster.text = block.cluster;
        description.text = block.standarddescription;
    }

    public void TestStack()
    {
        OnTestStack?.Invoke();
    }
}
