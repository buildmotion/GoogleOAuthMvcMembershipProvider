[T4Scaffolding.Scaffolder(Description = "Enter a description of Attribute here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$AttributeName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$attributePath = "Vergosity\Attributes"
$outputPath = Join-Path (Join-Path $attributePath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$baseNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "Attribute"
$fileName = "$AttributeName$Suffix"
Add-ProjectItemViaTemplate $outputPath$fileName `
	-Template AttributeTemplate `
	-Model @{ Namespace = "$namespace.Vergosity.Attributes"; Name = $AttributeName; BaseNamespace = $baseNamespace} `
	-SuccessMessage "Added Attribute output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force