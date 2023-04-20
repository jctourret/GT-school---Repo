using UnityEngine;
public class Block : MonoBehaviour, IReadable
{
    Rigidbody rb;
    public int id { get; set; }
    public string subject { get; set; }
    public string grade { get; set; }
    public int mastery { get; set; }
    public string domainid { get; set; }
    public string domain { get; set; }
    public string cluster { get; set; }
    public string standardid { get; set; }
    public string standarddescription { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        TowersUI.OnTestStack += TestBlock;
    }

    private void OnDisable()
    {
        TowersUI.OnTestStack -= TestBlock;
    }
    public void getData(Block block)
    {
        id = block.id;
        subject = block.subject;
        grade = block.grade;
        mastery = block.mastery;
        domainid = block.domainid;
        domain = block.domain;
        cluster = block.cluster;
        standardid = block.standardid;
        standarddescription = block.standarddescription;
    }

    void TestBlock()
    {
        rb.isKinematic = false;
        if(mastery == 0)
        {
            Destroy(gameObject);
        }
    }
}

