using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.2f;
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }


    private void OnEnable()
    {
        if (LastCube == null)
        {
            Debug.Log(GameManager.Instance);
            SetLastCube(GameManager.Instance.StartCube);
        }

        CurrentCube = this;

        GetComponent<Renderer>().material.color = GetRandomCOlor();
        if (LastCube)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
        }
    }

    private Color GetRandomCOlor()
    {
        return new Color(Random.RandomRange(0, 1f), Random.RandomRange(0, 1f), Random.RandomRange(0, 1f));
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _moveSpeed;

    }

    public void Stop()
    {

        _moveSpeed = 0;

        float hangOver = transform.position.z - LastCube.transform.position.z;


        if (!GameManager.Instance.IsFailed(hangOver))
        {

            float dir = hangOver > 0 ? 1f : -1f;

            SplitCubeOnZ(hangOver, dir);

            SetLastCube(this);
        }


    }

    private void SplitCubeOnZ(float hangOver, float dir)
    {

        float newZSize = LastCube.transform.localScale.z - Mathf.Abs((hangOver));
        float fallingBlockSize = transform.localScale.z - newZSize;


        float newZPosition = LastCube.transform.position.z + (hangOver / 2);


        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);


        float cubeEdge = transform.position.z + (newZSize / 2) * dir;

        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2 * dir;

        SpawnFallingCube(fallingBlockZPosition, fallingBlockSize);


    }

    private void SpawnFallingCube(float zPos, float size)
    {

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, size);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;


        Destroy(cube.gameObject, 1f);

    }

    public static void SetLastCube(MovingCube cube)
    {
        LastCube = cube;
    }


    public static void ResetCube()
    {
        CurrentCube = null;
        LastCube = null;
    }

}
