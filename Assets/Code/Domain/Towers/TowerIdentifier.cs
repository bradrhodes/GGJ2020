using System;
using System.Collections.Generic;

public class TowerIdentifier : IEquatable<TowerIdentifier>
{
	private static int _nextId;
	private readonly int _id;

	private TowerIdentifier(int id)
	{
		_id = id;
	}

	public static TowerIdentifier Create()
		=> new TowerIdentifier(_nextId++);

	public override bool Equals(object obj)
		=> Equals(obj as TowerIdentifier);

	public bool Equals(TowerIdentifier other)
		=> other != null && _id == other._id;

	public override int GetHashCode()
		=> 1969571243 + _id.GetHashCode();

	public override string ToString() 
		=> $"Tower {_id}";

	public static bool operator ==(TowerIdentifier left, TowerIdentifier right)
		=> EqualityComparer<TowerIdentifier>.Default.Equals(left, right);

	public static bool operator !=(TowerIdentifier left, TowerIdentifier right)
		=> !(left == right);
}
