using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RobotAgent : Agent
{
    [SerializeField, Tooltip("Target ul dorit")] Transform targetTransform;

    //articulatia pentru fiecare componenta
    [SerializeField] ArticulationBody part1;
    [SerializeField] ArticulationBody part2;
    [SerializeField] ArticulationBody part3;
    [SerializeField] ArticulationBody part4;
    [SerializeField] ArticulationBody part5;
    [SerializeField] ArticulationBody part6;

    [SerializeField,Tooltip("Viteza de rotatie")] float rotationSpeed;


    public override void OnEpisodeBegin()
    {
        part1.SetDriveTarget(ArticulationDriveAxis.Z, 0);
        part1.SetDriveTarget(ArticulationDriveAxis.X, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(part1.zDrive.target);
        sensor.AddObservation(part2.xDrive.target);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.ContinuousActions[0]);
        Debug.Log(actions.ContinuousActions[1]);

        part1.SetDriveTarget(ArticulationDriveAxis.Z, part1.zDrive.target + actions.ContinuousActions[0] + Time.deltaTime * rotationSpeed);
        part2.SetDriveTarget(ArticulationDriveAxis.X, part1.xDrive.target + actions.ContinuousActions[1] + Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent<Target>(out Target target))
        {
            SetReward(1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Obstacles>(out Obstacles obs))
        {
            SetReward(-1f);
            EndEpisode();
        }

    }

}
