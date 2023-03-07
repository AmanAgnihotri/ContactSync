namespace ContactSync;

public sealed record ContactUpdated(
  Contact Contact,
  Contact UpdatedContact,
  DateTime Time) : IEvent;
