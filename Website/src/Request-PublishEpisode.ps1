function Get-EpisodeFromMarkdown 
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory=$true)]
		[string]
		$FileName
	)
	
	$url = "https://api.github.com/repos/gaufung/DotNetWeekly/contents/doc/$($FileName)"
	$response = Invoke-RestMethod -Method GET -Uri $url
	if (-not $response -or -not $response.content)
	{
		Write-Host "Failed to get $FileName from GitHub" -ForegroundColor Red
		return
	}

	$content = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($response.Content))

	$contentFilePath = Join-Path -Path $env:TEMP -ChildPath $FileName
	if (Test-Path $contentFilePath) {
		Remove-Item $contentFilePath -Force
	}
	$content | Out-File $contentFilePath
	[string[]]$lines = Get-Content $contentFilePath 

	return $lines
}