# ADR 001: Use NUKE as the Build Tool

**Status:** Accepted  
**Date:** 2025-04-18

## Context

We require a flexible, C#-native build automation tool that integrates well with TeamCity and Octopus Deploy. We are building and packaging .NET-based ETL pipelines and database deployments.

## Options Considered

### NUKE
- ✅ Tight integration with .NET ecosystem
- ✅ Declarative and composable build definitions
- ✅ Supports TeamCity, Octopus Deploy, and TestContainers
- ❌ Learning curve for new developers unfamiliar with code-based build scripts

### Cake
- ✅ Also C#-based
- ❌ DSL feels less idiomatic than NUKE
- ❌ Less modern syntax and plugin ecosystem

### Azure DevOps Pipelines / YAML
- ✅ Native CI/CD integration
- ❌ Less flexibility in build reuse across local and CI

### MSBuild Scripts
- ✅ Standard with .NET
- ❌ Low readability and difficult to compose

## Decision

Use **NUKE** to drive build, test, and deployment for .NET-based projects.

## Advice
- **NUKE** provides the best alignment with .NET projects and supports both local development and CI/CD workflows.
- Treat your `Build.cs` file as a first-class citizen in your repo—document available targets in the VitePress docs site for easy discoverability.
