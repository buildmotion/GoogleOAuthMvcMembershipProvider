[T4Scaffolding.Scaffolder(Description = "Enter a description of Attribute here")][CmdletBinding()]
param(        
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$AttributeName,
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$AttributeSectionName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$attributePath = "Business\$AttributeSectionName\Attributes"
$outputPath = Join-Path (Join-Path $attributePath $CustomScaffolderName) $CustomScaffolderName
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$baseNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$Suffix = "Attribute"
$fileName = "$AttributeName$Suffix"
Add-ProjectItemViaTemplate $outputPath$fileName `
	-Template AttributeTemplate `
	-Model @{ Namespace = "$namespace.Business.$AttributeSectionName.Attributes"; Name = $AttributeName; BaseNamespace = $baseNamespace} `
	-SuccessMessage "Added Attribute output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force