# Contributing to DataVaultDotNet

Thank you for your interest in contributing to **DataVaultDotNet**!

This project is built on strong principles of **Behavior-Driven Development (BDD)** and **Test-Driven Development (TDD)**. We welcome contributions that follow these practices to ensure that the framework remains robust, expressive, and easy to understand.

---

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Development Workflow](#development-workflow)
- [Test Strategy](#test-strategy)
  - [1. Unit Tests](#1-unit-tests)
  - [2. BDD Feature Tests](#2-bdd-feature-tests)
- [How to Add a New Feature](#how-to-add-a-new-feature)
- [Coding Style](#coding-style)
- [Pull Request Checklist](#pull-request-checklist)
- [Questions?](#questions)

---

## Code of Conduct

We expect all contributors to adhere to the [Contributor Covenant Code of Conduct](https://www.contributor-covenant.org/). Be kind, respectful, and collaborative.

---

## Development Workflow

1. **Fork** the repository and create a feature branch.
2. Write your **feature specification first** (`.feature` file).
3. Drive implementation using **unit tests and/or BDD steps**.
4. Write clean, modular, and testable code.
5. Ensure all tests pass.
6. Submit a pull request describing your change.

---

## Test Strategy

We follow a **testing pyramid** philosophy:

### 1. Unit Tests

- Use **xUnit** for low-level logic (e.g., metadata reflection, SQL generation).
- Place unit tests in the `tests/` folder.
- Use `FluentAssertions` for readable assertions.

### 2. BDD Feature Tests

- All behavior changes must have a `.feature` file describing the desired behavior.
- Use **Gherkin syntax** with `Xunit.Gherkin.Quick`.
- Write steps using the **Screenplay pattern** (`Actor.AttemptsTo(...)`).
- Place feature files under `DataVaultDotNet.Specs/Features/`.

Example:

```gherkin
Feature: Build SQL Server staging table

  Scenario: Generate the staging table from source metadata
    Given the staging table is named "stg_Customer"
    When the deploy step is executed
    Then the staging table "stg_Customer" should exist in the database
    And it should have columns:
      | ColumnName | DataType |
      | CustomerId | NVARCHAR |
      | Name       | NVARCHAR |
```

---

## How to Add a New Feature

1. **Create a `.feature` file** that expresses the new behavior.
2. **Create step definitions** in `DataVaultDotNet.Specs/Steps/`.
3. **Create or reuse screenplay Tasks, Abilities, and Questions** to implement the behavior.
4. **Write unit tests** to verify internal logic (e.g., builder, adapter behavior).
5. **Run tests locally** and confirm they pass.
6. **Update documentation**, if needed (e.g., `README.md`, `/docs`, or ADRs).
7. **Submit a pull request** and reference the related issue or use-case.

---

## Coding Style

- Follow standard **.NET naming conventions** (`PascalCase` for types, `camelCase` for locals).
- Prefer expression-bodied members when appropriate.
- Keep methods **short and focused**.
- Write **XML comments** for public methods and types.
- Use `FluentAssertions` for consistent, expressive test assertions.
- Format code using `.editorconfig` or `dotnet format`.

---

## Pull Request Checklist

Before submitting your PR, please ensure:

- [ ] Feature is described in a `.feature` file
- [ ] All new logic is covered by unit tests
- [ ] You used the Screenplay pattern for step definitions
- [ ] Code builds and all tests pass (`nuke test`)
- [ ] You followed existing patterns and naming conventions
- [ ] You updated or created any necessary documentation
- [ ] You checked that ADRs are still consistent (update or add if needed)

---

## Questions?

Have a question or need help?

- Open a [GitHub Discussion](https://github.com/markplaton/DataVaultDotNet/discussions)
- File a [GitHub Issue](https://github.com/markplaton/DataVaultDotNet/issues)
- Reach out in the pull request comments

We appreciate your contributions and are excited to build a world-class, BDD-friendly data platform with you!

