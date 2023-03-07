# Contact Sync between Phone and Cloud 

## Requirements 

You maintain your list of contacts on a Cloud Server as well as on your phone. You create, modify and delete contacts on both systems and synchronize them periodically. Each contact consists of a Name and a Phone Number.

For all the questions below, ignore database issues and assume that all data that you need is contained in data structures in memory.

i. For simplicity, assume that the contact list is not very large. Describe the algorithm used to synchronize these changes.

ii. Write a doSync method to implement your synchronization algorithm on the master desktop. Assume that all required data transmitted by the handheld has been read in and can be passed to your method as parameters. All master data is already in memory and can also be passed to your method.

iii. Optional - would your algorithm change if the contact list was very large? Continue to assume that everything is contained in memory.

iv. Optional - would your algorithm change if there were multiple handhelds and many users using the master desktop?

## Assumptions 

- Assume that the Name of the contact is the primary identifier and is to be used as the key in distinguishing one contact from another.
- If the name is changed it is equivalent to the old name being deleted and a new name being created.
- All the possible actions that can be done on a contact are:
  1. Create(C)
  2. Delete(D)
  3. Update (U)
  4. No change(N)
- If multiple actions happen in sequence on the same contact either on the master desktop or the hand held device they will be combined using the following rules:
  1. C + C (not possible)
  2. C+U=C
  3. C+D=N
  4. U+C(not possible)
  5. U+U=U
  6. U+D=D
  7. D+C=U
  8. D+U(not possible)
  9. D + D (not possible)
- System time on the master and hand held are synchronized via time stamps.

Kindly provide the Key to resolving contentions on sync and the Synchronization algorithm.

Provide the doSync() implementation for the phone Handle all the scenarios outlined in the problem above.

---
