---
name: pr-guidelines
description: Write or update a pull request title and description for PicPic. Use when opening a PR, editing a PR body, or the user asks for a PR description or test plan.
---

# PR guidelines

Defaults, not a template. User asks for extra content, or the change warrants new sections → add them.

## Title

- Conventional Commits, enforced by `.github/workflows/pr-title.yml`.
- Types: `build|chore|ci|docs|feat|fix|perf|refactor|revert|style|test`, optional `(scope)`, optional `!`.
- Squash merge uses the title → write it as the final commit subject: lowercase, imperative, no trailing period.
- One phase/item per PR (see `CLAUDE.md`).

## Description

Start with **Summary**, end with **Test plan**. Between them, add what helps the reviewer:

- Summary: what changed and why, as short bullets. Name files/handlers/keys when it helps review.
- `Closes #N` when it fixes an issue.
- Optional sections when relevant, e.g. Measurements (table), Not in this PR, Decisions, Screenshots, Migration notes.
- Plain words. Say what was decided and why, not a changelog of every edit.
- Don't paste CI results (build/test/format); CI shows them.

## Test plan

Checkbox list, grouped by who/when verifies. Skip groups with no items. Tick (`[x]`) only what was actually verified.

### Verified by the agent
Anything the agent can run itself: build, tests, format, local publish, running the app against a scripted scenario. Do it before opening the PR, tick it, and say what was checked.

### Manual (you)
Needs a human: UI behavior, visual checks, real machine or account state, anything the agent can't drive. Steps must be copy-followable: exact action, expected result.

### After merge
Only checkable once the PR is on `master` or later: tag-triggered workflows, deployed site, releases, scheduled jobs. State the trigger (e.g. "push tag `v0.1.0`") and the expected result.

## Before opening

- Branch pushed, title passes the regex.
- Ask the user before commit, push, or PR (see global rules).
- Return the PR URL.
