name: Bug Report
description: Report a bug in the DataVaultDotNet framework
title: "[Bug] - "
labels: [bug]
body:
  - type: textarea
    attributes:
      label: Describe the bug
      placeholder: Tell us what went wrong.
    validations:
      required: true

  - type: textarea
    attributes:
      label: Steps to reproduce
      placeholder: Provide a minimal reproducible example or scenario.
    validations:
      required: true

  - type: input
    attributes:
      label: Environment
      placeholder: e.g., Windows, SQL Server 2022, .NET 8

  - type: textarea
    attributes:
      label: Additional context or logs
