using UnityEngine;

public class EnemyMovementBase : MonoBehaviour  // TODO likely to be derived into spider/boar/etc classes, and inherited from common base movement class
{
    Rigidbody2D mass;
    enum MoveDiection
    {
        Left = -1, None, Right
    }

	void Awake()
	{
		mass = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        mass.AddForce( new Vector2(10, 0), ForceMode2D.Force ); // TODO
    }
}