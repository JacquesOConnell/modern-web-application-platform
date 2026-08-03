# Chapter 1: Azure CLI Fundamentals and Resource Discovery

## Learning Objectives

By the end of this chapter, you should be able to:

- Verify that Azure CLI is installed
- Authenticate through Microsoft Entra ID
- Identify and select the active Azure subscription
- Discover resource groups and resources
- Inspect an Azure Web App
- Use output formats and JMESPath queries
- Explain why command-line administration is valuable in DevOps
- Troubleshoot common PowerShell formatting issues

## 1. What Is Azure CLI?

Azure CLI is a cross-platform interface for interacting with Azure Resource Manager and Azure services. It is useful for:

- Interactive administration
- Repeatable scripts
- CI/CD pipeline automation
- Resource discovery and troubleshooting
- Bulk operations
- Supporting Infrastructure as Code workflows

Most commands follow this pattern:

```text
az <command-group> <action> [parameters]
```

Examples:

```powershell
az group list
az webapp show
az storage account create
```

## 2. Shell Conventions

This chapter is **PowerShell-first** because the project is being administered from Windows Terminal.

### PowerShell: safest form

Prefer a single line when learning:

```powershell
az resource list --resource-group rg-modernwebapp-dev --output table
```

PowerShell uses the backtick for line continuation:

```powershell
az resource list `
  --resource-group rg-modernwebapp-dev `
  --output table
```

### Bash, Linux, or WSL

Bash uses a backslash:

```bash
az resource list \
  --resource-group rg-modernwebapp-dev \
  --output table
```

> **Common error:** PowerShell reports `unrecognized arguments: \` when a Bash backslash is pasted into PowerShell. Use one line or PowerShell backticks instead.

## 3. Verify the Installation

```powershell
az version
```

This confirms that the CLI is installed and shows component versions.

If `az` is not recognised:

1. Close and reopen the terminal after installation.
2. Run `where.exe az`.
3. Confirm the Azure CLI installation directory is included in `PATH`.

## 4. Sign In

```powershell
az login
```

This opens a browser and authenticates the user through Microsoft Entra ID.

### Security guidance

- Never record tokens from the login output.
- Do not commit subscription IDs or tenant-specific data unnecessarily.
- Human interactive login is suitable for local learning.
- Pipelines should use workload identity federation or managed identity rather than interactive login.

## 5. Inspect the Active Subscription

```powershell
az account show --output table
```

This identifies the subscription against which subsequent commands will run.

### Why this check matters

Running a command against the wrong subscription can create cost, security, and governance problems. Subscription verification should be a routine safety check before provisioning or deleting resources.

List every accessible subscription:

```powershell
az account list --output table
```

Select a subscription by name or ID:

```powershell
az account set --subscription "<subscription-name-or-id>"
```

Verify the change:

```powershell
az account show --query "{Name:name, IsDefault:isDefault}" --output table
```

## 6. Discover Resource Groups

List resource groups:

```powershell
az group list --output table
```

Inspect the ModernWebApp development resource group:

```powershell
az group show --name rg-modernwebapp-dev
```

Return selected properties:

```powershell
az group show --name rg-modernwebapp-dev --query "{Name:name, Location:location, ProvisioningState:properties.provisioningState, Tags:tags}"
```

### Resource-group purpose

A resource group is a logical lifecycle and management boundary. It supports:

- Organising related resources
- Applying RBAC at a shared scope
- Tagging and cost reporting
- Deploying and deleting related resources together

> **Destructive-action warning:** Deleting a resource group deletes the resources inside it. Always list and verify its contents first.

## 7. List Resources in a Resource Group

```powershell
az resource list --resource-group rg-modernwebapp-dev --output table
```

The project environment contains resources similar to:

| Resource type | Purpose |
|---|---|
| `Microsoft.Web/serverFarms` | App Service plan providing compute capacity |
| `Microsoft.Web/sites` | Linux Web App hosting the API |
| `microsoft.insights/components` | Application Insights telemetry resource |
| `microsoft.insights/actiongroups` | Notification target used by monitoring alerts |

This demonstrates that creating one service can result in supporting resources also being provisioned.

List only names and types:

```powershell
az resource list --resource-group rg-modernwebapp-dev --query "[].{Name:name, Type:type, Location:location}" --output table
```

## 8. Discover Azure Web Apps

List Web Apps:

```powershell
az webapp list --output table
```

Inspect the complete Web App object:

```powershell
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc
```

The default JSON output is valuable for learning and automation but can be lengthy.

## 9. Query Selected Web App Properties

Some complex objects cannot be displayed automatically as a table. Use `--query` to select a simple shape first:

```powershell
az webapp show `
  --resource-group rg-modernwebapp-dev `
  --name modernwebapp-dev-joc `
  --query "{Name:name, State:state, Location:location, Host:defaultHostName}" `
  --output table
```

Expected structure:

```text
Name                  State    Location           Host
--------------------  -------  -----------------  -------------------------------------------
modernwebapp-dev-joc  Running  southafricanorth   modernwebapp-dev-joc.azurewebsites.net
```

Return only the application state:

```powershell
az webapp show `
  --resource-group rg-modernwebapp-dev `
  --name modernwebapp-dev-joc `
  --query state `
  --output tsv
