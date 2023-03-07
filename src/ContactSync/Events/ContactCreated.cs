namespace ContactSync;

public sealed record ContactCreated(
  Contact Contact,
  DateTime Time) : IEvent;
