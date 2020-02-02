using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemyPresenter : MonoBehaviour, IHaveIdentity<EnemyIdentifier>
{
	private float _velocity;
    public float MinVelocity = 0.5f;
    private float MaxVelocity;
    public float FreezeTime = 1;
    public float enemyHP = 5f;

	private List<Vector3> _path;
	private Vector3 _target;
	private bool _endOfPathReached = true;

	[Inject]
	public EnemyParameters Parameters { private get; set; }

	[Inject]
	public EnemyPositions Positions { private get; set; }

	[Inject]
	public EnemiesAggregate Enemies { private get; set; }

	[Inject]
	public PathFinderAggregate PathFinder { private get; set; }

	public EnemyIdentifier Id => Parameters.EnemyId;

	void Start()
	{
		_velocity = Parameters.Velocity;
        MaxVelocity = _velocity;

		transform.position = Parameters.Position;

		PathFinder.Events
			.OfType<PathFinderEvent, PathFinderEvent.PathCalculated>()
			.Where(pathCalculated => pathCalculated.EnemyId == Parameters.EnemyId)
			.Subscribe(FollowPath);
	}

	private void FollowPath(PathFinderEvent.PathCalculated pathCalculated)
	{
		_path = pathCalculated.Path.Select(mapCoord => mapCoord.ToVector3()).ToList();

		_target = _path[0];
		_path.RemoveAt(0);

		_endOfPathReached = false;
	}

	void Update()
	{
		if (!_endOfPathReached)
			MoveTowardTarget();

		Positions[Parameters.EnemyId] = transform.position;
	}

	private void MoveTowardTarget()
	{
		var toTarget = _target - transform.position;

		if (toTarget.magnitude < 0.1f)
		{
			transform.position = _target;

			if (_path.Count > 0)
			{
				_target = _path[0];
				_path.RemoveAt(0);
			}
			else
			{
				_endOfPathReached = true;
			}
		}

		var dir = toTarget.normalized;

		transform.position += _velocity * dir * Time.deltaTime;

		transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag.Equals("Ice"))
        {
            if (_velocity > MinVelocity)
            {
                Debug.Log("I got froze!");
                StartCoroutine(Freezing());
                _velocity *= 0.5f;
            }
            else
            {
                _velocity = MinVelocity;
            }
        }
		else if (collision.gameObject.tag.Equals("Flame"))
        {
            enemyHP -= 0.2f;
        }
        else
        {
            enemyHP--; 
            Debug.Log("I got hit!");
        }

        if (enemyHP <= 0f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Freezing()
    {
        yield return new WaitForSeconds(FreezeTime);
        if (_velocity < MaxVelocity / 2)
        {
            _velocity *= 2;
        }
        else
        {
            _velocity = MaxVelocity;
        }
    }
}
