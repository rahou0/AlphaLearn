using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour
{
    ManageSystemHand msysHand;
    public BodySourceManager mBodySourceManager;
    public Text LeftText;
    public Text RightText;
    public GameObject mJointObject;
    public GameObject mJointObjectRightHand;
    public GameObject mJointObjectLeftHand;
    void Start()
    {
        msysHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
    }
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    public List<JointType> _joints = new List<JointType>
    {
        JointType.Head,
        JointType.HandLeft,
        JointType.HandRight,
    };

    void Update()
    {
        #region Get Kinect data
        Body[] data = mBodySourceManager.GetData();
        if (data == null)
            return;

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
                continue;

            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
        }
        #endregion

        #region Delete Kinect bodies
        List<ulong> knownIds = new List<ulong>(mBodies.Keys);
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                // Destroy body object
                Destroy(mBodies[trackingId]);

                // Remove from list
                mBodies.Remove(trackingId);
            }
        }
        #endregion

        #region Create Kinect bodies
        foreach (var body in data)
        {
            // If no body, skip
            if (body == null)
                continue;

            if (body.IsTracked)
            {
                // If body isn't tracked, create body
                if (!mBodies.ContainsKey(body.TrackingId))
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);

                // Update positions
                UpdateBodyObject(body, mBodies[body.TrackingId]);
            }
        }
        #endregion
    }

    private GameObject CreateBodyObject(ulong id)
    {
        // Create body parent
        GameObject body = new GameObject("Body:" + id);

        // Create joints
        foreach (JointType joint in _joints)
        {
            // Create Object
            GameObject newJoint;
            if (joint.ToString() == "Head")
                newJoint = Instantiate(mJointObject);
            else
                if (joint.ToString() == "HandLeft")
                newJoint = Instantiate(mJointObjectLeftHand);
            else
                newJoint = Instantiate(mJointObjectRightHand);
            newJoint.name = joint.ToString();
            Debug.Log(newJoint.name);
            // Parent to body

            newJoint.transform.parent = body.transform;
        }

        return body;
    }

    private void UpdateBodyObject(Body body, GameObject bodyObject)
    {
        switch (body.HandRightState)
        {
            case HandState.Open:
                msysHand.IsOpenR = true;
                RightText.text = "RIGHT hand : opened";
                break;
            case HandState.Closed:
                msysHand.IsOpenR = false;
                RightText.text = "RIGHT hand : closed";
                break;
            default:
                break;
        }
        switch (body.HandLeftState)
        {
            case HandState.Open:
                msysHand.IsOpenL = true;
                LeftText.text = "LEFT hand : opened";
                break;
            case HandState.Closed:
                msysHand.IsOpenL = false;
                LeftText.text = "LEFT hand : closed";
                break;
            default:
                break;
        }
        // Update joints

        foreach (JointType _joint in _joints)
        {
            // Get new target position

            Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition;
             if (_joint.ToString() == "Head")
              targetPosition = GetVector3FromJointHead(sourceJoint);
             else
            targetPosition = GetVector3FromJoint(sourceJoint);
            //targetPosition.z = 0;

            // Get joint, set new position
            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
        }
    }

    private Vector3 GetVector3FromJoint(Joint joint)
    {
        if (joint.Position.Y * 15 >= -1)
            return new Vector3(-(joint.Position.X * 15), joint.Position.Y * 15, joint.Position.Z * 50);
        else
            return new Vector3(joint.Position.X * 15, -1,joint.Position.Z * 50);
    }
    private Vector3 GetVector3FromJointHead(Joint joint)
    {
        return new Vector3(joint.Position.X * 25, joint.Position.Y * 25, joint.Position.Z * 50 );
    }
}
