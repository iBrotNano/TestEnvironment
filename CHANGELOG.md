# Changelog

## Version 4.7.1.1

### Installation

The NuGet can be installed from GitHub’s NuGet Repository. Releases can be installed from nuget.org.

---

**Info:** [Working with the NuGet registry - GitHub Docs](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#installing-a-package) documents how to reference GitHub’s NuGet repository.

---

```shell
dotnet add package MarcelMelzig.TestEnvironment --version 4.7.1.1
```

### What's new?

This is the initial release of the project. Hopefully it will help you to set up some useful test environments.

A fully automatic build pipeline is set up for the project. So you can await new features asap. All uses SemVer. Everything is published with docs production ready and you can even debug into the code with SourceLink.

### All Changes

- [#1](https://github.com/iBrotNano/TestEnvironment/issues/1) Set the version by an action 
- [#2](https://github.com/iBrotNano/TestEnvironment/issues/2) Add issue templates to the repo
- [#3](https://github.com/iBrotNano/TestEnvironment/issues/3) Tag the version of a build by an action
- [#11](https://github.com/iBrotNano/TestEnvironment/issues/11) Create a multi target build
- [#10](https://github.com/iBrotNano/TestEnvironment/issues/10) Configure source link
- [#9](https://github.com/iBrotNano/TestEnvironment/issues/9) Build a deterministic build
- [#8](https://github.com/iBrotNano/TestEnvironment/issues/8) Add a production build action
- [#6](https://github.com/iBrotNano/TestEnvironment/issues/6) Push the NuGet to nuget.org

### Updated Dependencies

| Name                                   | Previous Version | New Version |
| -------------------------------------- | ---------------- | ----------- |
| Microsoft.Data.Sqlite.Core             | 7.0.3            | 7.0.4       |
| Microsoft.EntityFrameworkCore          | 6.0.14           | 6.0.15      |
| Microsoft.EntityFrameworkCore          | 7.0.3            | 7.0.4       |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.14           | 6.0.15      |
| Microsoft.EntityFrameworkCore.InMemory | 7.0.3            | 7.0.4       |
| Microsoft.EntityFrameworkCore.Sqlite   | 6.0.14           | 6.0.15      |
| Microsoft.EntityFrameworkCore.Sqlite   | 7.0.3            | 7.0.4       |
