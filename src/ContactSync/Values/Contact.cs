namespace ContactSync;

public sealed record Contact(
  string Name,
  string PhoneNumber) : IComparable<Contact>, IComparable
{
  public bool Equals(Contact? other)
  {
    if (ReferenceEquals(null, other))
    {
      return false;
    }

    if (ReferenceEquals(this, other))
    {
      return true;
    }

    return Name == other.Name;
  }

  public override int GetHashCode()
  {
    return Name.GetHashCode();
  }

  public int CompareTo(Contact? other)
  {
    if (ReferenceEquals(this, other))
    {
      return 0;
    }

    return ReferenceEquals(null, other)
      ? 1
      : string.Compare(Name, other.Name, StringComparison.Ordinal);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj))
    {
      return 1;
    }

    if (ReferenceEquals(this, obj))
    {
      return 0;
    }

    return obj is Contact other
      ? CompareTo(other)
      : throw new ArgumentException(
        $"Object must be of type {nameof(Contact)}");
  }
}
