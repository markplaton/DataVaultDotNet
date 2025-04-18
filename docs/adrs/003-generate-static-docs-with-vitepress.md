# ADR 003: Generate Static Documentation Site using VitePress and Vue

**Status:** Accepted  
**Date:** 2025-04-18

## Context

We need a lightweight, developer-friendly static documentation site to publish project documentation, ADRs, and pipeline usage guides.

## Options Considered

### VitePress with Vue
- ✅ Markdown-based, easy authoring
- ✅ Native Vue support
- ✅ Fast local dev and deploy

### Docusaurus
- ✅ Rich React-based ecosystem
- ❌ Requires more config and React knowledge

### MkDocs
- ✅ Python-based simplicity
- ❌ Less customizable with Vue components

## Decision

Use **VitePress with Vue** for building the documentation site.

## Advice

- **VitePress** integrates well with Vue and supports a minimal Markdown-first authoring experience.
- Use the docs site not only for ADRs but also for:
  - Gherkin feature files
  - Developer onboarding
  - Examples of pipeline usage
  - Test data scenarios
  - Plugin and adapter architecture