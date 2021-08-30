function Build-App {
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [AllowEmptyString()]
        [String]
        $Path 
    )
    
    Process {
        if($Path -ne "") {
            if(Test-Path $Path) {
                $res = dotnet build $Path
            }
        }
        else {
            if(Test-Path D:\LabWebApp) {
                $res = dotnet build D:\LabWebApp
            }
        }

        if($null -ne $res) {
            Write-Host "Build script executed successfully" -BackgroundColor Green -ForegroundColor Black
        }
        else {
            Write-Host "Build script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
        }
    }
}

$p = Read-Host "Enter application folder path and press Enter"
Build-App $p

