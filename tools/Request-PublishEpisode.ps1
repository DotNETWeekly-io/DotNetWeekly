[CmdletBinding()]
param (
	[Parameter(Mandatory=$true)]
	[string]
	$Title,

	[Parameter(Mandatory=$true)]
	[string]
	$FileName,

	[Parameter(Mandatory=$true)]
	[string]
	$SecretKey,

	[Parameter(Mandatory=$true)]
	[string]
	$TenantId,

	[Parameter(Mandatory=$true)]
	[string]
	$ClientId,

	[Parameter(Mandatory=$true)]
	[string]
	$ResourceScope,

	# Parameter help description
	[Parameter(Mandatory=$true)]
	[string]
	$EndPoint
)

function Get-EpisodeFromMarkdown 
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory=$true)]
		[string]
		$FileName
	)
	
	$url = "https://api.github.com/repos/gaufung/DotNetWeekly/contents/docs/$($FileName)"
	$response = Invoke-RestMethod -Method GET -Uri $url
	if (-not $response -or -not $response.content)
	{
		Write-Host "Failed to get $FileName from GitHub" -ForegroundColor Red
		return
	}

	return [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String(($response.content)))
	
}

function Get-AADToken 
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory=$true)]
		[string]
		$TenantId,

		[Parameter(Mandatory=$true)]
		[string]
		$ClientId,

		[Parameter(Mandatory=$true)]
		[string]
		$ResourceScope,

		[Parameter(Mandatory=$true)]
		[string]
		$SecretKey
	)
	$url = "https://login.microsoftonline.com/" + $TenantId + "/oauth2/v2.0/token";
	$body = @{
		grant_type    = "client_credentials"
		client_id     = $ClientId
		scope         = $ResourceScope
		client_secret = $SecretKey
	}

	$headers = @{
		"Content-Type"    = "application/x-www-form-urlencoded"
		"Accept"          = "*/*"
		"Accept-Encoding" = "gzip, deflate, br"
	}

	$response = Invoke-RestMethod -Uri $url -Method POST -Body $body -Headers $headers 
	return $response.access_token;

}

$content = Get-EpisodeFromMarkdown -FileName $FileName
$data = @{
	Title = $Title
	Content = $content
}

$url = "$($EndPoint)/api/episodes"

$body = $data | ConvertTo-Json -Depth 99
$token = Get-AADToken -TenantId $TenantId -ClientId $ClientId -ResourceScope $ResourceScope -SecretKey $SecretKey
$headers = @{
	Authorization = "Bearer $($token)"
}

Invoke-RestMethod -Method Post -Uri $url -Body $body -ContentType "application/json; charset=utf-8" -Headers $headers