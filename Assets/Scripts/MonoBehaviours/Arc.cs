using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    //Coroutine to move gameObject 
    //Final destination is the end position of the frame
    //Duration will be the amount of time to move gameObject from start to finish position
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        //Find current gameObject position
        Vector3 startPosition = transform.position;

        float percentComplete = 0.0f;

        //Check if percentComplete is <100%
        while (percentComplete < 1.0f)
        {
            //Time taken since last frame / total desired duration = percentage of duration
            percentComplete += Time.deltaTime / duration;

            //Linear Interpolation to have object move smoothly and at a consistent speed
            //Same amount of time for object to arrive at destination will be consistent
            //Distance to travel each frame will be determined
            //Returns a point between start to finsih based off percentage
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete);

            yield return null;
        }

        //Deactivates object when destination is reached
        gameObject.SetActive(false);
    }
}
