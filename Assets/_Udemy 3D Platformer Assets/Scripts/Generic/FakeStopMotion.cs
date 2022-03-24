using UnityEngine;
public class FakeStopMotion : MonoBehaviour
{
    #region Variables
    public Animator Animator;
    public int FPS = 8;
    private float _time;
    #endregion

    #region FixedUpdate
    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        var updateTime = 1f / FPS;
        Animator.speed = 0;

        if (_time > updateTime)
        {
            _time -= updateTime;
            Animator.speed = 60f / FPS;
        }
    }
    #endregion
}