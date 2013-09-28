[T4Scaffolding.Scaffolder(Description = "Enter a description of Rule here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$RuleName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$rulePath = "Vergosity\Rules"
$outputPath = Join-Path (Join-Path $rulePath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "Rule"
$fileName = "$RuleName$Suffix"
Add-ProjectItemViaTemplate $outputPath$fileName -Template RuleTemplate `
	-Model @{ Namespace = "$namespace.Vergosity.Rules"; Name = $RuleName } `
	-SuccessMessage "Added Rule output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force