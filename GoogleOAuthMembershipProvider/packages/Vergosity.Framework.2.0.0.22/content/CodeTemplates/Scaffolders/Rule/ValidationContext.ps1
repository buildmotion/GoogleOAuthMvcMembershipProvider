[T4Scaffolding.Scaffolder(Description = "Enter a description of ValidationContext here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ValidationContextName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$rulePath = "Vergosity\Rules"
$outputPath = Join-Path (Join-Path $rulePath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "ValidationContext"
$fileName = "$ValidationContextName$Suffix"
Add-ProjectItemViaTemplate $outputPath$fileName -Template ValidationContextTemplate `
	-Model @{ Namespace = "$namespace.Vergosity.Rules"; Name = $ValidationContextName } `
	-SuccessMessage "Added ValidationContext output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force