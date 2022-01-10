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
	
}