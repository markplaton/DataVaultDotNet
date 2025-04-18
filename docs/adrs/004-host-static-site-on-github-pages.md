# ADR 004: Host Static Docs Site on GitHub Pages

**Status:** Accepted  
**Date:** 2025-04-18

## Context

We need a simple and free hosting solution for the generated static docs site.

## Options Considered

### GitHub Pages
- ✅ Free, version-controlled
- ✅ Easily integrates with GitHub Actions
- ✅ Great for internal teams

### Netlify
- ✅ Easy CI/CD and custom domains
- ❌ Third-party dependency

### Azure Static Web Apps
- ✅ Good Azure integration
- ❌ Overhead for small projects

## Decision

Use **GitHub Pages** for hosting the VitePress-generated static site.