```

This produces a script-friendly value such as:

```text
Running
```

Return only the hostname:

```powershell
$webAppHost = az webapp show `
  --resource-group rg-modernwebapp-dev `
  --name modernwebapp-dev-joc `
  --query defaultHostName `
  --output tsv

$webAppHost
```

## 10. Output Formats

| Format | Option | Best use |
|---|---|---|
| Table | `--output table` | Human-readable summaries |
| JSON | `--output json` | Automation and full object inspection |
| JSONC | `--output jsonc` | Colourised JSON for terminal reading |
| TSV | `--output tsv` | Assigning a scalar value to scripts |
| YAML | `--output yaml` | Readable structured output |

Examples:

```powershell
az group list --output json
az group list --output yaml
az group list --query "[].name" --output tsv
```

## 11. JMESPath Query Fundamentals

Azure CLI uses JMESPath for `--query` expressions.

Return one property:

```powershell
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc --query state --output tsv
```

Create a custom object:

```powershell
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc --query "{App:name, Status:state, URL:defaultHostName}" --output table
```

Select properties from a list:

```powershell
az resource list --resource-group rg-modernwebapp-dev --query "[].{Name:name, Type:type}" --output table
```

Filter a list by resource type:

```powershell
az resource list `
  --resource-group rg-modernwebapp-dev `
  --query "[?type=='Microsoft.Web/sites'].{Name:name, Location:location}" `
  --output table
```

## 12. Getting Help

Display top-level help:

```powershell
az --help
```

Display help for a command group:

```powershell
az webapp --help
```

Display help and examples for an action:

```powershell
az webapp show --help
```

Search interactively through command examples:

```powershell
az find "show a web app"
```

The goal is not to memorise every command. Learn the resource/action pattern and use built-in help to find syntax.

## 13. Troubleshooting Notes

### `az` is not recognised

- Open a new terminal after installation.
- Run `where.exe az`.
- Check the system `PATH`.

### `unrecognized arguments: \`

Cause: A Bash multiline command was pasted into PowerShell.

Fix: Put the command on one line or use PowerShell backticks.

### `Table output unavailable`

Cause: The command returned a complex object that cannot be flattened automatically.

Fix: Use JSON output or provide a `--query` that selects scalar fields.

### Wrong subscription

Verify and switch:

```powershell
az account list --output table
az account set --subscription "<subscription-name-or-id>"
az account show --output table
```

### Resource not found

Check exact names and the active subscription:

```powershell
az account show --output table
az group list --output table
az resource list --resource-group rg-modernwebapp-dev --output table
```

## 14. Interview Questions and Answers

### Why use Azure CLI instead of the Azure Portal?

The Portal is useful for discovery and one-off administration. Azure CLI is scriptable, repeatable, suitable for bulk operations, and integrates with CI/CD automation. It also makes operational procedures easier to version, review, and reproduce.

### Why verify the current subscription before deployment?

Azure CLI sends commands to the active subscription. Verifying it reduces the risk of deploying into the wrong environment, creating unplanned cost, or modifying resources outside the intended scope.

### What does `--query` do?

It applies a JMESPath expression to Azure CLI output. This lets an engineer select, reshape, or filter data before displaying it or passing it to another command.

### Why use `--output tsv` in scripts?

TSV avoids JSON quoting and formatting when a script needs a simple scalar value such as a resource ID, hostname, or state.

### What is the relationship between Azure CLI and Azure Resource Manager?

Azure CLI is a client interface. Its commands call Azure management APIs, generally through Azure Resource Manager, which authorises and processes resource operations.

### Give a project example.

In ModernWebApp, Azure CLI was used to inspect `rg-modernwebapp-dev`, discover the App Service plan, Linux Web App, Application Insights resource, and alert action group, and query the Web App's state and hostname without relying on the Portal.

## 15. Chapter 1 Cheat Sheet

```powershell
# Version and authentication
az version
az login

# Subscription safety checks
az account show --output table
az account list --output table
az account set --subscription "<subscription-name-or-id>"

# Resource groups
az group list --output table
az group show --name rg-modernwebapp-dev

# Resource discovery
az resource list --resource-group rg-modernwebapp-dev --output table

# Web Apps
az webapp list --output table
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc

# Selected Web App information
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc --query "{Name:name, State:state, Host:defaultHostName}" --output table

# Script-friendly state
az webapp show --resource-group rg-modernwebapp-dev --name modernwebapp-dev-joc --query state --output tsv

# Help
az --help
az webapp --help
az webapp show --help
```

## 16. Practical Exercise

Without using the Azure Portal:

1. Confirm the current subscription.
2. List all resource groups.
3. List resources in `rg-modernwebapp-dev`.
4. Return only resource names and types.
5. Return the Web App's state and hostname.
6. Explain why Application Insights and an action group appeared in the resource list.

## 17. Future Chapters

- Authentication patterns for users, service principals, managed identities, and workload identity federation
- Creating and safely deleting resource groups
- App Service lifecycle management, settings, logs, restarts, and deployment
- Storage accounts and secure access
- Virtual networks, private endpoints, and DNS
- Key Vault and managed identity
- Monitoring queries, diagnostics, alerts, and Application Insights
- Reusable PowerShell and Bash automation
- Azure DevOps pipeline integration

