[T4Scaffolding.Scaffolder(Description = "Enter a description of Rule here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$RuleName,
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$RuleSectionName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$rulePath = "Business\$RuleSectionName\Rules"
$outputPath = Join-Path (Join-Path $rulePath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "Rule"
$fileName = "$RuleName$Suffix"
Add-ProjectItemViaTemplate $outputPath$fileName -Template RuleTemplate `
	-Model @{ Namespace = "$namespace.Business.$RuleSectionName.Rules"; Name = $RuleName } `
	-SuccessMessage "Added Rule output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force