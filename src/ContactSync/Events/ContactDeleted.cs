namespace ContactSync;

public sealed record ContactDeleted(
  Contact Contact,
  DateTime Time) : IEvent;
