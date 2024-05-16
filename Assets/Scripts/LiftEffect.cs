using UnityEngine;

public class LiftEffect : MonoBehaviour
{
    [SerializeField] public float liftForce = 20f;
    [SerializeField] public float additionalLift = 10f;
    [SerializeField] public float boostLift = 30f;
    [SerializeField] public float downwardForce = -20f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            bool isBoosting = Input.GetKey(KeyCode.W);
            bool isDescending = Input.GetKey(KeyCode.S);

            float forceToApply = liftForce;
            if (rb.velocity.y < 0)
            {
                forceToApply += additionalLift;
            }
            if (isBoosting)
            {
                forceToApply = boostLift;
            }
            if (isDescending)
            {
                forceToApply = downwardForce;
            }

            Vector2 lift = new Vector2(0, forceToApply);
            rb.AddForce(lift);
        }
    }
}
