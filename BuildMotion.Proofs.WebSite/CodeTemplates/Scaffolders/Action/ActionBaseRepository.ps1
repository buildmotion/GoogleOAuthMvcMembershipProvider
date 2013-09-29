[T4Scaffolding.Scaffolder(Description = "Enter a description of Action here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $false, ValueFromPipelineByPropertyName = $true)][string]$ActionBaseName,
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][bool]$UseProviderRepository,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$actionPath = "Vergosity\Actions"
$outputPath = Join-Path (Join-Path $actionPath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "ActionBase"
$fileName = "$ActionBaseName$Suffix"

Add-ProjectItemViaTemplate $outputPath$fileName `
	-Template ActionBaseRepositoryTemplate `
	-Model @{ Namespace = "$namespace.Vergosity.Actions"; ClassName=$fileName; BaseNamespace = $namespace; UseProviderRepository = $UseProviderRepository; BaseName = $ActionBaseName } `
	-SuccessMessage "Added ActionBase output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force