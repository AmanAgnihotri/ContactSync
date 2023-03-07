namespace ContactSync;

public interface IEvent
{
  Contact Contact { get; }

  DateTime Time { get; }
}
