---
name: pr-guidelines
description: Use when asked to open a PR or update a PR (title, description, test plan) for PicPic.
---

# PR guidelines

Defaults, not a template. User asks for extra content, or the change warrants new sections → add them.

## Title

- Conventional Commits; allowed types checked by `.github/workflows/pr-title.yml`.
- Squash merge uses the title → write it as the final commit subject: lowercase, imperative, no trailing period.

## Description

Start with **Summary**, end with **Test plan**. Between them, add what helps the reviewer:

- Summary: what changed and why, as short bullets. Name files/handlers/keys when it helps review.
- `Closes #N` when it fixes an issue.
- Optional sections when relevant, e.g. Measurements (table), Not in this PR, Decisions, Screenshots, Migration notes.
- Plain words. Say what was decided and why, not a changelog of every edit.
- Don't paste CI results (build/test/format); CI shows them.

## Test plan

Checkbox list. Skip groups with no items. Tick (`[x]`) only what was actually verified. Never list CI/PR checks passing: merge is blocked until they pass.

Put each item in exactly one group, by asking in order:

1. Can it only be checked after the PR is on `master`? → **After merge** (whoever does the check).
2. Otherwise, can the agent run it itself? → **Verified by the agent**.
3. Otherwise → **Manual (user)**.

### Verified by the agent
Build, tests, format, local publish, running the app against a scripted scenario. Do it before opening the PR, tick it, say what was checked.

### Manual (user)
Needs a human, checkable before merge: UI behavior, visual checks, real machine or account state. Steps must be copy-followable: exact action, expected result.

### After merge
Only checkable on `master` or later: manual or tag-triggered workflows, Actions sidebar names, deployed site, releases, scheduled jobs. State the trigger (e.g. "run Release workflow with version `0.1.1`") and the expected result.
