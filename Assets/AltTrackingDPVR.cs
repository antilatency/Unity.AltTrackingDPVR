using Antilatency.Alt.Tracking;
using Antilatency.DeviceNetwork;
using Antilatency.SDK;
using UnityEngine;
using UnityEngine.Events;

public class AltTrackingDPVR : AltTracking
{
    public Transform DpvrTrackingSpace;
    public float MinimalAQualityToAlign = 0.01f;

    private Transform _bSpace;
    private Transform _b;

    private Antilatency.TrackingAlignment.ILibrary _alignmentLibrary;
    private Antilatency.TrackingAlignment.ITrackingAlignment _alignment;

    private Quaternion _initialPlacementRotation;

    protected override NodeHandle GetAvailableTrackingNode()
    {
        return GetFirstIdleTrackerNode();
    }

    protected override void Awake()
    {
        base.Awake();
        _alignmentLibrary = Antilatency.TrackingAlignment.Library.load();

        _bSpace = transform;
        _b = DpvrTrackingSpace.transform;
    }

    protected override Pose GetPlacement()
    {
        var result = Pose.identity;

        using (var localStorage = StorageClient.GetLocalStorage())
        {

            if (localStorage == null)
            {
                return result;
            }

            var placementCode = localStorage.read("placement", "default");

            if (string.IsNullOrEmpty(placementCode))
            {
                Debug.LogError("Failed to get placement code");
            }
            else
            {
                result = _trackingLibrary.createPlacement(placementCode);
            }

            _initialPlacementRotation = result.rotation;
            StartTrackingAlignment();
            return result;
        }
    }

    protected virtual void OnFocusChanged(bool focus)
    {
        if (focus)
        {
            StartTrackingAlignment();
        }
        else
        {
            StopTrackingAlignment();
        }
    }

    private void StartTrackingAlignment()
    {
        if (_alignment != null)
        {
            StopTrackingAlignment();
        }
        _alignment = _alignmentLibrary.createTrackingAlignment(_initialPlacementRotation, ExtrapolationTime);
    }

    private void StopTrackingAlignment()
    {
        if (_alignment == null)
        {
            return;
        }

        _alignment.Dispose();
        _alignment = null;
    }

    private void OnApplicationFocus(bool focus)
    {
        OnFocusChanged(focus);
    }

    private void OnApplicationPause(bool pause)
    {
        OnFocusChanged(!pause);
    }

    protected override void Update()
    {
        base.Update();
        if (!TrackingTaskState)
        {
            return;
        }

        Quaternion bRotation = _b.localRotation;

        var curTime = Time.realtimeSinceStartup;

        if (!GetRawTrackingState(out State rawTrackingState))
        {
            _bSpace.localRotation = Quaternion.identity;
            _bSpace.localPosition = Vector3.zero;
            return;
        }

        if (_alignment != null &&
            rawTrackingState.stability.stage == Stage.Tracking6Dof &&
            rawTrackingState.stability.value >= MinimalAQualityToAlign)
        {
            var result = _alignment.update(rawTrackingState.pose.rotation, bRotation, curTime);

            ExtrapolationTime = result.timeBAheadOfA;
            _placement.rotation = result.rotationARelativeToB;
            _bSpace.localRotation = result.rotationBSpace;
        }
        else
        {
            _bSpace.localRotation = Quaternion.identity;
        }

        if (!GetTrackingState(out State trackingState))
        {
            _bSpace.localPosition = Vector3.zero;
            return;
        }

        _bSpace.localPosition = trackingState.pose.position;
    }
}
