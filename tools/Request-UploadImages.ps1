[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [string]
    $ResourceGroupName = 'dotnetweekly',

    [Parameter(Mandatory=$false)]
    [string]
    $StorageAccountName = 'dotnetweeklyimages',

    [Parameter(Mandatory=$true)]
    [string]
    $EpisodeName,

    [Parameter(Mandatory=$true)]
    [string[]]
    $ImageFilePaths
)

function Get-ImageMimeMapping {
    param (
        [Parameter(Mandatory=$true)]
        [string]
        $fileName
    )
    $extension = [System.IO.Path]::GetExtension($fileName);
    $mimeTypeMap = @{
        '.png'                          = 'image/png'
        '.jpe'                          = 'image/jpeg'
        '.jpeg'                         = 'image/jpeg'
        '.jpg'                          = 'image/jpeg'
        '.gif'                          = 'image/gif'
        '.bmp'                          = 'image/bmp'
        '.svg'                          = 'image/svg+xml'
        '.webp'                         = 'image/webp'
        '.jfif'                         = 'image/jfif'
    }

    return $mimeTypeMap[$extension];
}

$accessToken = Get-AzAccessToken;
if (-not $accessToken -or $accessToken.ExpiresOn.UtcDateTime -gt [System.DateTime]::UtcNow) {
    Connect-AzAccount
}

$storageAccount = Get-AzStorageAccount -ResourceGroupName $ResourceGroupName -Name $StorageAccountName

if (-not $storageAccount) {
    Write-Error "Storage Account $($StorageAccountName) doesn't exist in the $($ResourceGroupName) resource group";
    return
}

$context = $storageAccount.Context 

$container = Get-AzStorageContainer -Context $context -Container $EpisodeName

if (-not $container) {
    New-AzStorageContainer -Name $EpisodeName -Permission Container -Context $context
}

$container = Get-AzStorageContainer -Context $context -Name $EpisodeName

$ImageFilePaths | Foreach-Object {
    $fileName = [System.IO.Path]::GetFileName($_);
    $contentType = Get-ImageMimeMapping $fileName
    $blob2HT = @{
        File             = $_
        Container        = $EpisodeName
        Blob             = $fileName
        Context          = $context
        StandardBlobTier = 'Cool'
        Properties         = @{"ContentType" = $contentType }
    }
    $blob = Set-AzStorageBlobContent @blob2HT
    $uri = $blob.ICloudBlob.Uri
    Write-Host "$($_) -> $($uri)"
}
