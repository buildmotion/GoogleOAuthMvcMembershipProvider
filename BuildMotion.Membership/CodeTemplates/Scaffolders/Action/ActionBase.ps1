[T4Scaffolding.Scaffolder(Description = "Enter a description of Action here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ActionSectionName,
	[parameter(Mandatory = $false, ValueFromPipelineByPropertyName = $true)][string]$ActionBaseName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$actionPath = "Business\$ActionSectionName\Actions"
$outputPath = Join-Path (Join-Path $actionPath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "ActionBase"
$fileName = "$Suffix"

Add-ProjectItemViaTemplate $outputPath$fileName `
	-Template ActionBaseTemplate `
	-Model @{ Namespace = "$namespace.Business.$ActionSectionName.Actions"; ClassName=$fileName; BaseNamespace = $namespace; BaseName = $ActionBaseName } `
	-SuccessMessage "Added ActionBase output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force