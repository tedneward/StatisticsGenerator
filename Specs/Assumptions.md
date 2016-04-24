# Assumptions

#### Value columns will hold double values.
In the event that this is not wide enough, we can change the aliased "Value"
type in the StatisticsGenerator library assembly to decimal; unfortunately,
since C# won't let us export an aliased type as a legitimate type from the
assembly, so that it can be used opaquely from the assembly's clients, this
would require changing the client code (tests and Shell) manually to using
decimals instead of the doubles currently used.

#### There will never be zero ValueXXX headers
"If a period is not present, you can terminate processing immediately.

#### Practical maximum ValueXXX headers: 20K
As described in Scott's email.

#### Multiple Configurations are expected per run.
Additionally, a Configuration triplet is what defines uniqueness; thus, a
"CashPrem/Average/MinValue" is fine alongside a "CashPrem/MinValue/MinValue"
configuration. Both should be calculated.

However, I assume that multiple "CashPrem/Average/Average" can be safely
ignored. "If you have duplicates, you only need to run the calculation once."

#### Data elements are unique to Scenario ID/VarName pairings.
"If you see a duplicate combo, you should emit a warning and ignore the duplicate
item."

This is going to be an interesting requirement to have, given that it has also
been established in the spec that this needs to be a one-pass processor, without
keeping the entire file in memory; presumably, it will not be violating the
boundary conditions to keep a list of the tuples already processed, so as to be
able to verify this.



