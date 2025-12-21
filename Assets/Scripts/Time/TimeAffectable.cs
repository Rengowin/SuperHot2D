using UnityEngine;

public abstract class TimeAffectable : MonoBehaviour
{
    [Header("Local Time Control")]
    [Tooltip("Lowest time scale this object can reach")]
    public float minLocalTimeScale = 0.02f;

    protected float LocalTimeScale
    {
        get
        {
            if (PlayerTimeSource.Instance == null)
                return Time.timeScale;

            float playerFactor = PlayerTimeSource.Instance.NormalizedSpeed;

            return Mathf.Max(
                Mathf.Lerp(minLocalTimeScale, Time.timeScale, playerFactor),
                minLocalTimeScale
            );
        }
    }

    protected float ScaledDeltaTime =>
        Time.unscaledDeltaTime * LocalTimeScale;
}
