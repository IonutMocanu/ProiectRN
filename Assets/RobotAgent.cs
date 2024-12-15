using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RobotAgent : Agent
{
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    [SerializeField, Tooltip("Target ul dorit")] Transform targetTransform;

    //articulatia pentru fiecare componenta
    [SerializeField] ArticulationBody part1;
    [SerializeField] ArticulationBody part2;
    [SerializeField] ArticulationBody part3;
    [SerializeField] ArticulationBody part4;
    [SerializeField] ArticulationBody part5;
    [SerializeField] ArticulationBody part6;

    [SerializeField,Tooltip("Viteza de rotatie")] float rotationSpeed;


    //public bool succes;
    //public bool notSucces;


    //public void Awake()
    //{
    //    succes = false;
    //    notSucces = false;
    //}

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("A atins ceva");
        if (other.TryGetComponent<Target>(out Target target))
        {
            SetReward(1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Obstacles>(out Obstacles obs))
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("A atins ceva");
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("A inceput alt episod");
        part1.SetDriveTarget(ArticulationDriveAxis.Z, 0);
        part2.SetDriveTarget(ArticulationDriveAxis.X, 0);
        part3.SetDriveTarget(ArticulationDriveAxis.X, 0);
        part4.SetDriveTarget(ArticulationDriveAxis.Z, 0);
        part5.SetDriveTarget(ArticulationDriveAxis.X, 0);
        part6.SetDriveTarget(ArticulationDriveAxis.Z, 0);
        targetTransform.localPosition = new Vector3(Random.Range(-0.45f,0.45f), Random.Range(1.657f, 2f), Random.Range(-0.45f, 0.45f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(part1.zDrive.target);
        sensor.AddObservation(part2.xDrive.target);
        sensor.AddObservation(part3.xDrive.target);
        sensor.AddObservation(part4.zDrive.target);
        sensor.AddObservation(part5.xDrive.target);
        sensor.AddObservation(part6.zDrive.target);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.ContinuousActions[0]);
        //Debug.Log(actions.ContinuousActions[1]);

        part1.SetDriveTarget(ArticulationDriveAxis.Z, part1.zDrive.target + actions.ContinuousActions[0] * Time.deltaTime * rotationSpeed);
        part2.SetDriveTarget(ArticulationDriveAxis.X, part2.xDrive.target + actions.ContinuousActions[1] * Time.deltaTime * rotationSpeed);
        part3.SetDriveTarget(ArticulationDriveAxis.X, part3.xDrive.target + actions.ContinuousActions[2] * Time.deltaTime * rotationSpeed);
        part4.SetDriveTarget(ArticulationDriveAxis.Z, part4.zDrive.target + actions.ContinuousActions[3] * Time.deltaTime * rotationSpeed);
        part5.SetDriveTarget(ArticulationDriveAxis.X, part5.xDrive.target + actions.ContinuousActions[4] * Time.deltaTime * rotationSpeed);
        part6.SetDriveTarget(ArticulationDriveAxis.Z, part6.zDrive.target + actions.ContinuousActions[5] * Time.deltaTime * rotationSpeed);

    }

    public void HandleChildCollision(bool succes)
    {
        if (succes)
        {
            SetReward(1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        else 
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

}
