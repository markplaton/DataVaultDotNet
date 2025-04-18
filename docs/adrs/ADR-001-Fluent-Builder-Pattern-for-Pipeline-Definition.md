# ADR-001: Fluent Builder Pattern for Pipeline Definition

**Decision:** Use a fluent builder pattern to define data pipelines.

**Status:** Accepted

**Context:**
We want to enable composable, readable, and testable pipeline definitions for staging, raw vault, business vault, and information marts. Pipeline definitions should support IntelliSense, strong typing, and fluent chaining.

**Decision:**
A fluent interface is used for pipeline setup:

```csharp
pipelineBuilder
    .WithSource(...)
    .WithHub<CustomerHub>()
    .WithSatellite<CustomerDetailsSatellite>()
    .Build();
```

**Consequences:**

Improves developer experience

Encourages consistent, expressive APIs

Harder to serialize/deserialize than declarative models


**Advice:**
Use fluent chaining for programmatic definitions and provide YAML parsing as a secondary input mechanism for ops or analysts.