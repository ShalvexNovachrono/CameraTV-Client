using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Camera MainCamera;

    public float speed = 2f;

    public Vector3 JumpVector;
    public float JumpForce = 1.1f;

    Rigidbody rigidbody;
    public Vector3 MovementDirection;

    private RaycastHit RaycastHitFront, RaycastHitBack, RaycastHitRight, RaycastHitLeft, RaycastHitUp, RaycastHitDown;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        JumpVector = new Vector3(0.0f, 1.0f, 0.0f);
        MainCamera.gameObject.transform.SetParent(transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        FrontRay();
        BackRay();
        RightRay();
        LeftRay();
        UpRay();
        DownRay();

        //float MovementInX = Input.GetAxis("Horizontal");
        //float MovementInZ = Input.GetAxis("Vertical");

        //MovementDirection = new Vector3(MovementInX, rigidbody.velocity.y, MovementInZ);
        //rigidbody.velocity = (MovementDirection * speed);
        
        if (isCollision[5] && Input.GetKeyDown(KeyCode.Space))
        {     
            JumpVector.z = rigidbody.velocity.z;
            JumpVector.x = rigidbody.velocity.x;
            rigidbody.AddForce(JumpVector * JumpForce, ForceMode.Impulse);
        }

    }


    public float defaultRaySize = 0.01f, maxRaySize = 0.1f, icreamentRaySizeBy = 0.01f; // defaultRaySize is Dectection Range, where collision can get detected
    public float[] currentRaySize = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
    // Index 0 = front, 1 = back, 2 = right, 3 = left, 4 = up , 5 = down
    public bool[] isCollision = {false, false, false, false, false, false};
    // Index 0 = front, 1 = back, 2 = right, 3 = left, 4 = up , 5 = down


    public void FrontRay()
    {
        int Index = 0;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, transform.forward * (currentRaySize[Index] * 10));
        Debug.DrawRay(transform.position, transform.forward * (currentRaySize[Index] * 10), Color.yellow);

        if (currentRaySize[Index] < maxRaySize)
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitFront, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index] = (RaycastHitFront.point.z - transform.position.z) / 10;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }

    public void BackRay()
    {
        int Index = 1;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, (transform.forward * (currentRaySize[Index] * 10)) * -1);
        Debug.DrawRay(transform.position, (transform.forward * (currentRaySize[Index] * 10)) * -1, Color.yellow);

        if (currentRaySize[Index] < maxRaySize)
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitBack, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index] = (RaycastHitBack.point.y - transform.position.y) / 10;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }

    public void RightRay()
    {
        int Index = 2;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, transform.right * (currentRaySize[Index] * 10));
        Debug.DrawRay(transform.position, transform.right * (currentRaySize[Index] * 10), Color.red);

        if (currentRaySize[Index] < maxRaySize)
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitRight, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index] = (RaycastHitRight.point.x - transform.position.x) / 10;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }

    public void LeftRay()
    {
        int Index = 3;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, (transform.right * (currentRaySize[Index] * 10)) * -1);
        Debug.DrawRay(transform.position, (transform.right * (currentRaySize[Index] * 10)) * -1, Color.red);

        if (currentRaySize[Index] < (maxRaySize))
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitLeft, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index] = (RaycastHitLeft.point.x - transform.position.x) / 10;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }

    public void UpRay()
    {
        int Index = 4;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, (transform.up * currentRaySize[Index] * 10));
        Debug.DrawRay(transform.position, (transform.up * currentRaySize[Index] * 10), Color.black);

        if (currentRaySize[Index] < maxRaySize)
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitUp, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index] = (RaycastHitUp.point.y - transform.position.y) / 10;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }

    public void DownRay()
    {
        int Index = 5;
        isCollision[Index] = false;
        Ray ray = new Ray(transform.position, (transform.up * (currentRaySize[Index] * 10)) * -1);
        Debug.DrawRay(transform.position, (transform.up * (currentRaySize[Index] * 10)) * -1, Color.black);

        if (currentRaySize[Index] < maxRaySize)
        {
            currentRaySize[Index] += icreamentRaySizeBy;
        }

        if (Physics.Raycast(ray, out RaycastHitDown, (currentRaySize[Index] * 10)))
        {
            currentRaySize[Index]-=icreamentRaySizeBy;
            if (currentRaySize[Index] < defaultRaySize)
            {
                isCollision[Index] = true;
            }
        }
    }
}
