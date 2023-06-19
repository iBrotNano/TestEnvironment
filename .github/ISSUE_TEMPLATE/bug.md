---
name: Bug
about: Not intended for users! 
title: Summary
labels: bug
assignees: iBrotNano
---

## Bug

### Description

A description of the faulty behavior.

### Open Questions?

- [ ] @Who: What must be discussed?

### Blockers

- [ ] Something blocks my work.

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

- [ ] Test to validate the requirement.

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
- [ ] Are there license conflicts for new dependencies?
- [ ] Exisits a `global.json`
- [ ] Remove deactivated code
- [ ] Are all TODOs in the code done?
- [ ] Write meaningful comments
- [ ] Are there any compiler warnings?
- [ ] Do all unit tests pass in Visual Studio?
- [ ] Do all unit tests pass with `dotnet test`?
- [ ] `dotnet format Contracts.Service.sln --verify-no-changes --verbosity diagnostic`
- [ ] Is the version number correctly configured?
- [ ] Phrase a meaningful commit comment
- [ ] Check-in the changes and push them to the server
- [ ] Does the build on the buildserver succeed?
- [ ] Create a PR

### Notes

Notes about the development of the issue.

✔ Best Practice
Text

✖ Error
Text

⚠ WarningText

ℹ Info
Text

🗒 Info
Text

## Debug

- [ ] ID: 🟢🔴🟡 Result: As Expected

## Documentation

- [ ] Do I need a new PIA or update an existing one?
- [ ] Update the README.md
- [ ] Update the CHANGELOG.md
- [ ] Describe the setup of the story if needed for end users
- [ ] Does something in the wiki needed to be updated?
- [ ] Needs other stuff been documented?

### Decisions

| Decision          | Cause                      |
| ----------------- | -------------------------- |
| What was decided? | Why was the decision made? |

### PIAs

Link to related PIA

### Links

- Any other documentation should be linked here. Intern and extern.

## Demo

- [ ] Setup a fresh demo environment
- [ ] Check all acceptance tests

## Deployment

- [ ] Merge `feature` into `master` or `hotfix` into `production` and `master` and remove the `bugfix` branch
- [ ] Check if the compiled artifact is valid
- [ ] Cleanup the Git history locally on the dev system