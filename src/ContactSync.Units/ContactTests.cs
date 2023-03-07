namespace ContactSync.Units;

public sealed class ContactTests
{
  [Fact(DisplayName = "Contacts are equal based on name.")]
  public void TestContactEquality()
  {
    Contact a = new("Some Name", "+1234567890");
    Contact b = new("Some Name", "+0987654321");

    Assert.Equal(a, b);
  }

  [Fact(DisplayName = "DoSync adds new contacts properly.")]
  public void TestCreateContact()
  {
    Contact a = new("A", "1");
    Contact b = new("B", "2");

    IEvent event01 = new ContactCreated(a, DateTime.UtcNow);
    IEvent event02 = new ContactCreated(b, DateTime.UtcNow.AddSeconds(1));

    IEnumerable<Contact> empty = ArraySegment<Contact>.Empty;

    Assert.Equal(new[] {a, b}, empty.DoSync(new[] {event01, event02}));
  }

  [Fact(DisplayName = "DoSync throws when re-adding existing contact.")]
  public void TestCreateOnExistingContact()
  {
    Contact a = new("A", "1");

    IEvent @event = new ContactCreated(a, DateTime.UtcNow);

    IEnumerable<Contact> withA = new[] {a};

    Assert.Throws<InvalidDataException>(() => withA.DoSync(new[] {@event}));
  }

  [Fact(DisplayName = "DoSync updates phone number properly.")]
  public void TestUpdateOnContactNumber()
  {
    Contact a = new("A", "1");
    Contact b = new("A", "2");

    IEvent @event = new ContactUpdated(a, b, DateTime.UtcNow);

    IEnumerable<Contact> withA = new[] {b};

    Assert.Equal(new[] {a}, withA.DoSync(new[] {@event}));
  }

  [Fact(DisplayName = "DoSync updates contact properly.")]
  public void TestUpdateContact()
  {
    Contact a = new("A", "1");
    Contact b = new("B", "2");

    IEvent @event = new ContactUpdated(a, b, DateTime.UtcNow);

    IEnumerable<Contact> withA = new[] {a};

    Assert.Equal(new[] {b}, withA.DoSync(new[] {@event}));
  }

  [Fact(DisplayName = "DoSync deletes contact properly.")]
  public void TestDeleteContact()
  {
    Contact a = new("A", "1");
    Contact b = new("B", "2");

    IEvent @event = new ContactDeleted(a, DateTime.UtcNow);

    IEnumerable<Contact> contacts = new[] {a, b};

    Assert.Equal(new[] {b}, contacts.DoSync(new[] {@event}));
  }

  [Fact(DisplayName = "DoSync throws when deleting non-existing contact.")]
  public void TestDeleteOnNonExistingContact()
  {
    Contact a = new("A", "1");

    IEvent @event = new ContactDeleted(a, DateTime.UtcNow);

    IEnumerable<Contact> empty = ArraySegment<Contact>.Empty;

    Assert.Throws<InvalidDataException>(() => empty.DoSync(new[] {@event}));
  }

  [Fact(DisplayName = "DoSync syncs contacts properly.")]
  public void TestRealisticScenario()
  {
    Contact a = new("A", "1");
    Contact b = new("B", "2");
    Contact c = new("C", "3");

    DateTime time = DateTime.UtcNow;

    IEnumerable<IEvent> createEvents = new List<IEvent>
    {
      new ContactCreated(a, time),
      new ContactCreated(b, time),
      new ContactCreated(c, time)
    };

    IEnumerable<Contact> empty = ArraySegment<Contact>.Empty;

    Assert.Equal(new[] {a, b, c}, empty.DoSync(createEvents));

    DateTime updateTime = time.AddSeconds(1);

    Contact d = a with {PhoneNumber = "11"};
    Contact e = b with {PhoneNumber = "22"};
    Contact f = c with {PhoneNumber = "33"};

    IEnumerable<IEvent> updateEvents = new List<IEvent>
    {
      new ContactUpdated(a, d, updateTime),
      new ContactUpdated(b, e, updateTime),
      new ContactUpdated(c, f, updateTime)
    };

    IEnumerable<Contact> def = new[] {d, e, f};

    Assert.Equal(def, empty.DoSync(createEvents).DoSync(updateEvents));

    DateTime deleteTime = updateTime.AddSeconds(1);

    IEnumerable<IEvent> deleteEvents = new List<IEvent>
    {
      new ContactDeleted(a, deleteTime),
      new ContactDeleted(b, deleteTime),
      new ContactDeleted(c, deleteTime)
    };

    IEnumerable<Contact> contacts = empty
      .DoSync(createEvents)
      .DoSync(updateEvents)
      .DoSync(deleteEvents);

    Assert.Equal(empty, contacts);
  }
}
