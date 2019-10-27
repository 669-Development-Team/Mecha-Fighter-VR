using UnityEngine;

namespace Control
{
    public class GestureHandler : MonoBehaviour
    {
        // Types of gestures. Add new entries to the end.
        public enum Gestures
        {
            None = 0,
            Projectile,
            Uppercut,
            GroundPound
        }

        [SerializeField] private float motionDistance = 0.2f;
        [SerializeField] private float motionTimeThreshold = 0.5f;

        private float motionStartTime = 0f;

        // Cached positions
        private Vector3 leftStart;
        private Vector3 leftEnd;
        private Vector3 rightStart;
        private Vector3 rightEnd;

        // Record the time at the start of having both grips held down
        public void StartMotionTime()
        {
            motionStartTime = Time.time;
        }

        // Check if the time from releasing both grips since the start of the motion is within the threshold
        public bool IsWithinMotionTimeThreshold()
        {
            return Time.time - motionStartTime < motionTimeThreshold;
        }

        /// <summary>
        /// Detect which gesture was performed based on specific parameters defined by each gesture
        /// </summary>
        /// <param name="leftStartPos">The initial position of the left controller when the grip is first pressed</param>
        /// <param name="leftEndPos">The final position of the left controller when the grip is released</param>
        /// <param name="rightStartPos">The initial position of the right controller when the grip is first pressed</param>
        /// <param name="rightEndPos">The final position of the right controller when the grip is released</param>
        /// <returns>The gesture that the motion satisfied</returns>
        public Gestures CheckGesture(Vector3 leftStartPos, Vector3 leftEndPos, Vector3 rightStartPos, Vector3 rightEndPos)
        {
            // Cache positions
            leftStart = leftStartPos;
            leftEnd = leftEndPos;
            rightStart = rightStartPos;
            rightEnd = rightEndPos;

            Gestures result = 0;

            // There may be a better way of doing this chain of if statements such as the Visitor Pattern but I can't seem to figure out a way of doing it
            if (IsProjectileGesture())
            {
                result = Gestures.Projectile;
            }
            else if (IsUppercutGesture())
            {
                result = Gestures.Uppercut;
            }
            else if (IsGroundPoundGesture())
            {
                result = Gestures.GroundPound;
            }

            // Reset cached positions
            ResetPositions();

            Debug.Log("Gesture triggered: " + result);
            return result;
        }

        #region GestureCheckers

        // Checks if the motion is a forward thrust with both hands
        private bool IsProjectileGesture()
        {
            bool checkForwardMotion = leftEnd.z - leftStart.z >= motionDistance && rightEnd.z - rightStart.z >= motionDistance;

            // TODO: check if the motion is within a narrow range (ie. more straight and parallel as opposed to a big hug)

            return CheckDistance() && checkForwardMotion;
        }

        // Checks if the motion is a vertical lift with one hand
        private bool IsUppercutGesture()
        {
            return rightEnd.y - rightStart.y >= motionDistance * 2f;
        }

        // Checks if the motion is a vertical slam with both hands
        private bool IsGroundPoundGesture()
        {
            bool checkDownwardMotion = leftStart.y - leftEnd.y >= motionDistance * 2f &&
                                       rightStart.y - rightEnd.y >= motionDistance * 2f;

            return CheckDistance() && checkDownwardMotion;
        }

        #endregion

        // Checks if the motion covers enough distance
        private bool CheckDistance()
        {
            return Vector3.Distance(leftStart, leftEnd) > motionDistance &&
                   Vector3.Distance(rightStart, rightEnd) > motionDistance;
        }

        // Reset cached positions
        private void ResetPositions()
        {
            leftStart = Vector3.zero;
            leftEnd = Vector3.zero;
            rightStart = Vector3.zero;
            rightEnd = Vector3.zero;
        }
    }
}
