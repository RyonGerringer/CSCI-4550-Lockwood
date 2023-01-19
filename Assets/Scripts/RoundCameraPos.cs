using UnityEngine;
using Cinemachine;

// Must import Cinemachine and inherit from CinemachineExtension to write an extension component
public class RoundCameraPos : CinemachineExtension
{
    public float PixelsPerUnit = 32;

    // Required by all CinemachineExtension classes, and called after the Confiner component is finished processing
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // Determine if we are in the 'Body' stage of the camera's post-processing
        if (stage == CinemachineCore.Stage.Body)
        {
            // Get the final camera position as a 3D vector
            Vector3 pos = state.FinalPosition;

            // Create a new vector with the new, rounded, pixel-bounded position
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);

            // Set the new position to the difference in the old and the rounded one
            state.PositionCorrection += pos2 - pos;
        }
    }

    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
