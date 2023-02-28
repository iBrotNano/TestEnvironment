---
name: Bug
about: Report a bug
title: Summary
labels: bug
assignees: iBrotNano

---

## Bug

### Open Questions?

1. @Who: What must be discussed?

### Blockers

1. Something blocks my work.

### Description

A description of the faulty behavior.

#### How to reproduce

1. Step 1
1. Step 2

### Input

What data goes into the system and what is their source?

### Output

What data goes out of the system and why?

### Conditions

What conditions must be met? High pressure? Many users?

### Side effects

Are there any?

## Acceptance tests

1. First test to validate the requirement.
1. Second test.

## Design

Architecture of the system and/or layout of the GUI. Specification of the input and output data.

## Dissection

| Integration test | ID                                          |
| --------------- | ------------------------------------------- |
| Action          | What has to be done to validate the system? |
| Expected result | What is a valid result?                     |

## Development

### TODOs

- [ ] Create a `bugfix`  or `hotfix` branch
- [ ] Try to reproduce the bug
- [ ] Investigate why it happens
- [ ] Plan the needed changes to fix the bug
- [ ] Write tests
- [ ] Fix the bug
- [ ] Check if the exception handling is well done
- [ ] Check if further tests must be written
- [ ] Write meaningful comments
- [ ] Are there any compiler warnings?
- [ ] Do all unit tests pass?
- [ ] Is the version number correctly configured?
- [ ] Phrase a meaningful commit comment
- [ ] Check-in the changes and push them to the server

### Notes

Notes about the development of the issue.

## Debug

| ID   | Result | Pass |
| ---- | ------ | ---- |
| 1   |        | 🟢🔴🟡  |

## Documentation

- [ ] Do I need a new PIA or update an existing one?
- [ ] Update the README.md
- [ ] Update the CHANGELOG.md
- [ ] Describe the setup of the story if needed for end users
- [ ] Does something in the wiki needed to be updated?
- [ ] Needs other stuff been documented?

### Decisions

| Decision          | Description                |
| ----------------- | -------------------------- |
| What was decided? | Why was the decision made? |

### PIAs

Link to related PIA

### Links

- [ ] Any other documentation should be linked here. Intern and extern.

## Demo

- [ ] Setup a fresh demo environment

Results of the acceptance tests.

## Deployment

- [ ] Merge `feature` into `master`  or `hotfix` into `production` and `master` with a `squash` and remove the `bugfix` branch
- [ ] Check if the compiled artifact is valid
- [ ] Cleanup the Git history locally on the dev system
