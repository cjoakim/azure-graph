# Azure Graph API - with CLI

## Links

- https://docs.microsoft.com/en-us/azure/governance/resource-graph/concepts/query-language
- https://docs.microsoft.com/en-us/azure/kusto/query/distinctoperator
- https://docs.microsoft.com/en-us/azure/governance/resource-graph/samples/starter
- https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest

## Examples

```
$ az extension add --name resource-graph

$ az graph query --help

$ az graph query -q "project name, type, location | order by name asc"
$ az graph query -q "project name, type, location | distinct location | order by location asc"
```
