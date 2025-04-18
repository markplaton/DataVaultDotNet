# ADR 002: Use TestContainers for Local SQL Server Testing

**Status:** Accepted  
**Date:** 2025-04-18

## Context

We want to ensure reliable local integration testing of pipelines with SQL Server and Snowflake compatibility.

## Options Considered

### TestContainers for .NET
- ✅ Easy to spin up ephemeral SQL Server instances
- ✅ Supports parallel test execution
- ✅ Matches CI/CD container-based testing environments

### Manually maintained SQL Server containers
- ❌ More manual effort
- ❌ Prone to state leakage

### Embedded SQL Server (LocalDB)
- ❌ Not available for Linux/Mac
- ❌ Different behavior than full SQL Server

## Decision

Use **TestContainers for .NET** to spin up SQL Server containers for local and CI integration testing.

## Advice

- Use **TestContainers** to create isolated and repeatable test environments with ephemeral SQL Server or Snowflake test containers.
- Avoid reliance on long-lived shared test databases that may accumulate state and cause flaky tests.
