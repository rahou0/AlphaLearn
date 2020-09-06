using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour
{
    ManageSystemHand msysHand;
    public BodySourceManager mBodySourceManager;
    public bool is2D;
    ViewPanel viewPAnel;
    public GameObject mJointObject;
    public GameObject mJointObjectRightHand;
    public GameObject mJointObjectLeftHand;
    public GameObject Spinnafkhi;
    void Start()
    {
        if(is2D)
            viewPAnel = GameObject.Find("ViewPanel").GetComponent<ViewPanel>();
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
            GameObject newJoint=null;
            if (joint.ToString() == "Head")
                newJoint = Instantiate(mJointObject);
            else
                if (joint.ToString() == "HandLeft")
                newJoint = Instantiate(mJointObjectLeftHand);
            else
                newJoint = Instantiate(mJointObjectRightHand);
            newJoint.name = joint.ToString();
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
                if (msysHand.IsOpenR != true)
                {
                    msysHand.IsOpenR = true;
                }
                break;
            case HandState.Closed:
                if (msysHand.IsOpenR != false)
                {
                    msysHand.IsOpenR = false;
                }
                break;
            default:
                break;
        }
        switch (body.HandLeftState)
        {
            case HandState.Open:
                if (msysHand.IsOpenL != true)
                {
                    msysHand.IsOpenL = true;
                }
                break;
            case HandState.Closed:
                if (msysHand.IsOpenL != false)
                {
                    msysHand.IsOpenL = false;
                }
                break;
            default:
                break;
        }
        // Update joints
        if (is2D)
        {
            foreach (JointType _joint in _joints)
            {
                // Get new target position
                Joint sourceJoint = body.Joints[_joint];
                Vector3 targetPosition;
                targetPosition = GetVector3FromJointFor2D(sourceJoint);
                targetPosition.z = 0;

                // Get joint, set new position
                Transform jointObject = bodyObject.transform.Find(_joint.ToString());
                jointObject.position = targetPosition;
            }
        }
        else
        {
            foreach (JointType _joint in _joints)
            {
                // Get new target position
                Joint sourceJoint = body.Joints[_joint];
                Vector3 targetPosition;
                
                    if (_joint.ToString() == "Head")
                        targetPosition = GetVector3FromJointHead(sourceJoint);
                    else
                        targetPosition = GetVector3FromJointForTest(sourceJoint);
                    //targetPosition.z = 0;

                    // Get joint, set new position
                    Transform jointObject = bodyObject.transform.Find(_joint.ToString());
                    jointObject.position = targetPosition;
                
                
            }
        }
       
    }

    private Vector3 GetVector3FromJoint(Joint joint)
    {
        Debug.Log(" X = "+ joint.Position.Z+  " Y = " + joint.Position.Y+" Z = " + joint.Position.Z);
        if (joint.Position.Y * 15 >= -1)
            return new Vector3(15-(-(joint.Position.X * 15)), 15-(joint.Position.Y * 15), 50-(joint.Position.Z * 50));
        else
            return new Vector3(joint.Position.X * 15, -1,joint.Position.Z * 50);
    }
    private Vector3 GetVector3FromJointHead(Joint joint)
    {
        //Debug.Log(" Y = " + joint.Position.Y);
        if(joint.Position.Z>1.5)
            return new Vector3(-joint.Position.X * 25, joint.Position.Y * 25, 40 );
        else
            return new Vector3(-joint.Position.X * 25, joint.Position.Y * 25, joint.Position.Z * 28);

    }
    private Vector3 GetVector3FromJointForTest(Joint joint)
    {
        //Debug.Log(" X = " + joint.Position.Z *25+ " Y = " + joint.Position.Y *25+ " Z = " + joint.Position.Z*25);
        return new Vector3(-joint.Position.X * 25, joint.Position.Y * 25, joint.Position.Z * 25);
    }
    private Vector3 GetVector3FromJointFor2D(Joint joint)
    {
        switch (viewPAnel.currentPanel)
        {
            case "Main Panel":
                return new Vector3(joint.Position.X * 400, joint.Position.Y * 400, joint.Position.Z * 25);
            case "Splash Screen":
                return new Vector3((joint.Position.X * 400)+452, (joint.Position.Y * 400)+ 225.5f, joint.Position.Z * 25);
            case "Missions Panel":
                return new Vector3((joint.Position.X * 400) -400, (joint.Position.Y * 400) + 730, joint.Position.Z * 25);
            case "Online Panel":
                return new Vector3((joint.Position.X * 400) + 400, (joint.Position.Y * 400) + 730, joint.Position.Z * 25);
            case "Armory Panel":
                return new Vector3((joint.Position.X * 400) + 1225, (joint.Position.Y * 400) + 730, joint.Position.Z * 25);
            case "About Panel":
                return new Vector3((joint.Position.X * 400)-925, joint.Position.Y * 400, joint.Position.Z * 25);
            case "Settings Panel":
                return new Vector3((joint.Position.X * 400) + 925, joint.Position.Y * 400, joint.Position.Z * 25);
            case "Exit Panel":
                return new Vector3(joint.Position.X * 400, (joint.Position.Y * 400)-600, joint.Position.Z * 25);
            case "Campaign Panel":
                return new Vector3((joint.Position.X * 400)-1225, (joint.Position.Y * 400)+730, joint.Position.Z * 25);
            default:
                return new Vector3(joint.Position.X * 400, joint.Position.Y * 400, joint.Position.Z * 25);
        }
    }
    public double LengthDistance(CameraSpacePoint point)
    {
        return Math.Sqrt(
            point.X * point.X +
            point.Y * point.Y +
            point.Z * point.Z
        );
    }

    public double Length(Joint p1, Joint p2)
    {
        return Math.Sqrt(
            Math.Pow(p1.Position.X - p2.Position.X, 2) +
            Math.Pow(p1.Position.Y - p2.Position.Y, 2) +
            Math.Pow(p1.Position.Z - p2.Position.Z, 2));
    }
    public double Length(params Joint[] joints)
    {
        double length = 0;

        for (int index = 0; index < joints.Length - 1; index++)
        {
            length += Length(joints[index], joints[index + 1]);
        }

        return length;
    }
    /*
    public int NumberOfTrackedJoints(params Joint[] joints)
    {
        int trackedJoints = 0;

        foreach (var joint in joints)
        {
            if (joint.TrackingState == JointTrackingState.Tracked)
            {
                trackedJoints++;
            }
        }

        return trackedJoints;
    }
    public double Height(this Body skeleton)
    {
        const double HEAD_DIVERGENCE = 0.1;

        var head = skeleton.Joints[JointType.Head];
        var neck = skeleton.Joints[JointType.ShoulderCenter];
        var spine = skeleton.Joints[JointType.Spine];
        var waist = skeleton.Joints[JointType.HipCenter];
        var hipLeft = skeleton.Joints[JointType.HipLeft];
        var hipRight = skeleton.Joints[JointType.HipRight];
        var kneeLeft = skeleton.Joints[JointType.KneeLeft];
        var kneeRight = skeleton.Joints[JointType.KneeRight];
        var ankleLeft = skeleton.Joints[JointType.AnkleLeft];
        var ankleRight = skeleton.Joints[JointType.AnkleRight];
        var footLeft = skeleton.Joints[JointType.FootLeft];
        var footRight = skeleton.Joints[JointType.FootRight];

        // Find which leg is tracked more accurately.
        int legLeftTrackedJoints = NumberOfTrackedJoints(hipLeft,
                                                         kneeLeft,
                                                         ankleLeft,
                                                         footLeft);
        int legRightTrackedJoints = NumberOfTrackedJoints(hipRight,
                                                          kneeRight,
                                                          ankleRight,
                                                          footRight);

        double legLength = legLeftTrackedJoints > legRightTrackedJoints ?
                           Length(hipLeft, kneeLeft, ankleLeft, footLeft) :
                           Length(hipRight, kneeRight, ankleRight, footRight);

        return Length(head, neck, spine, waist) + legLength + HEAD_DIVERGENCE;
    }*/
}
