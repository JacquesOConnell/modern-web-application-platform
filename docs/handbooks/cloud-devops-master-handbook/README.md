# Cloud & DevOps Master Handbook

**Author:** Jacques O'Connell  
**Purpose:** A practical, project-based reference for cloud engineering, DevOps implementation, troubleshooting, certification revision, and technical interviews.

## About This Handbook

This handbook captures the tools, architecture decisions, commands, implementation steps, and lessons learned while building the **Modern Web Application Platform** portfolio project.

It is designed as a long-term reference rather than a command-memorisation guide. Each handbook explains:

- What problem the technology solves
- Where it fits in a delivery architecture
- How it was applied in the portfolio project
- Safe, repeatable implementation steps
- Common errors and troubleshooting methods
- Interview-ready explanations
- Quick-reference commands and checklists

## Handbook Index

| Handbook | Current Status | Focus |
|---|---|---|
| [Azure CLI](azure-cli/README.md) | Chapter 1 complete | Azure administration and automation from the command line |
| [Azure DevOps](azure-devops/README.md) | Planned | Boards, Repos, Pipelines, artifacts, environments, and service connections |
| [Terraform](terraform/README.md) | Planned | Reusable multi-environment Infrastructure as Code |
| [Docker](docker/README.md) | Planned | Images, containers, secure builds, and registries |
| [Kubernetes](kubernetes/README.md) | Planned | Workloads, networking, security, Helm, and managed Kubernetes |
| [Git](git/README.md) | Planned | Branching, Pull Requests, collaboration, and troubleshooting |
| [Linux](linux/README.md) | Planned | Administration, networking, services, security, and automation |
| [Microsoft Azure](azure/README.md) | Planned | Azure architecture, identity, compute, networking, data, and monitoring |
| [Amazon Web Services](aws/README.md) | Planned | AWS architecture, operations, security, and automation |
| [Google Cloud](gcp/README.md) | Planned | GCP architecture, operations, security, and automation |

## Portfolio Project Context

The primary practical reference is the **Modern Web Application Platform**, which uses:

```text
Azure Boards
    -> Azure Repos and GitHub
    -> Feature branches and Pull Requests
    -> Azure Pipelines
    -> Build, test, and SonarQube analysis
    -> Pipeline artifact
    -> Azure App Service deployment
    -> Monitoring and security controls
```

Planned extensions include Azure CLI automation, Terraform, Bicep, Docker, Azure Container Registry, Kubernetes, Key Vault, Application Insights, AWS, and GCP.

## Documentation Standards

- Commands specify the intended shell.
- Destructive commands include a warning and verification step.
- Secrets, tokens, tenant identifiers, and subscription identifiers are never committed.
- Examples use placeholders such as `<subscription-id>` where values are sensitive or environment-specific.
- Completed, current, and planned work are clearly distinguished.
- Recommendations favour least privilege, repeatability, observability, and cost control.

## Suggested Reading Paths

### AZ-400 revision

1. Azure DevOps
2. Git
3. Azure CLI
4. Terraform
5. Docker
6. Kubernetes
7. Azure

### Multi-cloud DevOps interviews

1. Git
2. Linux
3. Azure DevOps
4. Terraform
5. Docker
6. Kubernetes
7. Azure, AWS, and GCP comparisons

## Roadmap

- [x] Create the master handbook structure
- [x] Create Azure CLI Chapter 1
- [ ] Document the existing Azure DevOps implementation
- [ ] Add implementation screenshots and architecture diagrams
- [ ] Add troubleshooting records from the portfolio project
- [ ] Add chapter summaries and command cheat sheets
- [ ] Create a complete interview question-and-answer handbook
- [ ] Export a polished PDF edition

