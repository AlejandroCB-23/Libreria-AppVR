namespace Alex.OcularVergenceLibrary
{
    using UnityEngine;
    using Wave.Essence.Eye;


    public static class VergenceFunctions
    {

        public static bool IsEyeTrackingAvailable()
        {
            return EyeManager.Instance != null && EyeManager.Instance.IsEyeTrackingAvailable();
        }

        public static bool TryGetCombinedEyeRay(out Ray ray)
        {
            ray = new Ray();

            if (EyeManager.Instance == null || !EyeManager.Instance.IsEyeTrackingAvailable())
                return false;

            Vector3 origin, direction = Vector3.zero;
            if (EyeManager.Instance.GetCombinedEyeOrigin(out origin) &&
                EyeManager.Instance.GetCombindedEyeDirectionNormalized(out direction))
            {
                Transform head = Camera.main.transform;
                ray.origin = head.TransformPoint(origin);
                ray.direction = head.TransformDirection(direction);
                return true;
            }

            return false;
        }

        public static float CalculateVergenceAngle(float interpupillaryDistance, float distanceToObject)
        {
            if (distanceToObject == 0f) return 0f;
            return Mathf.Rad2Deg * 2f * Mathf.Atan((interpupillaryDistance / 2f) / distanceToObject);
        }

        public static bool TryGetInterpupillaryDistance(out float pd)
        {
            pd = 0f;
            if (EyeManager.Instance == null) return false;

            Vector3 left, right;
            if (EyeManager.Instance.GetLeftEyeOrigin(out left) && EyeManager.Instance.GetRightEyeOrigin(out right))
            {
                Transform head = Camera.main.transform;
                Vector3 leftWorld = head.TransformPoint(left);
                Vector3 rightWorld = head.TransformPoint(right);
                pd = Vector3.Distance(leftWorld, rightWorld);
                return true;
            }

            return false;
        }

        public static bool TryRaycastHit(out RaycastHit hit, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers)
        {
            hit = new RaycastHit();
            if (TryGetCombinedEyeRay(out Ray ray))
            {
                return Physics.Raycast(ray, out hit, maxDistance, layerMask);
            }
            return false;
        }
    }
}




