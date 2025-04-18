# ADR 005: Base Snowflake Implementation on Quickstart Guide

**Status:** Accepted  
**Date:** 2025-04-18

## Context

We are designing a Snowflake-compatible Data Vault adapter. We want a working baseline to validate our implementation.

## Decision

Use the structure, patterns, and implementation ideas from the official Snowflake Data Vault Quickstart as the foundation for our implementation.

[Snowflake Quickstart – Data Vault](https://quickstarts.snowflake.com/guide/vhol_data_vault)

### Advice
- Where possible, **proxy Snowflake features** using SQL Server to enable local testing before deploying to Snowflake.
- Follow the structure of the [Snowflake Quickstart Guide](https://quickstarts.snowflake.com/guide/vhol_data_vault) for naming conventions and architectural layout.
- Use in-memory representations for features like **Streams**, **Pipes**, and **Tasks** when working with SQL Server in development mode.
