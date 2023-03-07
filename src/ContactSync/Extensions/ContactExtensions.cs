namespace ContactSync;

public static class ContactExtensions
{
  public static IEnumerable<Contact> DoSync(
    this IEnumerable<Contact> contacts,
    IEnumerable<IEvent> events)
  {
    ISet<Contact> set = new SortedSet<Contact>(contacts);

    foreach (IEvent @event in events.OrderBy(v => v.Time))
    {
      switch (@event)
      {
        case ContactCreated data:
          {
            if (!set.Contains(data.Contact))
            {
              set.Add(data.Contact);
            }
            else
            {
              throw new InvalidDataException(
                $"Contact with name {data.Contact.Name} already exists!");
            }
          }

          break;

        case ContactUpdated data:
          {
            set.Remove(data.Contact);
            set.Add(data.UpdatedContact);
          }

          break;

        case ContactDeleted data:
          {
            if (set.Contains(data.Contact))
            {
              set.Remove(data.Contact);
            }
            else
            {
              throw new InvalidDataException(
                $"Contact with name {data.Contact.Name} does not exist!");
            }
          }

          break;
      }
    }

    return set;
  }
}
