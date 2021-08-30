function Remove-ItemSafely {
    [CmdletBinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [AllowEmptyString()]
        [String]
        $Path ,
    
        [Switch]
        $Recurse
    )
    
    Process {
        if($Path -ne "") {
            if(Test-Path $Path) {
                Remove-Item $Path -Recurse:$Recurse -WhatIf:$WhatIfPreference
                Write-Host "Old version of published application removed successfully" -BackgroundColor Yellow -ForegroundColor Black
                $pa = Read-Host "Enter path to application project or solution and press Enter"
                Publish-App $pa $Path
            }
            else {
                Write-Host "Old version of published application remove failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
            }
        }
        else {
            if(Test-Path D:\PublishedApp) {
                Remove-Item D:\PublishedApp -Recurse:$Recurse -WhatIf:$WhatIfPreference
                Write-Host "Old version of published application removed successfully" -BackgroundColor Yellow -ForegroundColor Black
                $pa = Read-Host "Enter path to application project or solution and press Enter"
                Publish-App $pa D:\PublishedApp
            }
            else {
                Write-Host "Old version of published application remove failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
            }
        }
    }
}

function Publish-App {
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [AllowEmptyString()]
        [String]
        $PathToApp,

        [AllowEmptyString()]
        [String]
        $PathToFolder

    )
    
    Process {
        if($PathToApp -ne "") {
            if(Test-Path $PathToApp) {
                $res = dotnet publish $PathToApp -o $PathToFolder
            }
        }
        else {
            if(Test-Path D:\LabWebApp\LabWebApp.sln) {
                $res = dotnet publish D:\LabWebApp\LabWebApp.sln -o $PathToFolder
            }
        }

        if($null -ne $res) {
            Write-Host "Publish application script executed successfully" -BackgroundColor Green -ForegroundColor Black
        }
        else {
            Write-Host "Publish application script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
        }
    }
}

$p = Read-Host "Enter old published application folder path and press Enter"
$res = Remove-ItemSafely $p -Recurse
