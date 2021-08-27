function Remove-ItemSafely {
    [CmdletBinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [String[]]
        $Path ,
    
        [Switch]
        $Recurse
    )
    
    Process {
        foreach($p in $Path) {
            if(Test-Path $p) {
                Remove-Item $p -Recurse:$Recurse -WhatIf:$WhatIfPreference
            }
        }
    }
}

Remove-ItemSafely D:\PublishedApp -Recurse
Write-Host "Old version of published application removed successfully" -BackgroundColor Yellow -ForegroundColor Black
dotnet publish D:\LabWebApp\LabWebApp.sln -o D:\PublishedApp
Write-Host "Publish application script executed successfully" -BackgroundColor Green -ForegroundColor Black