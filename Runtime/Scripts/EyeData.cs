namespace Alex.OcularVergenceLibrary
{
    using UnityEngine;
    using Wave.Essence.Eye;


    public static class EyeData
    {
        public static bool TryGetRawEyeData(out EyeTrackingData eyeData)
        {
            eyeData = new EyeTrackingData();

            if (EyeManager.Instance == null || !EyeManager.Instance.IsEyeTrackingAvailable())
                return false;

            Vector3 leftOrigin = Vector3.zero, leftDirection = Vector3.forward;
            Vector3 rightOrigin = Vector3.zero, rightDirection = Vector3.forward;
            Vector3 combinedOrigin = Vector3.zero, combinedDirection = Vector3.forward;

            bool leftEyeValid = EyeManager.Instance.GetLeftEyeOrigin(out leftOrigin) &&
                               EyeManager.Instance.GetLeftEyeDirectionNormalized(out leftDirection);

            bool rightEyeValid = EyeManager.Instance.GetRightEyeOrigin(out rightOrigin) &&
                                EyeManager.Instance.GetRightEyeDirectionNormalized(out rightDirection);

            bool combinedEyeValid = EyeManager.Instance.GetCombinedEyeOrigin(out combinedOrigin) &&
                                   EyeManager.Instance.GetCombindedEyeDirectionNormalized(out combinedDirection);

            if (leftEyeValid || rightEyeValid || combinedEyeValid)
            {
                eyeData = new EyeTrackingData(
                    leftOrigin, leftDirection,
                    rightOrigin, rightDirection,
                    combinedOrigin, combinedDirection
                );
                return true;
            }

            return false;
        }

        public static bool TryGetWorldEyeData(out EyeTrackingData eyeData)
        {
            eyeData = new EyeTrackingData();

            if (!TryGetRawEyeData(out EyeTrackingData rawData))
                return false;

            Transform headTransform = Camera.main.transform;
            if (headTransform == null)
                return false;

            Vector3 leftOriginWorld = headTransform.TransformPoint(rawData.leftEyeOrigin);
            Vector3 leftDirectionWorld = headTransform.TransformDirection(rawData.leftEyeDirection);

            Vector3 rightOriginWorld = headTransform.TransformPoint(rawData.rightEyeOrigin);
            Vector3 rightDirectionWorld = headTransform.TransformDirection(rawData.rightEyeDirection);

            Vector3 combinedOriginWorld = headTransform.TransformPoint(rawData.combinedEyeOrigin);
            Vector3 combinedDirectionWorld = headTransform.TransformDirection(rawData.combinedEyeDirection);

            eyeData = new EyeTrackingData(
                leftOriginWorld, leftDirectionWorld,
                rightOriginWorld, rightDirectionWorld,
                combinedOriginWorld, combinedDirectionWorld
            );

            return true;
        }

        public static bool TryGetLeftEyeWorldData(out Vector3 origin, out Vector3 direction)
        {
            origin = Vector3.zero;
            direction = Vector3.forward;

            if (EyeManager.Instance == null || !EyeManager.Instance.IsEyeTrackingAvailable())
                return false;

            Vector3 localOrigin, localDirection;
            if (EyeManager.Instance.GetLeftEyeOrigin(out localOrigin) &&
                EyeManager.Instance.GetLeftEyeDirectionNormalized(out localDirection))
            {
                Transform headTransform = Camera.main.transform;
                if (headTransform != null)
                {
                    origin = headTransform.TransformPoint(localOrigin);
                    direction = headTransform.TransformDirection(localDirection);
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetRightEyeWorldData(out Vector3 origin, out Vector3 direction)
        {
            origin = Vector3.zero;
            direction = Vector3.forward;

            if (EyeManager.Instance == null || !EyeManager.Instance.IsEyeTrackingAvailable())
                return false;

            Vector3 localOrigin, localDirection;
            if (EyeManager.Instance.GetRightEyeOrigin(out localOrigin) &&
                EyeManager.Instance.GetRightEyeDirectionNormalized(out localDirection))
            {
                Transform headTransform = Camera.main.transform;
                if (headTransform != null)
                {
                    origin = headTransform.TransformPoint(localOrigin);
                    direction = headTransform.TransformDirection(localDirection);
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetCombinedEyeWorldData(out Vector3 origin, out Vector3 direction)
        {
            origin = Vector3.zero;
            direction = Vector3.forward;

            if (EyeManager.Instance == null || !EyeManager.Instance.IsEyeTrackingAvailable())
                return false;

            Vector3 localOrigin, localDirection;
            if (EyeManager.Instance.GetCombinedEyeOrigin(out localOrigin) &&
                EyeManager.Instance.GetCombindedEyeDirectionNormalized(out localDirection))
            {
                Transform headTransform = Camera.main.transform;
                if (headTransform != null)
                {
                    origin = headTransform.TransformPoint(localOrigin);
                    direction = headTransform.TransformDirection(localDirection);
                    return true;
                }
            }

            return false;
        }
    }


    [System.Serializable]
    public struct EyeTrackingData
    {
        public Vector3 leftEyeOrigin;
        public Vector3 leftEyeDirection;
        public Vector3 rightEyeOrigin;
        public Vector3 rightEyeDirection;
        public Vector3 combinedEyeOrigin;
        public Vector3 combinedEyeDirection;

        public EyeTrackingData(Vector3 leftOrigin, Vector3 leftDir, Vector3 rightOrigin, Vector3 rightDir, 
                              Vector3 combinedOrigin, Vector3 combinedDir)
        {
            leftEyeOrigin = leftOrigin;
            leftEyeDirection = leftDir;
            rightEyeOrigin = rightOrigin;
            rightEyeDirection = rightDir;
            combinedEyeOrigin = combinedOrigin;
            combinedEyeDirection = combinedDir;
        }
    }

}
