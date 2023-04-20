using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaTower : MonoBehaviour
{
    enum Mastery
    {
        Glass,
        Wood,
        Metal
    }
    [SerializeField]
    public List<Block> blocks = new List<Block>();

    [SerializeField]
    int blocksNumber;
    [SerializeField]
    float cubeHorOffset = 1.5f;
    [SerializeField]
    GameObject jengaBlockPrefab;
    int cubesPerLevel=3;
    [SerializeField]
    Material glassMat;
    [SerializeField]
    Material woodMat;
    [SerializeField]
    Material metalMat;

    List<GameObject> glassBlocks = new List<GameObject>();
    private void OnEnable()
    {
        TowersManager.onTowerBuilding += BuildTower;
    }

    private void OnDisable()
    {
        TowersManager.onTowerBuilding -= BuildTower;
    }
    public void BuildTower()
    {
        sortBlocks();
        Vector3 towerPosition = gameObject.transform.position;
        int height = blocks.Count / cubesPerLevel;
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j < cubesPerLevel; j++)
            {
                GameObject jengaBlock;
                if (i % 2 == 0)  // Rotate every other level
                {
                    Vector3 blockPosition = new Vector3(towerPosition.x + (j % 3) * cubeHorOffset, towerPosition.y + i, towerPosition.z + 1.5f);
                    jengaBlock = Instantiate(jengaBlockPrefab, blockPosition, Quaternion.identity);
                    jengaBlock.transform.SetParent(transform); // Set the parent of the block to the tower object
                }
                else
                {
                    Vector3 blockPosition = new Vector3(towerPosition.x + 1.5f, towerPosition.y + i, towerPosition.z + (j % 3) * cubeHorOffset);
                    jengaBlock = Instantiate(jengaBlockPrefab, blockPosition, Quaternion.identity);
                    jengaBlock.transform.Rotate(new Vector3(0, 90, 0));
                    jengaBlock.transform.SetParent(transform); // Set the parent of the block to the tower object
                }

                

                if (j + (i * cubesPerLevel) <= blocks.Count-1)
                {
                    jengaBlock.GetComponent<Block>().getData(blocks[j + (i * cubesPerLevel)]);

                    Renderer rend = jengaBlock.GetComponent<Renderer>();
                    BlockUI ui = jengaBlock.GetComponent<BlockUI>();
                    switch ((Mastery)blocks[j + (i * cubesPerLevel)].mastery)
                    {
                        case Mastery.Glass:
                            glassBlocks.Add(jengaBlock);
                            rend.material = glassMat;
                            ui.texts[0].text = "";
                            ui.texts[1].text = "";
                            break;
                        case Mastery.Wood:
                            rend.material = woodMat;
                            ui.texts[0].text = "Learned";
                            ui.texts[1].text = "Learned";
                            break;
                        case Mastery.Metal:
                            rend.material = metalMat;
                            ui.texts[0].text = "Mastered";
                            ui.texts[1].text = "Mastered";
                            break;
                    }
                }
                else
                {
                    Destroy(jengaBlock);
                }
            }
        }
    }

    void sortBlocks()
    {
        // Sort by domain
        for (int i = 0; i < blocks.Count; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < blocks.Count; j++)
            {
                if (blocks[j].domain[0] < blocks[minIndex].domain[0])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                Block temp = blocks[i];
                blocks[i] = blocks[minIndex];
                blocks[minIndex] = temp;
            }
        }
        // Sort by cluster
        for (int i = 0; i < blocks.Count; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < blocks.Count; j++)
            {
                if (blocks[j].cluster[0] < blocks[minIndex].cluster[0])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                Block temp = blocks[i];
                blocks[i] = blocks[minIndex];
                blocks[minIndex] = temp;
            }
        }

        // Sort by standardID
        for (int i = 0; i < blocks.Count; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < blocks.Count; j++)
            {
                if (blocks[j].standardid[0] < blocks[minIndex].standardid[0])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                Block temp = blocks[i];
                blocks[i] = blocks[minIndex];
                blocks[minIndex] = temp;
            }
        }
    }

}
