# Contact Sync between Phone and Cloud

See [Problem](/docs/Problem.md).

## Sync Algorithm

Assuming we already have a list of contacts and a time-ordered list of changes (create, update, or delete) on contacts, we can create a resultant list of contacts by initialising it with all known contacts and then iterating through the changes and doing the following on the list:

1. If it's a create operation, add the given contact; throw if the contact already exists as it's an impossible situation according to the problem.

2. If it's an update operation, remove the associated contact and add the updated contact.

3. If it's a delete operation, delete the contact.

Contact consists of a name and phone number.
Two contacts are equal if their names are equal.
The resultant list can be a sorted set to ensure quick contains check.

## Sync Implementation

See [ContactExtensions](/src/ContactSync/Extensions/ContactExtensions.cs).

## Q/A

Q1. Would your algorithm change if the contact list was very large? Continue to assume that everything is contained in memory.

A1. If everything is contained in memory, there is no need to change the algorithm even if the contact list is very large.

Q2. Would your algorithm change if there were multiple handhelds and many users using the master desktop?

A2. The DoSync algorithm itself won't change. To accomodate multiple users, another abstraction can be created which keeps track of each user and their respective contacts. The same algorithm can then be applied on any given user's contact list, given a list of changes in their contact list.

## Instructions

You will need .NET 7 runtime installed on your machine. ContactSync is a class library so only its tests can be run.

Change directory to the root level of the ContactSync folder and run the following to list all available tests:

```
dotnet test -t
```

To run the tests themselves, run the following:

```
dotnet test
```

---
