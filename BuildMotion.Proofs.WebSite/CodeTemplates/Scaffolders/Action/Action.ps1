[T4Scaffolding.Scaffolder(Description = "Enter a description of Action here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ActionName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$actionPath = "Vergosity\Actions"
$outputPath = Join-Path (Join-Path $actionPath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "Action"
$fileName = "$ActionName$Suffix"

Add-ProjectItemViaTemplate $outputPath$fileName `
	-Template ActionTemplate `
	-Model @{ Namespace = "$namespace.Vergosity.Actions"; ClassName=$ActionName; BaseNamespace = $namespace } `
	-SuccessMessage "Added Action output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force