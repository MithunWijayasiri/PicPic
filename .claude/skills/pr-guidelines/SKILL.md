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

Checkbox list, grouped by who/when verifies. Skip groups with no items. Tick (`[x]`) only what was actually verified.

### Verified by the agent
Anything the agent can run itself: build, tests, format, local publish, running the app against a scripted scenario. Do it before opening the PR, tick it, and say what was checked.

### Manual (you)
Needs a human: UI behavior, visual checks, real machine or account state, anything the agent can't drive. Steps must be copy-followable: exact action, expected result.

### After merge
Only checkable once the PR is on `master` or later: manual or tag-triggered workflows, deployed site, releases, scheduled jobs. State the trigger (e.g. "run Release workflow with version `0.1.1`") and the expected result.
