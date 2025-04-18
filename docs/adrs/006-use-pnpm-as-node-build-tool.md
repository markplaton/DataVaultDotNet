## ADR-006: Use pnpm as Node Build Tool for Docs

**Decision**: Use `pnpm` instead of `npm` or `yarn` to manage documentation site dependencies and scripts.

**Status**: Accepted

**Context**:
The documentation site is built using VitePress, a Vue-powered static site generator. To install and manage its dependencies, a Node-based package manager is required. `pnpm` offers performance and consistency improvements over `npm` and `yarn`, including disk-saving symlinks, strict dependency resolution, and reproducible builds.

**Decision**:
- Use `pnpm` to manage all frontend dependencies in the `/docs` directory
- Use local `pnpm` scripts for `docs:dev`, `docs:build`, and `docs:serve`
- Document usage in developer setup instructions

**Consequences**:
- Developers need to install `pnpm` (1-time setup)
- Builds are faster and more reliable
- Avoids dependency duplication in CI environments

**Advice**:
Include `pnpm-lock.yaml` and `.npmrc` in version control. Add documentation scripts to `package.json`. Recommend installing `pnpm` globally with `npm install -g pnpm`. Update CI pipelines (e.g., GitHub Actions) to use `pnpm install` instead of `npm ci`.
