using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorGameObject
{
    [SerializeField] private Transform bricksCollectedPosition;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;
    [SerializeField] private BrickChar brickChar;
    [SerializeField] private float vector;
    [SerializeField] public LayerMask brickStairLayer;
    //[SerializeField] private float gravityScale;

    public List<BrickChar> brickCharList = new List<BrickChar>();

    //private List<BrickChar> bricksToRemove = new List<BrickChar>();

    //private bool isOnBridge = false;
    private Rigidbody rigidbody;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        //rigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        DistributeBricks();
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        ChangeColor((ColorType)Random.Range(1, 5));
    }


    public virtual void Move()
    {

    }
    public void AddBrick()
    {
        BrickChar brickCollected = Instantiate(brickChar, bricksCollectedPosition);
        brickCollected.ChangeColor(this.colorType);
        brickCollected.transform.localPosition = new Vector3(0, vector * brickCharList.Count, 0);
        brickCharList.Add(brickCollected);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "brickGround")
        {
            if (other.gameObject.GetComponent<BrickGround>().colorType == this.colorType)
            {
                AddBrick();
                Destroy(other.gameObject);
            }
        }
    }

    public void DistributeBricks()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0f, 0.5f), Vector3.down, out hit, 5f, brickStairLayer))
        {
            Debug.DrawRay(transform.position + new Vector3(0f, 0f, 0.25f), Vector3.down * 5f, Color.red);
            if (brickCharList.Count > 0)
            {
                BrickStair brickStair = hit.collider.gameObject.GetComponent<BrickStair>();
                if (brickStair.colorType != this.colorType) // de cho raycast k kich hoat lien tuc code -> dan den xoa het gach collected
                {
                    brickStair.GetComponent<Collider>().isTrigger = false;
                    brickStair.colorType = this.colorType;
                    brickStair.ChangeColor(this.colorType);
                    brickStair.GetComponent<Renderer>().enabled = true;

                    //bricksToRemove.Add(brickCharList[brickCharList.Count - 1]);
                    //Debug.Log(brickStair.colorType);
                    Destroy(brickCharList[brickCharList.Count - 1].gameObject);
                    brickCharList.RemoveAt(brickCharList.Count - 1);
                }

            }

        }
    }

    public bool CanMoveForward(float vectorZ)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0f, 0.5f), Vector3.down, out hit, 5f, brickStairLayer)) //Check xem co dang tren cau hay la dung truoc cau hay k
        {
            BrickStair brickStair = hit.collider.gameObject.GetComponent<BrickStair>();

            if (vectorZ > 0f && brickCharList.Count == 0 && brickStair.colorType != this.colorType)
            {
                return false;
            }
            else if (vectorZ > 0f && brickCharList.Count == 0 && brickStair.colorType == this.colorType)
            {
                return true;
            }
        }
        return true;
    }

}
