# User secrets
Secret Manager
- secrets are managed (how they are stored is transparent to the user);
- scoped to the Project or Projects;
- stored as config files (in plain text);
- stored on the local machine using your user profile folder;

## Configuration in .NET
- Configuration performed using 1 or more configuration providers.
- These providers read key-values pairs from configuration sources.
- Most .NET projects have a (default) configuration set up.
- Configuration sources have a default order of precedence.

### Default Hierarchy
Of the same key exists in a later source, it will over-write the previous one: **last loaded key wins**:
1. appsettings.json
2. appsettings.<Environment>.json
3. User Secrets (only in Development)
4. Environments Variables
5. Command Line.

### Commands for dotnet cli

```
// initialize user-secrets container
dotnet user-secrets init

// add secret
dotnet user-secrets set "Password" "usususu"
```