using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineRide : Character
{
    SplineContainer spline;
    float distancePercentage = 0f;
    float splineLength;

    [HideInInspector] public bool isOnSpline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spline"))
        {
            spline = other.GetComponent<SplineContainer>();
            splineLength = spline.CalculateLength();
            isOnSpline = true;
            StartCoroutine(SplineMovement());
        }
    }

    IEnumerator SplineMovement()
    {
        while (isOnSpline)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;

            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

            if (distancePercentage > 1f)
            {
                distancePercentage = 0f;
                isOnSpline = false;
            }

            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.05f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);

            yield return null;
        }
        OutOfSpline();
    }

    void OutOfSpline()
    {
        transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        distancePercentage = 0f;
        splineLength = 0f;
        spline = null;
        isOnSpline = false;
    }
}
