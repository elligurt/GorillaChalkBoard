using UnityEngine;

namespace GorillaChalkBoard
{
    public static class ErrorManager
    {
        public static void CheckAndShowError(GameObject chalkboard)
        {
            if (chalkboard == null)
            {
                return;
            }

            if (ImageManager.GetLoadedImageCount() == 0)
            {
                Transform errorObj = chalkboard.transform.Find("Error");
                if (errorObj != null)
                {
                    errorObj.gameObject.SetActive(true);
                    Debug.Log("[GorillaChalkBoard] No images found, enabling 'Error' object");
                }
                else
                {
                    Debug.LogWarning("[GorillaChalkBoard] Could not find 'Error' object");
                }
            }
            else
            {
                Debug.Log("[GorillaChalkBoard] Images detected, loading and displaying images.");
            }
        }
    }
}
