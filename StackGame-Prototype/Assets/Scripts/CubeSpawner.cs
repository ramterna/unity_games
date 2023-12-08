using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube _cubePrefab;


    public void SpawnCube()
    {
        var cube = Instantiate(_cubePrefab);

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameManager.Instance.StartCube.gameObject)
        {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.transform.position.y + _cubePrefab.transform.localScale.y, transform.position.z);

        }
        else
        {
            cube.transform.position = transform.position;
        }

    }
}
