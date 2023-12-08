using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private MovingCube _startCube;

    public MovingCube StartCube
    {
        get { return _startCube; }
    }


    public CubeSpawner CubeSpawner
    {
        get { return _cubepawner; }
    }

    [SerializeField]
    private CubeSpawner _cubepawner;

    protected override void Awake()
    {
        MovingCube.SetLastCube(_startCube);
    }





    private void Update()
    {
        if (Input.GetButtonDown(("Fire1")))
        {
            MovingCube.CurrentCube.Stop();
            _cubepawner.SpawnCube();
        }
    }

    public bool IsFailed(float hangOver)
    {
        if (Mathf.Abs(hangOver) >= MovingCube.LastCube.transform.localScale.z)
        {
            ReloadScene();
            return true;
        }


        return false;
    }


    public void ReloadScene()
    {
        MovingCube.ResetCube();
        SceneManager.LoadScene(0);
    }
}
